using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// UpdatePromptWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatePromptWindow : Window
    {
        private static readonly ILog _loger = LogManager.GetLogger("UpdatePromptWindow");
        public UpdatePromptWindow()
        {
            InitializeComponent();
            InitDescription();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //启动MVP客户端
            this.Hide();
            string clientPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ClientApp"];
            Process clientApp = new Process { StartInfo = { FileName = clientPath } };
            clientApp.Start();
            this.Close();
        }
        //立即更新
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// 初始化版本描述信息
        /// </summary>
        private void InitDescription()
        {
            try
            {
                string xmlPath = Environment.GetEnvironmentVariable("SystemDrive") + "\\MvpUpdater\\Download\\VersionManagement.xml";
                XDocument xDoc = XDocument.Load(xmlPath);
                //获取所有下载的版本
                var versions = xDoc.Root.Element("DownloadedVersions").Elements("Version");
                //动态创建文本控件
                foreach (var version in versions)
                {
                    TextBlock tbVer = new TextBlock()
                    {
                        Text = "版本号：" + version.Value,
                        FontSize = 16,
                        TextWrapping = TextWrapping.Wrap
                    };
                    TextBlock tbDes = new TextBlock()
                    {
                        Text = version.Attribute("Description").Value,
                        FontSize = 16,
                        TextWrapping = TextWrapping.Wrap
                    };
                    spContent.Children.Add(tbVer);
                    spContent.Children.Add(tbDes);
                }
            }
            catch (Exception ex)
            {
                _loger.Error("InitDescription()方法：" + ex.Message);
            }
        }
    }
}
