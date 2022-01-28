import subprocess
import json
import logging
import datetime
import csv
import argparse
import itertools


def arguments():
    parser = argparse.ArgumentParser()
    parser.add_argument('-v', '--versions', type=int, required=True, help=r"Number of versions to keep per service")
    parser.add_argument('-b', '--bucket', type=str, required=False, help=r"GCS bucket for log files")
    parser.add_argument('-p', '--project', type=str, required=False, help=r"GCP project to prune")
    parser.add_argument('-d', '--dryrun', action="store_true", required=False, help=r"Show versions to be deleted")
    return parser.parse_args()


def main(versions_to_keep: int, bucket_name: str = None, single_project: str = None, dryrun_enabled: bool = False):
    logging.info("Script started")
    projects_to_prune = import_projects("projects.csv", single_project)
    logging.info(f'Version retention set to {versions_to_keep} versions per service')
    for index, project in enumerate(projects_to_prune, 1):
        logging.info(f"Pruning project {index} of {len(projects_to_prune)}: {project}")
        service_id_list = get_services(project)
        for service_id in service_id_list:
            versions_list_sliced = get_versions(project, service_id, versions_to_keep)
            for version_to_delete in versions_list_sliced:
                if version_to_delete["traffic_split"] == 0.0:
                    if dryrun_enabled is False:
                        delete_version(project, service_id, version_to_delete["id"])
                    else:
                        logging.info(f'DRYRUN: {project} {service_id} {version_to_delete["id"]} will be deleted!')
                else:
                    logging.info(
                        f'Traffic split for {project} {service_id} {version_to_delete["id"]} is not 0, skipping')
    logging.info("Script finished")
    if bucket_name is not None:
        copy_to_bucket(log_file, bucket_name)
    else:
        logging.warning(f"No GCS bucket specified")


def import_projects(csv_file: str = "projects.csv", single_project_id: str = None) -> list:
    """Imports projects from csv and argument then returns a list"""
    list_of_projects = []
    try:
        with open(csv_file, 'rt') as my_csv:
            reader = csv.reader(my_csv)
            project_list = list(reader)
            list_of_projects = list(itertools.chain(*project_list))
    except Exception as e:
        logging.warning(f"{e} CSV import failed. Is there a valid CSV named {csv_file} in the working directory?")
    if single_project_id is not None:
        list_of_projects.append(single_project_id)
    return list_of_projects


def get_services(project: str) -> list:
    """Returns a list of AppEngine services in a project"""
    service_id_list = []
    try:
        services_cmd = f"gcloud app services list --project={project} --format=json"
        services_output = subprocess.run(services_cmd, shell=True, capture_output=True, text=True)
        services_list = json.loads(services_output.stdout)
    except Exception as e:
        logging.error(f"Failed to get services for {project} - {e}")
        return service_id_list
    service_id_list = [service_dict["id"] for service_dict in services_list]
    logging.info(f"Found {len(service_id_list)} appengine service(s) in {project}")
    logging.info(service_id_list)
    return service_id_list


def get_versions(project: str, service_id: str, version_retention: int) -> 'list[dict]':
    """Returns a list of dictionaries containing AppEngine versions to delete for a single
    AppEngine service based on version retention set"""
    versions_cmd = f'gcloud app versions list \
--project={project} \
--service={service_id} \
--format="json" \
--sort-by="~version.createTime"'
    versions_output = subprocess.run(versions_cmd, shell=True, capture_output=True, text=True)
    versions_list = json.loads(versions_output.stdout)
    logging.info(f"Found {len(versions_list)} version(s) of {service_id} in {project}")
    versions_list_sliced = versions_list[version_retention:]
    logging.info(f"Found {len(versions_list_sliced)} old version(s) of {service_id} to delete")
    return versions_list_sliced


def delete_version(project: str, service_id: str, version_id: str):
    """Deletes an AppEngine version"""
    gcloud_delete_version_cmd = f'gcloud app versions delete \
--project={project} \
--service={service_id} {version_id} --quiet'
    gcloud_delete_version_output = subprocess.run(gcloud_delete_version_cmd, shell=True, capture_output=True, text=True)
    logging.info(gcloud_delete_version_output)


def copy_to_bucket(log_file: str, bucket: str):
    """Copies log file to GCS bucket"""
    subprocess.run(f'gsutil cp {log_file} gs://{bucket}', shell=True)


if __name__ == "__main__":
    # Set up logging to log file and console
    now = datetime.datetime.now()
    now_string = now.strftime("%d-%m-%y_%H-%M-%S")
    log_file = f"appengine-prune-{now_string}.log"
    logging_format = '%(asctime)s:%(levelname)s:%(message)s'
    logging.basicConfig(filename=log_file, level=logging.DEBUG, format=logging_format)
    console = logging.StreamHandler()
    console.setLevel(logging.DEBUG)
    logging.getLogger().addHandler(console)
    formatter = logging.Formatter(logging_format)
    console.setFormatter(formatter)

    args = arguments()
    main(args.versions, args.bucket, args.project, args.dryrun)
