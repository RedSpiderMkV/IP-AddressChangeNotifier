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

        public bool GetExternalIpAddress()
        {
            try
            {
                ExternalIpAddress = new WebClient().DownloadString(ipCheckUrl);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving IP address from url " + ipCheckUrl);
            }
        }

        private const string ipCheckUrl = "http://www.binaryworld.webspace.virginmedia.com/Content/tools/ipcheck.php";
    }
}
