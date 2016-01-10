using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Aostar.MVP.WebClient
{
    class Program
    {
        static readonly string _baseDir = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string _path = Path.Combine(_baseDir, "result.txt");
        static void Main(string[] args)
        {
            try
            {
                var ip = Tools.GetIp();
                var macAddress = Tools.GetMacAddress();
                var server = ConfigurationManager.AppSettings["server"];
                var type = ConfigurationManager.AppSettings["type"];
                string param = "";
                //如果是客户端,需要上传版本号
                if (type == "0")
                {
                    //如果版本号是从外部传入进来的【用于每一次更新版本上传版本号】
                    if (args != null && args.Length == 1 && args[0].Contains("version"))
                    {
                        string clientVersion = args[0].Split(':')[1];
                        param = string.Format("param={{ip:'{0}',mac:'{1}',type:'{2}',recoveryVersion:'{3}'}}",
                            ip, macAddress, type, clientVersion);
                    }
                    //获取客户端版本号【用于安装完客户端上传版本号】
                    else
                    {
                        string clientVersion = GetVersion();
                        param = string.Format("param={{ip:'{0}',mac:'{1}',type:'{2}',recoveryVersion:'{3}'}}",
                            ip, macAddress, type, clientVersion);
                    }
                }
                //如果是简易客户端,不需要传版本号
                else
                {
                    param = string.Format("param={{ip:'{0}',mac:'{1}',type:'{2}'}}", ip, macAddress, type);
                }
                var result = Tools.PostString(server, param);
                File.WriteAllText(_path, server + " " + param + "   " + result);
            }
            catch (Exception ex)
            {
                File.WriteAllText(_path, ex.Message);
            }
        }
        /// <summary>
        /// 获取客户端程序集版本号
        /// </summary>
        /// <returns>版本号</returns>
        static string GetVersion()
        {
            string curVerXml = _baseDir + "updateSer\\CurrentVersion\\CurrentVersion.xml";
            if (!File.Exists(curVerXml))
            {
                File.WriteAllText(_path, "没有找到版本文件！");
                return "";
            }
            XDocument doc = XDocument.Load(curVerXml);
            var version = doc.Element("AutoUpdate").Element("CurrentVersion");
            if (version == null) return "";
            return version.Value;
        }
    }
}
