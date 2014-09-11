using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPAddressChaneNotifier
{
    internal class FileHandler
    {
        private const string dataFile = "ipcheck.txt";
        private string externalIpAddress;
        private DateTime currentDate;

        public FileHandler(string externalIpAddress)
        {
            this.currentDate = DateTime.Now;
            this.externalIpAddress = externalIpAddress;

            if (!File.Exists(dataFile))
            {
                this.writeDataToFile();
            }
        }

        public void WriteToFile()
        {
            string[] contents = File.ReadAllLines(dataFile);
            string content;

            if (contents.Length == 2)
            {
                content = contents[0];
            }
            else
            {
                // File appears to be missing data, write a new one.
                writeDataToFile();
            }
        }

        private void writeDataToFile()
        {
            // Clear the current contents
            File.WriteAllText(dataFile, String.Empty);
            // Write new contents to file
            File.WriteAllText(dataFile, this.externalIpAddress + Environment.NewLine + currentDate.ToShortDateString());
        }
    }
}
