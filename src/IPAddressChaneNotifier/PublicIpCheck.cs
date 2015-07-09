using System;
using System.Net;

namespace IPAddressChangeNotifier
{
    /// <summary>
    /// Check and record external IP address.
    /// </summary>
    internal class PublicIpCheck
    {
        #region Events

        public delegate void NewIpAddressEvent(string NewIpAddress, DateTime ChangeDate, EventArgs e);
        public event NewIpAddressEvent OnNewIpAddress;

        #endregion

        #region Properties

        /// <summary>
        /// Public IP address.
        /// </summary>
        public string ExternalIpAddress { get; private set; }
        
        /// <summary>
        /// Flag indicating whether new IP is different to recorded IP.
        /// </summary>
        public bool IPAddressChanged { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the IP address information after retrieving it.
        /// </summary>
        public void UpdateExternalIpAddressRecord()
        {
            getExternalIpAddress();
            updateAndNotifyIpAddressChange();
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Update IP address and notify any listeners if address has changed.
        /// </summary>
        private void updateAndNotifyIpAddressChange()
        {
            FileHandler dataFileHandler = new FileHandler(ExternalIpAddress);

            if (!String.Equals(dataFileHandler.RecordedIpAddress, this.ExternalIpAddress))
            {
                // IP Address has changed, fire notify event and update file.
                dataFileHandler.WriteToFile();

                NewIpAddressEvent handler = OnNewIpAddress;
                if (handler != null)
                {
                    handler(this.ExternalIpAddress, dataFileHandler.CurrentDate, new EventArgs());
                    IPAddressChanged = true;
                } // end if
            } // end if
        } // end method

        /// <summary>
        /// Retrieve the external IP address via web request.
        /// </summary>
        private void getExternalIpAddress()
        {
            try
            {
                string jsonResponse = new WebClient().DownloadString(ipCheckUrl_m);
                jsonResponse = jsonResponse.Split(':')[1].Replace('"', ' ').Trim();

                ExternalIpAddress = jsonResponse.Substring(0, jsonResponse.Length - 1).Trim();
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving IP address from url " + ipCheckUrl_m);
            } // end try-catch
        } // end method

        #endregion

        #region Private Data

        // Address checking URL.
        private const string ipCheckUrl_m = "http://www.portvisibility.co.uk/visibility/tools/myip.php";

        #endregion
    } // end class
} // end namespace
