using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DownloadPlug
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                MessageBox.Show("参数个数不对！");
                return;
            }
            string url = args[0].ToString();
            string fileName = args[1].ToString();
            DownloadManager dMan = new DownloadManager(url, fileName);
            dMan.StartQuietDown();
        }
    }
}
