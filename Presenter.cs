using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxHell
{
    public static class Presenter
    {
        public static string[] HelpStrings = {
            "",
            "Converts .resx to .resw and sorts resource files",
            "into native WinRT directories /Strings/{language}/resource.resw",            
            "",
            "ResxHell.exe -verbose -import SOURCEPATH -export DESTINATIONPATH -fallbackLang ru",
            "",
            "  -import          Imports .resx files from path recursively to local temporary",
            "                   folder and converts to .resw",
            "  -export          Exports .resw files from local temp folder to specified path",
            "                   and sorts resources into language folders",
            "  -verbose         Show additional details",
            "  -fallbackLang    [Optional] [Default = \"en\"] Sets fallback language when ",
            "                   the .resx does not have it's own language qualifier",
            "  -workingDir      [Optional] Set directory where to resource files",
            "                   imported and exported",
            "",
            "This program is designed to work with Visual Studio pre-build event",
            "so you can put for example:",
            "   ResxHell.exe -import \"$(ProjectDir)\\\"",
            "on your localization PCL post-build event and",
            "   ResxHell.exe -export \"$(ProjectDir)\\\"",
            "pre-build event on your Windows RT 8.1+ project",
        };

        public static string[] HelloStrings = {
            "ResxHell v.1.1 by Konrad Bartecki",
            "Source code available at github.com/konradbartecki",
            "Contact email: konradbartecki@outlook.com",
            ""
        };

        public static void Print(string[] strings)
        {
            foreach (string s in strings)
            {
                Console.WriteLine(s);
            }
        }
    }
}
