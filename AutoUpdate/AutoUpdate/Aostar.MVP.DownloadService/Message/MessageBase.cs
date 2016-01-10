using Newtonsoft.Json;

namespace Aostar.MVP.DownloadService
{
    //为了和Java配合Json的消息格式，所有消息中定义的属性都必须以小写字母开头
    /// <summary>
    /// 消息基类
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// 新版自动更新程序标志
        /// </summary>
        public string newFlag
        {
            get
            {
                return "new";
            }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string messageType { get; set; }
        /// <summary>
        /// 将对象序列化成json字符串
        /// </summary>        
        /// <returns>Json</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// json反序列化
        /// </summary>        
        /// <param name="jsonString">json字符串</param>
        /// <returns>MessageBase</returns>
        public static MessageBase FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<MessageBase>(jsonString);
        }
    }
}
