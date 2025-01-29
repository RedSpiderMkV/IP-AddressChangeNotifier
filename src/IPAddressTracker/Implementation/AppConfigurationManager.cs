using System.Configuration;
using IPAddressTracker.Interface;

namespace IPAddressTracker.Implementation
{
    internal class AppConfigurationManager : IAppConfigurationManager
    {
        public string? IPAddressFilePath { get; }
        public string? LogFile { get; }

        public AppConfigurationManager()
        {
            LogFile = ConfigurationManager.AppSettings["log_file"];
            IPAddressFilePath = ConfigurationManager.AppSettings["ip_address_file_path"];
        }
    }
}
