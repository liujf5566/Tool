using System;
using System.IO;
using System.Net;

namespace DownloadPlug
{
    /// <summary>
    /// HTTP辅助类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="httpURL">http服务器路径</param>
        /// <param name="fileDir">存储下载文件的本地目录</param>
        /// <param name="fileName">文件名</param>        
        public static void Download(string httpURL, string fileDir, string fileName)
        {
            WebResponse httpResponse = null;
            Stream responseStream = null;
            FileStream outputStream = null;
            try
            {
                if (!httpURL.EndsWith("/"))
                {
                    httpURL = httpURL + @"/" + fileName;
                }
                else
                {
                    httpURL += fileName;
                }
                HttpWebRequest reqHttp = (HttpWebRequest)WebRequest.Create(httpURL);
                httpResponse = reqHttp.GetResponse();
                responseStream = httpResponse.GetResponseStream();
                //将流写入文件               
                string filePath = string.Format("{0}\\{1}", fileDir, fileName);
                outputStream = new FileStream(filePath, FileMode.Create);
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                int readCount = responseStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = responseStream.Read(buffer, 0, bufferSize);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //释放资源
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (outputStream != null)
                {
                    outputStream.Close();
                }
            }
        }
    }
}
