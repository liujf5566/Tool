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
namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadArgs
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        ///<summary>
        ///程序文件路径
        ///</summary>
        public string AppPath { get; set; }

        /// <summary>
        /// Gets or sets the save path.
        /// </summary>
        /// <value>The save path.</value>
        public string SavePath { get; set; }

        /// <summary>
        /// Gets or sets the ex info.
        /// </summary>
        /// <value>The ex info.</value>
        public ExpansionInfo ExInfo { get; set; }

        /// <summary>
        /// Gets or sets the main info.
        /// </summary>
        /// <value>The main info.</value>
        public XmlMainInfo MainInfo { get; set; }

        /// <summary>
        /// Gets or sets the exclude files.
        /// </summary>
        /// <value>The exclude files.</value>
        public ExcludeInfo ExcludeInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is zip XML.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is zip XML; otherwise, <c>false</c>.
        /// </value>
        public bool IsZipXml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is zip file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is zip file; otherwise, <c>false</c>.
        /// </value>
        public bool IsZipFile { get; set; }
    }
}