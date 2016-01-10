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
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcludeInfo
    {
        private List<string> _files;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeInfo"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ExcludeInfo(string path)
        {
            SetFiles(path);
        }

        /// <summary>
        /// Gets the args.
        /// </summary>
        /// <value>The args.</value>
        public string Args
        {
            get { return ConfigurationManager.AppSettings["ExcludeFiles"]; }
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public List<string> Files
        {
            get { return _files; }
        }

        /// <summary>
        /// Sets the files.
        /// </summary>
        private void SetFiles(string path)
        {
            _files = new List<string>();

            string[] exItems = Args.Split(char.Parse("|"));

            foreach (string item in exItems)
            {
                _files.AddRange(Directory.GetFiles(path, item, SearchOption.AllDirectories));
            }
        }
    }
}