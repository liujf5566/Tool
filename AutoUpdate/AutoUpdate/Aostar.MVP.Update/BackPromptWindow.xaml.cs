using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// PromptWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BackPromptWindow : Window
    {
        public BackPromptWindow()
        {
            InitializeComponent();
        }
        //设置窗口显示位置
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double height = SystemParameters.WorkArea.Height;
            double width = SystemParameters.WorkArea.Width;
            this.Top = height - this.Height;
            this.Left = width - this.Width;
        }
        //允许窗体拖动
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //关闭窗口
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        //下载安装
        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            //检查客户端程序是否正在运行
            string clientApp = ConfigurationManager.AppSettings["ClientApp"];
            var clientPros = ProcessHelper.GetCurrentUserProcess(Path.GetFileNameWithoutExtension(clientApp));
            //如果客户端正在运行,关闭客户端
            if (clientPros != null && clientPros.Count != 0)
            {
                foreach (var client in clientPros)
                {
                    client.Kill();
                }
            }
            //启动更新程序
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }
    }
}
