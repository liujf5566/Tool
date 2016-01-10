using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace UNInstallApplication
{
    public partial class FrmUNInstall : Form
    {
        private readonly ILog _loger;
        public FrmUNInstall()
        {
            InitializeComponent();
            _loger = LogManager.GetLogger("FrmUNInstall");
        }
        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string dir = fbd.SelectedPath;
                txtPath.Text = dir;
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUNInstall_Click(object sender, EventArgs e)
        {
            string dir = txtPath.Text;
            if (string.IsNullOrWhiteSpace(dir))
            {
                MessageBox.Show("请先选择要卸载的程序的路径！");
                btnBrowse.Focus();
                return;
            }
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("指定的路径不存在！请重新选择卸载路径。");
                btnBrowse.Focus();
                return;
            }
            var result = MessageBox.Show(string.Format("将删除\"{0}\"下的所有文件！确定吗？", dir), "卸载提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string info = string.Format("删除{0}目录下的所有文件", dir);
                lstInfo.Items.Add(info);
                _loger.Info(info);

                DeleteFolder(dir);

                bool bDelRootPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDeleteRootPath"]);
                bool bDelStartMenu = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDeleteStartMenu"]);
                bool bDelCacheFile = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDeleteCacheFile"]);
                bool bDelDestopIcon = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDeleteDestopIcon"]);
                //删除根目录
                if (bDelRootPath)
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir);
                        lstInfo.Items.Add("删除根目录");
                        lstInfo.Items.Add(dir);
                        _loger.InfoFormat("删除根目录：{0}", dir);
                    }
                }
                //删除开始目录
                if (bDelStartMenu)
                {
                    string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                    string appMenu = ConfigurationManager.AppSettings["StartMenuName"];
                    startMenuPath = Path.Combine(startMenuPath, "Programs", appMenu);
                    if (Directory.Exists(startMenuPath))
                    {
                        Directory.Delete(startMenuPath, true);
                        lstInfo.Items.Add("删除开始目录");
                        lstInfo.Items.Add(startMenuPath);
                        _loger.InfoFormat("删除开始目录：{0}", startMenuPath);
                    }
                }
                //删除缓存文件
                if (bDelCacheFile)
                {
                    string cacheFilePath = ConfigurationManager.AppSettings["CacheFilePath"];
                    string[] strArr = cacheFilePath.Split('}');
                    string enumName = strArr[0].TrimStart('{');
                    string folderPath = Environment.GetFolderPath(GetEnumValue(enumName));
                    cacheFilePath = folderPath + strArr[1];
                    if (Directory.Exists(cacheFilePath))
                    {
                        Directory.Delete(cacheFilePath, true);
                        lstInfo.Items.Add("删除缓存目录");
                        lstInfo.Items.Add(cacheFilePath);
                        _loger.InfoFormat("删除缓存目录：{0}", cacheFilePath);
                    }
                }
                //删除桌面快捷方式
                if (bDelDestopIcon)
                {
                    string destopIconName = ConfigurationManager.AppSettings["DestopIconName"];
                    string destopIconPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string[] lnkFileArr = Directory.GetFiles(destopIconPath, destopIconName + ".lnk");
                    if (lnkFileArr != null && lnkFileArr.Length == 1)
                    {
                        string lnk = lnkFileArr[0];
                        if (File.Exists(lnk))
                        {
                            File.Delete(lnk);
                            lstInfo.Items.Add("删除桌面快捷方式");
                            lstInfo.Items.Add(lnk);
                            _loger.InfoFormat("删除桌面快捷方式：{0}", destopIconPath);
                        }
                    }
                }
                MessageBox.Show("卸载完成！");
            }
        }

        /// <summary>
        /// 通过枚举名称获取枚举
        /// </summary>
        /// <param name="enumName">名称</param>
        /// <returns>该名称对应的枚举</returns>
        private Environment.SpecialFolder GetEnumValue(string enumName)
        {
            var res = Enum.Parse(typeof(Environment.SpecialFolder), enumName);
            return (Environment.SpecialFolder)res;
        }


        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        private void DeleteFolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            //当前目录下的所有文件
            FileInfo[] fileArray = dir.GetFiles();
            foreach (FileInfo file in fileArray)
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    _loger.Error(ex.Message);
                    //卸载进程
                    var proArr = ProcessHelper.GetCurrentUserProcess(file.Name);
                    proArr = proArr ?? ProcessHelper.GetCurrentUserProcess(Path.GetFileNameWithoutExtension(file.Name));
                    if (proArr != null)
                    {
                        foreach (var pro in proArr)
                        {
                            pro.Kill();
                            pro.WaitForExit();
                        }
                    }
                    //再删除文件
                    File.Delete(file.FullName);
                }
                lstInfo.Items.Add(file.FullName);
                _loger.Info(file.FullName);
            }
            //当前目录下的文件夹            
            DirectoryInfo[] subDirArray = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirArray)
            {
                //递归删除所有文件
                DeleteFolder(subDir.FullName);
                //删除空目录
                Directory.Delete(subDir.FullName);
            }
        }
    }
}
