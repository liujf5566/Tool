using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AppUpdate.Communal
{
	/// <summary>
	/// IniFiles 的摘要说明。
	/// </summary>
	public class IniFiles
	{
		public string path; 
		[DllImport("kernel32")] 
		private static extern long WritePrivateProfileString(string section, string key,string val,string filePath); 
		[DllImport("kernel32")] 
		private static extern int GetPrivateProfileString(string section, string key,string def, StringBuilder retVal, int size,string filePath);

		public IniFiles(string INIPath)
		{
			this.path = INIPath;
		}
		/// <summary>
		/// 写INI文件
		/// </summary>
		/// <param name="Section"></param>
		/// <param name="Key"></param>
		/// <param name="value"></param>
		public void IniWritevalue(string Section,string Key,string value) 
		{ 
			WritePrivateProfileString(Section,Key,value,this.path); 
		} 
		/// <summary>
		/// 读取INI文件
		/// </summary>
		/// <param name="Section"></param>
		/// <param name="Key"></param>
		/// <returns></returns>
		public string IniReadvalue(string Section,string Key) 
		{ 
			StringBuilder temp = new StringBuilder(255); 
 
			int i = GetPrivateProfileString(Section,Key,"",temp, 255, this.path); 
			return temp.ToString(); 
		} 

	}
}
