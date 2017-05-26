using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Finding_the_file_or_the_catalog
{
    class Program
    {
        static bool folderOnly = false;
        static string startFolder;
        static string searchTerm;
        static void Main(string[] args)
        {
            if (!ParseArgs(args))
            {
                PrintUsage();
                return;
            }
            Console.WriteLine("Searching {0} for\"{1}\"{2}",
                startFolder,searchTerm,
                folderOnly);
            DoSearch();
        }
        private static void DoSearch()
        {
            DirectoryInfo di = new DirectoryInfo(startFolder);
            DirectoryInfo[] directories = di.GetDirectories(searchTerm,
                SearchOption.TopDirectoryOnly);
            int numResults = directories.Length;
            PrintSearchResults(directories);
            if (!folderOnly)
            {
                FileInfo[] files = di.GetFiles(searchTerm,
                    SearchOption.TopDirectoryOnly);
                PrintSearchResults(files);
                numResults += files.Length;
            }
            Console.WriteLine("{0}results found",numResults);
        }
        private static void PrintSearchResults(
            DirectoryInfo[] directories)
        {
            foreach(DirectoryInfo di in directories)
            {
                Console.WriteLine("{0}\t{1}",
                    di.Name,di.Parent.FullName);
            }
        }
        private static void PrintSearchResults(
            FileInfo[] files)
        {
            foreach (FileInfo fi in files)
            {
                Console.WriteLine("{0}\t{1}",
                    fi.Name,fi.DirectoryName);
            }
        }
        static void PrintUsage()
        {
            Console.WriteLine("Usage:Find[-directory] SearchTerm StartFolder");
            Console.WriteLine("Ex:Find - directory code D:\\Projects");
            Console.WriteLine("* wildcards are accepted");
        }
        static bool ParseArgs(string[] args)
        {
            if (args.Length < 2)
            {
                return false;
            }
            startFolder = args[args.Length - 1];
            searchTerm = args[args.Length - 2];
            return true;
        }
    }
}
