﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IPAddressChangeNotifier
{
    internal class PublicIpCheck
    {
        #region Events

        public delegate void NewIpAddressEvent(string NewIpAddress, DateTime ChangeDate, EventArgs e);
        public event NewIpAddressEvent OnNewIpAddress;

        #endregion

        #region Properties

        public string ExternalIpAddress { get; private set; }
        public bool IPAddressChanged { get; private set; }

        #endregion

        #region Public Methods

        public void UpdateExternalIpAddressRecord()
        {
            getExternalIpAddress();
            updateAndNotifyIpAddressChange();
        } // end method

        #endregion

        #region Private Methods

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

        private void getExternalIpAddress()
        {
            try
            {
                string jsonResponse = new WebClient().DownloadString(ipCheckUrl);
                jsonResponse = jsonResponse.Split(':')[1].Replace('"', ' ').Trim();

                ExternalIpAddress = jsonResponse.Substring(0, jsonResponse.Length - 1).Trim();
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving IP address from url " + ipCheckUrl);
            } // end try-catch
        } // end method

        #endregion

        #region Private Data

        private const string ipCheckUrl = "http://www.portvisibility.co.uk/visibility/tools/myip.php";

        #endregion
    } // end class
} // end namespace