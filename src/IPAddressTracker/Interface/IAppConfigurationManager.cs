
namespace IPAddressTracker.Interface
{
    public interface IAppConfigurationManager
    {
        string IPAddressFilePath { get; }
        string LogFile { get; }
        string ExternalIPAddressChangeExe { get; }
        string ExternalExeArgs { get; }
    }
}
