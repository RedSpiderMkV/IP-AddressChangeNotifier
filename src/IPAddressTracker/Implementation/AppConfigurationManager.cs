using System.Configuration;
using IPAddressTracker.Interface;

namespace IPAddressTracker.Implementation
{
    internal class AppConfigurationManager : IAppConfigurationManager
    {
        public string IPAddressFilePath { get; }
        public string LogFile { get; }
        public string ExternalIPAddressChangeExe { get; }
        public string ExternalExeArgs { get; }
        public bool ForceExternalExe { get; }

        public AppConfigurationManager()
        {
            LogFile = ConfigurationManager.AppSettings["log_file"];
            IPAddressFilePath = ConfigurationManager.AppSettings["ip_address_file_path"];
            ExternalIPAddressChangeExe = ConfigurationManager.AppSettings["external_ip_change_exe"];
            ExternalExeArgs = ConfigurationManager.AppSettings["external_exe_args"];
            ForceExternalExe = bool.Parse(ConfigurationManager.AppSettings["force_external_exe"]);
        }
    }
}
