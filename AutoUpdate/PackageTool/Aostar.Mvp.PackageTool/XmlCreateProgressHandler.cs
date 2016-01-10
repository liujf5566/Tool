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
    /// <summary>
    /// 
    /// </summary>
    public delegate void XmlCreateProgressHandler(object sender, XmlCreateProgressArgs e);


    /// <summary>
    /// 
    /// </summary>
    public class XmlCreateProgressArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="XmlCreateProgressArgs"/> is complete.
        /// </summary>
        /// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
        public bool Complete { get; set; }

        /// <summary>
        /// Gets or sets the type of the progress.
        /// </summary>
        /// <value>The type of the progress.</value>
        public CreateTyep ProgressType { get; set; }

        /// <summary>
        /// Gets or sets the file count.
        /// </summary>
        /// <value>The file count.</value>
        public int FileCount { get; set; }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets the MSG.
        /// </summary>
        /// <value>The MSG.</value>
        public string Msg { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CreateTyep
    {
        /// <summary>
        /// {35A90EBF-F421-44A3-BE3A-47C72AFE47FE}
        /// </summary>
        CreateInfo,
        /// <summary>
        /// {35A90EBF-F421-44A3-BE3A-47C72AFE47FE}
        /// </summary>
        CreateXml,

        /// <summary>
        /// 
        /// </summary>
        CreateFile,
    }
}