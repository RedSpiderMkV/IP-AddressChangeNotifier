using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPAddressChangeNotifier
{
    internal class FileHandler
    {
        public string RecordedIpAddress { get; private set; }
        public DateTime CurrentDate { get; private set; }

        private const string dataFile = "ipcheck.txt";
        private string externalIpAddress;
        private DateTime recordedLastChangeDate;

        public FileHandler(string externalIpAddress)
        {
            this.CurrentDate = DateTime.Now;
            this.externalIpAddress = externalIpAddress;

            if (!File.Exists(dataFile))
            {
                // File doesn't exist, create a new file.
                this.writeDataToFile();
            }
            else
            {
                if (!getFileContents())
                {
                    // Couldn't get info from file, set to safe values.
                    recordedLastChangeDate = CurrentDate;
                    RecordedIpAddress = String.Empty;
                }
            }
        }

        public void WriteToFile()
        {
            writeDataToFile();
        }

        private bool getFileContents()
        {
            string[] contents = File.ReadAllLines(dataFile);

            if (contents.Length == 2)
            {
                try
                {
                    this.RecordedIpAddress = contents[0];
                    this.recordedLastChangeDate = DateTime.Parse(contents[1]);

                    return true;
                }
                catch (Exception)
                {
                    // Error retrieving data from file.
                    return false;
                }
            }
            else
            {
                // File appears to be missing data, write a new one.
                recordedLastChangeDate = this.CurrentDate;
                writeDataToFile();
            }

            return false;
        }

        private void writeDataToFile()
        {
            // Clear the current contents
            File.WriteAllText(dataFile, String.Empty);
            // Write new contents to file
            File.WriteAllText(dataFile, this.externalIpAddress + Environment.NewLine + this.recordedLastChangeDate.ToShortDateString());
        }
    }
}
