namespace ExportData
{
    partial class DataForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtNormal = new System.Windows.Forms.TextBox();
            this.tvPerson = new System.Windows.Forms.TreeView();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBarState = new System.Windows.Forms.ProgressBar();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.Logpage = new System.Windows.Forms.TabPage();
            this.MailPage = new System.Windows.Forms.TabPage();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccepter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabControl();
            this.pageNormal = new System.Windows.Forms.TabPage();
            this.pageError = new System.Windows.Forms.TabPage();
            this.pageWarn = new System.Windows.Forms.TabPage();
            this.txtError = new System.Windows.Forms.TextBox();
            this.txtWarn = new System.Windows.Forms.TextBox();
            this.tabInfo.SuspendLayout();
            this.Logpage.SuspendLayout();
            this.MailPage.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.pageNormal.SuspendLayout();
            this.pageError.SuspendLayout();
            this.pageWarn.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件：";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(57, 9);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(588, 21);
            this.txtFile.TabIndex = 1;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(651, 7);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(66, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "浏览";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtNormal
            // 
            this.txtNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNormal.Location = new System.Drawing.Point(3, 3);
            this.txtNormal.Multiline = true;
            this.txtNormal.Name = "txtNormal";
            this.txtNormal.ReadOnly = true;
            this.txtNormal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNormal.Size = new System.Drawing.Size(503, 343);
            this.txtNormal.TabIndex = 3;
            // 
            // tvPerson
            // 
            this.tvPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvPerson.CheckBoxes = true;
            this.tvPerson.Location = new System.Drawing.Point(529, 69);
            this.tvPerson.Name = "tvPerson";
            this.tvPerson.Size = new System.Drawing.Size(260, 432);
            this.tvPerson.TabIndex = 4;
            this.tvPerson.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvPerson_AfterCheck);
            this.tvPerson.Click += new System.EventHandler(this.tvPerson_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(651, 36);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(138, 23);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "设置发件人信息";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtSender
            // 
            this.txtSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSender.Location = new System.Drawing.Point(57, 38);
            this.txtSender.Name = "txtSender";
            this.txtSender.ReadOnly = true;
            this.txtSender.Size = new System.Drawing.Size(588, 21);
            this.txtSender.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "发件人：";
            // 
            // progressBarState
            // 
            this.progressBarState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarState.Location = new System.Drawing.Point(10, 478);
            this.progressBarState.Name = "progressBarState";
            this.progressBarState.Size = new System.Drawing.Size(513, 23);
            this.progressBarState.TabIndex = 5;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(723, 7);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(66, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tabInfo
            // 
            this.tabInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabInfo.Controls.Add(this.Logpage);
            this.tabInfo.Controls.Add(this.MailPage);
            this.tabInfo.Location = new System.Drawing.Point(10, 65);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(513, 407);
            this.tabInfo.TabIndex = 6;
            this.tabInfo.SelectedIndexChanged += new System.EventHandler(this.tabInfo_SelectedIndexChanged);
            // 
            // Logpage
            // 
            this.Logpage.Controls.Add(this.tabLog);
            this.Logpage.Location = new System.Drawing.Point(4, 22);
            this.Logpage.Name = "Logpage";
            this.Logpage.Padding = new System.Windows.Forms.Padding(3);
            this.Logpage.Size = new System.Drawing.Size(523, 381);
            this.Logpage.TabIndex = 0;
            this.Logpage.Text = "日志";
            this.Logpage.UseVisualStyleBackColor = true;
            // 
            // MailPage
            // 
            this.MailPage.Controls.Add(this.btnSend);
            this.MailPage.Controls.Add(this.txtContent);
            this.MailPage.Controls.Add(this.label5);
            this.MailPage.Controls.Add(this.txtSubject);
            this.MailPage.Controls.Add(this.label4);
            this.MailPage.Controls.Add(this.txtAccepter);
            this.MailPage.Controls.Add(this.label3);
            this.MailPage.Location = new System.Drawing.Point(4, 22);
            this.MailPage.Name = "MailPage";
            this.MailPage.Padding = new System.Windows.Forms.Padding(3);
            this.MailPage.Size = new System.Drawing.Size(505, 381);
            this.MailPage.TabIndex = 1;
            this.MailPage.Text = "邮件";
            this.MailPage.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(409, 299);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(66, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(56, 81);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(419, 202);
            this.txtContent.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "内容：";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(56, 46);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(419, 21);
            this.txtSubject.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "主题：";
            // 
            // txtAccepter
            // 
            this.txtAccepter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccepter.Location = new System.Drawing.Point(56, 14);
            this.txtAccepter.Name = "txtAccepter";
            this.txtAccepter.ReadOnly = true;
            this.txtAccepter.Size = new System.Drawing.Size(419, 21);
            this.txtAccepter.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "收件人：";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.pageNormal);
            this.tabLog.Controls.Add(this.pageError);
            this.tabLog.Controls.Add(this.pageWarn);
            this.tabLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLog.Location = new System.Drawing.Point(3, 3);
            this.tabLog.Name = "tabLog";
            this.tabLog.SelectedIndex = 0;
            this.tabLog.Size = new System.Drawing.Size(517, 375);
            this.tabLog.TabIndex = 4;
            // 
            // pageNormal
            // 
            this.pageNormal.Controls.Add(this.txtNormal);
            this.pageNormal.Location = new System.Drawing.Point(4, 22);
            this.pageNormal.Name = "pageNormal";
            this.pageNormal.Padding = new System.Windows.Forms.Padding(3);
            this.pageNormal.Size = new System.Drawing.Size(509, 349);
            this.pageNormal.TabIndex = 0;
            this.pageNormal.Text = "正常信息";
            this.pageNormal.UseVisualStyleBackColor = true;
            // 
            // pageError
            // 
            this.pageError.Controls.Add(this.txtError);
            this.pageError.Location = new System.Drawing.Point(4, 22);
            this.pageError.Name = "pageError";
            this.pageError.Padding = new System.Windows.Forms.Padding(3);
            this.pageError.Size = new System.Drawing.Size(509, 349);
            this.pageError.TabIndex = 1;
            this.pageError.Text = "错误信息";
            this.pageError.UseVisualStyleBackColor = true;
            // 
            // pageWarn
            // 
            this.pageWarn.Controls.Add(this.txtWarn);
            this.pageWarn.Location = new System.Drawing.Point(4, 22);
            this.pageWarn.Name = "pageWarn";
            this.pageWarn.Size = new System.Drawing.Size(509, 349);
            this.pageWarn.TabIndex = 2;
            this.pageWarn.Text = "警告信息";
            this.pageWarn.UseVisualStyleBackColor = true;
            // 
            // txtError
            // 
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Location = new System.Drawing.Point(3, 3);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtError.Size = new System.Drawing.Size(503, 343);
            this.txtError.TabIndex = 4;
            // 
            // txtWarn
            // 
            this.txtWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWarn.Location = new System.Drawing.Point(0, 0);
            this.txtWarn.Multiline = true;
            this.txtWarn.Name = "txtWarn";
            this.txtWarn.ReadOnly = true;
            this.txtWarn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWarn.Size = new System.Drawing.Size(509, 349);
            this.txtWarn.TabIndex = 4;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 513);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.progressBarState);
            this.Controls.Add(this.tvPerson);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtSender);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导入导出";
            this.tabInfo.ResumeLayout(false);
            this.Logpage.ResumeLayout(false);
            this.MailPage.ResumeLayout(false);
            this.MailPage.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.pageNormal.ResumeLayout(false);
            this.pageNormal.PerformLayout();
            this.pageError.ResumeLayout(false);
            this.pageError.PerformLayout();
            this.pageWarn.ResumeLayout(false);
            this.pageWarn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtNormal;
        private System.Windows.Forms.TreeView tvPerson;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarState;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage Logpage;
        private System.Windows.Forms.TabPage MailPage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccepter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabLog;
        private System.Windows.Forms.TabPage pageNormal;
        private System.Windows.Forms.TabPage pageError;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TabPage pageWarn;
        private System.Windows.Forms.TextBox txtWarn;
    }
}

