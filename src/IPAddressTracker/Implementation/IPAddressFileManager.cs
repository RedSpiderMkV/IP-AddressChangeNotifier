using IPAddressTracker.Interface;
using Serilog;

namespace IPAddressTracker.Implementation
{
    public class IPAddressFileManager : IIPAddressFileManager
    {
        #region Properties

        public string IPAddress { get; }

        #endregion

        #region Private Data

        private readonly string _ipAddressFile;
        private readonly ILogger _logger;

        #endregion

        public IPAddressFileManager(ILogger logger, IAppConfigurationManager appConfigurationManager)
        {
            _ipAddressFile = appConfigurationManager.IPAddressFilePath;
            _logger = logger;

            if (!File.Exists(_ipAddressFile))
            {
                IPAddress = "0.0.0.0";
            }
            else
            {
                IPAddress = File.ReadAllLines(_ipAddressFile)[0];
            }

            if (!IPAddressIsValid(IPAddress))
            {
                throw new Exception("Invalid IP address in file record...");
            }
        }

        public void UpdateIPAddress(string ipAddress)
        {
            _logger.Information($"Writing {ipAddress} to {_ipAddressFile}");
            File.WriteAllText(_ipAddressFile, ipAddress);
        }

        #region Private Methods

        private static bool IPAddressIsValid(string ipAddress)
        {
            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }

        #endregion
    }
}
