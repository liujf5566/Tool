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

namespace Aostar.MVP.Update.Communal
{
    /// <summary>
    /// 文件过大异常类
    /// </summary>
    public class FiletoolargeException : Exception
    {
        public FiletoolargeException(string Message)
            : base(Message)
        {
        }
    }
}