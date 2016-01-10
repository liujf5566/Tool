using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Aostar.MVP.Main
{
    class Program
    {
        private static readonly ILog _loger;
        /// <summary>
        /// 当前正在运行的所有进程
        /// </summary>
        private static readonly Process[] _procArray;
        /// <summary>
        /// 安装路径
        /// </summary>
        private static readonly string _installPath;
        /// <summary>
        /// 客户端程序所在的路径
        /// </summary>
        private static readonly string _clientPath;
        /// <summary>
        /// 更新程序所在的路径
        /// </summary>
        private static readonly string _updatePath;
        /// <summary>
        /// 版本文件所在路径
        /// </summary>
        private static readonly string _versionFile;
        static Program()
        {
            _loger = LogManager.GetLogger("Program");
            _procArray = Process.GetProcesses();
            _installPath = AppDomain.CurrentDomain.BaseDirectory;
            _clientPath = _installPath + ConfigurationManager.AppSettings["ClientApp"];
            _updatePath = _installPath + "Aostar.MVP.Update.exe";
            _versionFile = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download\\VersionManagement.xml";
        }
        static void Main(string[] args)
        {
            //检查下载服务是否启动,如果未启动则启动它。
            ThreadPool.QueueUserWorkItem(new WaitCallback(VerifyDownloadService));
            //检查客户端是否已启动
            if (ClientIsRuning())
            {
                MessageBox.Show("系统已运行，请不要重复打开！");
                return;
            }
            //如果备份文件夹存在,更新备份文件
            if (Directory.Exists(_installPath + "UpdateBackup"))
            {
                string backupDir = _installPath + "UpdateBackup";
                CopyFolder(backupDir, _installPath);
                Directory.Delete(backupDir, true);
            }
            //如果本地存在新版本
            if (File.Exists(_versionFile))
            {
                StartUpdate();
            }
            //如果本地不存在新版本
            else
            {
                StartMVPClient();
            }
        }
        /// <summary>
        /// 检查下载服务是否启动,如果未启动则启动它
        /// </summary>
        private static void VerifyDownloadService(object state)
        {
            Process pro = _procArray.FirstOrDefault(p => p.ProcessName == "Aostar.MVP.DownloadService");
            //下载服务已启动
            if (pro != null)
            {
                return;
            }
            try
            {
                //检查log4.net文件是否存在【为了兼容以前的自动更新程序,升级到1.2.0后此段代码可以去掉】
                string updateSer = _installPath + "updateSer\\";
                if (!File.Exists(updateSer + "log4net.dll"))
                {
                    File.Copy(_installPath + "log4net.dll", updateSer + "log4net.dll");
                }
                //安装并启动下载服务
                string installService = ConfigurationManager.AppSettings["installService"];
                pro = new Process()
                {
                    StartInfo =
                    {
                        FileName = updateSer + installService,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                pro.Start();
            }
            catch (Exception ex)
            {
                _loger.Error("VerifyDownloadService()方法：" + ex.Message);
            }
        }

        /// <summary>
        /// 检查客户端是否已启动
        /// </summary>                
        /// <returns>true or false</returns>
        private static bool ClientIsRuning()
        {
            string clientProcess = Path.GetFileNameWithoutExtension(_clientPath);
            string curUser = Environment.UserName;
            return _procArray.FirstOrDefault(p => p.ProcessName == clientProcess &&
                                            ProcessHelper.GetProcessUserName(p.Id) == curUser) != null;
        }
        /// <summary>
        /// 启动MVP客户端进程
        /// </summary>
        private static void StartMVPClient()
        {
            Process clientApp = new Process { StartInfo = { FileName = _clientPath } };
            clientApp.Start();
        }
        /// <summary>
        /// 启动更新进程
        /// </summary>
        private static void StartUpdate()
        {
            Process updateApp = new Process { StartInfo = { FileName = _updatePath } };
            updateApp.Start();
        }
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="spath">源文件夹路径</param>
        /// <param name="dPath">目标文件夹路径</param>        
        private static void CopyFolder(string spath, string dPath)
        {
            try
            {
                //创建目的文件夹
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }
                //拷贝文件
                DirectoryInfo sDir = new DirectoryInfo(spath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    try
                    {
                        file.CopyTo(Path.Combine(dPath, file.Name), true);
                    }
                    catch (Exception ex)
                    {
                        _loger.Error("CopyFolder()方法：" + ex.Message);
                        continue;
                    }
                }
                //循环子文件夹
                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    CopyFolder(subDir.FullName, Path.Combine(dPath, subDir.Name));
                }
            }
            catch (Exception ex)
            {
                _loger.Error("CopyFolder()方法：" + ex.Message);
            }
        }
    }
}
