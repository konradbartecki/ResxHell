﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResxHell.Model;

namespace ResxHell
{
    class Program
    {
        private static void Main(string[] args)
        {
            var argsList = args.ToList();
            if (argsList.Contains("-help")
                || argsList.Contains("/?")
                || argsList.Contains("-commands")
                || argsList.Contains("--help"))
            {
                Presenter.Print(Presenter.HelpStrings);
                return;
            }

            Presenter.Print(Presenter.HelloStrings);

            if (argsList.Contains("-verbose"))
            {
                Config.ShowVerbose = true;
            }

            if (argsList.Contains("-fallbackLang"))
            {
                int i = argsList.IndexOf("-fallbackLang");
                Config.DefaultLanguage = argsList[i + 1];
                Console.WriteLine("Fallback language set to: " + Config.DefaultLanguage);
            }
            if (argsList.Contains("-workingDir"))
            {
                int i = argsList.IndexOf("-workingDir");
                Config.LocalDir = argsList[i + 1];
                Console.WriteLine("workingDir set to {0}", Config.LocalDir);
            }
            if (argsList.Contains("-import"))
            {
                try
                {
                    int i = argsList.IndexOf("-import");
                    string rawpath = argsList[i + 1];
                    string path = Helpers.GetCorrectPath(rawpath);
                    Console.WriteLine("Import path: {0}", path);
                    ResourcesManagement.ImportAndRenameResxFiles(path);
                    Console.WriteLine("Imported and converted resource files");
                }
                catch (Exception e)
                { 
                    Console.WriteLine(e);
                    Console.WriteLine(e.StackTrace);
                }

            }
            if (argsList.Contains("-export"))
            {
                try
                { 
                    int i = argsList.IndexOf("-export");
                    string rawpath = argsList[i + 1];
                    string path = Helpers.GetCorrectPath(rawpath);
                    Console.WriteLine("Export path: {0}", path);
                    ResourcesManagement.DetectLanguagesAndSort(path);
                    Console.WriteLine("Exported resource files");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public static void Log(string logMessage, TextWriter w)
            {   
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
    }
}
