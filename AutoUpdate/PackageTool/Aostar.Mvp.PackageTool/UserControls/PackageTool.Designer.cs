using System.Windows.Forms;

namespace Aostar.MVP.Update.Config
{
    partial class PackageTool : UserControl
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
		
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (this.components != null) {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
		
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearDes = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSrc = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.btnProduce = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.prbProd = new System.Windows.Forms.ProgressBar();
            this.DescriptionValue = new System.Windows.Forms.TextBox();
            this.ofdSrc = new System.Windows.Forms.OpenFileDialog();
            this.ofdExpt = new System.Windows.Forms.OpenFileDialog();
            this.SaveDir = new System.Windows.Forms.FolderBrowserDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.MsgL = new System.Windows.Forms.TextBox();
            this.FileList = new System.Windows.Forms.ListView();
            this.FullNameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.combVer1 = new System.Windows.Forms.ComboBox();
            this.combVer2 = new System.Windows.Forms.ComboBox();
            this.combVer3 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 457);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "生成消息";
            // 
            // btnSearDes
            // 
            this.btnSearDes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearDes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearDes.Location = new System.Drawing.Point(610, 138);
            this.btnSearDes.Name = "btnSearDes";
            this.btnSearDes.Size = new System.Drawing.Size(39, 21);
            this.btnSearDes.TabIndex = 5;
            this.btnSearDes.Text = "选择";
            this.btnSearDes.UseVisualStyleBackColor = true;
            this.btnSearDes.Visible = false;
            this.btnSearDes.Click += new System.EventHandler(this.btnSearDes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "发布位置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "文件列表";
            // 
            // btnSrc
            // 
            this.btnSrc.AllowDrop = true;
            this.btnSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSrc.Location = new System.Drawing.Point(608, 84);
            this.btnSrc.Name = "btnSrc";
            this.btnSrc.Size = new System.Drawing.Size(39, 21);
            this.btnSrc.TabIndex = 2;
            this.btnSrc.Text = "选择";
            this.btnSrc.UseVisualStyleBackColor = true;
            this.btnSrc.Click += new System.EventHandler(this.btnSrc_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "主 程 序";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "更新描述";
            // 
            // txtSrc
            // 
            this.txtSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSrc.Location = new System.Drawing.Point(68, 85);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.ReadOnly = true;
            this.txtSrc.Size = new System.Drawing.Size(534, 21);
            this.txtSrc.TabIndex = 1;
            // 
            // btnProduce
            // 
            this.btnProduce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProduce.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProduce.Location = new System.Drawing.Point(572, 481);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(75, 23);
            this.btnProduce.TabIndex = 0;
            this.btnProduce.Text = "生成(&G)";
            this.btnProduce.UseVisualStyleBackColor = true;
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Location = new System.Drawing.Point(70, 139);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new System.Drawing.Size(534, 21);
            this.txtDest.TabIndex = 4;
            this.txtDest.Visible = false;
            // 
            // prbProd
            // 
            this.prbProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbProd.Location = new System.Drawing.Point(68, 431);
            this.prbProd.Name = "prbProd";
            this.prbProd.Size = new System.Drawing.Size(581, 15);
            this.prbProd.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbProd.TabIndex = 2;
            // 
            // DescriptionValue
            // 
            this.DescriptionValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionValue.Location = new System.Drawing.Point(68, 36);
            this.DescriptionValue.Multiline = true;
            this.DescriptionValue.Name = "DescriptionValue";
            this.DescriptionValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionValue.Size = new System.Drawing.Size(581, 43);
            this.DescriptionValue.TabIndex = 7;
            this.DescriptionValue.Text = "修改了BUG，并且加入了5个新功能！";
            // 
            // ofdSrc
            // 
            this.ofdSrc.DefaultExt = "*.exe";
            this.ofdSrc.Filter = "程序文件(*.exe)|*.exe|所有文件(*.*)|*.*";
            this.ofdSrc.Title = "请选择主程序文件";
            // 
            // ofdExpt
            // 
            this.ofdExpt.DefaultExt = "*.*";
            this.ofdExpt.Filter = "所有文件(*.*)|*.*";
            this.ofdExpt.Multiselect = true;
            this.ofdExpt.Title = "请选择主程序文件";
            // 
            // SaveDir
            // 
            this.SaveDir.Description = "请选择程序发布目录";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 434);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "生成进度";
            // 
            // MsgL
            // 
            this.MsgL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgL.BackColor = System.Drawing.Color.DarkGray;
            this.MsgL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgL.Location = new System.Drawing.Point(68, 455);
            this.MsgL.Name = "MsgL";
            this.MsgL.Size = new System.Drawing.Size(581, 14);
            this.MsgL.TabIndex = 10;
            // 
            // FileList
            // 
            this.FileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileList.CheckBoxes = true;
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FullNameCol});
            this.FileList.Location = new System.Drawing.Point(70, 165);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(579, 218);
            this.FileList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.FileList.TabIndex = 16;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            this.FileList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.FileList_ItemChecked);
            // 
            // FullNameCol
            // 
            this.FullNameCol.Text = "文件名称";
            this.FullNameCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FullNameCol.Width = 556;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 17;
            // 
            // lblFileCount
            // 
            this.lblFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileCount.Location = new System.Drawing.Point(423, 413);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(226, 12);
            this.lblFileCount.TabIndex = 17;
            this.lblFileCount.Text = "已选择的文件数: 0 个";
            this.lblFileCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-1, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "压缩包名称";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Location = new System.Drawing.Point(70, 10);
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Size = new System.Drawing.Size(140, 21);
            this.name.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(519, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "是否强制安装";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(602, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // combVer1
            // 
            this.combVer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combVer1.FormattingEnabled = true;
            this.combVer1.Location = new System.Drawing.Point(272, 10);
            this.combVer1.Name = "combVer1";
            this.combVer1.Size = new System.Drawing.Size(75, 20);
            this.combVer1.TabIndex = 25;
            this.combVer1.SelectedIndexChanged += new System.EventHandler(this.combVer1_SelectedIndexChanged);
            // 
            // combVer2
            // 
            this.combVer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combVer2.FormattingEnabled = true;
            this.combVer2.Location = new System.Drawing.Point(353, 10);
            this.combVer2.Name = "combVer2";
            this.combVer2.Size = new System.Drawing.Size(75, 20);
            this.combVer2.TabIndex = 26;
            this.combVer2.SelectedIndexChanged += new System.EventHandler(this.combVer2_SelectedIndexChanged);
            // 
            // combVer3
            // 
            this.combVer3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combVer3.FormattingEnabled = true;
            this.combVer3.Location = new System.Drawing.Point(434, 10);
            this.combVer3.Name = "combVer3";
            this.combVer3.Size = new System.Drawing.Size(75, 20);
            this.combVer3.TabIndex = 27;
            this.combVer3.SelectedIndexChanged += new System.EventHandler(this.combVer3_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(219, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "版本号:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(73, 116);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 29;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(70, 390);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(72, 16);
            this.chkAll.TabIndex = 30;
            this.chkAll.Text = "文件全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // PackageTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.combVer3);
            this.Controls.Add(this.combVer2);
            this.Controls.Add(this.combVer1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FileList);
            this.Controls.Add(this.MsgL);
            this.Controls.Add(this.DescriptionValue);
            this.Controls.Add(this.prbProd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDest);
            this.Controls.Add(this.txtSrc);
            this.Controls.Add(this.btnProduce);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSrc);
            this.Controls.Add(this.btnSearDes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(670, 550);
            this.Name = "PackageTool";
            this.Size = new System.Drawing.Size(670, 550);
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label label1;
        private Button btnSearDes;
        private Label label2;
        private Label label4;
        private Button btnSrc;
        private Label label5;
        private Label label7;
        private TextBox txtSrc;
        private Button btnProduce;
        private TextBox txtDest;
        private ProgressBar prbProd;
        private TextBox DescriptionValue;
        private OpenFileDialog ofdSrc;
        private OpenFileDialog ofdExpt;
        private FolderBrowserDialog SaveDir;
        private Label label8;
        private TextBox MsgL;
        private ListView FileList;
        private ColumnHeader FullNameCol;
        private Label label3;
        private Label lblFileCount;
        private Label label13;
        private TextBox name;
        private Label label6;
        private CheckBox checkBox1;
        private ComboBox combVer1;
        private ComboBox combVer2;
        private ComboBox combVer3;
        private Label label9;
        private LinkLabel linkLabel1;
        private CheckBox chkAll;

    }
}