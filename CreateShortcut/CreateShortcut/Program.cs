using System;
using System.Configuration;
using System.IO;
using SHORT = IWshRuntimeLibrary;

namespace CreateShortcut
{
    class Program
    {
        private static string _rootDir;
        private static string _shortcutName;
        private static string _appName;
        private static string _menuName;
        private static string _icon;
        private static StreamWriter _sw;
        private static void Main(string[] args)
        {
            _rootDir = AppDomain.CurrentDomain.BaseDirectory;
            _shortcutName = ConfigurationManager.AppSettings["ShortcutName"];
            _appName = ConfigurationManager.AppSettings["AppName"];
            string iconName = ConfigurationManager.AppSettings["IconName"];
            _menuName = ConfigurationManager.AppSettings["StartMenuName"];
            _icon = Path.Combine(_rootDir, iconName);
            string logPath = Path.Combine(_rootDir, "createShortcut.txt");
            _sw = new StreamWriter(logPath, true);
            _sw.AutoFlush = true;
            //创建桌面快捷键
            CreateDestopShortcut();
            //创建开始菜单快捷键
            CreateStartMenuShortcut();
            _sw.Close();
        }
        /// <summary>
        /// 创建桌面快捷键
        /// </summary>
        static void CreateDestopShortcut()
        {
            try
            {
                string destopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                CreateLink(destopDir, _shortcutName, _appName, _icon);
                _sw.WriteLine("创建桌面快捷方式成功！");
            }
            catch (Exception ex)
            {
                _sw.WriteLine("创建桌面快捷方式失败！错误信息:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 创建开始目录程序菜单
        /// </summary>
        static void CreateStartMenuShortcut()
        {
            try
            {
                var startMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                string startmenuPro = Path.Combine(startMenu, "Programs");
                if (!Directory.Exists(startmenuPro))
                {
                    startmenuPro = Path.Combine(startMenu, "程序");
                }
                var startLinkDir = Path.Combine(startmenuPro, _menuName);
                if (!Directory.Exists(startLinkDir))
                {
                    Directory.CreateDirectory(startLinkDir);
                }
                CreateLink(startLinkDir, _shortcutName, _appName, _icon);
                _sw.WriteLine("创建开始目录程序菜单成功！");
            }
            catch (Exception ex)
            {
                _sw.WriteLine("创建开始目录程序菜单失败！错误信息:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 创建快捷键
        /// </summary>
        /// <param name="pathLink">快捷键路径</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="appName">应用</param>
        /// <param name="ico">图标</param>
        static void CreateLink(string pathLink, string shortcutName, string appName, string ico)
        {
            try
            {
                pathLink = Path.Combine(pathLink, shortcutName + ".lnk");
                //如果存在快捷键,先删除
                if (File.Exists(pathLink))
                {
                    File.Delete(pathLink);
                }
                SHORT.WshShell shell = new SHORT.WshShell();
                SHORT.IWshShortcut shortcut = shell.CreateShortcut(pathLink);
                shortcut.TargetPath = Path.Combine(_rootDir, appName);
                shortcut.WorkingDirectory = _rootDir;
                shortcut.Description = shortcutName;
                shortcut.IconLocation = ico;
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
