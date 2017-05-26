using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stderr = Console.OpenStandardError();
            string problememes="";
            string target = args[0];
            if (File.Exists(target))
            {
                problememes = "File exists";
                stderr.Write(Encoding.ASCII.GetBytes(problememes), 0, problememes.Length);
            }
            else if (Directory.Exists(target))
            {
                problememes = "Directory Exists";
                stderr.Write(Encoding.ASCII.GetBytes(problememes), 0, problememes.Length);
                
            }
        }
    }
}
