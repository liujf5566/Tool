using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DownloadPlug
{
    /// <summary>
    /// 下载管理
    /// </summary>
    public class DownloadManager
    {
        /// <summary>
        /// 远程ftpurl
        /// </summary>
        private readonly string _url;
        /// <summary>
        /// 要下载的文件名称
        /// </summary>
        private readonly string _fileName;
        /// <summary>
        /// 下载路径
        /// </summary>
        private static readonly string _downloadDir;
        private static readonly StreamWriter _sw;       
        /// <summary>
        /// 是否启用Http下载
        /// </summary>
        private static readonly bool _enabledHttp;

        static DownloadManager()
        {            
            string strHttp = ConfigurationManager.AppSettings["EnabledHttp"];
            bool.TryParse(strHttp, out _enabledHttp);

            _downloadDir = AppDomain.CurrentDomain.BaseDirectory;
            _sw = new StreamWriter(Path.Combine(_downloadDir, "DownloadPlug.log"), true);
            _sw.AutoFlush = true;
        }
        public DownloadManager(string url, string fileName)
        {
            _url = url;
            _fileName = fileName;
        }

        /// <summary>
        /// 开始静默下载
        /// </summary>
        /// <param name="ftpUrl">ftp地址</param>
        /// <param name="fileName">文件名</param>
        /// <returns>如果下载成功返回true,否则返回false</returns>
        public void StartQuietDown()
        {
            try
            {
                //使用Http下载
                if (_enabledHttp)
                {
                    HttpHelper.Download(_url, _downloadDir, _fileName);
                    _sw.WriteLine("下载完成，ftpurl:{0};fileName:{1};downloadDir:{2}", _url, _fileName, _downloadDir);
                }
                //使用Ftp下载
                else
                {
                    //按照服务端配置的格式解析下载地址
                    string[] ipAddress = Regex.Split(_url, "##", RegexOptions.IgnoreCase);
                    string ip = ipAddress[0];
                    string userName = null;
                    string pw = null;
                    if (ipAddress.Length > 1)
                    {
                        userName = ipAddress[1];
                        pw = ipAddress[2];
                        //解密
                        pw = EncryptHelper.UNEncryptByBase64(pw);
#if DEBUG
                    MessageBox.Show(string.Format("解密后：{0}", pw));
#endif
                    }
                    FTPHelper ftp = new FTPHelper(userName, pw);
                    ftp.Download(ip, _downloadDir, _fileName);
                    _sw.WriteLine("下载完成，ftpurl:{0};fileName:{1};downloadDir:{2}", ip, _fileName, _downloadDir);
                }

            }
            catch (Exception ex)
            {
#if DEBUG
                 MessageBox.Show("下载失败！\n请检测ftp服务是否异常。！");
#endif
                _sw.WriteLine("{0}执行静默下载{1}出错！错误信息:{2}", DateTime.Now.ToString(), _fileName, ex.Message);

            }
            finally
            {
                if (_sw != null)
                {
                    _sw.Close();
                }
                Application.Exit();
            }
        }
    }
}
