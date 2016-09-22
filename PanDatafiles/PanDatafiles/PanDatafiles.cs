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
        private string _fileName;
        private object _data;
        public INI(object data, string filename)
        {
            this._data = data;
            this._fileName = filename;
        }
        public object DataInFile
        {
            get
            {
                StreamReader reader = new StreamReader(_fileName);
                string[] lines = new string[0];

                Type type = this._data.GetType();
                object data = this._data;


                for (int i = 0; true; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null) { break; }
                    if (line != "")
                    {
                        if (line[0] != '\n' && line[0] != ' ' && line[0] != ';' && line[0] != '#' && line[0] != '[')
                        {
                            Array.Resize<string>(ref lines, lines.Length + 1);
                            lines[lines.Length - 1] = line;
                        }
                    }
                }

                foreach (string str in lines)
                {
                    string[] keyvalue = str.Split(new Char[] { '=' }, 2);

                    System.Reflection.FieldInfo field = type.GetField(keyvalue[0], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    object val = null;
                    if (typeof(int) == field.GetValue(data).GetType())
                    {
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
                }

                reader.Close();
                reader.Dispose();

                return data;
            }
            set
            {
                object data = value;
                StreamWriter writer = new StreamWriter(this._fileName);

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
        }
    }
}
