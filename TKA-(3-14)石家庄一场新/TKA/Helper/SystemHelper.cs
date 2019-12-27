using System;
using System.Management;

namespace TKA.Helper
{
    public class SystemHelper
    {
        public static OSBit GetOSBit()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return OSBit.OS64Bit;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return OSBit.OS32Bit;
            }
        }
    }

    public enum OSBit
    {
        OS32Bit,
        OS64Bit
    }
}
