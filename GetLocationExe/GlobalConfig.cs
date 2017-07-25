using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetLocationExe
{
    class GlobalConfig
    {
        public static string dirPath = @"C:\Users\" + Environment.UserName + @"\AppData\Local\IPLocate\";
        public static string filePath = @"C:\Users\" + Environment.UserName + @"\AppData\Local\IPLocate\History.txt";
        public static string GetExtenalIpURL = "http://hijoyusers.joymeng.com:8100/test/getNameByOtherIp";
        public static string IPLocateURL = "http://www.freegeoip.net/xml/";
    }
}
