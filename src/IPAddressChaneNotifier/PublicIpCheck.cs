using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IPAddressChangeNotifier
{
    internal class PublicIpCheck
    {
        public delegate void NewIpAddressEvent(string NewIpAddress, DateTime ChangeDate, EventArgs e);
        public event NewIpAddressEvent OnNewIpAddress;

        public string ExternalIpAddress { get; private set; }
        public bool IPAddressChanged = false;

        public void UpdateExternalIpAddressRecord()
        {
            this.getExternalIpAddress();

            dataFileHandler = new FileHandler(ExternalIpAddress);

            if (!String.Equals(dataFileHandler.RecordedIpAddress, this.ExternalIpAddress))
            {
                // IP Address has changed, fire notify event and update file.
                dataFileHandler.WriteToFile();

                NewIpAddressEvent handler = OnNewIpAddress;
                if (handler != null)
                {
                    handler(this.ExternalIpAddress, dataFileHandler.CurrentDate, new EventArgs());
                    IPAddressChanged = true;
                }
            }
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
        private FileHandler dataFileHandler;
    }
}
