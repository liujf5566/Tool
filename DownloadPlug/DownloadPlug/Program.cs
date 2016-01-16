using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DownloadPlug
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length != 2)
                {
#if DEBUG
                MessageBox.Show("参数个数不对！");
#endif
                    Application.Exit();
                }
                string url = args[0].ToString();
                string fileName = args[1].ToString();
                DownloadManager dMan = new DownloadManager(url, fileName);
                dMan.StartQuietDown();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownloadPlug.log"), true))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("{0}执行Main()方法出错,错误信息:{1}", DateTime.Now.ToString(), ex.Message);
                }
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
