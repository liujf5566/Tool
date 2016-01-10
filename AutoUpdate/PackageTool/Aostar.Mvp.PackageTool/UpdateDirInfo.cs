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
using System.IO;

namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// 升级目录信息
    /// </summary>
    public class UpdateDirInfo
    {
        private readonly DirectoryInfo DirInfo;
        private readonly string _current = "";
        private readonly bool _isEmpty;
        private readonly string _parent = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDirInfo"/> class.
        /// </summary>
        /// <param name="fullDir">The full dir.</param>
        /// <param name="rootDir">The root dir.</param>
        public UpdateDirInfo(string fullDir, string rootDir)
        {
            DirInfo = new DirectoryInfo(fullDir);
            Parents = new List<string>();

            FullName = fullDir;
            RelativeName = "";
            fullDir = fullDir.Replace(rootDir, "");

            if (fullDir.Length != 0)
            {
                RelativeName = fullDir.Substring(fullDir.IndexOf('\\') + 1);
                Parents.AddRange(RelativeName.Split('\\'));
                _current = DirInfo.Name;
                if (_current != rootDir)
                {
                    if (DirInfo.Parent != null)
                    {
                        if (DirInfo.Parent.FullName != rootDir)
                        {
                            _parent = DirInfo.Parent.Name;
                        }
                    }
                }
                _current = _current.Replace("'", "\'");
                _parent = _parent.Replace("'", "\'");
            }
            Parents.Remove(Current);
            _isEmpty = (_current.Length == 0 && _parent.Length == 0);
        }

        ///<summary>
        ///</summary>
        public string Parent
        {
            get { return _parent; }
        }

        ///<summary>
        ///</summary>
        public string Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; private set; }

        /// <summary>
        /// Gets or sets the name of the relative.
        /// </summary>
        /// <value>The name of the relative.</value>
        public string RelativeName { get; private set; }

        public List<string> Parents { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return _isEmpty; }
        }


        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// 	<see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(FullName))
            {
                return base.ToString();
            }
            return FullName;
        }
    }
}