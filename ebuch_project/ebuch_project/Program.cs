using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ebuch_project
{
    class Program
    {
        
        static public string instruction = "type picture (BMP,PNG or JPG(comments)) action(code(-ENC)/decode(-DEC))file_name\n finish_name)";
        static string startFolder;
        static string searchTerm;
        static public List<FileInfo> WalkAllDirectories(DirectoryInfo root, Regex reg)
        {
            Queue<DirectoryInfo> queue = new Queue<DirectoryInfo>();
            queue.Enqueue(root);
            List<FileInfo> list = new List<FileInfo>();
            while (queue.Count != 0)
            {
                DirectoryInfo parent = queue.Dequeue();
                //Console.WriteLine(parent.FullName);
                foreach (var child in parent.GetFiles())
                {
                    if (reg.Matches(child.FullName).Count != 0)
                    {
                        list.Add(child);
                    }
                }
                try
                {
                    foreach (var child in parent.GetDirectories())
                        queue.Enqueue(child);
                }
                catch
                {

                }
            }
            

            return list;
        }
        private static void DoSearch()
        {
            DirectoryInfo di = new DirectoryInfo(startFolder);
            Regex reg_ = new Regex(@"\w*[.jpeg|.bmp|.png]{4}$");
            List<FileInfo> files = WalkAllDirectories(di, reg_);
            int numResults = files.Count;
            PrintSearchResults(files);
            Console.WriteLine("{0}results:", numResults);
        }
        private static void PrintSearchResults(
            List<FileInfo> files)
        {
            foreach (FileInfo di in files)
            {
                Console.WriteLine("{0}\t{1}",
                    di.Name, di.Directory.FullName);
            }
        }
        private static void PrintSearchResults(
            FileInfo[] files)
        {
            foreach (FileInfo fi in files)
            {
                Console.WriteLine("{0}\t{1}",
                    fi.Name, fi.DirectoryName);
            }
        }
        static void PrintUsage()
        {
            Console.WriteLine("start_directory_name");
            Console.WriteLine("file_name");
        }
        static void Main(string[] args)
        {
            string text = "ewrfsdafsdasdf";
            string request = "";
            byte[] type = new byte[3];
            byte[] action = new byte[3];
            byte[] request_bytes;
            bool flag;
            flag = true;
            //Stream input = Console.OpenStandardInput();
            //Stream output = Console.OpenStandardOutput();
            Stream stderr = Console.OpenStandardError();
            string startDirectory = "";
            string finishDirectory = "";
            string question = "";
            string answer = "";
            while (flag == true)
            {
                question = "do you want to use my program?";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                answer = "yes";
                if (answer.ToLower() == "no")
                {
                    flag = false;
                    break;
                }
                question = "Do you know the location your file?";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                answer = "yes";
                if (answer.ToLower() == "no")
                {
                    PrintUsage();
                    Console.WriteLine("Searching {0} for\n {1}",
                startFolder, searchTerm);
                    //
                    startFolder = @"c:\Users\Магомед\Desktop";
                    //
                    searchTerm = "df";
                    //
                    DoSearch();
                }
                question = "Type your picture";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                //
                answer = "bmp";
                //
                request += answer;
                question = "action(Enc/Dec)";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                //
                answer = "dec";
                //
                request += answer;
                question = "Path to file";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                //
                answer = @"c:\Users\Магомед\Desktop\Project\project\ebuch_project\ebuch_project\bin\Debug\juj.bmp";
                //
                startDirectory += answer;
                request += answer;
                stderr.Write(Encoding.ASCII.GetBytes(instruction), 0, instruction.Length);
                stderr.Write(Encoding.ASCII.GetBytes(request), 0, request.Length);
                stderr.Write(Encoding.ASCII.GetBytes(finishDirectory), 0, finishDirectory.Length);
                request_bytes = Encoding.ASCII.GetBytes(request);
                Array.Copy(request_bytes, type, 3);
                Array.Copy(request_bytes, 3, action, 0, 3);
                question = "Finish_File";
                stderr.Write(Encoding.ASCII.GetBytes(question), 0, question.Length);
                //
                answer = "answer.txt";
                //
                finishDirectory += answer;
                //if (Encoding.UTF8.GetString(type) == "PNG")
                //{
                //    byte[] DATA;
                //    lenght = fi.Length;
                //    DATA = new byte[(int)lenght];
                //    input.Read(DATA, 0, (int)lenght);
                //    Png_Encriptor encriptor = new Png_Encriptor(DATA);
                //    DATA = encriptor.encript();
                //    output.Write(DATA, 0, DATA.Length);

                //}
                if (Encoding.UTF8.GetString(type).ToLower() == "bmp")
                {
                    byte[] DATA;
                    FileStream fsSource = new FileStream(startDirectory, FileMode.Open, FileAccess.Read);
                    DATA = new byte[fsSource.Length];
                    fsSource.Read(DATA, 0, DATA.Length);
                    fsSource.Close();
                    BMP_Encriptor encriptor = new BMP_Encriptor(DATA);
                    if (Encoding.ASCII.GetString(action).ToLower() == "enc")
                    {
                        DATA = encriptor.Encript(text);
                        using (FileStream fsFinish = File.Create(finishDirectory))
                        {
                            fsFinish.Write(DATA, 0, DATA.Length);
                            fsFinish.Close();
                        }
                    }
                    if (Encoding.ASCII.GetString(action).ToLower() == "dec")
                    {
                        string Finish = encriptor.Decript();
                        if (Finish.Length <= 50)
                        {
                            stderr.Write(Encoding.ASCII.GetBytes(Finish), 0, Finish.Length);
                        }
                        else
                        {
                            FileStream fsFinish = File.Create(finishDirectory);
                            fsFinish.Write(Encoding.ASCII.GetBytes(Finish), 0, Finish.Length);
                            fsFinish.Close();
                        }
                    }
                }
                //if (Encoding.UTF8.GetString(type).ToLower() == "jpg")
                //{
                //    byte[] DATA;
                //    FileStream fsSource = new FileStream(startDirectory, FileMode.Open, FileAccess.Read);
                //    DATA = new byte[fsSource.Length];
                //    fsSource.Read(DATA, 0, DATA.Length);
                //    fsSource.Close();
                //    JPG_Encriptor encriptor = new JPG_Encriptor(DATA);
                //    FileStream fsFinish = File.Create(finishDirectory);
                //    if (Encoding.ASCII.GetString(action).ToLower() == "enc")
                //    {
                //        DATA = encriptor.Encript(text);
                //        fsFinish.Write(DATA, 0, DATA.Length);
                //        fsFinish.Close();
                //    }
                //    if (Encoding.ASCII.GetString(action).ToLower() == "dec")
                //    {
                //        string Finish = encriptor.Decript();
                //        if (Finish.Length <= 50)
                //        {
                //            stderr.Write(Encoding.ASCII.GetBytes(Finish), 0, Finish.Length);
                //        }
                //        else
                //        {
                //            fsFinish.Write(Encoding.ASCII.GetBytes(Finish), 0, Finish.Length);
                //            fsFinish.Close();
                //        }
                //    }
                //}
            }
        }
    }
}
