
namespace IPAddressTracker.Interface
{
    internal interface IAppConfigurationManager
    {
        string? IPAddressFilePath { get; }
        string? LogFile { get; }
    }
}
