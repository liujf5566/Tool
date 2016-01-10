using log4net;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace Aostar.MVP.DownloadService
{
    public partial class MVPDownloadService : ServiceBase
    {
        private static readonly ILog _loger;
        private static readonly MessageManagement _messageMaganer;
        private static readonly DownloadManagement _downloadManager;
        private Timer _timer;
        static MVPDownloadService()
        {
            _loger = LogManager.GetLogger("MVPDownloadService");
            _messageMaganer = new MessageManagement();
            _downloadManager = new DownloadManagement();
        }
        public MVPDownloadService()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _downloadManager.DownloadCompleted += _messageMaganer.SendDownloadCompletedMessage;
            int heartTime = GetHeatTimes();
            _timer = new Timer(new TimerCallback(DoWork), null, 1000, heartTime);//延迟1秒开始工作
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
            if (_messageMaganer != null)
            {
                _messageMaganer.CloseUDP();
            }
        }
        /// <summary>
        /// 获取心跳时间间隔(毫秒)
        /// </summary>
        /// <returns>频率</returns>
        private int GetHeatTimes()
        {
            int sleepTimeversion = 7200 * 1000;//默认值2小时
            int heartFrequency = Convert.ToInt32(ConfigurationManager.AppSettings["HeartFrequency"]);
            if (heartFrequency > 0)
            {
                sleepTimeversion = heartFrequency * 1000;
            }
            return sleepTimeversion;
        }
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="state">state</param>
        private void DoWork(object state)
        {
            try
            {
                //向服务端发送消息
                _messageMaganer.SendRequestMessage();
                //接收消息
                string content = _messageMaganer.Receive();
                if (!string.IsNullOrEmpty(content))
                {
                    //如果有资源
                    if (content.Contains("downloadIP"))
                    {
                        ResponseMessage responseMsg = ResponseMessage.FromJson(content);
                        _downloadManager.DownloadVersion(responseMsg);
                    }
                    //如果没有资源
                    else
                    {
                        _loger.Info("DoWork(object)方法：服务端未返回下载资源。");
                    }
                }
            }
            catch (Exception ex)
            {
                _loger.Error("DoWork(object)方法：" + ex.Message);
                //停止服务
                this.Stop();
            }
        }
    }
}
