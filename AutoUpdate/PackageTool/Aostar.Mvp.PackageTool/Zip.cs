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
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// Zips the file main.
        /// </summary>
        /// <param name="zipSrc">The zip dir.</param>
        /// <param name="filezipName">Name of the filezip.</param>
        public static void ZipFile(string zipSrc, string filezipName)
        {
            try
            {
                string[] filenames;

                if (File.Exists(zipSrc))
                {
                    filenames = new[] {zipSrc};
                }
                else if (Directory.Exists(zipSrc))
                {
                    filenames = Directory.GetFiles(zipSrc, "*.*", SearchOption.AllDirectories);
                }
                else
                {
                    new FileNotFoundException("指定源不存在!");
                    return;
                }


                using (var s = new ZipOutputStream(File.Create(filezipName)))
                {
                    s.SetLevel(6); // 0 - store only to 9 - means best compression
                    var buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        var entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }

        /// <summary>
        /// Uns the zip.
        /// </summary>
        /// <param name="filezipName">Name of the filezip.</param>
        /// <param name="UpzipDir">The upzip dir.</param>
        public static void UnZip(string filezipName, string UpzipDir)
        {
            var s = new ZipInputStream(File.OpenRead(filezipName));

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(UpzipDir);
                string fileName = Path.GetFileName(theEntry.Name);

                //生成解压目录
                Directory.CreateDirectory(directoryName);

                if (fileName != String.Empty)
                {
                    //解压文件到指定的目录
                    FileStream streamWriter = File.Create(UpzipDir +"\\"+ theEntry.Name);

                    int size = 2048;
                    var data = new byte[2048];
                    while (true)
                    {
                        if (size == s.Length)
                        {
                            break;
                        }
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
        public static void ZipFile2(string strFile, string strZip)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                strFile += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            zip(strFile, s, strFile);
            s.Finish();
            s.Close();
        }

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {

                if (Directory.Exists(file))
                {
                    zip(file, s, staticFile);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }


        public static string unZipFile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件),准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件,创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            if (size == s.Length)
                            {
                                break;
                            }
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
                    //if (s.Available == 1)
                    //{
                    //    break;
                    //}
                }
                s.Close();

                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }

        public static void UnZip2(string TargetFile, string fileDir)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile));

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                //theEntry = s.GetNextEntry();
                string directoryName = Path.GetDirectoryName(fileDir);
                string fileName = Path.GetFileName(theEntry.Name);

                //生成解压目录

                Directory.CreateDirectory(directoryName);

                if (fileName != String.Empty)
                {
                    //如果文件的压缩后大小为0那么说明这个文件是空的,因此不需要进行读出写入

                    if (theEntry.CompressedSize == 0)
                        break;
                    //解压文件到指定的目录

                    directoryName = Path.GetDirectoryName(fileDir + theEntry.Name);
                    //建立下面的目录和子目录

                    Directory.CreateDirectory(directoryName);

                    FileStream streamWriter = File.Create(fileDir + theEntry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        if (size == s.Length)
                        {
                            break;
                        }
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