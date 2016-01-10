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
using System.Globalization;
using System.Resources;

namespace Aostar.MVP.Update.Communal
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class PublicValue
    {
        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public static bool IsDebug;

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
        public static int ExitCode;


        public static string LanType = CultureInfo.CurrentCulture.Name;

        private static ResourceManager _CurrentResource;

        /// <summary>
        /// 主程序名称
        /// </summary>
        public static string AppName { get; set; }

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
    }
}