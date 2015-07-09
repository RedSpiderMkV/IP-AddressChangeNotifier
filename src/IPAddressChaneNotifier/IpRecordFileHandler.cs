using System;
using System.IO;

namespace IPAddressChangeNotifier
{
    internal class IpRecordFileHandler
    {
        #region Public Methods

        public IpRecordFileHandler()
        {
            if (!File.Exists(dataFile_m))
            {
                File.Create(dataFile_m).Dispose();
            } // end if
        } // end method

        public IpRecord GetIpRecord()
        {
            try
            {
                string[] contents = File.ReadAllLines(dataFile_m);
                DateTime recordDateTime = DateTime.Parse(contents[1]);

                return new IpRecord(contents[0], recordDateTime);
            }
            catch (Exception)
            {
                return null;
            } // end try-catch
        } // end method

        public bool WriteIpAddressToFile(IpRecord ipRecord)
        {
            string ipAddress = ipRecord.IpAddress;
            string dateTime = ipRecord.RecordDate.ToShortDateString();

            try
            {
                File.WriteAllText(dataFile_m, ipAddress + Environment.NewLine + dateTime);
                return true;
            }
            catch (Exception)
            {
                return false;
            } // end try-catch
        } // end method

        #endregion

        #region Private Data

        private const string dataFile_m = "ipCheck.txt";

        #endregion
    } // end class
} // end namespace
