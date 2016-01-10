using System.Management;
using System.Net;
using System.Net.Sockets;

namespace Aostar.MVP.DownloadService
{
    public class HardwareHelper
    {
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>IP</returns>
        public static string GetIpAddress()
        {
            string IP = "";
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return IP;
        }

        /// <summary>
        /// 机器唯一标识
        /// </summary>
        /// <returns></returns>
        public static string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo["SerialNumber"].ToString().Trim();
                }
                return sBIOSSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }

        /// <summary>
        /// 机器码
        /// </summary>
        public static string ComputerIdentification
        {
            get
            {
                return "'" + string.Format("{0}##{1}", GetBIOSSerialNumber(), GetMacAddress()) + "'";
            }
        }
    }
}
