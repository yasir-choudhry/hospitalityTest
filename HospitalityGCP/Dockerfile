#FROM gcr.io/google-appengine/aspnetcore:2.1
#COPY . /app
#WORKDIR /app
#ENTRYPOINT ["dotnet", "HospitalityGCP.dll"]
#

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
RUN dotnet tool install --global dotnet-dev-certs && dotnet dev-certs https -ep /https/aspnetapp.pfx -p hospitalitygcp
# Copy csproj and restore as distinct layers
COPY ./HospitalityGCP/HospitalityGCP.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /https/ /https/

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "HospitalityGCP.dll"]