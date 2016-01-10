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
using System;
using System.IO;
using System.Text;

namespace Aostar.MVP.Update.Communal
{
    public class CrcClass
    {
        private GetFileCRCHandle getFileCRC;

        #region 计算流的CRC

        /// <summary>
        /// 获取流的CRC校验码 
        /// </summary>
        /// <param name="_buffer"></param>
        /// <returns></returns>
        public static uint GetByteCRC(byte[] _buffer)
        {
            var crc32 = new CRC32();
            return crc32.ByteCRC(_buffer);
        }

        #endregion

        #region 技术字符串CRC

        /// <summary>
        /// 获取String
        /// </summary>
        /// <exception cref="ArgumentNullException">_string 为null</exception>
        /// <returns></returns>
        public static uint GetStringCRC(string _string)
        {
            if (_string == null)
            {
                throw (new ArgumentNullException("_string", "参数_string 为null"));
            }
            else
            {
                var crc32 = new CRC32();
                byte[] buffer = Encoding.Default.GetBytes(_string);
                return crc32.ByteCRC(buffer);
            }
        }

        #endregion

        #region 计算文件CRC

        /// <summary>
        /// 校验文件路径
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <exception cref="OverflowException">溢出</exception>
        /// <exception cref="FiletoolargeException">文件长度大于int32</exception>
        /// <returns></returns>
        public static uint GetFileCRC(string _filePath)
        {
            int bufferLength = 2048;
            var crc32 = new CRC32();
            long readedLegth = 0;
            uint result = 0;
            using (var file = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                if (file.Length > long.MaxValue)
                {
                    throw (new FiletoolargeException("文件长度大于int64"));
                }
                else
                {
                    uint state = 0xffffffff;

                    checked
                    {
                        while (readedLegth < file.Length)
                        {
                            if ((readedLegth + bufferLength) >= file.Length)
                            {
                                var buffer = new byte[file.Length - readedLegth];
                                file.Read(buffer, 0, buffer.Length);
                                result = crc32.ByteCRC(buffer, ref state);
                                readedLegth += (file.Length - readedLegth);
                                file.Seek(readedLegth, SeekOrigin.Begin);
                            }
                            else
                            {
                                var buffer = new byte[bufferLength];
                                file.Read(buffer, 0, bufferLength);
                                result = crc32.ByteCRC(buffer, ref state);
                                readedLegth += bufferLength;
                                file.Seek(readedLegth, SeekOrigin.Begin);
                            }
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Gets the file CRC.
        /// </summary>
        /// <param name="_filePath">The _file path.</param>
        /// <param name="_bufferLengt">The _buffer lengt.</param>
        /// <returns></returns>
        public static uint GetFileCRC(string _filePath, int _bufferLengt)
        {
            int bufferLength = _bufferLengt;
            var crc32 = new CRC32();
            long readedLegth = 0;
            uint result = 0;
            using (var file = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                if (file.Length > long.MaxValue)
                {
                    throw (new FiletoolargeException("文件长度大于int64"));
                }
                else
                {
                    uint state = 0xffffffff;

                    checked
                    {
                        while (readedLegth < file.Length)
                        {
                            if ((readedLegth + bufferLength) >= file.Length)
                            {
                                var buffer = new byte[file.Length - readedLegth];
                                file.Read(buffer, 0, buffer.Length);
                                result = crc32.ByteCRC(buffer, ref state);
                                readedLegth += (file.Length - readedLegth);
                                file.Seek(readedLegth, SeekOrigin.Begin);
                            }
                            else
                            {
                                var buffer = new byte[bufferLength];
                                file.Read(buffer, 0, bufferLength);
                                result = crc32.ByteCRC(buffer, ref state);
                                readedLegth += bufferLength;
                                file.Seek(readedLegth, SeekOrigin.Begin);
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 开始异步获取文件CRC
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_callBack">回调函数</param>
        /// <param name="_state"></param>
        public void BeginGetFileCRC(string _filePath, AsyncCallback _callBack, object _state)
        {
            getFileCRC = new GetFileCRCHandle(GetFileCRC);

            getFileCRC.BeginInvoke(_filePath, 2048, _callBack, _state);
        }

        /// <summary>
        /// 开始异步获取文件CRC
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_bufferLength">缓冲区大小</param>
        /// <param name="_callBack">回调函数</param>
        /// <param name="_state"></param>
        public void BeginGetFileCRC(string _filePath, int _bufferLength, AsyncCallback _callBack, object _state)
        {
            getFileCRC = new GetFileCRCHandle(GetFileCRC);

            getFileCRC.BeginInvoke(_filePath, _bufferLength, _callBack, _state);
        }

        public uint EndGetFileCRC(IAsyncResult _asyncResult)
        {
            if (_asyncResult.IsCompleted)
            {
                uint result = getFileCRC.EndInvoke(_asyncResult);
                return result;
            }
            else
            {
                return 0;
            }
        }

        #region Nested type: GetFileCRCHandle

        /// <summary>
        /// 获取 文件、字符串、流 的CRC 
        /// </summary>
        private delegate uint GetFileCRCHandle(string _filePath, int _bufferLength);

        #endregion
    }
}