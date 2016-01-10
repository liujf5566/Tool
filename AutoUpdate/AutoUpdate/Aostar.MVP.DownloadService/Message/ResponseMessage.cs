using Newtonsoft.Json;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 服务端返回的消息
    /// </summary>
    public class ResponseMessage : MessageBase
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string downloadIP { get; set; }
        /// <summary>
        /// 新版本(最大版本号)
        /// </summary>
        public string maxVersion { get; set; }
        /// <summary>
        /// 所有需要下载的版本
        /// </summary>
        public VersionInfo[] allVersion { get; set; }
        /// <summary>
        /// 服务端返回的用于下载的持续时间(单位：分钟)
        /// <remarks>
        /// 当超过此时间时,如果不向服务端发消息的话,服务端会认为客户端已断开连接【注：客户端使用此时间时,要比实际值小;因为向服务端发送消息会花费一些时间,默认减1分钟】
        /// </remarks>         
        /// </summary>
        public int heartTime { get; set; }
        /// <summary>
        /// json反序列化
        /// </summary>        
        /// <param name="jsonString">json字符串</param>
        /// <returns>ResponseMessage</returns>
        public new static ResponseMessage FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<ResponseMessage>(jsonString);
        }
    }
    /// <summary>
    /// 版本信息
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// 版本名称
        /// </summary>
        public string versionName { get; set; }
        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool isMust { get; set; }
    }
}
