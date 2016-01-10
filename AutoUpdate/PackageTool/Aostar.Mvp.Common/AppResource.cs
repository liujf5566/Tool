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
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Aostar.MVP.Update.Communal
{
    internal class AppResource
    {
        public static ResourceManager GetResource()
        {
            return GetResource(CultureInfo.CurrentCulture.Name);
        }

        public static ResourceManager GetResource(string ciName)
        {
            var ci = new CultureInfo(ciName);
            Thread.CurrentThread.CurrentCulture = ci;
            string CiName = ci.Name;
            string AssemblyPath = Application.StartupPath + "\\Languages\\AppUpdate.Resource.dll";
            Assembly A_Path = Assembly.LoadFrom(AssemblyPath);
            return new ResourceManager("AppUpdate.Resource." + CiName, A_Path);
        }
    }
}