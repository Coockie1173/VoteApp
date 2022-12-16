using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace VoteApp.Platforms.DeviceStuff
{
    internal class GetDeviceInfo : IGetDeviceInfo
    {
        public string GetDeviceID()
        {
            /*
            //https://www.codeproject.com/Questions/371096/get-maq-address-in-message-box-using-csharp
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return null;*/

            ManagementObjectCollection mbcList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbcList = mbs.Get();
            string processorid = "";
            foreach (ManagementObject mo in mbcList)
            {
                processorid = mo["ProcessorID"].ToString();
            }

            if(processorid == "")
            {
                return null;
            }
            return processorid;
        }
    }
}
