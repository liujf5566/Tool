using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CloseIEWarning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var list = Process.GetProcesses().Where(pr => pr.ProcessName.ToLower() == "iexplore").ToList();
                if (list.Count == 0) return;
                for (int i = 0; i < list.Count; i++)
                    try
                    {
                        list[i].Kill();
                    }
                    catch { }

                this.Close();
            }
            catch (Exception ex) { }
            finally { this.Close(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
