namespace HospitalityGCP.Models
{
    public class ConfigSettings
    {
        public string AppName { get; set; }
        public string MailAPIKey { get; set; }
        public LoggingSettings Logging { get; set; }
        public ConnectionStringSettings ConnectionStrings { get; set; }
        public EmailTemplates EmailTemplates { get; set; }
        public bool ImpersonateOn { get; set; }
        public string HospitalityTeamEmail { get; set; }
    }

    public class LoggingSettings
    {
        public bool IncludeScopes { get; set; }
    }

    public class ConnectionStringSettings
    {
        public string DefaultConnection { get; set; }
    }

    public class EmailTemplates
    {
        public string ConfirmTemplate { get; set; }
        public string CancelTemplate { get; set; }
        public string TestTemplate { get; set; }
    }
}

