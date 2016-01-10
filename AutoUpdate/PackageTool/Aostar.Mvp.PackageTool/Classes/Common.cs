using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Aostar.MVP.Update.Config.Classes
{
   public static class Common
    {
       public static string GetAppConfig(string appKey)
       {
           XmlDocument xDoc = new XmlDocument();
           xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

           var xNode = xDoc.SelectSingleNode("//appSettings");

           var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");

           if (xElem != null)
           {
               return xElem.Attributes["value"].Value;
           }
           return string.Empty;
       }

       public static void SetAppConfig(string appKey, string value)
       {
           XmlDocument xDoc = new XmlDocument();
           string fileName = System.Windows.Forms.Application.ExecutablePath + ".config";
           xDoc.Load(fileName);

           var xNode = xDoc.SelectSingleNode("//appSettings");

           var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");

           if (xElem != null)
           {
               xElem.Attributes["value"].Value = value;
           }
           xDoc.Save(fileName);
       }

       public static bool Equal(this string str, string otherStr)
       {
           return (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(otherStr)) ||
                (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(otherStr) && str == otherStr);
       }
    }
}
