using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace ExportData.Helper
{
    /// <summary>
    /// 邮件发送辅助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 是否启用SSL加密
        /// </summary>
        public bool IsSSL { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Accepter { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> Attachments { get; set; }

        private readonly SmtpClient _client;
        /// <summary>
        /// 构造函数(初始化发件人信息)
        /// </summary>
        /// <param name="userName">发件人地址</param>
        /// <param name="passWord">密码</param>
        /// <param name="host">服务器</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="isSSL">是否启用SSL</param>
        public MailHelper(string userName, string passWord, string host, string displayName, bool isSSL)
        {
            UserName = userName;
            PassWord = passWord;
            Host = host;
            DisplayName = displayName;
            IsSSL = false;

            _client = new SmtpClient();
            _client.Host = host;
            _client.Port = 25;
            //是否使用安全套接字层加密连接
            _client.EnableSsl = IsSSL;
            //不使用默认凭证，注意此句必须放在 client.Credentials 的上面
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(userName, passWord);
            //邮件通过网络直接发送到服务器
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
        public void Send()
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(UserName, DisplayName, Encoding.UTF8);
                    mail.To.Add(Accepter);
                    mail.Subject = Subject;
                    mail.SubjectEncoding = Encoding.Default;
                    mail.Body = Content;
                    mail.BodyEncoding = Encoding.Default;
                    mail.IsBodyHtml = false;
                    mail.Priority = MailPriority.Normal;
                    //添加附件
                    Attachment attachment = null;
                    if (Attachments.Count > 0)
                    {
                        for (int i = 0; i < Attachments.Count; i++)
                        {
                            string pathFileName = Attachments[i];
                            string extName = Path.GetExtension(pathFileName).ToLower();
                            //判断附件类型
                            if (extName == ".rar" || extName == ".zip")
                            {
                                attachment = new Attachment(pathFileName, MediaTypeNames.Application.Zip);
                            }
                            else
                            {
                                attachment = new Attachment(pathFileName, MediaTypeNames.Application.Octet);
                            }
                            ContentDisposition cd = attachment.ContentDisposition;
                            cd.CreationDate = File.GetCreationTime(pathFileName);
                            cd.ModificationDate = File.GetLastWriteTime(pathFileName);
                            cd.ReadDate = File.GetLastAccessTime(pathFileName);
                            mail.Attachments.Add(attachment);
                        }
                    }
                    _client.Send(mail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
