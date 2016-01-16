using System;
using System.Text;

namespace DownloadPlug
{
    /// <summary>
    /// 加密辅助类
    /// </summary>
    public static class EncryptHelper
    {
        /// <summary>
        /// 使用Base64加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptByBase64(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 使用Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UNEncryptByBase64(string str)
        {
            byte[] outBytes = Convert.FromBase64String(str);
            return Encoding.Default.GetString(outBytes);
        }
    }
}
