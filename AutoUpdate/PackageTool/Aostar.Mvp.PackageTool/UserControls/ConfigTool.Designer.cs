using System.Windows.Forms;
namespace Aostar.MVP.Update.Config
{
    partial class ConfigTool : UserControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDbType = new System.Windows.Forms.ComboBox();
            this.tbServerAddress = new System.Windows.Forms.TextBox();
            this.tbDbName = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbNewestVersion2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbIsMust = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbMaxConnection = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbFtpUsername = new System.Windows.Forms.TextBox();
            this.tbFtpPwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbDownloadType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbConfig.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "数据库类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "主机名或IP地址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "数据库名称：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "用户名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(446, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "密码：";
            // 
            // cmbDbType
            // 
            this.cmbDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbType.FormattingEnabled = true;
            this.cmbDbType.Items.AddRange(new object[] {
            "MySql"});
            this.cmbDbType.Location = new System.Drawing.Point(98, 31);
            this.cmbDbType.Name = "cmbDbType";
            this.cmbDbType.Size = new System.Drawing.Size(102, 21);
            this.cmbDbType.TabIndex = 1;
            this.cmbDbType.SelectedIndexChanged += new System.EventHandler(this.cmbDbType_SelectedIndexChanged);
            // 
            // tbServerAddress
            // 
            this.tbServerAddress.Location = new System.Drawing.Point(356, 31);
            this.tbServerAddress.Name = "tbServerAddress";
            this.tbServerAddress.Size = new System.Drawing.Size(223, 20);
            this.tbServerAddress.TabIndex = 2;
            this.tbServerAddress.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // tbDbName
            // 
            this.tbDbName.Location = new System.Drawing.Point(98, 65);
            this.tbDbName.Name = "tbDbName";
            this.tbDbName.Size = new System.Drawing.Size(102, 20);
            this.tbDbName.TabIndex = 3;
            this.tbDbName.TextChanged += new System.EventHandler(this.tbDbName_TextChanged);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(356, 65);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(84, 20);
            this.tbUserName.TabIndex = 5;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(482, 65);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(97, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(240, 65);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(42, 20);
            this.tbPort.TabIndex = 4;
            this.tbPort.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(205, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "端口：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tbDbName);
            this.groupBox1.Controls.Add(this.cmbDbType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.tbServerAddress);
            this.groupBox1.Location = new System.Drawing.Point(24, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 146);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接配置";
            // 
            // gbConfig
            // 
            this.gbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConfig.Controls.Add(this.groupBox4);
            this.gbConfig.Controls.Add(this.groupBox3);
            this.gbConfig.Location = new System.Drawing.Point(24, 197);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(612, 381);
            this.gbConfig.TabIndex = 11;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "配置项";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbNewestVersion2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.cbIsMust);
            this.groupBox4.Location = new System.Drawing.Point(30, 227);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(559, 65);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "最新版本号配置";
            // 
            // tbNewestVersion2
            // 
            this.tbNewestVersion2.Location = new System.Drawing.Point(86, 26);
            this.tbNewestVersion2.Name = "tbNewestVersion2";
            this.tbNewestVersion2.Size = new System.Drawing.Size(140, 20);
            this.tbNewestVersion2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "最新版本号：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(474, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(301, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "是否强制安装";
            // 
            // cbIsMust
            // 
            this.cbIsMust.AutoSize = true;
            this.cbIsMust.Location = new System.Drawing.Point(280, 29);
            this.cbIsMust.Name = "cbIsMust";
            this.cbIsMust.Size = new System.Drawing.Size(15, 14);
            this.cbIsMust.TabIndex = 26;
            this.cbIsMust.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbAddress);
            this.groupBox3.Controls.Add(this.tbMaxConnection);
            this.groupBox3.Controls.Add(this.tbFtpUsername);
            this.groupBox3.Controls.Add(this.cmbDownloadType);
            this.groupBox3.Controls.Add(this.tbFtpPwd);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btnChange);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(30, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(559, 152);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "下载配置";
            // 
            // tbMaxConnection
            // 
            this.tbMaxConnection.Location = new System.Drawing.Point(113, 71);
            this.tbMaxConnection.Name = "tbMaxConnection";
            this.tbMaxConnection.Size = new System.Drawing.Size(67, 20);
            this.tbMaxConnection.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "下载地址：";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(474, 113);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 27;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "最大下载链接数：";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(252, 30);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(297, 20);
            this.tbAddress.TabIndex = 1;
            // 
            // tbFtpUsername
            // 
            this.tbFtpUsername.Location = new System.Drawing.Point(251, 71);
            this.tbFtpUsername.Name = "tbFtpUsername";
            this.tbFtpUsername.Size = new System.Drawing.Size(126, 20);
            this.tbFtpUsername.TabIndex = 5;
            this.tbFtpUsername.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // tbFtpPwd
            // 
            this.tbFtpPwd.Location = new System.Drawing.Point(419, 71);
            this.tbFtpPwd.Name = "tbFtpPwd";
            this.tbFtpPwd.PasswordChar = '*';
            this.tbFtpPwd.Size = new System.Drawing.Size(130, 20);
            this.tbFtpPwd.TabIndex = 6;
            this.tbFtpPwd.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(190, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "用户名：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "下载类型：";
            // 
            // cmbDownloadType
            // 
            this.cmbDownloadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDownloadType.FormattingEnabled = true;
            this.cmbDownloadType.Items.AddRange(new object[] {
            "ftp",
            "http"});
            this.cmbDownloadType.Location = new System.Drawing.Point(78, 30);
            this.cmbDownloadType.Name = "cmbDownloadType";
            this.cmbDownloadType.Size = new System.Drawing.Size(102, 21);
            this.cmbDownloadType.TabIndex = 1;
            this.cmbDownloadType.SelectedIndexChanged += new System.EventHandler(this.cmbDownloadType_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(383, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "密码：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConfigTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbConfig);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigTool";
            this.Size = new System.Drawing.Size(670, 596);
            this.Load += new System.EventHandler(this.CtrlProcessExcelAndGenerateSql_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbConfig.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDbType;
        private System.Windows.Forms.TextBox tbServerAddress;
        private System.Windows.Forms.TextBox tbDbName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbConfig;
        private TextBox tbMaxConnection;
        private TextBox tbAddress;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private TextBox tbNewestVersion2;
        private Label label11;
        private Button btnAdd;
        private Label label10;
        private CheckBox cbIsMust;
        private GroupBox groupBox3;
        private Button btnChange;
        private ComboBox cmbDownloadType;
        private Label label12;
        private TextBox tbFtpUsername;
        private TextBox tbFtpPwd;
        private Label label9;
        private Label label13;
        private Button button1;

    }
}
