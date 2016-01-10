using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CloseIEWarning
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!CheckIEPro()) return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static bool CheckIEPro()
        {
            var list = Process.GetProcesses().Where(pr => pr.ProcessName.ToLower() == "iexplore").ToList();
            if (list.Count == 0) return false;
            return true;
        }
    }
}
