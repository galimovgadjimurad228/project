using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combine_path_to_My_documents_or_My_Pictures
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Environment.SpecialFolder folder
                in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                string path = Environment.GetFolderPath(folder);
                Console.WriteLine("{0}\t==>\t{1}",folder,path);
            }
        }
    }
}
