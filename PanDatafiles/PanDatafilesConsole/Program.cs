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
            INI cfile = new INI(@"D:/alex1.ini");
            Console.WriteLine(INI.GetInfoAboutField(cl));
            INI.SetField(cl, "s", DateTime.Now.ToShortTimeString());
            Console.WriteLine(INI.GetInfoAboutField(cl));
            //cfile.WriteToFile(cl);

            cfile.ReadFromFile<myclass>(ref cl);
            int a = 9;
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
