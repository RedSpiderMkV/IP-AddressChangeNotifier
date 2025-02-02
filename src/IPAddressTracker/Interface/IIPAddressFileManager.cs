namespace IPAddressTracker.Interface
{
    public interface IIPAddressFileManager
    {
        string IPAddress { get; }
        void UpdateIPAddress(string ipAddress);
    }
}