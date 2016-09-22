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
            myclass cl2 = new myclass();
            INI cfile2 = new INI(cl2, @"D:/alex2.ini");
            cl2.CW();
            cl2 = (myclass)cfile2.DataInFile;
            cl2.CW();
            cl2 = new myclass();
            cfile2.DataInFile = cl2;

            //Console.WriteLine(INI.GetInfoAboutField(cl));
            //INI.SetField(cl, "s", DateTime.Now.ToShortTimeString());
            //Console.WriteLine(INI.GetInfoAboutField(cl));
            Console.ReadLine();
        }
    }
    class myclass
    {
        public int a = 1;
        private int b = 2;
        public int c = 3;
        public string s = "str";
        public string CW()
        {
            string str = string.Format("a={0},\nb={1},\nc={2},\ns={3}.", a, b, c, s);
            Console.WriteLine(str);
            return str;
        }
    }
}
