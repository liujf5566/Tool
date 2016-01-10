using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 版本控制
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// 下载过的版本路径
        /// </summary>
        static readonly string _downloadedVersion;
        /// <summary>
        /// 当前版本路径
        /// </summary>
        static readonly string _curVersionPath;
        static VersionHelper()
        {
            _downloadedVersion = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download\\VersionManagement.xml";
            _curVersionPath = AppDomain.CurrentDomain.BaseDirectory + "CurrentVersion\\CurrentVersion.xml";
        }
        /// <summary>
        /// 获取请求服务端时,使用的版本号
        /// <remarks>
        /// 使用已下载的版本向服务端发送请求
        /// </remarks>
        /// </summary>
        /// <returns>版本号</returns>
        public static string GetQuestVersion()
        {
            List<string> downloadedVers = GetDownloadedVersion();
            //如果存在已下载的版本,返回下载过的最大版本
            if (downloadedVers != null && downloadedVers.Count > 0)
            {
                var orderedVers = downloadedVers.OrderByDescending(v => new Version(v));
                return orderedVers.ElementAt(0);
            }
            //如果不存在已下载的版本,返回当前版本
            return GetCurrentVersion();
        }


        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <returns>当前版本</returns>
        public static string GetCurrentVersion()
        {
            XDocument doc = XDocument.Load(_curVersionPath);
            var version = doc.Element("AutoUpdate").Element("CurrentVersion");
            if (version == null) return null;
            return version.Value;
        }

        /// <summary>
        /// 获取已下载的版本
        /// </summary>
        /// <returns>已下载的版本</returns>
        public static List<string> GetDownloadedVersion()
        {
            if (File.Exists(_downloadedVersion))
            {
                XDocument doc = XDocument.Load(_downloadedVersion);
                var versions = doc.Root.Element("DownloadedVersions").Elements("Version");
                List<string> downloadedVersions = new List<string>();
                foreach (var v in versions)
                {
                    downloadedVersions.Add(v.Value);
                }
                return downloadedVersions;
            }
            return null;
        }
    }
}
