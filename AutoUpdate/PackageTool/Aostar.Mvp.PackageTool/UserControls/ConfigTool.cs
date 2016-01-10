using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aostar.MVP.Update.Config.Classes;
using System.IO;
using System.Text.RegularExpressions;

namespace Aostar.MVP.Update.Config
{
    public partial class ConfigTool 
    {
        DbConnection MySqlConnection, OracleConnection,selectedConnection;
        Dictionary<string, string> dicRoleNameId = new Dictionary<string, string>();
        Dictionary<string, string> dicAppidResource = new Dictionary<string, string>();
        string ftpRegex = "^ftp\\:";
        string httpRegex = "^http\\:";
        string versionRegex = "^\\d+\\.\\d+\\.\\d+$";
        public ConfigTool()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            selectedConnection.ServerAddress = tbServerAddress.Text.Trim();
        }

        private void CtrlProcessExcelAndGenerateSql_Load(object sender, EventArgs e)
        {
            gbConfig.Enabled = false;
            LoadConnectionInfo();
            switch (int.Parse(Common.GetAppConfig("LastUsedDbType")))
            {
                case (int)EDbType.MySql:
                    selectedConnection = MySqlConnection;
                    break;
                default:
                    selectedConnection = OracleConnection;
                    break;
            }
            InitialConnection(selectedConnection);
        }

        void InitialConnection(DbConnection dbConnection)
        {
            cmbDbType.SelectedIndex = (int)dbConnection.DbType;
            tbServerAddress.Text = dbConnection.ServerAddress;
            tbDbName.Text = dbConnection.DbName;
            tbPort.Text = dbConnection.Port;
            tbUserName.Text = dbConnection.UserName;
            tbPassword.Text = dbConnection.Password;
        }

        void LoadConnectionInfo()
        {
            MySqlConnection = new DbConnection
            {
                DbType=EDbType.MySql,
                ServerAddress = Common.GetAppConfig("MySqlServerAddress"),
                DbName = Common.GetAppConfig("MySqlDbName"),
                Port=Common.GetAppConfig("MySqlPort"),
                UserName = Common.GetAppConfig("MySqlUserName"),
                Password = Common.GetAppConfig("MySqlPassword")
            };

            OracleConnection = new DbConnection
            {
                DbType = EDbType.Oracle,
                ServerAddress = Common.GetAppConfig("OracleServerAddress"),
                DbName = Common.GetAppConfig("OracleDbName"),
                Port = Common.GetAppConfig("OraclePort"),
                UserName = Common.GetAppConfig("OracleUserName"),
                Password = Common.GetAppConfig("OraclePassword")
            };
        }

