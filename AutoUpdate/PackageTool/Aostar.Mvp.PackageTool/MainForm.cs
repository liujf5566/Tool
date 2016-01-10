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
    public partial class MainForm
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Controls.Add(new PackageTool { Dock = DockStyle.Fill });
            tabControl1.TabPages[1].Controls.Add(new ConfigTool { Dock = DockStyle.Fill });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            (tabControl1.TabPages[1].Controls[0] as ConfigTool).DoSomethingWhenClosing();
        }

    }
}