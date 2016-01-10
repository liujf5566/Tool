using log4net;
using System;
using System.IO;
using System.IO.Pipes;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 命名管道服务端的辅助类
    /// </summary>
    public static class NamedPipeServerHelper
    {
        /// <summary>
        /// 当前进度
        /// </summary>
        public static string Process { get; set; }
        /// <summary>
        /// 数据包信息
        /// </summary>
        public static string BagInfo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public static string Status { get; set; }

        private static readonly ILog _loger;
        private static readonly NamedPipeServerStream _pipeServer;

        static NamedPipeServerHelper()
        {
            _loger = LogManager.GetLogger("NamedPipeServerHelper");
            _pipeServer = new NamedPipeServerStream("WaitDownloadPipe", PipeDirection.Out, 2, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        public static void Start()
        {
            _pipeServer.BeginWaitForConnection(ClientConnected, _pipeServer);
        }
        private static void ClientConnected(IAsyncResult result)
        {
            try
            {
                //接收客户端连接
                NamedPipeServerStream server = (NamedPipeServerStream)result.AsyncState;
                server.EndWaitForConnection(result);
                StreamWriter sw = new StreamWriter(server);
                sw.AutoFlush = true;
                //向客户端发送数据
                while (true)
                {
                    if (Status == "End")
                    {
                        sw.WriteLine("End");
                        break;
                    }
                    if (!string.IsNullOrEmpty(Process))
                    {
                        sw.WriteLine(Process);
                    }
                    if (!string.IsNullOrEmpty(BagInfo))
                    {
                        sw.WriteLine(BagInfo);
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                _loger.Error("ClientConnected()方法：" + ex.Message);
            }
            finally
            {
                if (_pipeServer != null)
                {
                    _pipeServer.Close();
                }
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public static void Close()
        {
            if (_pipeServer != null)
            {
                _pipeServer.Close();
            }
        }
    }
}
