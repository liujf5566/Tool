using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 下载状态
    /// </summary>
    public enum DownLoadStaus
    {
        /// <summary>
        /// 正在下载
        /// </summary>
        Downloading,
        /// <summary>
        /// 下载完成
        /// </summary>
        Downloaded
    }
    /// <summary>
    /// 下载管理
    /// </summary>
    public class DownloadManagement
    {
        private readonly ILog _loger;
        /// <summary>
        /// 下载目录
        /// </summary>
        private readonly string _downloadDir;
        /// <summary>
        /// 版本管理XML文件所在路径
        /// </summary>
        private readonly string _xmlPath;
        /// <summary>
        /// 下载状态
        /// </summary>
        private DownLoadStaus _downloadStatus;
        /// <summary>
        /// 当前正在下载的版本
        /// </summary>
        private string _downloadingVersion;

        public delegate void DownloadCompletedHandler(string version);
        /// <summary>
        /// 下载完成时执行的事件
        /// </summary>
        public event DownloadCompletedHandler DownloadCompleted;

        public DownloadManagement()
        {
            _loger = LogManager.GetLogger("DownloadManagement");
            _downloadDir = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download";
            if (!Directory.Exists(_downloadDir))
            {
                Directory.CreateDirectory(_downloadDir);
            }
            _xmlPath = _downloadDir + "\\VersionManagement.xml";
        }

        /// <summary>
        /// 下载新版本
        /// </summary>
        /// <param name="repMessage">服务端返回的下载信息</param>
        public void DownloadVersion(ResponseMessage repMessage)
        {
            //写下载状态文件
            using (StreamWriter sw = new StreamWriter(_downloadDir + "\\DownloadStatus.txt", false))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Downloading");
            }
            //获取版本管理文件
            XDocument xmlDoc = GetVersionXml();
            XElement downloadVersion = xmlDoc.Root.Element("DownloadedVersions");
            //如果在指定的时间内,下载未完成,向服务端发送消息
            _downloadStatus = DownLoadStaus.Downloading;
            int period = (repMessage.heartTime - 1) * 60 * 1000;//间隔时间(毫秒)
            Timer timer = new Timer(new TimerCallback(SendDownloadingMessage), _downloadingVersion, period, period);
            //执行下载            
            string ip = repMessage.downloadIP;
            VersionInfo[] versions = repMessage.allVersion;
            int curBag = 0;
            int bagNum = versions.Length;
            NamedPipeServerHelper.Start();
            NamedPipeServerHelper.Status = "Start";
            try
            {
                //如果是采用ftp方式下载
                if (ip.ToLower().Contains("ftp:"))
                {
                    //按照服务端配置的格式解析下载地址
                    string[] ipAddress = Regex.Split(ip, "##", RegexOptions.IgnoreCase);
                    ip = ipAddress[0];
                    string userName = null;
                    string pw = null;
                    if (ipAddress.Length > 1)
                    {
                        userName = ipAddress[1];
                        pw = ipAddress[2];
                    }
                    FTPHelper ftp = new FTPHelper(userName, pw);
                    foreach (VersionInfo v in versions)
                    {
                        NamedPipeServerHelper.BagInfo = string.Format("总共{0}个包，正在下载第{1}个包...", bagNum, ++curBag);
                        _downloadingVersion = v.versionName;
                        //下载更新文件配置信息
                        ftp.Download(ip, _downloadDir, _downloadingVersion + ".xml");
                        //获取文件信息
                        long size = 0;
                        string description = "";
                        BeforeExecute befExe = null;
                        AfterExecute aftExe = null;
                        GetUpdateFileInfo(out size, out description, out befExe, out aftExe);
                        //下载资源包
                        ftp.Download(ip, _downloadDir, _downloadingVersion + ".zip", size);
                        //添加下载的版本信息到XML文件
                        AddVersionInfoToXML(xmlDoc, downloadVersion, v.isMust, description, befExe, aftExe);
                        //解压文件
                        GenerateZip.ZipHelper.UnZip(_downloadDir + "\\" + _downloadingVersion + ".zip", _downloadDir + "\\TempDir\\" + _downloadingVersion);
                    }
                }
                //如果是采用Http方式下载
                else
                {
                    foreach (VersionInfo v in versions)
                    {
                        NamedPipeServerHelper.BagInfo = string.Format("总共{0}个包，正在下载第{1}个包...", bagNum, ++curBag);
                        _downloadingVersion = v.versionName;
                        //下载更新文件配置信息
                        HttpHelper.Download(ip, _downloadDir, _downloadingVersion + ".xml");
                        //获取文件信息
                        long size = 0;
                        string description = "";
                        BeforeExecute befExe = null;
                        AfterExecute aftExe = null;
                        GetUpdateFileInfo(out size, out description, out befExe, out aftExe);
                        //下载资源包
                        HttpHelper.Download(ip, _downloadDir, _downloadingVersion + ".zip", size);
                        //添加下载的版本信息到XML文件
                        AddVersionInfoToXML(xmlDoc, downloadVersion, v.isMust, description, befExe, aftExe);
                        //解压文件
                        GenerateZip.ZipHelper.UnZip(_downloadDir + "\\" + _downloadingVersion + ".zip", _downloadDir + "\\TempDir\\" + _downloadingVersion);
                    }
                }
                using (StreamWriter sw = new StreamWriter(_downloadDir + "\\DownloadStatus.txt", false))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Downloaded");
                }
                NamedPipeServerHelper.Status = "End";
                _downloadStatus = DownLoadStaus.Downloaded;
                OnDownloadCompleted(repMessage.maxVersion);
                //提示用户更新
                UpdatePrompt();
            }
            catch (Exception ex)
            {
                _loger.Error("DownloadVersion(ResponseMessage)方法：" + ex.Message);
                throw ex;
            }
            finally
            {
                if (timer != null)
                {
                    timer.Dispose();
                }
                NamedPipeServerHelper.Close();
            }
        }

        /// <summary>
        /// 获取更新文件信息
        /// </summary>
        /// <param name="size">文件大小</param>
        /// <param name="description">更新描述信息</param>
        /// <
        private void GetUpdateFileInfo(out long size, out string description, out BeforeExecute befExecute, out AfterExecute aftExecute)
        {
            XDocument versionDoc = XDocument.Load(string.Format("{0}\\{1}.xml",
                          _downloadDir, _downloadingVersion));
            XElement root = versionDoc.Root;
            string strSize = root.Element("FileList").Element("File").Attribute("Size").Value;
            size = long.Parse(strSize);
            description = root.Element("Main").Element("Description").Value;
            //获取扩展操作
            //升级前操作
            befExecute = null;
            XElement beforeElement = root.Element("Expansion").Element("BeforeExecute");
            string commandName = beforeElement.Attribute("Name").Value;
            if (!string.IsNullOrEmpty(commandName))
            {
                string commandArgs = beforeElement.Attribute("Args").Value;
                befExecute = new BeforeExecute { Name = commandName, Args = commandArgs };
            }
            //升级后操作
            aftExecute = null;
            XElement afterElement = root.Element("Expansion").Element("AfterExecute");
            commandName = afterElement.Attribute("Name").Value;
            if (!string.IsNullOrEmpty(commandName))
            {
                string commandArgs = afterElement.Attribute("Args").Value;
                aftExecute = new AfterExecute { Name = commandName, Args = commandArgs };
            }
        }

        /// <summary>
        /// 添加版本信息到XML文件
        /// </summary>
        /// <param name="xmlDoc">xml文档</param>
        /// <param name="downloadVersion">已下载版本节点</param>
        /// <param name="isMust">是否强制更新</param>
        /// <param name="description">更新描述信息</param>
        /// <param name="befExe">升级前操作</param>
        /// <param name="aftExe">升级后操作</param>
        private void AddVersionInfoToXML(XDocument xmlDoc, XElement downloadVersion, bool isMust, string description, BeforeExecute befExe, AfterExecute aftExe)
        {
            //添加版本节点
            XElement versionElement = new XElement("Version", _downloadingVersion);
            versionElement.SetAttributeValue("IsMust", isMust);
            //添加描述信息     
            versionElement.SetAttributeValue("Description", description);
            downloadVersion.Add(versionElement);
            //添加扩展操作
            if (befExe != null)
            {
                var befElement = xmlDoc.Root.Element("Expansion").Element("BeforeExecute");
                befElement.Attribute("Name").Value = befExe.Name;
                befElement.Attribute("Args").Value = befExe.Args;
            }
            if (aftExe != null)
            {
                var afterElement = xmlDoc.Root.Element("Expansion").Element("AfterExecute");
                afterElement.Attribute("Name").Value = aftExe.Name;
                afterElement.Attribute("Args").Value = aftExe.Args;
            }
            xmlDoc.Save(_xmlPath);
        }
        /// <summary>
        /// 获取版本管理的xml文件
        /// </summary>
        /// <returns>XML文件</returns>
        private XDocument GetVersionXml()
        {
            XDocument xmlDoc = null;
            if (File.Exists(_xmlPath))
            {
                xmlDoc = XDocument.Load(_xmlPath);
            }
            else
            {
                xmlDoc = new XDocument(new XElement("AutoUpdate",
                    new XElement("DownloadedVersions"),
                    new XElement("Expansion",
                        new XElement("BeforeExecute", new XAttribute("Name", string.Empty), new XAttribute("Args", string.Empty)),
                        new XElement("AfterExecute", new XAttribute("Name", string.Empty), new XAttribute("Args", string.Empty)))));
            }
            return xmlDoc;
        }
        /// <summary>
        /// 向服务端发送正在下载的消息
        /// </summary>
        /// <param name="state">state</param>
        private void SendDownloadingMessage(object state)
        {
            while (_downloadStatus == DownLoadStaus.Downloading)
            {
                DownloadingMessage downloadingMsg = new DownloadingMessage
                {
                    messageType = "downloading",
                    account = HardwareHelper.GetIpAddress(),
                    diskCode = HardwareHelper.GetBIOSSerialNumber(),
                    version = state.ToString()
                };
                MessageManagement.Send(downloadingMsg.ToJson());
            }
        }
        /// <summary>
        /// 下载完成时执行的方法
        /// </summary>
        /// <param name="version">下载完成的最新版本</param>
        private void OnDownloadCompleted(string version)
        {
            if (DownloadCompleted != null)
            {
                DownloadCompleted(version);
            }
        }
        /// <summary>
        /// 更新提示
        /// </summary>
        private void UpdatePrompt()
        {
            //检查客户端是否打开
            string clientApp = ConfigurationManager.AppSettings["ClientApp"];
            string clientProcess = Path.GetFileNameWithoutExtension(clientApp);
            bool isRunning = ProcessHelper.ProcessIsRunning(clientProcess);
            //如果客户端正在运行,提示用户更新
            if (isRunning)
            {
                string updatePath = AppDomain.CurrentDomain.BaseDirectory + "Aostar.MVP.Update.exe";
                System.Diagnostics.Process updateApp = new System.Diagnostics.Process { StartInfo = { FileName = updatePath, Arguments = "backPrompt" } };
                updateApp.Start();
            }
        }

        /// <summary>
        /// 升级前执行的操作
        /// </summary>
        class BeforeExecute
        {
            /// <summary>
            /// 命令名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 命令参数
            /// </summary>
            public string Args { get; set; }
        }

        /// <summary>
        /// 升级后执行的操作
        /// </summary>
        class AfterExecute
        {
            /// <summary>
            /// 命令名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 命令参数
            /// </summary>
            public string Args { get; set; }
        }
    }
}
