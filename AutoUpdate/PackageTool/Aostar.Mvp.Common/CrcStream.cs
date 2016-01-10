using System;
using System.IO;

namespace AppUpdate.Communal
{
    /// <summary>
    /// 
    /// </summary>
    public class CrcStream : Stream
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public CrcStream(Stream stream)
        {
            this.stream = stream;
        }
        /// <summary>
        /// 
        /// </summary>
        private Stream stream;

        ///   <summary>   
        ///   Gets   the   underlying   stream.   
        ///   </summary>   
        public Stream Stream
        {
            get { return this.stream; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead
        {
            get { return this.stream.CanRead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek
        {
            get { return this.stream.CanSeek; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite
        {
            get { return this.stream.CanWrite; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            this.stream.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        public override long Length
        {
            get { return this.stream.Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override long Position
        {
            get
            {
                return this.stream.Position;
            }
            set
            {
                this.stream.Position = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            count = this.stream.Read(buffer, offset, count);
            this.readCrc = this.CalculateCrc(this.readCrc, buffer, offset, count);
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);

            this.writeCrc = this.CalculateCrc(this.writeCrc, buffer, offset, count);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="crc"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        uint CalculateCrc(uint crc, byte[] buffer, int offset, int count)
        {
            unchecked
            {
                for (int i = offset, end = offset + count; i < end; i++)
                    crc = (crc >> 8) ^ table[(crc ^ buffer[i]) & 0xFF];
            }
            return crc;
        }
        /// <summary>
        /// 
        /// </summary>
        static private uint[] table = GenerateTable();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static private uint[] GenerateTable()
        {
            unchecked
            {
                uint[] table = new uint[256];

                uint crc;
                const uint poly = 0xEDB88320;
                for (uint i = 0; i < table.Length; i++)
                {
                    crc = i;
                    for (int j = 8; j > 0; j--)
                    {
                        if ((crc & 1) == 1)
                            crc = (crc >> 1) ^ poly;
                        else
                            crc >>= 1;
                    }
                    table[i] = crc;
                }

                return table;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        uint readCrc = unchecked(0xFFFFFFFF);

        ///   <summary>   
        ///   Gets   the   CRC   checksum   of   the   data   that   was   read   by   the   stream   thus   far.   
        ///   </summary>   
        public uint ReadCrc
        {
            get { return unchecked(this.readCrc ^ 0xFFFFFFFF); }
        }

        uint writeCrc = unchecked(0xFFFFFFFF);

        ///   <summary>   
        ///   Gets   the   CRC   checksum   of   the   data   that   was   written   to   the   stream   thus   far.   
        ///   </summary>   
        public uint WriteCrc
        {
            get { return unchecked(this.writeCrc ^ 0xFFFFFFFF); }
        }

        ///   <summary>   
        ///   Resets the read and write checksums.   
        ///   </summary>   
        public void ResetChecksum()
        {
            this.readCrc = unchecked(0xFFFFFFFF);
            this.writeCrc = unchecked(0xFFFFFFFF);
        }

        ///   <summary>   
        ///   获取文件的 CRC 校验码   
        ///   </summary>   
        ///   <param   name="file"></param>   
        ///   <returns></returns>   
        public static uint GetFileCRC(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                CrcStream crcStream = new CrcStream(fs);
                StreamReader reader = new StreamReader(crcStream);
                reader.ReadToEnd();

                return crcStream.ReadCrc;
            }
        }
    }


}


