using Install;
using Install.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Install
{
    internal class Install
    {
        static void Main(string[] args)
        {
            var versions = new List<int>()
            {
                2020
            };
            Addin.Name = "BJUB.addin";
            Addin.Versions = versions;

            var sourceAddin = FileUtil.GetAddins(Environment.CurrentDirectory, x => x.Name == Addin.Name)?.FirstOrDefault();
            var directories = FileUtil.GetMachineAddins()?.Where(x => Addin.Versions.Contains(int.Parse(x.Name)));
            Settings.Default.Name = Addin.Name;
            if (directories == null)
            {
                Log.Record("directories is null");
                return;
            }
            if (sourceAddin == null)
            {
                throw new ArgumentNullException(nameof(sourceAddin), "target directory cannot be null");
            }
            foreach (var item in directories)
            {
                FileUtil.CopyFile(sourceAddin, item);
            }
        }
    }
}
