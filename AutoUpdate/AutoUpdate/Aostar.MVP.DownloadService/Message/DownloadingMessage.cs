
namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// 正在下载资源的消息
    /// </summary>
    public class DownloadingMessage : MessageBase
    {
        /// <summary>
        /// 正在下载的版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        public string diskCode { get; set; }
        /// <summary>
        /// 当前账号
        /// </summary>
        public string account { get; set; }
    }
}
