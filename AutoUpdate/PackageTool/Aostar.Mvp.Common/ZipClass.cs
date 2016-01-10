using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
namespace AppUpdate.Communal
{
    public class ZipClass
    {
        /// <summary>
        /// 压缩数据备份
        /// </summary>
        /// <param name="ZipFilePath">要压缩数据备份路径</param>
        /// <param name="ZipFileName">要压缩数据备份名称</param>
        /// <param name="ZipedFileName">压缩数据备份后的名称</param>
        /// <param name="CompressionLevel">压缩率一般为6</param>
        /// <returns>如果成功返回True</returns>
        public static void ZipFile(string ZipFilePath, string ZipFileName, string ZipedFileName, int CompressionLevel)
        {
            string FileToZip = ZipFilePath.Trim() + "\\" + ZipFileName.Trim();
            string ZipedFile = ZipFilePath.Trim() + "\\" + ZipedFileName.Trim();
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException("压缩备份文件时出错,原因为:找不到" + FileToZip + "文件!");
            }
            FileStream fs = File.OpenRead(FileToZip);
            FileStream StreamToZip = new FileStream(FileToZip, FileMode.Open, FileAccess.Read);
            FileStream ZipFile = File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(ZipFileName.Trim());
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[fs.Length];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish();
            ZipStream.Close();
            fs.Close();
            StreamToZip.Close();
        }

        /// <summary>
        /// ZipFileMain
        /// </summary>
        /// <param name="args"></param>
        public static void ZipFileMain(string[] args)
        {
            string[] filenames = Directory.GetFiles(args[0]);
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(args[1]));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            foreach (string file in filenames)
            {
                FileStream fs = File.OpenRead(file);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(file);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                s.PutNextEntry(entry);
                s.Write(buffer, 0, buffer.Length);
            }
            s.Finish();
            s.Close();
        }
        /// <summary>
        /// 解压缩数据备份
        /// </summary>
        /// <param name="UnZipFilePath">要解压缩的数据备份路径+名称</param>
        /// <param name="UnZipedFileName">解压缩后的名称</param>
        /// <returns>如果成功返回True</returns>
        public static void UnZip(string UnZipFilePath, string UnZipedFileName)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(UnZipedFileName));
            ZipEntry ZipEntry;
            while ((ZipEntry = s.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(UnZipFilePath);
                string fileName = Path.GetFileName(ZipEntry.Name);
                //生成解压目录
                if (Directory.Exists(directoryName))
                { Directory.CreateDirectory(directoryName); }
                if (fileName != String.Empty)
                {
                    //解压文件到指定的目录
                    FileStream streamWriter = File.Create(directoryName + "\\" + ZipEntry.Name);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
        }
    }
}