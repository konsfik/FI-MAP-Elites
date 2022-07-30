using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common_Tools
{
    public static class Array_CSV_Extensions
    {
        public static string To_CSV(this int[,] value_table)
        {
            int w = value_table.GetLength(0);
            int h = value_table.GetLength(1);

            string csv_text = "";

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    string v = value_table[x, y].ToString();
                    csv_text += v;

                    if (x < w - 1)
                    {
                        csv_text += ",";
                    }
                    else
                    {
                        csv_text += "\n";
                    }
                }
            }

            return csv_text;
        }

        public static string To_CSV(this double[,] value_table)
        {
            int w = value_table.GetLength(0);
            int h = value_table.GetLength(1);

            string csv_text = "";

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    string v = value_table[x, y].ToString();
                    csv_text += v;

                    if (x < w - 1)
                    {
                        csv_text += ",";
                    }
                    else
                    {
                        csv_text += "\n";
                    }
                }
            }

            return csv_text;
        }

        public static string To_CSV(this bool[,] value_table)
        {
            int w = value_table.GetLength(0);
            int h = value_table.GetLength(1);

            string csv_text = "";

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    string v = value_table[x, y].ToString();
                    csv_text += v;

                    if (x < w - 1)
                    {
                        csv_text += ",";
                    }
                    else
                    {
                        csv_text += "\n";
                    }
                }
            }

            return csv_text;
        }

        public static string To_CSV_0_1(this bool[,] value_table)
        {
            int w = value_table.GetLength(0);
            int h = value_table.GetLength(1);

            string csv_text = "";

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    string v = value_table[x, y] ? "1" : "0";

                    csv_text += v;

                    if (x < w - 1)
                    {
                        csv_text += ",";
                    }
                    else
                    {
                        csv_text += "\n";
                    }
                }
            }

            return csv_text;
        }

        

    }
}
