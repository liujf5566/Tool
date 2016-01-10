using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace AppUpdate.Communal
{
    /// <summary>
    /// 进程管理类
    /// </summary>
    public class ProcessManager
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
                MessageBox.Show(ex.Message.ToString());
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
            Process p = null;
            if (FindProcess(AppName, out p))
            {
                for (int i = 0; i < p.Threads.Count; i++)
                    p.Threads[i].Dispose();
                p.Kill();
                IsRun = true;

            }
            return IsRun;
        }
         /// <summary>
        /// 查找指定进程是否存在
        /// </summary>
        /// <returns>成功返回true</returns>
        public static bool FindProcess(string AppName)
        {  
            Process p = null;
          return  FindProcess(AppName, out p);
        }
        /// <summary>
        /// 查找指定进程是否存在
        /// </summary>
        /// <param name="AppName">名称</param>
        /// <returns>成功返回true</returns>
        public static bool FindProcess(string AppName, out Process p)
        {
            bool IsRun = false;
            p = null;
            Process[] allProcess = Process.GetProcesses();
            foreach (Process ps in allProcess)
            {
                if (ps.ProcessName.ToLower() + ".exe" == AppName.ToLower())
                {
                    p = ps;
                    IsRun = true;
                    break;
                }
            }
         
            return IsRun;
        }
    }
}