using System;
using System.IO;

namespace AppUpdate.Communal
{
    /// <summary>
    /// 操作日志类
    /// </summary>
    public class OperationLog
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log">当前日志</param>
        public static void WriteLog(string log)
        {
            WriteLogFile(log, "Log", Environment.UserName, "");
        }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log">当前日志</param>
        /// <param name="LogTag">日志Tag</param>
        public static void WriteLog(string log, string LogTag)
        {
            WriteLogFile(log, LogTag, Environment.UserName, "");
        }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log">当前日志</param>
        /// <param name="LogTag">日志Tag</param>
        /// <param name="userName">用户名</param>
        public static void WriteLog(string log, string LogTag, string userName)
        {
            WriteLogFile(log, LogTag, userName, "");
        }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log">当前日志</param>
        /// <param name="LogTag">日志Tag</param>
        /// <param name="userName">用户名</param>
        /// <param name="PathStr">存放路径</param>
        public static void WriteLog(string log, string LogTag, string userName, string PathStr)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\" + PathStr))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\" + PathStr);
            }
            WriteLogFile(log, LogTag, userName, PathStr);
        }

        private static void WriteLogFile(string log, string LogTag, string userName, string PathStr)
        {
            if (PublicValue.IsDebug) DebugLog(log, LogTag, userName, PathStr);
        }

        private static void DebugLog(string log, string LogTag, string userName, string PathStr)
        {
            StreamWriter LogStream = null;
            string FileName = "";
            try
            {
                FileStream LogFile;



                if (PathStr.Trim().Equals(string.Empty))
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\log\"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\log\");
                    }
                    FileName = AppDomain.CurrentDomain.BaseDirectory + @"\log\" + "OperationLog.Log";
                }
                else
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\log\" + PathStr))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\log\" + PathStr);
                    }
                    FileName = AppDomain.CurrentDomain.BaseDirectory + @"\log\" + PathStr + @"\OperationLog.Log";
                }

                if (File.Exists(FileName))
                {
                    LogFile = new FileStream(FileName, FileMode.Append);

                }
                else
                {
                    LogFile = new FileStream(FileName, FileMode.Create);
                }
                LogStream = new StreamWriter(LogFile, System.Text.UTF8Encoding.UTF8);
                if (userName.Trim().Equals(string.Empty))
                {
                    LogStream.WriteLine(DateTime.Now.ToString() + "---" + LogTag.ToString() + "---" + log.ToString());
                }
                else
                {
                    LogStream.WriteLine(DateTime.Now.ToString() + "---" + userName.Trim() + "---" + LogTag.ToString() + "---" + log.ToString());
                }
            }
            catch { }
            finally
            {
                if (LogStream != null)
                {
                    LogStream.Close();
                }
            }
        }
    }
}