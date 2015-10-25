using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResxHell.Model;

namespace ResxHell
{
    public static class Helpers
    {
        public static string GetCorrectPath(string path)
        {
            string tempstring = path;
            //ends with "
            if (tempstring.EndsWith("\""))
            {
                var chars = path.ToCharArray();
                chars[chars.Length - 1] = '\\';
                string fixedstring = new string(chars);
                ShowVerboseInfo(path, fixedstring);
                tempstring = fixedstring;
            }
            //missing / or \
            if (!tempstring.EndsWith(@"\"))
            {
                string fixedpath = path + @"\";
                ShowVerboseInfo(path, fixedpath);
                tempstring = fixedpath;
            }

            return tempstring;
        }

        private static void ShowVerboseInfo(string oldPath, string newPath)
        {
            if (Config.ShowVerbose)
            {
                Console.WriteLine("Incorrect path argument detected: {0}", oldPath);
                Console.WriteLine("Correcting path to: {0}", newPath);
            }
        }
    }
}
