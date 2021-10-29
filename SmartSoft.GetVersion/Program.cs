using System;
using System.Diagnostics;

namespace SmartSoft.GetVersion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileVersionInfo fileversioninfo = FileVersionInfo.GetVersionInfo(args[0]);
            Console.WriteLine(fileversioninfo.ProductVersion);
            Console.ReadKey();
        }
    }
}
