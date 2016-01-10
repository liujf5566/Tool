/////////////////////////////////////////////////////////////////////////////
//
// 文 件 名: XmlObject.cs
//
// 功能介绍: 
//
// 创 建 者: 郭正奎
// 创建时间: 2008-12-22 17:19
// 修订历史: 2008-12-22 17:19
//
//  (c)2007-2008 保留所有版权
//
// 
// 
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class PackageTool
    {
        private readonly SetPasBarHandler _setPassBar;
        private bool _fileLoading;
        private UpdateXmlConfig _xmlConfig;

        /// <summary>
        ///
        /// </summary>
        public PackageTool()
        {
            InitializeComponent();
            Load += MainForm_Load;

            _setPassBar = new SetPasBarHandler(SetPasBar);
        }

        private string AppPath
        {
            //get { return txtSrc.Text.Trim().Substring(0, txtSrc.Text.Trim().LastIndexOf("\\")); }
            get { return SaveDir.SelectedPath; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void XmlConfig_XmlCreateProgressEvent(object sender, XmlCreateProgressArgs e)
        {
            Invoke(_setPassBar, e);
        }

        private void SetPasBar(XmlCreateProgressArgs e)
        {
            switch (e.ProgressType)
            {
                case CreateTyep.CreateInfo:
                    prbProd.Style = ProgressBarStyle.Continuous;
                    prbProd.Value++;

                    if (prbProd.Value == prbProd.Maximum)
                    {
                        prbProd.Value = 0;
                    }
                    MsgL.Text = e.Msg;
                    if (e.Complete)
                    {
                        prbProd.Maximum = e.FileCount;
                    }

                    break;
                case CreateTyep.CreateXml:
                    prbProd.Value = e.Progress;
                    MsgL.Text = e.Msg;
                    if (e.Complete)
                    {
                        prbProd.Value = 0;
                    }
                    break;
                case CreateTyep.CreateFile:
                    prbProd.Value = e.Progress;
                    MsgL.Text = e.Msg;
                    if (e.Complete)
                    {
                        prbProd.Value = prbProd.Maximum;
                        btnProduce.Text = "生成(&G)";
                        _xmlConfig = null;
                    }
                    break;
            }
        }

        private void btnProduce_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(name.Text.Trim()))
            {
                MessageBox.Show("压缩包名称不能为空,请填写!","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                name.Focus();
                return;
            }
            // 建立新线程
            if (btnProduce.Text == "生成(&G)")
            {
                //if (!File.Exists(txtSrc.Text))
                if (string.IsNullOrEmpty(txtSrc.Text.Trim()))
                {
                    MessageBox.Show(this, "请选择主入口程序!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSrc_Click(sender, e);
                    return;
                }
               
                //if (txtDest.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show(this, "请选择程序发布目录!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    btnSearDes_Click(sender, e);
                //    return;
                //}
                linkLabel1.Visible = false;
                var files = new List<string>();
                foreach (ListViewItem item in FileList.Items)
                {
                    if (item.Checked)
                    {
                        files.Add(item.Text);
                    }
                }

                //FileVersionInfo VerInfo = FileVersionInfo.GetVersionInfo(txtSrc.Text.Trim());

                var threadArgs = new ThreadArgs
                                     {
                                         MainInfo = new XmlMainInfo
                                                        {
                                                            AppName = txtSrc.Text.Trim(),
                                                            Version =name.Text.Trim() ,
                                                            IsMust=checkBox1.Checked.ToString().ToUpper(),
                                                            Description = DescriptionValue.Text.Trim(),
                                                            UpdateTime = DateTime.Now,
                                                            UpdateType = "",
                                                        },
                                         SavePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Templates), Guid.NewGuid().ToString()),
                                         //txtDest.Text.Trim(),
                                        
                                         ExcludeInfo = new ExcludeInfo(AppPath)
                                     };

                threadArgs.ExcludeInfo.Files.AddRange(files);
                Directory.CreateDirectory(threadArgs.SavePath);
                

                threadArgs.IsZipFile =true;

                if (_xmlConfig == null)
                {
                    _xmlConfig = new UpdateXmlConfig(threadArgs);
                    _xmlConfig.XmlCreateProgressEvent += XmlConfig_XmlCreateProgressEvent;
                }

                prbProd.Minimum = 0;
                prbProd.Value = 0;
                prbProd.Maximum = 100;

                _xmlConfig.Start();
                btnProduce.Text = "停止(&S)";
                linkLabel1.Text = threadArgs.SavePath;
                linkLabel1.Visible = true;

            }
            else
            {
                linkLabel1.Visible = false;
                _xmlConfig.Stop();
                btnProduce.Text = "生成(&G)";
            }
        }

        private void btnSearDes_Click(object sender, EventArgs e)
        {
            if (SaveDir.ShowDialog(this) == DialogResult.OK)
            {
                txtDest.Text = SaveDir.SelectedPath;
            }
        }

        private void btnSrc_Click(object sender, EventArgs e)
        {
            if (SaveDir.ShowDialog(this) == DialogResult.OK)
            {
                txtSrc.Text = SaveDir.SelectedPath;
                //FileVersionInfo version = FileVersionInfo.GetVersionInfo(ofdSrc.FileName);
                //name.Text = version.FileVersion;
                LoadFiles();

                lblFileCount.Text = string.Format("已选择的文件数: {0} 个", FileList.CheckedItems.Count);
            }
        }

        private void LoadFiles()
        {
            _fileLoading = true;

            string[] files = Directory.GetFileSystemEntries(AppPath, "*.*", SearchOption.AllDirectories);
            SuspendLayout();
            FileList.Items.Clear();

            var Items = new List<ListViewItem>();
            foreach (string file in files)
            {
                Items.Add(new ListViewItem(file) {Checked = true});
            }
            FileList.Items.AddRange(Items.ToArray());
            ResumeLayout(false);

            _fileLoading = false;

            chkAll.Checked = true;
        }

        //private void ReadOldUrls()
        //{
        //    var urls = new List<string>();
        //    if (File.Exists(Path.Combine(Path.GetTempPath(), "temp.tep")))
        //    {
        //        using (var SR = new StreamReader(Path.Combine(Path.GetTempPath(), "temp.tep")))
        //        {
        //            string temp = SR.ReadLine();
        //            while (!string.IsNullOrEmpty(temp))
        //            {
        //                if (!urls.Contains(temp))
        //                {
        //                    urls.Add(temp);
        //                }

        //                temp = SR.ReadLine();
        //            }
        //        }
        //    }
        //    txtUrl.Items.AddRange(urls.ToArray());
        //}

        //private void WriterUrls(string url)
        //{
        //    string urlfile = Path.Combine(Path.GetTempPath(), "temp.tep");
        //    if (!txtUrl.Items.Contains(url))
        //    {
        //        txtUrl.Items.Add(url);
        //    }
        //    using (var SW = new StreamWriter(urlfile, false))
        //    {
        //        foreach (string Item in txtUrl.Items)
        //        {
        //            SW.WriteLine(Item);
        //        }
        //    }
        //}

        //private void txtUrl_Validated(object sender, EventArgs e)
        //{
        //    WriterUrls(txtUrl.Text.Trim());
        //}

        private void FileList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!_fileLoading) lblFileCount.Text = string.Format("已选择的文件数: {0} 个", FileList.CheckedItems.Count);

            if(this.FileList.SelectedItems.Count>0)
            {
                ListViewItem obj = this.FileList.SelectedItems[0];
                string file = Convert.ToString(obj.Text);
                if (File.Exists(file))
                {
                    string parent = Directory.GetParent(file).FullName;
                    foreach (ListViewItem item in FileList.Items)
                    {
                        if (obj.Checked && item.Text == parent)
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        #region Nested type: SetPasBarHandler

        private delegate void SetPasBarHandler(XmlCreateProgressArgs e);

        #endregion

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                combVer1.Items.Add(i);
            }
            for (int i = 0; i < 100; i++)
            {
                combVer2.Items.Add(i);
            }
            for (int i = 0; i < 1000; i++)
            {
                combVer3.Items.Add(i);
            }
            if(combVer1.Items.Count>0)
            {
                combVer1.SelectedIndex = 1;
            }
            combVer2.SelectedIndex = 0;
            combVer3.SelectedIndex = 0;
        }

        private void combVer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVersion();
        }

        private void combVer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVersion();
        }

        private void combVer3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVersion();
        }

        private void SetVersion()
        {
            name.Text = combVer1.Text.ToString() + "." + combVer2.Text.ToString() + "." + combVer3.Text.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAll.Checked)
            {
                foreach (ListViewItem item in FileList.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in FileList.Items)
                {
                    item.Checked = false;
                }
            }
        }



        //private void button2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dig=new OpenFileDialog();
        //    if (dig.ShowDialog(this) == DialogResult.OK)
        //    {
        //        textBox1.Text = dig.SafeFileName;
               
                
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dig = new OpenFileDialog();
        //    if (dig.ShowDialog(this) == DialogResult.OK)
        //    {
        //        textBox2.Text = dig.SafeFileName;


        //    }
        //}
    }
}