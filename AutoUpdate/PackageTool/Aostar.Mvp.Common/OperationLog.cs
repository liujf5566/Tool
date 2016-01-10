/////////////////////////////////////////////////////////////////////////////
//
// 文 件 名: XmlObject.cs
//
// 功能介绍: 
//
// 创 建 者: 郭正奎
// 创建时间: 2008-12-22 17:19
// 修订历史: 2008-12-22 17:19
//
//  (c)2007-2008 保留所有版权
//
// 
// 
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Text;

namespace Aostar.MVP.Update.Communal
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
                LogStream = new StreamWriter(LogFile, Encoding.UTF8);
                if (userName.Trim().Equals(string.Empty))
                {
                    LogStream.WriteLine(DateTime.Now + "---" + LogTag + "---" + log);
                }
                else
                {
                    LogStream.WriteLine(DateTime.Now + "---" + userName.Trim() + "---" + LogTag + "---" + log);
                }
            }
            catch
            {
            }
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