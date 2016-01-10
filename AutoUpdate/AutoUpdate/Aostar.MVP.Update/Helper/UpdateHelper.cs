using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// 更新辅助类
    /// </summary>
    public class UpdateHelper
    {
        private static readonly ILog _loger;
        /// <summary>
        /// 文件下载路径
        /// </summary>
        private static readonly string _downloadDir;
        /// <summary>
        /// 文件源路径
        /// </summary>
        private static readonly string _sourceDir;
        /// <summary>
        /// 程序安装路径
        /// </summary>
        private static readonly string _installDir;
        /// <summary>
        /// 版本管理XML文件
        /// </summary>
        private static readonly XDocument _verXMLDoc;
        /// <summary>
        /// XAML中的布局控件
        /// </summary>
        private readonly Grid _grid;
        /// <summary>
        /// 定时器
        /// </summary>
        private readonly DispatcherTimer _timer;
        /// <summary>
        /// 用于显示更新进度的进度条
        /// </summary>
        private readonly ProgressBar _progressBar;
        /// <summary>
        /// 用于显示当前进度的状态信息
        /// </summary>
        private readonly TextBlock _tbProgressInfo;
        /// <summary>
        /// 执行后台工作的对象
        /// </summary>
        private readonly BackgroundWorker _backWorker;
        /// <summary>
        /// 需要升级的文件数量
        /// </summary>
        private readonly int _fileNum;
        /// <summary>
        /// 用于备份覆盖失败的文件的列表
        /// </summary>
        private readonly List<Tuple<string, string>> _fileBackupList;
        /// <summary>
        /// 当前进度值
        /// </summary>
        private int _currentProcess;
        /// <summary>
        /// 记录最新版本
        /// </summary>
        private string _lastVersion;
        static UpdateHelper()
        {
            _loger = LogManager.GetLogger("UpdateHelper");
            _downloadDir = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download";
            _sourceDir = _downloadDir + "\\TempDir";
            _installDir = AppDomain.CurrentDomain.BaseDirectory;
            _verXMLDoc = XDocument.Load(_downloadDir + "\\VersionManagement.xml");
        }
        /// <summary>
        /// 检查是否有更新前操作
        /// </summary>
        /// <returns>如果需要更新前操作返回true,否则返回false</returns>
        public static bool HaveExecuteBeforeUpdate()
        {
            var befElement = _verXMLDoc.Root.Element("Expansion").Element("BeforeExecute");
            string commandName = befElement.Attribute("Name").Value;
            //如果没有可执行的命令
            if (string.IsNullOrEmpty(commandName))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 执行更新前操作(主要用于解决全量包升级)
        /// </summary>
        public static void ExecuteBeforeUpdate()
        {
            try
            {
                var befElement = _verXMLDoc.Root.Element("Expansion").Element("BeforeExecute");
                string commandName = befElement.Attribute("Name").Value;
                //如果没有可执行的命令
                if (string.IsNullOrEmpty(commandName))
                {
                    return;
                }
                //获取要执行的命令所在路径(只执行最新版本的扩展操作)
                DirectoryInfo sourceDir = new DirectoryInfo(_sourceDir);
                DirectoryInfo[] subDirArray = sourceDir.GetDirectories();
                if (subDirArray == null || subDirArray.Length == 0)
                {
                    _loger.Error("UpdateFiles()方法：未获取到版本文件夹！");
                    return;
                }
                string version = subDirArray[0].Name;
                if (subDirArray.Length > 1)
                {
                    var verDirs = subDirArray.OrderBy(verDir => Version.Parse(verDir.Name));
                    version = verDirs.Last().Name;
                }
                string commandPath = string.Format("{0}\\{1}\\{2}", _sourceDir, version, commandName);
                string args = befElement.Attribute("Args").Value;
                //执行命令
                Process beforeUpdate = new Process
                {
                    StartInfo =
                    {
                        FileName = commandPath,
                        Arguments = args,
                        WorkingDirectory = _installDir
                    }
                };
                beforeUpdate.Start();
                beforeUpdate.WaitForExit();
            }
            catch (Exception ex)
            {
                _loger.Error("ExecuteBeforeUpdate()方法：" + ex.Message);
            }
        }
        /// <summary>
        /// 执行更新后操作(主要用于解决全量包升级)
        /// </summary>
        public static void ExecuteAfterUpdate()
        {
            try
            {
                var aftElement = _verXMLDoc.Root.Element("Expansion").Element("AfterExecute");
                string commandName = aftElement.Attribute("Name").Value;
                //如果没有可执行的命令
                if (string.IsNullOrEmpty(commandName))
                {
                    return;
                }
                //获取要执行的命令所在路径(只执行最新版本的扩展操作)
                DirectoryInfo sourceDir = new DirectoryInfo(_sourceDir);
                DirectoryInfo[] subDirArray = sourceDir.GetDirectories();
                if (subDirArray == null || subDirArray.Length == 0)
                {
                    _loger.Error("UpdateFiles()方法：未获取到版本文件夹！");
                    return;
                }
                string version = subDirArray[0].Name;
                if (subDirArray.Length > 1)
                {
                    var verDirs = subDirArray.OrderBy(verDir => Version.Parse(verDir.Name));
                    version = verDirs.Last().Name;
                }
                string commandPath = string.Format("{0}\\{1}\\{2}", _sourceDir, version, commandName);
                string args = aftElement.Attribute("Args").Value;
                //执行命令
                Process afterUpdate = new Process
                {
                    StartInfo =
                    {
                        FileName = commandPath,
                        Arguments = args,
                        WorkingDirectory = _installDir
                    }
                };
                afterUpdate.Start();
                afterUpdate.WaitForExit();
            }
            catch (Exception ex)
            {
                _loger.Error("ExecuteAfterUpdate方法：" + ex.Message);
            }
        }

        public UpdateHelper(Grid grid, DispatcherTimer timer)
            : this()
        {
            _grid = grid;
            _progressBar = grid.FindName("proBar") as ProgressBar;
            _tbProgressInfo = grid.FindName("tbProcess") as TextBlock;
            _progressBar.Maximum = _fileNum;
            _timer = timer;
            _fileBackupList = new List<Tuple<string, string>>();
        }
        public UpdateHelper()
        {
            if (!Directory.Exists(_sourceDir))
            {
                _loger.Error("UpdateHelper()方法：文件源路径为空！");
                return;
            }
            if (!Directory.Exists(_installDir))
            {
                _loger.Error("UpdateHelper()方法：目标路径为空！");
                return;
            }
            //获取需要更新的文件数量           
            GetFilesCount(_sourceDir, ref _fileNum);
            //初始化后台工作
            _backWorker = new BackgroundWorker();
            _backWorker.WorkerReportsProgress = true;
            _backWorker.WorkerSupportsCancellation = true;
            _backWorker.DoWork += _backWorker_DoWork;
            _backWorker.ProgressChanged += _backWorker_ProgressChanged;
            _backWorker.RunWorkerCompleted += _backWorker_RunWorkerCompleted;
        }

        void _backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateFiles();
        }
        void _backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _progressBar.Value = _currentProcess;
        }
        void _backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            if (e.Error == null)
            {
                //更新后操作
                ExecuteAfterUpdate();
                //设置版本
                SetVersion();
                //重新设置界面信息
                _grid.Dispatcher.Invoke(new Action(() => UpdateUI()));
                //删除源文件
                DeleteDownloadFolder();
                //启动下载服务
                VerifyDownloadService();
                //向后台发送信息
                SendMessage();
            }
        }
        /// <summary>
        /// 检查下载服务是否启动,如果未启动则启动它
        /// </summary>
        private static void VerifyDownloadService()
        {
            var procArray = Process.GetProcesses();
            Process pro = procArray.FirstOrDefault(p => p.ProcessName == "Aostar.MVP.DownloadService");
            //下载服务已启动
            if (pro != null)
            {
                return;
            }
            try
            {
                //检查log4.net文件是否存在【为了兼容以前的自动更新程序,升级到1.2.0后此段代码可以去掉】
                string updateSer = _installDir + "updateSer\\";
                if (!File.Exists(updateSer + "log4net.dll"))
                {
                    File.Copy(_installDir + "log4net.dll", updateSer + "log4net.dll");
                }
                //安装并启动下载服务              
                pro = new Process()
                {
                    StartInfo =
                    {
                        FileName = updateSer + "InstallDownloadSer.bat",
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
        /// 向后台发送统计信息
        /// </summary>
        private void SendMessage()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = _installDir + "Aostar.MVP.WebClient.exe";
            proc.StartInfo.Arguments = "version:" + _lastVersion;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
        }
        /// <summary>
        /// 设置版本
        /// </summary>
        private void SetVersion()
        {
            //设置当前版本号
            string curVerPath = _installDir + "updateSer\\CurrentVersion\\CurrentVersion.xml";
            XDocument doc = XDocument.Load(curVerPath);
            doc.Root.Element("CurrentVersion").Value = _lastVersion;
            doc.Save(curVerPath);
        }
        /// <summary>
        /// 更新窗体界面
        /// </summary>
        private void UpdateUI()
        {
            _grid.Children.Remove(_grid.FindName("imgContent") as Image);
            _grid.Children.Remove(_grid.FindName("spBottom") as StackPanel);
            //左侧图片
            Image imgLeft = new Image()
            {
                Source = new BitmapImage(new Uri("Images/complete_shade.png", UriKind.Relative)),
                Stretch = System.Windows.Media.Stretch.None
            };
            Grid.SetRow(imgLeft, 1);
            Grid.SetColumn(imgLeft, 0);
            //右边的内容
            TextBlock tbContent = new TextBlock()
            {
                Text = "已将您的软件升级到最新版本，您可以使用啦！",
                FontSize = 16,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
            Grid.SetRow(tbContent, 1);
            Grid.SetColumn(tbContent, 1);
            //底部的按钮
            Button btnRun = new Button()
            {
                Width = 100,
                Height = 30,
                Margin = new System.Windows.Thickness(10, 7, 7, 10),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Content = "立即运行",
                FontSize = 16,
            };
            btnRun.Click += btnRun_Click;
            Grid.SetRow(btnRun, 2);
            Grid.SetColumn(btnRun, 1);
            Grid.SetColumnSpan(btnRun, 2);

            _grid.Children.Add(imgLeft);
            _grid.Children.Add(tbContent);
            _grid.Children.Add(btnRun);
        }
        //启动客户端进程
        void btnRun_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (_grid.Parent as MainWindow).Hide();
            string clientPath = _installDir + ConfigurationManager.AppSettings["ClientApp"];
            Process clientApp = new Process { StartInfo = { FileName = clientPath } };
            clientApp.Start();
            (_grid.Parent as MainWindow).Close();
        }
        /// <summary>
        /// 更新程序
        /// </summary>
        public void Update()
        {
            if (_backWorker != null && !_backWorker.IsBusy)
            {
                _backWorker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 更新文件
        /// </summary>
        private void UpdateFiles()
        {
            //获取版本文件夹
            DirectoryInfo sourceDir = new DirectoryInfo(_sourceDir);
            DirectoryInfo[] subDirArray = sourceDir.GetDirectories();
            if (subDirArray == null || subDirArray.Length == 0)
            {
                _loger.Error("UpdateFiles()方法：未获取到版本文件夹！");
                return;
            }
            //对版本文件夹排序
            var verDirs = subDirArray.OrderBy(verDir => Version.Parse(verDir.Name));
            //依次更新版本文件夹下的文件到安装目录下
            foreach (var verDir in verDirs)
            {
                CopyFolder(verDir.FullName, _installDir);
            }
            _lastVersion = verDirs.Last().Name;
        }
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="spath">源文件夹路径</param>
        /// <param name="dPath">目标文件夹路径</param>        
        private void CopyFolder(string spath, string dPath)
        {
            //创建目的文件夹
            if (!Directory.Exists(dPath))
            {
                Directory.CreateDirectory(dPath);
            }
            //拷贝文件
            FileInfo curFile = null;
            string destDir = null;
            DirectoryInfo sDir = new DirectoryInfo(spath);
            FileInfo[] fileArray = sDir.GetFiles();
            foreach (FileInfo file in fileArray)
            {
                try
                {
                    curFile = file;
                    destDir = dPath;
                    //拷贝文件
                    file.CopyTo(Path.Combine(dPath, file.Name), true);
                    //报告当前进度
                    _backWorker.ReportProgress(++_currentProcess);
                    //设置进度信息
                    _tbProgressInfo.Dispatcher.Invoke(new Action(() =>
                    {
                        float ratio = ((float)_currentProcess / (float)_fileNum) * 100f;
                        _tbProgressInfo.Text = string.Format("{0}  {1}%", file.Name, ratio.ToString("f0"));
                    }));
                }
                catch (Exception ex)
                {
                    _loger.Error("CopyFolder(spath,dpath)方法：" + ex.Message);
                    //如果失败，可能文件正在使用，将其备份到“UpdateBackup”目录下,
                    //再启动Main程序后检查此目录是否存在,如果存在,用该目录下的所有文件覆盖安装目录。
                    string updateBackupDir = Path.Combine(_installDir, "UpdateBackup");
                    //如果当前目标目录不是安装路径的根目录,寻找子目录
                    if (destDir != _installDir)
                    {
                        string[] dirNameArr = destDir.Split(new string[1] { _installDir }, StringSplitOptions.RemoveEmptyEntries);
                        updateBackupDir = Path.Combine(updateBackupDir, dirNameArr[0]);
                    }
                    //创建目录
                    if (!Directory.Exists(updateBackupDir))
                    {
                        Directory.CreateDirectory(updateBackupDir);
                    }
                    //备份文件
                    curFile.CopyTo(Path.Combine(updateBackupDir, curFile.Name), true);
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

        /// <summary>
        /// 获取需要更新的文件数量
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        /// <param name="num">记录文件总数</param>
        /// <returns>文件数量</returns>
        public void GetFilesCount(string dirPath, ref int num)
        {
            //获取目录中的文件
            string[] fileArray = Directory.GetFiles(dirPath);
            if (fileArray != null && fileArray.Length > 0)
            {
                //增加文件数量
                num += fileArray.Length;
            }
            //获取子目录
            string[] subDirArray = Directory.GetDirectories(dirPath);
            if (subDirArray != null && subDirArray.Length > 0)
            {
                foreach (string subDir in subDirArray)
                {
                    //递归获取文件
                    GetFilesCount(subDir, ref num);
                }
            }
        }

        /// <summary>
        /// 删除下载文件夹
        /// </summary>
        public static void DeleteDownloadFolder()
        {
            try
            {
                Directory.Delete(_downloadDir, true);
            }
            catch (Exception ex)
            {
                _loger.Error("DeleteSourceFolder()方法：" + ex.Message);
            }
        }
    }
}
