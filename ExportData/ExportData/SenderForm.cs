using System;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ExportData
{
    public partial class SenderForm : Form
    {
        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        public string MailAdderss { get; private set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string MailPW { get; private set; }
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string SMTPServer { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayNmae { get; set; }
        /// <summary>
        /// 是否启用SSL加密
        /// </summary>
        public bool IsSSL { get; set; }
        private readonly string _xmlPath;

        public SenderForm()
        {
            InitializeComponent();
            _xmlPath = @"..\..\Sender.xml";
        }
        private void SenderForm_Load(object sender, EventArgs e)
        {
            XElement root = XElement.Load(_xmlPath);
            var address = root.Element("Address").Value;
            var pw = root.Element("PW").Value;
            var smtp = root.Element("SMTP").Value;
            var displayName = root.Element("DisplayName").Value;
            if (address != null)
            {
                txtMailAddress.Text = address.ToString();
            }
            if (pw != null)
            {
                txtPW.Text = Decode(pw.ToString());
                if (!string.IsNullOrEmpty(pw.ToString()))
                {
                    chkPW.Checked = true;
                }
            }
            if (smtp != null)
            {
                cmbHost.Text = smtp.ToString();
            }
            if (displayName != null)
            {
                txtDisplayName.Text = displayName.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMailAddress.Text.Trim()))
            {
                MessageBox.Show("请输入有效的邮箱地址！");
                txtMailAddress.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            if (string.IsNullOrEmpty(txtPW.Text.Trim()))
            {
                MessageBox.Show("请输入密码！");
                txtPW.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            if (string.IsNullOrEmpty(cmbHost.Text.Trim()))
            {
                MessageBox.Show("请输入SMTP服务器！");
                cmbHost.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            MailAdderss = txtMailAddress.Text.Trim();
            MailPW = txtPW.Text.Trim();
            SMTPServer = cmbHost.Text.Trim();
            DisplayNmae = txtDisplayName.Text.Trim();
            WriteXml();
        }
        /// <summary>
        /// 将数据写入XML
        /// </summary>
        private void WriteXml()
        {
            XElement root = XElement.Load(_xmlPath);
            root.Element("Address").Value = txtMailAddress.Text;
            if (chkPW.Checked)
            {
                root.Element("PW").Value = Encrypt(txtPW.Text);
            }
            root.Element("SMTP").Value = cmbHost.Text;
            root.Element("DisplayName").Value = txtDisplayName.Text;
            root.Save(_xmlPath);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Encrypt(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Decode(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            return Encoding.Default.GetString(outputb);
        }
    }
}
