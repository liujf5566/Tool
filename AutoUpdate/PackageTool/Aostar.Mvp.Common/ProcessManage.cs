using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace AppUpdate.Communal
{
    /// <summary>
    /// 进程管理类
    /// </summary>
    public class ProcessManage
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="AppName">程序名称</param>
        public static void StartProcess(string AppName)
        {
            try
            {
                Process.Start(AppName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("重新启动程序时出错,原因为:" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        /// <param name="AppName">程序名称</param>
        /// <returns>成功返回true</returns>
        public static bool StopProcess(string AppName)
        {
            bool IsRun = false;
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == AppName.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                    IsRun = true;
                }
            }
            return IsRun;
        }
    }
}