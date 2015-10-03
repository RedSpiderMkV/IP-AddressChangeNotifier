using System;
using System.IO;

namespace IPAddressChangeNotifier
{
    /// <summary>
    /// Set and retrieve IP address record data.
    /// </summary>
    internal class IpRecordFileHandler
    {
        #region Public Methods

        /// <summary>
        /// Instantiate new object to handle IP address record data.
        /// Create the record file if it doesn't exist.
        /// </summary>
        public IpRecordFileHandler()
        {
            if (!File.Exists(dataFile_m))
            {
                File.Create(dataFile_m).Dispose();
            } // end if
        } // end method

        /// <summary>
        /// Retrieve IP record currently stored in file.
        /// </summary>
        /// <returns>Current IP record.</returns>
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

        /// <summary>
        /// Write new IP record to file.
        /// </summary>
        /// <param name="ipRecord">New IP record.</param>
        /// <returns>True if write was successful.</returns>
        public bool WriteIpAddressRecordToFile(IpRecord ipRecord)
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

        // IP address record file.
        private const string dataFile_m = "ipCheck.txt";

        #endregion
    } // end class
} // end namespace
