using System;

namespace IPAddressChangeNotifier
{
    class Program
    {
        private static PublicIpCheck publicIpChecker = new PublicIpCheck();

        static void Main(string[] args)
        {
            Console.Title = "IP Check";

            try
            {
                publicIpChecker.UpdateExternalIpAddressRecord();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + Environment.NewLine + "Terminating program");
                return;
            }

            if (publicIpChecker.IPAddressChanged)
            {
                System.Windows.Forms.MessageBox.Show("IP Address changed.  New IP: " + publicIpChecker.ExternalIpAddress);
            }
        }
    }
}
