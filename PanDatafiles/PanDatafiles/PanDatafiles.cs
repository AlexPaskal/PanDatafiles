using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace PanDatafiles
{
    public class JSON
    {
        
    }
    public class XML
    {

    }
    public class INI
    {
        public static string GetInfoAboutField(object obj)
        {
            string res = "";

            System.Type type = obj.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                res += field.Name;
                res += " = ";

                res += field.GetValue(obj);
                res += " (";

                res += field.GetValue(obj).GetType();

                res += ")\n";
            }

            return res;
        }
        public static void SetField(object obj, string fieldname, object fieldvalue)
        {
            System.Type type = obj.GetType();
            System.Reflection.FieldInfo field = type.GetField(fieldname, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            field.SetValue(obj, fieldvalue);
        }

        public INI(string path)
        {
            filepath = path;

            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
        }
        private string filepath;
        public void WriteToFile(object data)
        {
            //string dataToFile = "";
            //dataToFile += "[Section]\n";

            //System.Type type = data.GetType();
            //System.Reflection.FieldInfo[] fields = type.GetFields();

            //foreach (FieldInfo field in fields)
            //{
            //    dataToFile += field.Name;
            //    dataToFile += "=";
            //    dataToFile += field.GetValue(data);
            //    dataToFile += "\n";
            //}

            //File.WriteAllText(filepath, dataToFile);
            StreamWriter writer = new StreamWriter(filepath);

            writer.WriteLine("[Section]");

            System.Type type = data.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields();

            foreach (FieldInfo field in fields)
            {
                writer.WriteLine(string.Format("{0}={1}", field.Name, field.GetValue(data)));
            }

            writer.Close();
            writer.Dispose();
        }
        public void ReadFromFile<T>(ref T data)
        {
            StreamReader reader = new StreamReader(filepath);
            string[] lines = new string[0];

            for(int i = 0; true; i++)
            {
                string line = reader.ReadLine();
                if (line == null) { break; }
                if (line != "")
                {
                    if (line[0] != '\n' && line[0] != ' ' && line[0] != ';' && line[0] != '#' && line[0] != '[')
                    {
                        Array.Resize<string>(ref lines, lines.Length+1);
                        lines[lines.Length - 1] = line;
                    }
                }
            }

            foreach (string str in lines)
            {
                string[] keyvalue = str.Split(new Char[] {'='}, 2);
                //System.Type type = data.GetType();
                Type type = typeof(T);



                System.Reflection.FieldInfo field = type.GetField(keyvalue[0], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                
                object val = null;
                if(typeof(int) == field.GetValue(data).GetType()){
                    val = Convert.ToInt32(keyvalue[1]);
                }
                else if (typeof(string) == field.GetValue(data).GetType())
                {
                    val = (string)keyvalue[1];
                }
                else if (typeof(bool) == field.GetValue(data).GetType())
                {
                    if (keyvalue[1] == "1" || keyvalue[1].ToLower() == "t" || keyvalue[1] == "+" || keyvalue[1].ToLower() == "true" || keyvalue[1].ToLower() == "y" || keyvalue[1].ToLower() == "yes")
                    {
                        val = true;
                    }
                    else if (keyvalue[1] == "0" || keyvalue[1].ToLower() == "f" || keyvalue[1] == "-" || keyvalue[1].ToLower() == "false" || keyvalue[1].ToLower() == "n" || keyvalue[1].ToLower() == "no")
                    {
                        val = false;
                    }
                    else
                    {
                        val = null;
                    }
                }
                field.SetValue(data, val);
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            reader.Close();
            reader.Dispose();
        }

        //public object DataInFile
        //{
        //    get
        //    {
        //        return null;
        //    }
        //    set
        //    {
        //        value = null;
        //    }
        //}


        /*static public void WriteToFile()
        {

        }
        static public object ReadFromFile()
        {
            return null;
        }
        static public string WriteToString()
        {
            return null;
        }
        static public object ReadFromString()
        {
            return null;
        }*/
    }
}
