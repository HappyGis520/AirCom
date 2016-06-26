using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
namespace ZipOneCode.ZipProvider
{
    public class ZipHelper:sing
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationZipFilePath"></param>
        public static void CreateZip(string sourceFilePath, string destinationZipFilePath)
        {
            if (sourceFilePath[sourceFilePath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                sourceFilePath += System.IO.Path.DirectorySeparatorChar;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(destinationZipFilePath));
            zipStream.SetLevel(6);  // 压缩级别 0-9
            CreateZipFiles(sourceFilePath, zipStream);
            zipStream.Finish();
            zipStream.Close();
        }
        /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:\WorkSpace\a.zip）,全路径包括文件名和.zip扩展名</param>
        /// <param name="staticFile"></param>
        private static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream)
        {
            Crc32 crc = new Crc32();
            string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
            foreach (string file in filesArray)
            {
                if (Directory.Exists(file))                     //如果当前是文件夹，递归
                {
                    CreateZipFiles(file, zipStream);
                }
                else                                            //如果是文件，开始压缩
                {
                    FileStream fileStream = File.OpenRead(file);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(sourceFilePath.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempFile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipStream.PutNextEntry(entry);
                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dirPath">压缩文件夹的路径</param>
        /// <param name="fileName">生成的zip文件路径</param>
        /// <param name="level">压缩级别 0 - 9 0是存储级别 9是最大压缩</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public void CompressDirectory(string dirPath, string fileName, int level, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            using (ZipOutputStream s = new ZipOutputStream(File.Create(fileName)))
            {
                s.SetLevel(level);
                CompressDirectory(dirPath, dirPath, s, buffer);
                s.Finish();
                s.Close();
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="root">压缩文件夹路径</param>
        /// <param name="path">压缩文件夹内当前要压缩的文件夹路径</param>
        /// <param name="s"></param>
        /// <param name="buffer">读取文件的缓冲区大小</param>
        private void CompressDirectory(string root, string path, ZipOutputStream s, byte[] buffer)
        {
            root = root.TrimEnd('/') + @"/";
            string[] fileNames = Directory.GetFiles(path);
            string[] dirNames = Directory.GetDirectories(path);
            string relativePath = path.Replace(root, "");
            if (relativePath != "")
            {
                relativePath = relativePath.Replace("//", "/") + "/";
            }
            int sourceBytes;
            foreach (string file in fileNames)
            {

                ZipEntry entry = new ZipEntry(relativePath + Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }

            foreach (string dirName in dirNames)
            {
                string relativeDirPath = dirName.Replace(root, "");
                ZipEntry entry = new ZipEntry(relativeDirPath.Replace("//", "/") + "/");
                s.PutNextEntry(entry);
                CompressDirectory(root, dirName, s, buffer);
            }
        }

        /// <summary>
        /// 解压缩zip文件
        /// </summary>
        /// <param name="zipFilePath">解压的zip文件路径</param>
        /// <param name="extractPath">解压到的文件夹路径</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public void Extract(string zipFilePath, string extractPath, int bufferSize)
        {
            extractPath = extractPath.TrimEnd('/') + "//";
            byte[] data = new byte[bufferSize];
            int size;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry entry;
                while ((entry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);

                    //先创建目录
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(extractPath + directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(extractPath + entry.Name.Replace("/", "//")))
                        {
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
                        }
                    }
                }
            }
        }

    }
}
