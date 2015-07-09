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
                publicIpChecker.UpdateExternalIpAddressRecord();

                if (publicIpChecker.IPAddressChanged)
                {
                    System.Windows.Forms.MessageBox.Show("IP Address changed.  New IP: " + publicIpChecker.ExternalIpAddress);
                } // end if
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + Environment.NewLine + "Terminating program");
            } // end try-catch
        } // end method
    } // end class
} // end namespace
