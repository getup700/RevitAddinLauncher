///************************************************************************************
///   Author:Tony Stark
///   CreateTime:2023/4/8 15:29:42
///   Mail:2609639898@qq.com
///   Github:https://github.com/getup700
///
///   Description:
///
///************************************************************************************

using Install.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Install
{
    public class FileUtil
    {
        public static List<DirectoryInfo> GetMachineAddins()
        {
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var revitAddins = Path.Combine(programData, @"Autodesk\Revit\Addins");
            DirectoryInfo addinsDirectoryInfo = new DirectoryInfo(revitAddins);
            //if (!addinsDirectoryInfo.Exists)
            //{
            //    Log.Record($"{revitAddins}is not exist");
            //    return null;
            //}
            var directories = addinsDirectoryInfo?.GetDirectories()?.ToList();
            if (directories == null)
            {
                Console.WriteLine("Addins文件夹不存在，无法加载插件");
                return null;
            }
            for (int i = directories.Count() - 1; i >= 0; i--)
            {
                if (!RevitVersion.Versions.Any(x => x.ToString() == directories[i].Name))
                {
                    directories.RemoveAt(i);
                    continue;
                }
            }
            return directories;
        }

        public static IEnumerable<FileInfo> GetAddins(string directory, Predicate<FileInfo> predicate = null)
        {
            var files = new DirectoryInfo(directory)?.GetFiles("*.addin")?.ToList();
            if (predicate != null)
            {
                files = files.Where(predicate.Invoke).ToList();
            }
            return files;
        }

        /// <summary>
        /// 将file复制到指定文件夹，并替换路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="source"></param>
        public static void CopyFile(FileInfo file, DirectoryInfo source)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file),"addin file cannot be null");
            }
            if (source == null)
            {
                throw new ArgumentNullException(nameof (source),"target directory cannot be null");
            }
            using (StreamReader streamReader = new StreamReader(file.FullName))
            {
                string path = Path.Combine(source.FullName, file.Name);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    while (streamReader.Peek() != -1)
                    {
                        string text = streamReader.ReadLine();
                        string value = string.Empty;
                        if (text != null && text.Contains("{0}"))
                        {
                            value = string.Format(text, source);
                        }
                        else if (text != null)
                        {
                            value = text;
                        }
                        streamWriter.WriteLine(value);

                    }
                }
            }
        }

    }
}
