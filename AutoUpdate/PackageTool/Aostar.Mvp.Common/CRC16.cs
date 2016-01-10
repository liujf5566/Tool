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

namespace Aostar.MVP.Update.Communal
{
    /// <summary> 
    /// CRC16 的摘要说明。 
    /// </summary> 
    public class CRC16
    {
        #region CRC 16 位校验表

        /// <summary> 
        /// 16 位校验表 Upper 表 
        /// </summary> 
        public static ushort[] uppercrctab = new ushort[]
                                                 {
                                                     0x0000, 0x1231, 0x2462, 0x3653, 0x48c4, 0x5af5, 0x6ca6, 0x7e97,
                                                     0x9188, 0x83b9, 0xb5ea, 0xa7db, 0xd94c, 0xcb7d, 0xfd2e, 0xef1f
                                                 };

        /// <summary> 
        /// 16 位校验表 Lower 表 
        /// </summary> 
        public static ushort[] lowercrctab = new ushort[]
                                                 {
                                                     0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
                                                     0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef
                                                 };

        #endregion

        private static uint crc;

        /// <summary> 
        /// Crc16 
        /// </summary> 
        /// <param name="value"></param> 
        public static void CalcCRC(int value)
        {
            var h = (ushort) ((crc >> 12) & 0x0f);
            var l = (ushort) ((crc >> 8) & 0x0f);
            var temp = (ushort) crc;
            temp = (ushort) (((temp & 0x00ff) << 8) | value);
            temp = (ushort) (temp ^ (uppercrctab[(h - 1) + 1] ^ lowercrctab[(l - 1) + 1]));
            crc = temp;
        }


        /// <summary> 
        /// Crc16 
        /// </summary> 
        /// <param name="buffer"></param> 
        public static uint GetCRC(byte[] buffer)
        {
            return GetCRC(buffer, 0, buffer.Length);
        }

        /// <summary> 
        /// Crc16 
        /// </summary> 
        /// <param name="buffer"></param> 
        /// <param name="offset"></param> 
        /// <param name="length"></param> 
        public static uint GetCRC(byte[] buffer, int offset, int length)
        {
            crc = 0;

            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset < 0 || length < 0 || offset + length > buffer.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = offset; i < length; i++)
            {
                CalcCRC(buffer[i]);
            }

            return crc;
        }

        public static uint GetCRC(string filename)
        {
            crc = 0;

            try
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    var length = (int) stream.Length;
                    var bytes = new byte[length];

                    stream.Read(bytes, 0, length);
                    GetCRC(bytes, 0, length);

                    stream.Close();
                }
            }
            catch (Exception)
            {
            }

            return crc;
        }
    }
}