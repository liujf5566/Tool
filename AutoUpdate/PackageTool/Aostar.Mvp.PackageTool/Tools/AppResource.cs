using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace AppUpdate.Communal
{
    class AppResource
    {
        public static ResourceManager GetResource()
        {
            return GetResource(CultureInfo.CurrentCulture.Name);
        }
        public static ResourceManager GetResource(string ciName)
        {
            CultureInfo ci = new CultureInfo(ciName);
            Thread.CurrentThread.CurrentCulture = ci;
            string CiName = ci.Name;
            string AssemblyPath = Application.StartupPath + "\\Languages\\AppUpdate.Resource.dll";
            Assembly A_Path = Assembly.LoadFrom(AssemblyPath);
            return new ResourceManager("AppUpdate.Resource." + CiName, A_Path);
        }
    }
}