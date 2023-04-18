///************************************************************************************
///   Author:Tony Stark
///   CreateTime:2023/4/8 17:52:04
///   Mail:2609639898@qq.com
///   Github:https://github.com/getup700
///
///   Description:
///
///************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Install
{
    internal class Log
    {
        public static void Record(string message)
        {
            var item = $"\n{DateTime.Now} {message}";
            Loggers.Add(item);
        }

        private static List<string> Loggers = new List<string>() { "安装日志" };

        public static void Print()
        {
            if (Loggers.Count == 1)
            {
                return;
            }
            foreach (var item in Loggers)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

    }
    internal enum SererityLevel
    {

    }
}
