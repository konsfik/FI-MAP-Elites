using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Visualization
{
    public static class Array_Visualization_Utilities
    {
        public static SKBitmap To_HeatMap(
            this bool[,] values_table,
            SKColor true_color,
            SKColor false_color
            )
        {
            int w = values_table.GetLength(0);
            int h = values_table.GetLength(1);

            SKBitmap image = new SKBitmap(w, h);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    bool value = values_table[x, y];
                    if (value == true)
                    {
                        image.SetPixel(x, y, true_color);
                    }
                    else
                    {
                        image.SetPixel(x, y, false_color);
                    }
                }
            }
            return image;
        }

        public static SKBitmap To_HeatMap(
            this int[,] values_table,
            int min_value,
            int max_value,
            SKColor out_of_range__low__color,
            SKColor out_of_range__high__color
            )
        {
            SKBitmap image = new SKBitmap(values_table.GetLength(0), values_table.GetLength(1));
            for (int x = 0; x < values_table.GetLength(0); x++)
            {
                for (int y = 0; y < values_table.GetLength(1); y++)
                {
                    double value = values_table[x, y];
                    if (value < min_value)
                    {
                        image.SetPixel(x, y, out_of_range__low__color);
                    }
                    else if (value > max_value)
                    {
                        image.SetPixel(x, y, out_of_range__high__color);
                    }
                    else
                    {
                        if (min_value == max_value)
                        {
                            SKColor c = new SKColor(0, 0, 0);
                            image.SetPixel(x, y, c);
                        }
                        else
                        {
                            double cv = Math_Utilities.Q__Mapped(value, min_value, max_value, 0.0, 255.0);
                            byte ci = (byte)cv;
                            SKColor c = new SKColor(ci, ci, ci);
                            image.SetPixel(x, y, c);
                        }


                    }
                }
            }
            return image;
        }

        public static SKBitmap To_HeatMap(
            this double[,] values_table,
            double min_value,
            double max_value,
            SKColor out_of_range__low__color,
            SKColor out_of_range__high__color,
            SKColor not_a_number__color
            )
        {
            int w = values_table.GetLength(0);
            int h = values_table.GetLength(1);

            SKBitmap image = new SKBitmap(w, h);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (min_value == max_value)
                    {
                        double value = values_table[x, y];
                        if (Double.IsNaN(value))
                        {
                            image.SetPixel(x, y, not_a_number__color);
                        }
                        else
                        {
                            SKColor c = new SKColor(0, 0, 0);
                            image.SetPixel(x, y, c);
                        }
                    }
                    else
                    {
                        double value = values_table[x, y];
                        if (Double.IsNaN(value))
                        {
                            image.SetPixel(x, y, not_a_number__color);
                        }
                        else if (value < min_value)
                        {
                            image.SetPixel(x, y, out_of_range__low__color);
                        }
                        else if (value > max_value)
                        {
                            image.SetPixel(x, y, out_of_range__high__color);
                        }
                        else
                        {
                            double cv = Math_Utilities.Q__Mapped(value, min_value, max_value, 0.0, 255.0);
                            byte ci = (byte)cv;
                            SKColor c = new SKColor(ci, ci, ci);
                            image.SetPixel(x, y, c);
                        }
                    }
                }
            }
            return image;
        }
    }
}
