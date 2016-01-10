using log4net;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// WaitDownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WaitDownloadWindow : Window
    {
        private static readonly ILog _loger = LogManager.GetLogger("WaitDownloadWindow");
        private readonly BackgroundWorker _backWorker;
        /// <summary>
        /// 记录是否是正常完成下载
        /// </summary>
        private bool _isNormal;

        public WaitDownloadWindow()
        {
            InitializeComponent();
            _backWorker = new BackgroundWorker();
            _backWorker.DoWork += _backWorker_DoWork;
            _backWorker.RunWorkerCompleted += _backWorker_RunWorkerCompleted;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //设置窗口显示位置
            double height = SystemParameters.WorkArea.Height;
            double width = SystemParameters.WorkArea.Width;
            this.Top = height - this.Height;
            this.Left = width - this.Width;
            //开始后台工作
            _backWorker.RunWorkerAsync();
        }
        void _backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //如果是正常完成,进入安装界面
            if (_isNormal)
            {
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.ShowDialog();
            }
            NamedPipeClientHelper.Close();
            this.Close();
        }
        void _backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                NamedPipeClientHelper.Connect();
                //获取管道服务发送的数据
                string content = NamedPipeClientHelper.Read();
                while (!string.IsNullOrEmpty(content) && content != "End")
                {
                    //如果是进度信息
                    if (content.Contains("/"))
                    {
                        string[] proArray = content.Split('/');
                        //设置进度条信息
                        this.Dispatcher.Invoke(new Action(() =>
                            {
                                double curProValue = double.Parse(proArray[0]);
                                double totalProValue = double.Parse(proArray[1]);
                                double rate = (curProValue / totalProValue) * 100;
                                proBar.Maximum = totalProValue;
                                proBar.Value = curProValue;
                                tbProInfo.Text = string.Format("{0}%", rate.ToString("f0"));
                            }));
                    }
                    //如果是包信息
                    else
                    {
                        tbBagInfo.Dispatcher.Invoke(new Action(() => tbBagInfo.Text = content));
                    }
                    content = NamedPipeClientHelper.Read();
                    _isNormal = true;
                }
            }
            catch (Exception ex)
            {
                _isNormal = false;
                _loger.Error("_backWorker_DoWork()方法：" + ex.Message);
                MessageBox.Show("获取下载数据出错！");
            }
            finally
            {
                //NamedPipeClientHelper.Close();
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