        private void cmbDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbDbType.SelectedIndex)
            {
                case (int)EDbType.MySql:
                    selectedConnection = MySqlConnection;
                    break;
                default:
                    selectedConnection = OracleConnection;
                    break;
            }
            InitialConnection(selectedConnection);
            Common.SetAppConfig("LastUsedDbType", cmbDbType.SelectedIndex.ToString());
            label5.Text = cmbDbType.SelectedIndex == (int)EDbType.MySql ? "数据库名称：" : "服务名：";
        }

        private void tbDbName_TextChanged(object sender, EventArgs e)
        {
            selectedConnection.DbName = tbDbName.Text.Trim();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            selectedConnection.Port = tbPort.Text.Trim();

        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            selectedConnection.UserName = tbUserName.Text.Trim();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            selectedConnection.Password = tbPassword.Text.Trim();
        }

        public void DoSomethingWhenClosing()
        {
            if (MySqlConnection != null)
            {
                Common.SetAppConfig("MySqlServerAddress", MySqlConnection.ServerAddress);
                Common.SetAppConfig("MySqlDbName", MySqlConnection.DbName);
                Common.SetAppConfig("MySqlPort", MySqlConnection.Port);
                Common.SetAppConfig("MySqlUserName", MySqlConnection.UserName);
                Common.SetAppConfig("MySqlPassword", MySqlConnection.Password);
            }
            if (OracleConnection != null)
            {

                Common.SetAppConfig("OracleServerAddress", OracleConnection.ServerAddress);
                Common.SetAppConfig("OracleDbName", OracleConnection.DbName);
                Common.SetAppConfig("OraclePort", OracleConnection.Port);
                Common.SetAppConfig("OracleUserName", OracleConnection.UserName);
                Common.SetAppConfig("OraclePassword", OracleConnection.Password);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            #region Validate inputs
            if (string.IsNullOrEmpty(tbAddress.Text.Trim()))
            {
                MessageBox.Show("请输入下载地址");
                return;
            }
            if (string.IsNullOrEmpty(tbMaxConnection.Text.Trim()))
            {
                MessageBox.Show("请输入最大下载链接数");
                return;
            }
            if (cmbDownloadType.Text == "ftp")
            {
                if (!Regex.IsMatch(tbAddress.Text.Trim(), ftpRegex))
                {
                    MessageBox.Show(string.Format("下载地址请以'{0}'开头", ftpRegex));
                    return;
                }
            }
            if (cmbDownloadType.Text == "http")
            {
                if (!Regex.IsMatch(tbAddress.Text.Trim(), httpRegex))
                {
                    MessageBox.Show(string.Format("下载地址请以'{0}'开头", httpRegex));
                    return;
                }
            }
            int maxNum;
            string info = "最大下载链接数必须为一个大于0的整数";
            if (int.TryParse(tbMaxConnection.Text.Trim(), out maxNum))
            {
                if (maxNum < 1)
                {
                    MessageBox.Show(info);
                    return;
                }
            }
            else
            {
                MessageBox.Show(info);
                return;
            }
            #endregion

            string sign = cmbDownloadType.Text == "http" ? tbAddress.Text.Trim() :
                string.Format("{0}##{1}##{2}", tbAddress.Text.Trim(), tbFtpUsername.Text.Trim(), tbFtpPwd.Text.Trim());
            string maxnum = tbMaxConnection.Text;
            string sql = string.Format("update sign_transfer set sign='{0}',maxnum='{1}' where type=1", sign, maxnum);
            try
            {
                using (DbOperator dbOperator = new DbOperator(selectedConnection))
                {
                    dbOperator.ExecuteNonQuery(sql);
                    MessageBox.Show("修改成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbOperator dbOperator = new DbOperator(selectedConnection))
                {
                    List<string> addressConfigItems = GetAddressConfig(dbOperator);
                    SetAddressConfigItems(addressConfigItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<string> GetAddressConfig(DbOperator dbOperator)
        {
            List<string> configItems = new List<string>();
            string sql = "select sign,maxnum from sign_transfer where type=1";
            DataSet ds = dbOperator.Execute(sql);
            DataRow firstRow = ds.Tables[0].Rows[0];
            string sign = firstRow[0].ToString();
            string maxnum = firstRow[1].ToString();
            configItems.Add(sign);
            configItems.Add(maxnum);
            return configItems;
        }

        void SetAddressConfigItems(List<string> configItems)
        {
            gbConfig.Enabled = true;
            string sign = configItems[0];
            string maxnum = configItems[1];

            tbMaxConnection.Text = maxnum;
            string downloadType = Regex.IsMatch(sign, ftpRegex) ? "ftp" : "http";
            cmbDownloadType.Text = downloadType;
            string[] signItems = Regex.Split(sign,"##");
            string address = downloadType == "http" ? sign : signItems[0];
            tbAddress.Text = address;
            if (downloadType == "ftp")
            {
                string userName = signItems[1];
                string pwd = signItems[2];
                tbFtpUsername.Enabled = true;
                tbFtpUsername.Text = userName;
                tbFtpPwd.Enabled = true;
                tbFtpPwd.Text = pwd;
            }
            else
            {

                tbFtpUsername.Enabled = false;
                tbFtpUsername.Text = "";
                tbFtpPwd.Enabled = false;
                tbFtpPwd.Text = "";
            }
        }

        private void cmbDownloadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFtp = cmbDownloadType.Text == "ftp";
            tbFtpPwd.Enabled = tbFtpUsername.Enabled = isFtp;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(tbNewestVersion2.Text.Trim(), versionRegex))
                {
                    MessageBox.Show("最新版本号的格式必须是：数字.数字.数字");
                    return;
                }
                using (DbOperator dbOperator = new DbOperator(selectedConnection))
                {
                    string sql = string.Format("insert into version_config(version,must) values('{0}','{1}')",
                        tbNewestVersion2.Text.Trim(), cbIsMust.Checked ? "yes" : "no");
                    dbOperator.ExecuteNonQuery(sql);

                    sql = string.Format("update sign_transfer set version='{0}' where type=1", tbNewestVersion2.Text.Trim());
                    dbOperator.ExecuteNonQuery(sql);
                    MessageBox.Show("添加成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
