using System;

namespace IPAddressChangeNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IP Check";

            try
            {
                PublicIpCheck publicIpChecker = new PublicIpCheck();
                publicIpChecker.OnNewIpAddress += new PublicIpCheck.NewIpAddressEvent(publicIpChecker_OnNewIpAddress);

                publicIpChecker.UpdateExternalIpAddressRecord();
            }
            catch (Exception)
            {
                // pass silently
            } // end try-catch
        }

        static void publicIpChecker_OnNewIpAddress(string NewIpAddress, DateTime ChangeDate, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("IP Address changed.  New IP: " + NewIpAddress);
        } // end method
    } // end class
} // end namespace
