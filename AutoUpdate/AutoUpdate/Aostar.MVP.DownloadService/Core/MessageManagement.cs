using log4net;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 消息管理类
    /// </summary>
    public class MessageManagement
    {
        /// <summary>
        /// 用于写日志的对象
        /// </summary>
        private static readonly ILog _loger;
        /// <summary>
        /// 用于udp通信的客户端
        /// </summary>
        private static readonly UdpClient _udpClient;
        /// <summary>
        /// 配置的服务端IP地址
        /// </summary>
        private static readonly string _serverIP;
        /// <summary>
        /// 配置的服务端端口号
        /// </summary>
        private static readonly string _serverPort;
        /// <summary>
        /// 本机IP地址
        /// </summary>
        private readonly string _ipAddress;
        /// <summary>
        /// 本机机器码
        /// </summary>
        private readonly string _diskcode;
        static MessageManagement()
        {
            _loger = LogManager.GetLogger("MessageManagement");
            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            _serverIP = ConfigurationManager.AppSettings["ServerIP"];
            _serverPort = ConfigurationManager.AppSettings["ServerPort"];
        }
        public MessageManagement()
        {
            _ipAddress = HardwareHelper.GetIpAddress();
            _diskcode = HardwareHelper.GetBIOSSerialNumber();
        }
        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="msgBase">消息基类</param>
        public void Send(MessageBase msgBase)
        {
            Send(msgBase.ToJson());
        }
        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="messge">消息</param>
        public static void Send(string messge)
        {
            //服务器ip地址            
            if (string.IsNullOrEmpty(_serverIP))
            {
                _loger.Error("Send(Message)方法：配置的服务端IP地址为空！");
                return;
            }
            //服务器的多个端口            
            if (string.IsNullOrEmpty(_serverPort))
            {
                _loger.Error("Send(Message)方法：配置的服务端端口号为空！");
                return;
            }
            string[] splitServerPort = _serverPort.Split(',');
            try
            {
                int port = 0;
                IPEndPoint serverEndPoint = null;
                foreach (string p in splitServerPort)
                {
                    port = int.Parse(p);
                    serverEndPoint = new IPEndPoint(IPAddress.Parse(_serverIP), port);
                    byte[] data = Encoding.UTF8.GetBytes(messge);
                    _udpClient.Send(data, data.Length, serverEndPoint);
                }
                _loger.InfoFormat("向服务端发送的消息：{0}", messge);
            }
            catch (Exception ex)
            {
                _loger.Error("Send(Message)方法：" + ex.Message);
            }
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <returns>返回的消息</returns>
        public string Receive()
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = _udpClient.Receive(ref remoteEP);
                if (buffer == null)
                {
                    _loger.Error("Receive()方法：没有从服务端获取到数据！");
                }
                string content = Encoding.UTF8.GetString(buffer);
                _loger.InfoFormat("服务端返回的消息：{0}", content);
                return content;
            }
            catch (Exception ex)
            {
                _loger.Error("Receive()方法：" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 向服务端发送请求消息
        /// </summary>
        public void SendRequestMessage()
        {
            if (string.IsNullOrEmpty(_ipAddress))
            {
                _loger.Error("SendRequestMessage()方法：没有获取到本机IP地址！");
                return;
            }
            if (string.IsNullOrEmpty(_diskcode))
            {
                _loger.Error("SendRequestMessage()方法：没有获取到本机机器码！");
                return;
            }
            //获取当前版本
            string curVersion = VersionHelper.GetQuestVersion();
            if (string.IsNullOrEmpty(curVersion))
            {
                _loger.Error("SendRequestMessage()方法：没有获取到当前版本！");
                return;
            }
            //请求消息
            RequestMessage request = new RequestMessage()
            {
                messageType = "request",
                account = _ipAddress,
                diskCode = _diskcode,
                currentVersion = curVersion
            };
            Send(request);
        }
        /// <summary>
        /// 发送下载完成消息
        /// </summary>
        public void SendDownloadCompletedMessage(string version)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SendDownloadCompletedMessage), version);
        }
        /// <summary>
        /// 发送下载完成消息
        /// </summary>
        /// <param name="version">下载完成的(最大)版本号</param>
        private void SendDownloadCompletedMessage(object version)
        {
            try
            {
                DownloadCompletedMessage dcMsg = new DownloadCompletedMessage();
                dcMsg.messageType = "downloadCompleted";
                dcMsg.account = _ipAddress;
                dcMsg.diskCode = _diskcode;
                dcMsg.version = version.ToString();
                dcMsg.time = DateTime.Now.ToString("yyyyMMddHHmmss");
                //向服务端发送消息
                Send(dcMsg);
                //接收服务端回应的消息(如果服务端未收到消息,尝试再次发送,最多尝试3次)
                string content = Receive();
                if (string.IsNullOrEmpty(content) ||
                    !string.IsNullOrEmpty(content) && !content.Contains("receiveOk"))
                {
                    int time = 3;
                    while (time > 0 && string.IsNullOrEmpty(content))
                    {
                        Send(dcMsg);
                        content = Receive();
                        time--;
                    }
                }
            }
            catch (Exception ex)
            {
                _loger.Error("SendDownloadCompletedMessage(object)方法：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 关闭UDP连接
        /// </summary>
        public void CloseUDP()
        {
            _udpClient.Close();
        }
    }
}
