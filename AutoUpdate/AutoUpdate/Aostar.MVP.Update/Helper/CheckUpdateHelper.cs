using log4net;
using System;
using System.IO;
using System.Xml.Linq;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// 检查本地更新辅助类
    /// </summary>
    public class CheckUpdateHelper
    {
        private static readonly ILog _loger;
        /// <summary>
        /// 下载目录
        /// </summary>
        private static readonly string _downloadDir;
        static CheckUpdateHelper()
        {
            _loger = LogManager.GetLogger("CheckUpdateHelper");
            _downloadDir = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download";
        }
        /// <summary>
        /// 是否下载完成
        /// </summary>
        /// <returns>如果下载完成返回true,否则返回false</returns>
        public static bool IsDownloaded()
        {
            string txtPath = _downloadDir + "\\DownloadStatus.txt";
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(txtPath);
                return sr.ReadLine() == "Downloaded";
            }
            catch (Exception ex)
            {
                _loger.Error("IsDownloaded()方法：" + ex.Message);
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
        /// <summary>
        /// 是否强制更新
        /// </summary>
        /// <returns>如果强制更新返回true,否则返回false</returns>
        public static bool IsMustUpdate()
        {
            string xmlPath = _downloadDir + "\\VersionManagement.xml";
            try
            {
                XDocument xDoc = XDocument.Load(xmlPath);
                var versions = xDoc.Root.Element("DownloadedVersions").Elements("Version");
                foreach (var v in versions)
                {
                    //只要有一个版本强制更新,就返回需要强制更新
                    if (v.Attribute("IsMust").Value == "true")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _loger.Error("IsMustUpdate()方法：" + ex.Message);
                return false;
            }
        }
    }
}
