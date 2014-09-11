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

            if (File.Exists(dataFile))
            {

            }
        }
    }
}
