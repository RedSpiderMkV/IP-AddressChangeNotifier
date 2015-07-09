using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPAddressChangeNotifier
{
    internal class IpRecord
    {
        #region Properties

        public string IpAddress { get; private set; }
        public DateTime RecordDate { get; private set; }

        #endregion

        #region Public Methods

        public IpRecord(string ipAddress, DateTime dateTime)
        {
            RecordDate = dateTime;
            IpAddress = ipAddress;
        } // end method

        #endregion

    } // end class
} // end namespace
