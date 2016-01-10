
namespace GenerateZip
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    public class ZipHelper
    {
        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            if (File.Exists(FileToUpZip))
            {
                if (!Directory.Exists(ZipedFolder))
                {
                    Directory.CreateDirectory(ZipedFolder);
                }
                ZipInputStream stream = new ZipInputStream(File.OpenRead(FileToUpZip));
                try
                {
                    ZipEntry entry;
                    while ((entry = stream.GetNextEntry()) != null)
                    {
                        if (entry.Name != string.Empty)
                        {
                            string path = Path.Combine(ZipedFolder, entry.Name);
                            if (path.EndsWith("/") || path.EndsWith(@"\"))
                            {
                                Directory.CreateDirectory(path);
                                continue;
                            }
                            string directoryName = Path.GetDirectoryName(path);
                            if (!Directory.Exists(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }
                            FileStream stream2 = File.Create(path);
                            try
                            {
                                bool flag;
                                int count = 0x800;
                                byte[] buffer = new byte[0x800];
                                goto Label_00FC;
                            Label_00D0:
                                count = stream.Read(buffer, 0, buffer.Length);
                                if (count <= 0)
                                {
                                    continue;
                                }
                                stream2.Write(buffer, 0, count);
                            Label_00FC:
                                flag = true;
                                goto Label_00D0;
                            }
                            finally
                            {
                                stream2.Close();
                                stream2.Dispose();
                                stream2 = null;
                            }
                        }
                    }
                }
                finally
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
        }

        public static bool Zip(string FileToZip, string ZipedFile, int level)
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, level);
            }
            return (File.Exists(FileToZip) && ZipFile(FileToZip, ZipedFile, level));
        }

        private static bool ZipFile(string FileToZip, string ZipedFile, int level)
        {
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream baseOutputStream = null;
            ZipOutputStream stream2 = null;
            ZipEntry entry = null;
            bool flag = true;
            try
            {
                baseOutputStream = File.OpenRead(FileToZip);
                byte[] buffer = new byte[baseOutputStream.Length];
                baseOutputStream.Read(buffer, 0, buffer.Length);
                baseOutputStream.Close();
                baseOutputStream = File.Create(ZipedFile);
                stream2 = new ZipOutputStream(baseOutputStream);
                entry = new ZipEntry(Path.GetFileName(FileToZip));
                stream2.PutNextEntry(entry);
                stream2.SetLevel(level);
                stream2.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (entry != null)
                {
                    entry = null;
                }
                if (stream2 != null)
                {
                    stream2.Finish();
                    stream2.Close();
                }
                if (baseOutputStream != null)
                {
                    baseOutputStream.Close();
                    baseOutputStream = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return flag;
        }

        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool flag = true;
            ZipEntry entry = null;
            FileStream stream = null;
            Crc32 crc = new Crc32();
            try
            {
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));
                s.PutNextEntry(entry);
                s.Flush();
                string[] files = Directory.GetFiles(FolderToZip);
                foreach (string str in files)
                {
                    stream = File.OpenRead(str);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(str)))
                    {
                        DateTime = DateTime.Now,
                        Size = stream.Length
                    };
                    stream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            string[] directories = Directory.GetDirectories(FolderToZip);
            foreach (string str2 in directories)
            {
                if (!ZipFileDictory(str2, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }
            return flag;
        }

        private static bool ZipFileDictory(string FolderToZip, string ZipedFile, int level)
        {
            if (!Directory.Exists(FolderToZip))
            {
                return false;
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(level);
            bool flag = ZipFileDictory(FolderToZip, s, "");
            s.Finish();
            s.Close();
            return flag;
        }

        public static int ZipFiles(string destFolder, string[] srcFiles, string folderName, string password)
        {
            ZipOutputStream stream = null;
            FileStream stream2 = null;
            int num = 0;
            try
            {
                Crc32 crc = new Crc32();
                stream = new ZipOutputStream(File.Create(destFolder));
                stream.SetLevel(srcFiles.Length);
                if ((password != null) && (password.Trim().Length > 0))
                {
                    stream.Password = password;
                }
                foreach (string str in srcFiles)
                {
                    if (!File.Exists(str))
                    {
                        throw new FileNotFoundException(str);
                    }
                    stream2 = File.OpenRead(str);
                    byte[] buffer = new byte[stream2.Length];
                    stream2.Read(buffer, 0, buffer.Length);
                    stream2.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    ZipEntry entry = new ZipEntry(Path.Combine(folderName, Path.GetFileName(str)))
                    {
                        DateTime = DateTime.Now,
                        Size = buffer.Length,
                        Crc = crc.Value
                    };
                    stream.PutNextEntry(entry);
                    stream.Write(buffer, 0, buffer.Length);
                    num++;
                }
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream != null)
                {
                    stream.Finish();
                    stream.Close();
                }
            }
            return num;
        }
    }
}
