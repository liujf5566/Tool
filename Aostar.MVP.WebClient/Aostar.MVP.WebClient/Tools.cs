using System.Linq;
using System.Management;
using System.Net;

namespace Aostar.MVP.WebClient
{
    public static class Tools
    {
        /// <summary>
        /// 重新使用WebClient可以实现Post数据 直接被服务器端Form接收
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostString(string url, string data)
        {
            using (HttpClient cl = new HttpClient())
            {
                cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                cl.Encoding = System.Text.Encoding.UTF8;
                string result = cl.UploadString(url, data);
                return result;
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
                if ((bool)mo["IPEnabled"])
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }

        public static string GetIp()
        {
            IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());


            return ipHostEntry.AddressList.First().ToString();
        }
    }
}