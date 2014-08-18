using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IPAddressChaneNotifier
{
    class Program
    {
        private const string path = "ipcheck.txt";
        
        private static string externalip;
        private static DateTime date;

        static void Main(string[] args)
        {
            Console.Title = "IP Check";
            date = DateTime.Now;

            if (!getExternalIp())
            {
                return;
            }

            // If file already exists, parse it and retrieve its information.
            // Otherwise, create a new file.
            if (File.Exists(path))
            {
                string[] contents = File.ReadAllLines(path);
                string content;

                if (contents.Count() > 0)
                {
                    content = contents[0];

                    try
                    {
                        // Get the date in file of when ip address changed.
                        DateTime.TryParse(contents[2], out date);
                    }
                    catch (Exception)
                    {
                        // pass exception
                    }
                }
                else
                {
                    File.WriteAllText(path, externalip);
                    return;
                }

                if (!string.Equals(content, externalip.Replace("\n", string.Empty)))
                {
                    System.Windows.Forms.MessageBox.Show("IP Address has changed\nOld address: " + content + " New address: " + externalip);
                }
            }

            writeToFile(date);
        }

        private static void writeToFile(DateTime date)
        {
            File.WriteAllText(path, string.Empty);
            File.WriteAllText(path, externalip + Environment.NewLine + date.ToShortDateString());
        }

        private static bool getExternalIp()
        {
            try
            {
                externalip = new WebClient().DownloadString("http://www.binaryworld.webspace.virginmedia.com/Content/tools/ipcheck.php");

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting external ip");
                Console.ReadLine();
            }

            return false;
        }
    }
}
