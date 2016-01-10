using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace Aostar.MVP.DownloadService
{
    /// <summary>
    /// FTP辅助类
    /// </summary>
    public class FTPHelper
    {
        private readonly string _userName;
        private readonly string _pw;
        /// <summary>
        /// 是否启用被动模式
        /// </summary>
        private readonly bool _bPassive;
        public FTPHelper(string userName, string pw)
        {
            _userName = userName;
            _pw = pw;
            string strPassive = ConfigurationManager.AppSettings["EnablePassive"];
            bool.TryParse(strPassive, out _bPassive);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpURL">ftp服务器路径</param>
        /// <param name="fileDir">存储下载文件的本地目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="size">文件大小</param>
        public void Download(string ftpURL, string fileDir, string fileName, long size = 0)
        {
            FtpWebResponse ftpResponse = null;
            Stream responseStream = null;
            FileStream outputStream = null;
            try
            {
                if (!ftpURL.EndsWith("/"))
                {
                    ftpURL = ftpURL + @"/" + fileName;
                }
                else
                {
                    ftpURL += fileName;
                }
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpURL);
                reqFTP.UsePassive = _bPassive;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_userName, _pw);
                ftpResponse = (FtpWebResponse)reqFTP.GetResponse();
                responseStream = ftpResponse.GetResponseStream();
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
                    if (size != 0)
                    {
                        NamedPipeServerHelper.Process = string.Format("{0}/{1}", outputStream.Length, size);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //释放资源
                if (ftpResponse != null)
                {
                    ftpResponse.Close();
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
