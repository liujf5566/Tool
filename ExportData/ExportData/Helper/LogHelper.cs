using System;
using System.Collections.Generic;

namespace ExportData.Helper
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 正常信息
        /// </summary>
        Normal,
        /// <summary>
        /// 警告信息
        /// </summary>
        Warn,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error
    }

    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 正常信息
        /// </summary>
        public static List<string> NormalInfo { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public static List<string> ErrorInfo { get; set; }
        /// <summary>
        /// 警告信息
        /// </summary>
        public static List<string> WarnInfo { get; set; }

        static LogHelper()
        {
            NormalInfo = new List<string>();
            ErrorInfo = new List<string>();
            WarnInfo = new List<string>();
        }
        /// <summary>
        /// 写正常信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void WriteNormalInfo(string message)
        {
            NormalInfo.Add(message);
        }
        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void WriteErrorInfo(string message)
        {
            ErrorInfo.Add(message);
        }
        /// <summary>
        /// 写警告信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void WriteWarnInfo(string message)
        {
            WarnInfo.Add(message);
        }
        /// <summary>
        /// 将日志信息显示在文本框
        /// </summary>
        /// <param name="tb">文本框</param>
        /// <param name="logLV">日志等级</param>
        public static void DisplayLog(System.Windows.Forms.TextBox tb, LogLevel logLV)
        {
            tb.Invoke(new Action(() =>
                {
                    tb.Clear();
                    switch (logLV)
                    {
                        case LogLevel.Normal:
                            foreach (string str in NormalInfo)
                            {
                                tb.AppendText(str + "\r\n");
                                tb.AppendText("==============================================================\r\n");
                            }
                            NormalInfo.Clear();
                            break;
                        case LogLevel.Warn:
                            foreach (string str in WarnInfo)
                            {
                                tb.AppendText(str + "\r\n");
                                tb.AppendText("==============================================================\r\n");
                            }
                            WarnInfo.Clear();
                            break;
                        case LogLevel.Error:
                            foreach (string str in ErrorInfo)
                            {
                                tb.AppendText(str + "\r\n");
                                tb.AppendText("==============================================================\r\n");
                            }
                            ErrorInfo.Clear();
                            break;
                        default:
                            break;
                    }
                }));
        }
    }
}
