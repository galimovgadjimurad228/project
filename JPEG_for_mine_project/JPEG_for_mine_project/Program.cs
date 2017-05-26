using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JPEG_for_mine_project
{
    class Program
    {
        struct HELP
        {
            internal string help(string problem)
            {
                problem = " Введите параметр: -Enc(вводит комментарий в картинку типа Jpeg) или -DEC(выводит на консоль закомментированный текст) ";
                return problem;
            }
        }
        static void Main(string[] args)
        {
            string path = @"shivr.txt";
            string text_for_test="";
            using (StreamReader reader = File.OpenText(path))
            {
                text_for_test = reader.ReadToEnd();
            }
            Stream input = Console.OpenStandardInput();
            Stream output = Console.OpenStandardOutput();
            Stream stderr = Console.OpenStandardError();
            HELP help = new HELP();

            string problem = "";
            if (args[0] == "-help")
            {
                problem=help.help(problem);
                stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);
                Environment.Exit(0);
            }

            byte[] Header = new byte[2];
            byte[] Lenght = new byte[2];
            byte[] title_for_comments=new byte[2];
            title_for_comments[0] = (byte)0XFF;
            title_for_comments[1] = (byte)0XFE; 

            byte[] comments_conteins = new byte[32000];
            byte[] text = Encoding.ASCII.GetBytes(text_for_test);
            int i = text.Length / 32000;
            bool repeat = true;
            byte[] lenght_mass_1 = new byte[2];
            int p = 0;
            
            input.Read(Header, 0, 2);
            output.Write(Header, 0, 2);
            
            uint j=2;
            
            if (BitConverter.ToUInt16(Header, 0) != 0XD8FF)
            { 
                problem="It is not JPEG";
                stderr.Write(Encoding.ASCII.GetBytes(problem),0,problem.Length);
                Environment.Exit(1);
            }
            
            while (input.Read(Header, 0, 2) == 2)
            {
                output.Write(Header, 0, 2);
                j += 2;
                if (input.Read(Lenght, 0, 2) < 2)
                {

                    j += 2;

                    problem="Problems with the Lenght";
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);
                    
                    problem = "the location of problem is "+j;
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);
                    
                    Environment.Exit(2);
                }

                j += 2;

                output.Write(Lenght, 0, 2);

                Array.Reverse(Lenght);

                UInt16 L = (UInt16)(BitConverter.ToUInt16(Lenght, 0) - 2);
                j += L;
                byte[] mass_1 = new byte[L];
                int l = input.Read(mass_1, 0, (int)L);

                if (l != L)
                {

                    problem = "Jpeg is Currupted";
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);

                    problem = "Expect"+L;
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);

                    problem = "read"+l;
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);

                    problem = "in the unit"+j;
                    stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);

                    Environment.Exit(3);
                }
                output.Write(mass_1, 0, mass_1.Length);
                if (BitConverter.ToUInt16(Header, 0) == 0xFEFF)
                {
                    if (args[0] == "-DEC")
                    {

                        stderr.Write(mass_1, 0, mass_1.Length);

                    }
                    
                    continue;
                }
                if (args[0] == "-Enc")
                {
                    if (text_for_test.Length >= 32000)
                    {
                        if (repeat == true)
                        {
                            string warning = "Warning: the size of comment is very big";
                            stderr.Write(Encoding.ASCII.GetBytes(warning), 0, warning.Length);
                            while (p != (i))
                            {
                                output.Write(title_for_comments, 0, 2);
                                UInt16 text_of_lenght_to_byte = (UInt16)(32000 + 2);
                                lenght_mass_1 = BitConverter.GetBytes(text_of_lenght_to_byte);
                                Array.Reverse(lenght_mass_1);
                                output.Write(lenght_mass_1, 0, lenght_mass_1.Length);
                                Array.Copy(text, p * 32000, comments_conteins, 0, 32000);
                                output.Write(comments_conteins, 0, 32000);
                                p++;
                            }
                            if (text.Length % 32000 != 0)
                            {
                                output.Write(title_for_comments, 0, 2);
                                UInt16 text_of_lenght_to_byte = (UInt16)(text.Length - (32000 * i) + 2);
                                lenght_mass_1 = BitConverter.GetBytes(text_of_lenght_to_byte);
                                Array.Reverse(lenght_mass_1);
                                output.Write(lenght_mass_1, 0, lenght_mass_1.Length);
                                Array.Copy(text, i * 32000, comments_conteins, 0, text.Length - (32000 * i));
                                output.Write(comments_conteins, 0, text.Length - (32000 * i));

                            }
                            repeat = false;
                        }
                    }
                    else
                    {
                        output.Write(title_for_comments, 0, 2);
                        UInt16 text_of_lenght_to_byte = (UInt16)(text_for_test.Length + 2);
                        lenght_mass_1 = BitConverter.GetBytes(text_of_lenght_to_byte);
                        Array.Reverse(lenght_mass_1);
                        output.Write(lenght_mass_1, 0, lenght_mass_1.Length);
                        output.Write(Encoding.ASCII.GetBytes(text_for_test), 0, text_for_test.Length);
                    }
                }
                if (BitConverter.ToUInt16(Header, 0) == 0XDAFF)
                {

                    int bytes = input.ReadByte();
                    output.WriteByte((byte)bytes);

                    while (bytes != -1)
                    {
                        bytes = input.ReadByte();

                        if (bytes == -1)
                        {
                            break;
                        }

                        output.WriteByte((byte)bytes);
                    }
                }
                
            }
        }
    }
}
