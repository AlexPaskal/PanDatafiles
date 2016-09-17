using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PanDatafiles;

namespace PanDatafilesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            myclass cl = new myclass();
            Console.WriteLine(INI.GetInfoAboutType<myclass>(cl));
            Console.ReadLine();
        }
    }
    class myclass
    {
        public int a = 1;
        private int b = 2;
        public int c = 3;
        public string s = "str";
    }
}
