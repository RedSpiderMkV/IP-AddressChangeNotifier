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

        #region Public Methods

        /// <summary>
        /// Update the IP address information after retrieving it.
        /// </summary>
        public void UpdateExternalIpAddressRecord()
        {
            IpRecordFileHandler recordHandler = new IpRecordFileHandler();
            IpRecord record = recordHandler.GetIpRecord();
            string externalIp = getExternalIpAddress();

            if (externalIp != null)
            {
                IpRecord newRecord = new IpRecord(externalIp, DateTime.Now);

                if (record == null || !string.Equals(record.IpAddress, externalIp))
                {
                    if (recordHandler.WriteIpAddressToFile(newRecord))
                    {
                        notifyIpAddressChange(newRecord);
                    } // end if
                } // end if
            } // end if
        } // end method

        #endregion

        #region Private Methods

        private void notifyIpAddressChange(IpRecord newIpRecord)
        {
            NewIpAddressEvent handler = OnNewIpAddress;
            if (handler != null)
            {
                handler(newIpRecord.IpAddress, newIpRecord.RecordDate, new EventArgs());
            } // end if
        } // end method

        /// <summary>
        /// Retrieve the external IP address via web request.
        /// </summary>
        private string getExternalIpAddress()
        {
            try
            {
                string jsonResponse = new WebClient().DownloadString(ipCheckUrl_m);
                jsonResponse = jsonResponse.Split(':')[1].Replace('"', ' ').Trim();

                return jsonResponse.Substring(0, jsonResponse.Length - 1).Trim();
            }
            catch (Exception)
            {
                return null;
            } // end try-catch
        } // end method

        #endregion

        #region Private Data

        // Address checking URL.
        private const string ipCheckUrl_m = "http://www.portvisibility.co.uk/visibility/tools/myip.php";

        #endregion
    } // end class
} // end namespace
