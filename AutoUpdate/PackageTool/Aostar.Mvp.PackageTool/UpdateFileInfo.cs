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
using System.IO;
using Aostar.MVP.Update.Communal;

namespace Aostar.MVP.Update.Config
{
    ///<summary>
    ///升级文件信息
    ///</summary>
    public class UpdateFileInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly FileInfo info;

        ///<summary>
        ///构造函数
        ///</summary>
        ///<param name="fullName">文件全名</param>
        public UpdateFileInfo(string fullName)
        {
            info = new FileInfo(fullName);

            Hidden = ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
        }


        /// <summary>
        /// 文件名
        /// </summary>
        public string FullName
        {
            get { return info.FullName; }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get { return info.Name; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size
        {
            get { return info.Length; }
        }

        /// <summary>
        /// 文件CRCKey
        /// </summary>
        public string CrcKey
        {
            get
            {
                return string.Format("{0:X}", CrcClass.GetFileCRC(info.FullName));
                ; // CrcStream.GetFileCRC(info.FullName).ToString();
            }
        }

        /// <summary>
        /// 文件更新时是否隐藏
        /// </summary>
        public bool Hidden { get; set; }

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