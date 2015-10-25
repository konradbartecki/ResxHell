using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResxHell.Model;

namespace ResxHell
{
    public static class ResourcesManagement
    {
        public static void ImportAndRenameResxFiles(string path)
        {           
            Directory.CreateDirectory(Config.LocalDir);
            ClearImportDirectory();
            var stringfiles = Directory.GetFiles(path, "*.resx", SearchOption.AllDirectories);
            if(Config.ShowVerbose)
                Console.WriteLine("Detected {0} .resx files", stringfiles.Count());
            foreach (string s in stringfiles)
            {
                var filename = Path.GetFileNameWithoutExtension(s);
                var newFilename = filename + ".resw";
                if (Config.ShowVerbose)
                {
                    Console.WriteLine("Copying file {0}", Path.GetFileName(s));
                }
                File.Copy(s, Path.Combine(Config.LocalDir, newFilename));
            }
            Console.WriteLine("Imported {0} resource files", stringfiles.Count());
        }

        public static void DetectLanguagesAndSort(string path)
        {
            path = Path.Combine(path, @"Strings\");
            Console.WriteLine("Detecting languages...");
            var SupportedLanguages = new List<string>();
            var ResourceFiles = new List<ResourceFile>();
            var stringfiles = Directory.GetFiles(Config.LocalDir, "*.resw", SearchOption.TopDirectoryOnly);
            foreach (string s in stringfiles)
            {
                var filename = Path.GetFileNameWithoutExtension(s);
                int i = filename.LastIndexOf('.');
                string resourceLang;
                if (i == -1)
                    resourceLang = Config.DefaultLanguage;
                else
                    resourceLang = filename.Substring(i + 1);

                if (resourceLang.Length == 5 && resourceLang.Contains("-"))
                    AddOnce(SupportedLanguages, ResourceFiles, resourceLang, s);
                else if (resourceLang.Length == 2)
                    AddOnce(SupportedLanguages, ResourceFiles, resourceLang, s);
                else
                    AddOnce(SupportedLanguages, ResourceFiles, Config.DefaultLanguage, s);
            }
            //Create directories
            foreach (string language in SupportedLanguages)
            {
                Directory.CreateDirectory(Path.Combine(path, language));
            }
            Console.WriteLine("Created {0} language directories", SupportedLanguages.Count);

            //Move resources into language directories
            foreach (ResourceFile resourceFile in ResourceFiles)
            {
                string filename = Path.GetFileName(resourceFile.Path);
                string LanguageDirectoryPath = Path.Combine(path, resourceFile.Language + @"\");
                string newFilePath = Path.Combine(LanguageDirectoryPath, filename);
                if(File.Exists(newFilePath))
                    File.Delete(newFilePath);
                File.Move(resourceFile.Path, newFilePath);
            }
        }
        private static void AddOnce(List<string> list, List<ResourceFile> resourceFiles, string resourceLang, string path)
        {
            if(Config.ShowVerbose)
                Console.WriteLine("- File: {0} - detected as: {1} language", Path.GetFileName(path), resourceLang);
            resourceFiles.Add(new ResourceFile { Path = path, Language = resourceLang });
            if (list.Contains(resourceLang)) return;
            Console.WriteLine("New language detected: {0}", resourceLang);
            list.Add(resourceLang);
        }

        private static void ClearImportDirectory()
        {
            Console.WriteLine("Clearing output directory {0}", Config.LocalDir);
            var stringfiles = Directory.GetFiles(Config.LocalDir);
            if (stringfiles.Any())
            {
                foreach (string s in stringfiles)
                {
                    File.Delete(s);
                }
            }
            var directories = Directory.GetDirectories(Config.LocalDir);
            if (directories.Any())
            {
                foreach (string s in directories)
                {
                    Directory.Delete(s, true);
                }
            }
        }
    }
}
