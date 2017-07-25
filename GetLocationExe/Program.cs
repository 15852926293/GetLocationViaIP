using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLocationExe
{
    class Function
    {
        public static void CreateHisoryFile()
        {
            FileStream fs = null;
            if (Directory.Exists(GlobalConfig.dirPath) == false)
            {
                Directory.CreateDirectory(GlobalConfig.dirPath);
            }
            if (File.Exists(GlobalConfig.filePath) == false)
            {
                fs = File.Create(GlobalConfig.filePath);
                fs.Close();
            }
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Function.CreateHisoryFile();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
