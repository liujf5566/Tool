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
    ///<summary>
    ///</summary>
    public class ExpansionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpansionInfo"/> class.
        /// </summary>
        public ExpansionInfo()
        {
            BeforeExecuteName = "";
            BeforeExecuteArgs = "";
            AfterExecuteName = "";
            AfterExecuteArgs = "";
        }


        ///<summary>
        ///</summary>
        public string BeforeExecuteName { get; set; }

        ///<summary>
        ///</summary>
        public string BeforeExecuteArgs { get; set; }

        ///<summary>
        ///</summary>
        public string AfterExecuteName { get; set; }

        ///<summary>
        ///</summary>
        public string AfterExecuteArgs { get; set; }
    }
}