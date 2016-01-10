using System;
using System.Collections.Generic;
using System.Text;

namespace AppUpdate.Communal
{
    /// <summary>
    /// 文件过大异常类
    /// </summary>
    public class FiletoolargeException : Exception
    {
        public FiletoolargeException(string Message)
            : base(Message)
        {

        }
    }
}
