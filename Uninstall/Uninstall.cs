using Install;
using Install.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Uninstall
{
    public class Uninstall
    {
        static void Main(string[] args)
        {
            //var type = GetType();
            //var name = GetValue(type, "Name");
            //var versions = GetValue(type, "Versions");

            string name = "BJUB.F.addin";

            var targetDirectories = FileUtil.GetMachineAddins();
            if (targetDirectories.Count() == 0)
            {
                throw new ArgumentException(nameof(targetDirectories), "file not found in specified folder");
            }

            foreach (var directoryInfo in targetDirectories)
            {
                var childrenFile = directoryInfo.GetFiles();
                var file = childrenFile.Where(x => x.Extension == ".addin" && x.Name == name).FirstOrDefault();
                if (file != null)
                {
                    file.Delete();
                }
            }
        }

        static Type GetType()
        {
            var path = $"{Environment.CurrentDirectory}\\Install.exe";
            Assembly assembly = Assembly.LoadFrom(path);
            var type1 = assembly.GetType("Install.Properties.Settings");
            var type2 = assembly.GetType("Install.Addin");
            return type2;
        }

        public static dynamic GetValue(Type type,string property)
        {
            if (property == "Name")
            {
                var name = type.GetProperty("Name").GetValue(type, null);
                return name;
            }
            else if (property == "Versions")
            {
                var versions = type.GetProperty("Versions").GetValue(type, null) as List<int>;
                return versions;
            }
            return null;
        }
    }
}
