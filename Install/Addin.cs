///************************************************************************************
///   Author:Tony Stark
///   CreateTime:2023/4/8 14:50:54
///   Mail:2609639898@qq.com
///   Github:https://github.com/getup700
///
///   Description:
///
///************************************************************************************

using Install.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Install
{
    public static class Addin
    {
        public static string Name
        {
            get => Settings.Default.Name;
            set
            {
                Settings.Default.Name = value;
                Settings.Default.Save();
            }
        }

        public static List<int> Versions
        {
            get
            {
                var versions = new List<int>();
                //foreach (var item in Settings.Default.Properties)
                //{
                //    if (item.ToString().Contains("Version"))
                //    {
                //        var value = Settings.Default.Properties[item.ToString()];
                //        versions.Add((int)value.DefaultValue);
                //    }
                //}
                foreach (var version in Settings.Default.Versions)
                {
                    var intValue = int.Parse(version);
                    versions.Add(intValue);
                }
                return versions;
            }
            set
            {
                if (Settings.Default.Versions != null)
                {
                    Settings.Default.Versions.Clear();
                }
                Settings.Default.Versions = new System.Collections.Specialized.StringCollection();
                value.ForEach(x => Settings.Default.Versions.Add(x.ToString()));
                Settings.Default.Save();
            }
        }

        public static List<int> GetVersions()
        {
            var versions = new List<int>();
            foreach (var version in Settings.Default.Versions)
            {
                var intValue = int.Parse(version);
                versions.Add(intValue);
            }
            return versions;
        }

    }
}
