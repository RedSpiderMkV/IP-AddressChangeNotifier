using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IPAddressChaneNotifier
{
    internal class PublicIpCheck
    {
        public string ExternalIpAddress { get; private set; }
        private FileHandler dataFileHandler;

        public void UpdateExternalIpAddressRecord()
        {
            getExternalIpAddress();

            dataFileHandler = new FileHandler();
        }

        private void getExternalIpAddress()
        {
            try
            {
                ExternalIpAddress = new WebClient().DownloadString(ipCheckUrl);
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving IP address from url " + ipCheckUrl);
            }
        }

        private const string ipCheckUrl = "http://www.binaryworld.webspace.virginmedia.com/Content/tools/ipcheck.php";
    }
}
