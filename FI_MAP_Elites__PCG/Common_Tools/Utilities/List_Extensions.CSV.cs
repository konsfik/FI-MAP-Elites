using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Common_Tools
{
    public static partial class List_Extensions
    {
        public static string Q__To_CSV<T>(this List<T> list, char delimiter, bool end_line)
        {
            if (list == null)
                throw new System.Exception("null list...");
            if (list.Count < 1)
                return "";

            string csv = list[0].ToString();
            for (int i = 1; i < list.Count; i++)
            {
                csv += delimiter;
                csv += list[i].ToString();
            }

            if (end_line)
                csv += "\n";

            return csv;
        }
    }
}