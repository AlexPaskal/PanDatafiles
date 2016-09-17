using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
        public static string GetInfoAboutType<T>(T obj)
        {
            string res = "";

            System.Type type = typeof(T);
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
    }
}
