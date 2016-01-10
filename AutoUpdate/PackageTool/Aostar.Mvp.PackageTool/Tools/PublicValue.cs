using System.Resources;
using System.Globalization;
namespace AppUpdate.Communal
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class PublicValue
    {
        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public static bool IsDebug = false;
        /// <summary>
        /// 0 为正常退出
        /// 1 为连接服务器出错退出
        /// 2 为初始化升级信息时出错退出
        /// 3 为获取文下载目录时出错退出
        /// 4 为获取文件列表时出错退出
        /// 5 为加载数据时出错退出
        /// 6-10 为加载XML文件时出错退出
        /// 99 为从主程序启动软件退出
        /// 100 为没有可用升级退出
        /// </summary>
        public static int ExitCode = 0;
        /// <summary>
        /// 主程序名称
        /// </summary>
        public static string AppName { get; set; }



        public static string LanType = CultureInfo.CurrentCulture.Name;
        /// <summary>
        /// 获取当前区域语言包
        /// </summary>
        public static ResourceManager CR
        {
            get
            {
                if (_CurrentResource == null)
                {
                    _CurrentResource = AppResource.GetResource(LanType);
                }
                return _CurrentResource;
            }
        }
        private static ResourceManager _CurrentResource = null;


    }


}