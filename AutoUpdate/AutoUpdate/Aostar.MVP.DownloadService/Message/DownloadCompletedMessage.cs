using Newtonsoft.Json;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 下载完成消息
    /// </summary>
    public class DownloadCompletedMessage : MessageBase
    {
        /// <summary>
        /// 账号(IP)
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        public string diskCode { get; set; }
        /// <summary>
        /// 下载完成时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 下载完成的版本(最大版本号)
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// json反序列化
        /// </summary>        
        /// <param name="jsonString">json字符串</param>
        /// <returns>DownloadCompletedMessage</returns>
        public new static DownloadCompletedMessage FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<DownloadCompletedMessage>(jsonString);
        }
    }
}
