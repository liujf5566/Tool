using System.ServiceProcess;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Aostar.MVP.DownloadService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new MVPDownloadService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
