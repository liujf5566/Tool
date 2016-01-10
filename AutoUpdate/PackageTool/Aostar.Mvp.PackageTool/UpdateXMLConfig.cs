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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using GenerateZip;

namespace Aostar.MVP.Update.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateXmlConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ThreadArgs _threadArgs;

        private int _createFileCount;
        private int _createProgress;

        private string _rootDir = "";

        /// <summary>
        /// 
        /// </summary>
        private Thread _xmlCreateThread;

        ///<summary>
        ///</summary>
        public UpdateXmlConfig(ThreadArgs args)
        {
            _threadArgs = args;
        }

        /// <summary>
        /// Occurs when [XML create progress event].
        /// </summary>
        public event XmlCreateProgressHandler XmlCreateProgressEvent;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _xmlCreateThread = new Thread(CreateConfigInfo) { IsBackground = true };
            _xmlCreateThread.IsBackground = true;
            _xmlCreateThread.Start(_threadArgs);
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            _xmlCreateThread.Suspend();
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            _xmlCreateThread.Resume();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            try
            {
                if (_xmlCreateThread != null)
                {
                    if (_xmlCreateThread.IsAlive)
                    {
                        _xmlCreateThread.Abort();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                _xmlCreateThread = null;
            }
        }


        ///<summary>
        ///</summary>
        ///<param name="obj"></param>
        private void CreateConfigInfo(object obj)
        {
            var args = obj as ThreadArgs;

            if (args == null) return;
            XmlMainInfo mainInfo = args.MainInfo;

            ExpansionInfo exInfo = args.ExInfo;

            _createFileCount = 0;
            Dictionary<UpdateDirInfo, List<UpdateFileInfo>> dirs;

            if (args.IsZipFile)
            {
                //初始化变量
                ZipHelper.i = 0;
                ZipHelper.currentZipFolder = null;
                ZipHelper.zipFiles = null;
                ZipHelper.zipFiles = args.ExcludeInfo.Files;

                ZipHelper.Zip(args.MainInfo.AppName,
                            Path.Combine(args.SavePath, mainInfo.Version) + ".zip",6);

                

                dirs = new Dictionary<UpdateDirInfo, List<UpdateFileInfo>>
                           {
                               {
                                   new UpdateDirInfo(
                                   args.MainInfo.AppName.Substring(0, args.MainInfo.AppName.LastIndexOf("\\")),
                                   args.MainInfo.AppName.Substring(0, args.MainInfo.AppName.LastIndexOf("\\"))),
                                   new List<UpdateFileInfo>
                                       {new UpdateFileInfo(Path.Combine(args.SavePath, mainInfo.Version) + ".zip")}
                                   }
                           };
            }
            else
            {
                dirs = GetAllFiles(args.MainInfo.AppName.Substring(0, args.MainInfo.AppName.LastIndexOf("\\")));
            }

            _createProgress = 1;

            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateXml,
                                        Msg = "正在创建配置文件!",
                                        Complete = false,
                                        FileCount = _createFileCount,
                                        Progress = _createProgress
                                    });

            //CreateLast(args, mainInfo);

            var versionXml = new XmlDocument();
            //建立Xml的定义声明
            XmlDeclaration dec = versionXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            versionXml.AppendChild(dec);


            //创建根节点
            XmlElement autoUpdater = versionXml.CreateElement("AutoUpdater");
            versionXml.AppendChild(autoUpdater);

            XmlNode main = versionXml.CreateElement("Main");
            XmlElement version = versionXml.CreateElement("Version");
            version.InnerText = mainInfo.Version;
            main.AppendChild(version);

            XmlElement IsMust = versionXml.CreateElement("IsMust");
            IsMust.InnerText = mainInfo.IsMust;
            main.AppendChild(IsMust);
            XmlElement description = versionXml.CreateElement("Description");
            description.InnerText = mainInfo.Description;
            main.AppendChild(description);
            XmlElement updateTime = versionXml.CreateElement("UpdateTime");
            updateTime.InnerText = mainInfo.UpdateTime.ToString("yyyy-MM-dd");
            main.AppendChild(updateTime);
            XmlElement appName = versionXml.CreateElement("AppName");
            appName.InnerText = mainInfo.AppName.Trim().Substring(mainInfo.AppName.Trim().LastIndexOf("\\") + 1);
            main.AppendChild(appName);
            XmlElement type = versionXml.CreateElement("Type");
            type.InnerText = mainInfo.UpdateType;
            main.AppendChild(type);

            autoUpdater.AppendChild(main);

            XmlNode expansion = versionXml.CreateElement("Expansion");
            XmlElement beforeExecute = versionXml.CreateElement("BeforeExecute");
            beforeExecute.SetAttribute("Name", "");
            beforeExecute.SetAttribute("Args", "");
            expansion.AppendChild(beforeExecute);

            XmlElement afterExecute = versionXml.CreateElement("AfterExecute");
            afterExecute.SetAttribute("Name", "");
            afterExecute.SetAttribute("Args","");
            expansion.AppendChild(afterExecute);

            autoUpdater.AppendChild(expansion);

            XmlElement fileList = versionXml.CreateElement("FileList");
            autoUpdater.AppendChild(fileList);
            const string xPath = "Directory";
            foreach (var item in dirs)
            {
                XmlNode parentDir;
                XmlElement xmlItem = null;
                if (!item.Key.IsEmpty)
                {
                    if (item.Key.Parent.Length != 0)
                    {
                        XmlNode tempParentDir = fileList;

                        for (int i = 0; i < item.Key.Parents.Count; i++)
                        {
                            tempParentDir =
                                tempParentDir.SelectSingleNode(xPath + "[@Name=\"" + item.Key.Parents[i] + "\"]");
                        }
                        parentDir = tempParentDir;
                    }
                    else
                    {
                        parentDir = fileList;
                    }

                    if (item.Key.Current.Length != 0)
                    {
                        xmlItem = versionXml.CreateElement("Directory");
                        xmlItem.SetAttribute("Name", item.Key.Current);
                    }

                    if (xmlItem != null)
                    {
                        if (item.Value.Count > 0)
                        {
                            NewFile(versionXml, item, xmlItem);
                        }
                        parentDir.AppendChild(xmlItem);
                    }
                }
                else
                {
                    parentDir = fileList;
                    if (item.Value.Count > 0)
                    {
                        NewFile(versionXml, item, parentDir);
                    }
                }
            }

            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateXml,
                                        Msg = "正在保存配置文件!",
                                        Complete = false,
                                        FileCount = _createFileCount,
                                        Progress = _createProgress
                                    });
            _createProgress++;


            if (args.IsZipXml)
            {
                ZipHelper.Zip(Path.Combine(args.SavePath, mainInfo.Version) + ".xml",
                            Path.Combine(args.SavePath, mainInfo.Version) + ".etc",6);
            }
            else
            {
                versionXml.Save(Path.Combine(args.SavePath, mainInfo.Version) + ".xml");
            }


            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateXml,
                                        Msg = "配置文件生成成功!",
                                        Complete = true,
                                        FileCount = _createFileCount,
                                        Progress = _createFileCount
                                    });
            _createProgress = 0;
            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateFile,
                                        Msg = "正在发布程序文件!",
                                        Complete = false,
                                        FileCount = _createFileCount,
                                        Progress = _createProgress
                                    });
            _createProgress++;

            //CopyFile(dirs, Path.Combine(args.SavePath, mainInfo.Version));

            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateFile,
                                        Msg = "新版本发布成功!",
                                        Complete = true,
                                        FileCount = _createFileCount,
                                        Progress = _createFileCount
                                    });

            Stop();
        }

        private void CopyFile(Dictionary<UpdateDirInfo, List<UpdateFileInfo>> dirs, string objPath)
        {
            try
            {
                if (!Directory.Exists(objPath))
                {
                    Directory.CreateDirectory(objPath);
                }

                foreach (var item in dirs)
                {
                    string tempDir = Path.Combine(objPath, item.Key.RelativeName);

                    if (!Directory.Exists(tempDir))
                    {
                        Directory.CreateDirectory(tempDir);
                    }

                    foreach (UpdateFileInfo file in item.Value)
                    {
                        string tempFile = Path.Combine(tempDir, file.Name);

                        OnXmlCreateProgress(new XmlCreateProgressArgs
                                                {
                                                    ProgressType = CreateTyep.CreateFile,
                                                    Msg =
                                                        string.Format("正在发布[{0}/{1}] {2}", _createProgress,
                                                                      _createFileCount,
                                                                      file.Name),
                                                    Complete = false,
                                                    FileCount = _createFileCount,
                                                    Progress = _createProgress
                                                });
                        _createProgress++;

                        File.Copy(file.FullName, tempFile, true);
                        if (file.Hidden)
                        {
                            File.SetAttributes(tempFile, FileAttributes.Archive);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// 按指定路径生成目录及子目录
        /// </summary>
        /// <param name="path">路径</param>
        private static void CreateDirtory(string path)
        {
            if (!File.Exists(path))
            {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                }
            }
        }

        /// <summary>
        /// 复制指定目录到目地目录。
        /// </summary>
        /// <param name="sourcePath">源目录</param>
        /// <param name="objPath">目地目录</param>
        private void CopyFile(string sourcePath, string objPath)
        {
            try
            {
                if (!Directory.Exists(objPath))
                {
                    Directory.CreateDirectory(objPath);
                }

                string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    string tempFile = file.Replace(sourcePath + "\\", "");

                    OnXmlCreateProgress(new XmlCreateProgressArgs
                                            {
                                                ProgressType = CreateTyep.CreateFile,
                                                Msg =
                                                    string.Format("正在发布[{0}/{1}] {2}", _createProgress, _createFileCount,
                                                                  tempFile),
                                                Complete = false,
                                                FileCount = _createFileCount,
                                                Progress = _createProgress
                                            });
                    _createProgress++;

                    tempFile = Path.Combine(objPath, tempFile);
                    CreateDirtory(tempFile);
                    File.Copy(file, Path.Combine(objPath, tempFile), true);
                }
            }
            catch
            {
            }
        }


        private static void CreateLast(ThreadArgs args, XmlMainInfo mainInfo)
        {
            var lastXml = new XmlDocument();
            XmlDeclaration dec1 = lastXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            lastXml.AppendChild(dec1);

            //创建根节点
            XmlElement root = lastXml.CreateElement("AutoUpdater");
            lastXml.AppendChild(root);
            XmlElement ver = lastXml.CreateElement("Version");
            ver.InnerText = mainInfo.Version;
            root.AppendChild(ver);
            XmlElement url = lastXml.CreateElement("Url");
            url.InnerText = string.Format("{0}/{1}", args.Url.Trim().Substring(0, args.Url.Trim().LastIndexOf("/")),
                                          mainInfo.Version + ".xml");
            root.AppendChild(url);

            lastXml.Save(Path.Combine(args.SavePath, "Last.xml"));
        }

        /// <summary>
        /// News the file.
        /// </summary>
        /// <param name="xmlDoc">The XML doc.</param>
        /// <param name="item">The item.</param>
        /// <param name="dir">The dir.</param>
        private void NewFile(XmlDocument xmlDoc, KeyValuePair<UpdateDirInfo, List<UpdateFileInfo>> item, XmlNode dir)
        {
            foreach (UpdateFileInfo info in item.Value)
            {
                OnXmlCreateProgress(new XmlCreateProgressArgs
                                        {
                                            ProgressType = CreateTyep.CreateXml,
                                            Msg =
                                                string.Format("正在处理[{0}/{1}] {2}", _createProgress, _createFileCount,
                                                              info.Name),
                                            Complete = false,
                                            FileCount = _createFileCount,
                                            Progress = _createProgress
                                        });
                _createProgress++;

                XmlElement file = xmlDoc.CreateElement("File");
                file.SetAttribute("Name", info.Name);
                file.SetAttribute("Size", info.Size.ToString());
                file.SetAttribute("Key", info.CrcKey);
                if (info.Hidden)
                {
                    file.SetAttribute("Hidden", info.Hidden.ToString());
                }
                dir.AppendChild(file);
            }
        }

        /// <summary>
        /// Gets all files.
        /// </summary>
        /// <param name="rootdir">The rootdir.</param>
        /// <returns></returns>
        private Dictionary<UpdateDirInfo, List<UpdateFileInfo>> GetAllFiles(string rootdir)
        {
            _rootDir = _threadArgs.MainInfo.AppName.Substring(0, _threadArgs.MainInfo.AppName.LastIndexOf("\\"));

            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateInfo,
                                        Msg = "正在创建文件信息!",
                                        Complete = false,
                                        FileCount = _createFileCount,
                                        Progress = 0
                                    });
            _createFileCount++;
            var result = new Dictionary<UpdateDirInfo, List<UpdateFileInfo>>();

            GetAllFiles(rootdir, result);

            OnXmlCreateProgress(new XmlCreateProgressArgs
                                    {
                                        ProgressType = CreateTyep.CreateInfo,
                                        Msg = "创建文件信息完成!",
                                        Complete = true,
                                        FileCount = _createFileCount,
                                        Progress = 1
                                    });


            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="result"></param>
        private void GetAllFiles(string dir, IDictionary<UpdateDirInfo, List<UpdateFileInfo>> result)
        {
            var files = new List<UpdateFileInfo>();
            var dirInfo = new UpdateDirInfo(dir, _rootDir);
            ;

            var tmpFiles = Directory.GetFiles(dir);
            foreach (string file in tmpFiles)
            {
                if (_threadArgs.ExcludeInfo.Files.Contains(file)) continue;
                OnXmlCreateProgress(
                    new XmlCreateProgressArgs
                        {
                            ProgressType = CreateTyep.CreateInfo,
                            Msg = "正在分析 " + file.Substring(file.LastIndexOf("\\") + 1),
                            Complete = false,
                            FileCount = _createFileCount,
                            Progress = 0
                        });

                _createFileCount++;

                files.Add(new UpdateFileInfo(file));
            }

            result.Add(dirInfo, files);

            foreach (string item in Directory.GetDirectories(dir))
            {
                GetAllFiles(item, result);
            }
        }

        /// <summary>
        /// Called when [XML create progress].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnXmlCreateProgress(XmlCreateProgressArgs e)
        {
            if (XmlCreateProgressEvent != null)
            {
                XmlCreateProgressEvent(this, e);
            }
        }
    }
}