using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPAddressChangeNotifier
{
    /// <summary>
    /// IP record information object.
    /// </summary>
    internal class IpRecord
    {
        #region Properties

        /// <summary>
        /// IP Address stored as string.
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        /// Date IP address was retrieved.
        /// </summary>
        public DateTime RecordDate { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Instantiate new IP record object.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <param name="dateTime">IP retrieval timestamp.</param>
        public IpRecord(string ipAddress, DateTime dateTime)
        {
            RecordDate = dateTime;
            IpAddress = ipAddress;
        } // end method

        #endregion

    } // end class
} // end namespace
