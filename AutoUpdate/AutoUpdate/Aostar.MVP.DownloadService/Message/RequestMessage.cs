using Newtonsoft.Json;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 客户端请求消息
    /// </summary>
    public class RequestMessage : MessageBase
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        public string currentVersion { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        public string diskCode { get; set; }
        /// <summary>
        /// 当前账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// json反序列化
        /// </summary>        
        /// <param name="jsonString">json字符串</param>
        /// <returns>RequestMessage</returns>
        public new static RequestMessage FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<RequestMessage>(jsonString);
        }
    }
}
