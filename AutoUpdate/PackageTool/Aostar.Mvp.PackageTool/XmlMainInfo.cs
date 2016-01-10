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

namespace Aostar.MVP.Update.Config
{
    ///<summary>
    ///XML文件Main结点信息
    ///</summary>
    public class XmlMainInfo
    {
        ///<summary>
        ///程序文件名称
        ///</summary>
        public string AppName { get; set; }
        public string IsMust { get; set; }

        /// <summary>
        /// 程序版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 本次升级描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 发布版本时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 升级类型
        /// </summary>
        public string UpdateType { get; set; }
    }
}