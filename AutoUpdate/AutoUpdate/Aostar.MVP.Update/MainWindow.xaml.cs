using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 要进行轮播的图片
        /// </summary>
        private readonly BitmapImage[] _bitImgArray;
        /// <summary>
        /// 定时器
        /// </summary>
        private readonly DispatcherTimer _timer;
        /// <summary>
        /// 图片下标
        /// </summary>
        private int _imgIndex = 1;
        public MainWindow()
        {
            UpdateHelper.ExecuteBeforeUpdate();
            InitializeComponent();
            _bitImgArray = new BitmapImage[4];
            InitImags();
            _timer = new DispatcherTimer();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //轮播图片(2秒一次)            
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += timer_Tick;
            _timer.Start();
            //开始更新程序
            UpdateHelper uHelper = new UpdateHelper(gridRoot, _timer);
            uHelper.Update();
        }
        //图片轮播
        void timer_Tick(object sender, EventArgs e)
        {
            imgContent.Source = _bitImgArray[_imgIndex];
            _imgIndex++;
            _imgIndex = _imgIndex < 0 || _imgIndex > 3 ? 0 : _imgIndex;
        }
        //允许窗体拖动
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        //关闭窗体
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            this.Close();
        }
        /// <summary>
        /// 初始化图片
        /// </summary>
        private void InitImags()
        {
            _bitImgArray[0] = new BitmapImage(new Uri("Images/AutoOne.png", UriKind.Relative));
            _bitImgArray[1] = new BitmapImage(new Uri("Images/AutoTwo.png", UriKind.Relative));
            _bitImgArray[2] = new BitmapImage(new Uri("Images/AutoThree.png", UriKind.Relative));
            _bitImgArray[3] = new BitmapImage(new Uri("Images/AutoFour.png", UriKind.Relative));
        }

    }
}
