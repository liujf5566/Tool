using ExportData.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ExportData
{
    public partial class DataForm : Form
    {
        private MailHelper _mailHelper;
        private readonly List<Person> _persons;
        /// <summary>
        /// 附件目录
        /// </summary>
        private string _accessoryDir;
        public DataForm()
        {
            InitializeComponent();
            _persons = new List<Person>();
        }
        /// <summary>
        /// 初始化通讯录
        /// </summary>
        private void InitializePersonBook()
        {
            DataTable dt = DataHelper.CreateDataTableByExcel(txtFile.Text, "统计");
            List<ItemInfo> items = DataHelper.CreatePersonBook(dt);
            TreeNode root = new TreeNode("所有项目组");
            foreach (var item in items)
            {
                //添加项目组
                TreeNode itemNode = new TreeNode(item.ItemName);
                //添加组中的人员
                foreach (var p in item.Persons)
                {
                    TreeNode personNode = new TreeNode(string.Format("{0}({1})", p.Name, p.Number));
                    personNode.Tag = p;
                    itemNode.Nodes.Add(personNode);
                    _persons.Add(p);
                }
                root.Nodes.Add(itemNode);
            }
            if (tvPerson.InvokeRequired)
            {
                tvPerson.Invoke(new Action(() =>
                    {
                        tvPerson.Nodes.Clear();
                        tvPerson.Nodes.Add(root);
                    }));
            }
            else
            {
                tvPerson.Nodes.Clear();
                tvPerson.Nodes.Add(root);
            }
        }
        /// <summary>
        /// 浏览Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFile.FileName;
                InitializePersonBook();
                btnCreate.Enabled = true;
            }
        }
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                //创建附件目录
                string dir = Path.GetFullPath(@"..\..\Accessory");
                string time = DateTime.Now.ToString("yyyyMMddhhmmss");
                _accessoryDir = string.Format("{0}\\{1}", dir, time);
                Directory.CreateDirectory(_accessoryDir);
                //开始生成内存数据
                int month = int.Parse(txtMonth.Text.Trim());
                DataTable dt = DataHelper.CreateDataTableByExcel(txtFile.Text, "任务明细");
                List<PersonalPerformance> pps = DataHelper.CreatePersonalPerformanceNew(dt, _persons, "研发中心", month);
                var noTaskPPS = DataHelper.CreatePersonalPerformance(_persons, pps, "研发中心", month);
                pps.AddRange(noTaskPPS);
                List<float> scores = DataHelper.GetSortedScores(pps);
                //开始生成新的Excel
                string path = "";
                progressBarState.Minimum = 0;
                progressBarState.Maximum = pps.Count;
                progressBarState.Step = 1;
                progressBarState.Value = 0;
                foreach (var pp in pps)
                {
                    if (progressBarState.InvokeRequired)
                    {
                        progressBarState.Invoke(new Action(() => progressBarState.PerformStep()));
                    }
                    else
                    {
                        progressBarState.PerformStep();
                    }
                    string accessory = pp.Person.Accessory;
                    try
                    {
                        path = string.Format("{0}\\{1}", _accessoryDir, accessory);
                        //创建Excel
                        ExcelCreate.Create(pp, path, scores);
                        LogHelper.WriteNormalInfo(accessory + "生成成功。");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteErrorInfo(string.Format("{0}生成失败！失败原因：{1}", accessory, ex.Message));
                    }
                }
                //显示日志
                LogHelper.DisplayLog(txtNormal, LogLevel.Normal);
                LogHelper.DisplayLog(txtWarn, LogLevel.Warn);
                LogHelper.DisplayLog(txtError, LogLevel.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 设置发件人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            SenderForm sf = new SenderForm();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                txtSender.Text = sf.MailAdderss;
                btnSend.Enabled = !string.IsNullOrEmpty(txtNormal.Text.Trim()) &&
                     !string.IsNullOrEmpty(txtSender.Text.Trim());
                if (btnSend.Enabled)
                {
                    _mailHelper = new MailHelper(sf.MailAdderss, sf.MailPW, sf.SMTPServer, sf.DisplayNmae, sf.IsSSL);
                }
            }
        }
        /// <summary>
        ///发送邮件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            //查找所有选中的人员
            List<TreeNode> tns = new List<TreeNode>();
            GetSelectedNodes(tvPerson.Nodes, tns);
            int nCount = tns.Count();
            if (nCount == 0)
            {
                MessageBox.Show("请选择收件人！");
                return;
            }
            //设置进度条信息
            progressBarState.Minimum = 0;
            progressBarState.Maximum = nCount;
            progressBarState.Step = 1;
            progressBarState.Value = 0;
            //对每一个选中的人员发邮件
            foreach (TreeNode node in tns)
            {
                if (progressBarState.InvokeRequired)
                {
                    progressBarState.Invoke(new Action(() => progressBarState.PerformStep()));
                }
                else
                {
                    progressBarState.PerformStep();
                }
                Person person = node.Tag as Person;
                string name = string.Format("{0}({1})", person.Name, person.Number);
                if (string.IsNullOrEmpty(person.Mail))
                {
                    LogHelper.WriteWarnInfo(name + "邮件地址为空！取消邮件发送。");
                    continue;
                }
                _mailHelper.Accepter = person.Mail;
                _mailHelper.Attachments = new List<string> 
                {
                    string.Format("{0}\\{1}", _accessoryDir, person.Accessory)
                };
                _mailHelper.Subject = txtSubject.Text;
                _mailHelper.Content = txtContent.Text;
                try
                {
                    _mailHelper.Send();
                    LogHelper.WriteNormalInfo(name + "邮件发送成功。");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteErrorInfo(string.Format("{0}邮件发送失败！失败原因：{1}", name, ex.Message));
                }
            }
            //显示日志
            LogHelper.DisplayLog(txtNormal, LogLevel.Normal);
            LogHelper.DisplayLog(txtWarn, LogLevel.Warn);
            LogHelper.DisplayLog(txtError, LogLevel.Error);
        }
        private void tabInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabInfo.SelectedIndex == 1)
            {
                btnSend.Enabled = !string.IsNullOrEmpty(txtNormal.Text.Trim()) &&
                     !string.IsNullOrEmpty(txtSender.Text.Trim());
            }
        }
        private void tvPerson_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeViewHelper.CheckControl(e);
        }
        private void tvPerson_Click(object sender, EventArgs e)
        {
            txtAccepter.Clear();
            List<TreeNode> tns = new List<TreeNode>();
            GetSelectedNodes(tvPerson.Nodes, tns);
            //添加收件人信息
            Person person = null;
            foreach (var tn in tns)
            {
                person = tn.Tag as Person;
                string name = string.Format("{0}({1})", person.Name, person.Number);
                if (txtAccepter.InvokeRequired)
                {
                    txtAccepter.Invoke(new Action(() =>
                    {
                        txtAccepter.AppendText(name + ",");
                    }));
                }
                else
                {
                    txtAccepter.AppendText(name + ",");
                }
            }
        }
        /// <summary>
        /// 查找所有选中的人员
        /// </summary>
        /// <param name="tns">节点集合</param>
        /// <param name="selectedNodes">存储选中的节点</param>
        private void GetSelectedNodes(TreeNodeCollection tns, List<TreeNode> selectedNodes)
        {
            foreach (TreeNode tn in tns)
            {
                if (tn.Checked && tn.Tag != null)
                {
                    selectedNodes.Add(tn);
                }
                if (tn.Nodes.Count > 0)
                {
                    GetSelectedNodes(tn.Nodes, selectedNodes);
                }
            }
        }
    }
}
