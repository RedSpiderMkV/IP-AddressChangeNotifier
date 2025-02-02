using IPAddressTracker.Constants;
using IPAddressTracker.Interface;
using Serilog;

namespace IPAddressTracker.Implementation
{
    public class IPAddressRemoteReader : IIPAddressRemoteReader
    {
        #region Private Data

        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public IPAddressRemoteReader(ILogger logger)
        {
            _logger = logger;
        }

        public string GetIPAddress()
        {
            string ipAddress = null;

            foreach (string link in IPAddressCheckerLinks.Links)
            {
                try
                {
                    _logger.Information($"Checking {link}...");
                    ipAddress = GetIPAddress(link);

                    _logger.Information($"Retrieved IP Address: {ipAddress}");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error retrieving IP address from {link}");
                    _logger.Error(ex.ToString());
                }
            }

            return ipAddress;
        }

        #endregion

        private static string GetIPAddress(string ipAddressCheckLink)
        {
            string ipAddress;
            using (var client = new HttpClient())
            {
                ipAddress = client.GetStringAsync(ipAddressCheckLink).Result.Trim();
            }

            return ipAddress;
        }
    }
}
