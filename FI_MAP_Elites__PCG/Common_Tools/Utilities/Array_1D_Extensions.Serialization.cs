using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static partial class Array_1D_Extensions
    {
        public static string Q__To_CSV_String<T>(this T[] items, char delimiter, bool end_line)
        {
            if (items == null) return "";

            int num_items = items.Length;

            if (num_items == 0) return "";

            string csv_string = items[0].ToString();
            for (int i = 1; i < num_items; i++)
            {
                csv_string += delimiter;
                csv_string += items[i].ToString();
            }

            if (end_line)
                csv_string += "\n";

            return csv_string;
        }
    }
}
