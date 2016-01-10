using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace Aostar.MVP.Main
{
    /// <summary>
    /// 进程辅助类
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// 获取使用该进程的用户名
        /// </summary>
        /// <param name="pID">进程ID</param>
        /// <returns>使用该进程的用户名</returns>
        public static string GetProcessUserName(int pID)
        {
            string userName = null;
            SelectQuery query = new SelectQuery("Select * from Win32_Process WHERE processID=" + pID);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            try
            {
                foreach (ManagementObject disk in searcher.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;
                    inPar = disk.GetMethodParameters("GetOwner");
                    outPar = disk.InvokeMethod("GetOwner", inPar, null);
                    userName = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                userName = "SYSTEM";
            }
            return userName;
        }
        /// <summary>
        /// 检查当前用户的某个进程是否正在运行
        /// </summary>        
        /// <param name="proName">进程名称</param>
        /// <returns>true?false</returns>
        public static bool ProcessIsRunning(string proName)
        {
            var pros = GetCurrentUserProcess(proName);
            return pros != null && pros.Count != 0;
        }
        /// <summary>
        /// 获取当前用户的某个进程【可能会有多个】
        /// </summary>
        /// <param name="proName">进程名</param>
        /// <returns>进程列表</returns>
        public static List<Process> GetCurrentUserProcess(string proName)
        {
            Process[] proArray = Process.GetProcessesByName(proName);
            if (proArray != null && proArray.Length != 0)
            {
                string curUser = Environment.UserName;
                List<Process> pros = new List<Process>();
                foreach (Process p in proArray)
                {
                    if (GetProcessUserName(p.Id) == curUser)
                    {
                        pros.Add(p);
                    }
                }
                return pros;
            }
            return null;
        }
    }
}
