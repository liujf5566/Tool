using log4net;
using System;
using System.IO;
using System.IO.Pipes;

namespace Aostar.MVP.Update
{
    /// <summary>
    /// 命名管道客户端辅助类
    /// </summary>
    public class NamedPipeClientHelper
    {
        private static readonly ILog _loger;
        private static readonly NamedPipeClientStream _pipeClient;
        private static StreamReader _sr = null;
        static NamedPipeClientHelper()
        {
            _loger = LogManager.GetLogger("NamedPipeClientHelper");
            _pipeClient = new NamedPipeClientStream(".", "WaitDownloadPipe", PipeDirection.In, PipeOptions.Asynchronous);
        }
        /// <summary>
        /// 连接到服务端
        /// </summary>
        public static void Connect()
        {
            try
            {
                _pipeClient.Connect(5000);
                _sr = new StreamReader(_pipeClient);
            }
            catch (Exception ex)
            {
                _loger.Error("Connect()方法：" + ex.Message);
                Close();
                throw;
            }
        }
        /// <summary>
        /// 从服务端读取数据
        /// </summary>
        /// <returns>读取到的数据</returns>
        public static string Read()
        {
            string content = null;
            if (_sr != null && _pipeClient.CanRead)
            {
                content = _sr.ReadLine();
            }
            return content;
        }
        /// <summary>
        /// 断开连接,释放资源
        /// </summary>
        public static void Close()
        {
            if (_sr != null)
            {
                _sr.Close();
            }
            if (_pipeClient != null)
            {
                _pipeClient.Close();
            }
        }
    }
}
