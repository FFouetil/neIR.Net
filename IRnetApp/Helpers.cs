using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neIR
{
    class Helpers
    {
        public static string CreateFileTypeFilter(string typeName, string extensions, string separator=" "){

            StringBuilder strTemp = new StringBuilder(typeName + " ({ext})|{ext}");
            string[] exts=extensions.Split(new string[]{separator},StringSplitOptions.RemoveEmptyEntries);
            StringBuilder formattedList=new StringBuilder();

            foreach (string e in exts) 
                formattedList.Append("*" + e + ";");

            formattedList.Remove(formattedList.Length-1,1); //removes the last ';'
            strTemp.Replace("{ext}", formattedList.ToString());

            return strTemp.ToString();
        }

    }
}
