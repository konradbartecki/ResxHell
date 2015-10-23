using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxHell.Model
{
    public static class Config
    {
        public static string LocalDir = Path.Combine(GetCurrentExeDir(),
            @"ResxTempFiles\");
        public static string DefaultLanguage = "en";

        public static string GetCurrentExeDir()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }
    }
}
