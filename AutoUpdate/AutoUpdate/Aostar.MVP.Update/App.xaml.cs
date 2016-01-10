using log4net;
using System;
using System.Configuration;
using System.Windows;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Aostar.MVP.Update
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //后台提示更新
            if (e.Args != null && e.Args.Length != 0 && e.Args[0] == "backPrompt")
            {
                BackPromptWindow promptWindow = new BackPromptWindow();
                promptWindow.ShowDialog();
            }
            else
            {
                bool isDowanloaded = CheckUpdateHelper.IsDownloaded();
                bool isMustUpdate = CheckUpdateHelper.IsMustUpdate();
                //如果下载完成
                if (isDowanloaded)
                {
                    //如果是强制更新
                    if (isMustUpdate)
                    {
                        MainWindow mw = new MainWindow();
                        mw.ShowDialog();
                    }
                    //如果不是强制更新
                    else
                    {
                        UpdatePromptWindow upw = new UpdatePromptWindow();
                        upw.ShowDialog();
                    }
                }
                //如果未下载完成
                else
                {
                    //如果是强制更新
                    if (isMustUpdate)
                    {
                        WaitDownloadWindow wdw = new WaitDownloadWindow();
                        wdw.ShowDialog();
                    }
                    //如果不是强制更新
                    else
                    {
                        //启动MVP客户端
                        string clientPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ClientApp"];
                        System.Diagnostics.Process clientApp = new System.Diagnostics.Process { StartInfo = { FileName = clientPath } };
                        clientApp.Start();
                    }
                }
            }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ILog loger = LogManager.GetLogger("App");
            loger.Error("App_OnDispatcherUnhandledException()方法：" + e.Exception.Message);
            this.Shutdown();
            e.Handled = true;
        }
    }
}
