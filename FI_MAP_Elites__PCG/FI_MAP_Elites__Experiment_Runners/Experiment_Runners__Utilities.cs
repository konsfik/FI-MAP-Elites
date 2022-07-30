using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

using SkiaSharp;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public static class Experiment_Runners__Utilities
    {
        public static string To_CSV__2D<T>(
            this T[] data,
            BS_Ortho_Tessellation tessellation
            ) 
        {
            int width = tessellation.num_cells__per__bc[0];
            string output = "";
            int len = data.Length;

            for (int i = 0; i < len; i++) {
                T value = data[i];
                output += value.ToString();

                int x = i % width;
                if(x == width-1)
                {
                    output += "\n";
                }
                else
                {
                    output += ",";
                }
            }

            return output;
        }

        public static string To_CSV<T>(
            this T[] data,
            BS_Ortho_Tessellation tessellation
            )
        {
            if (tessellation.num_bcs == 2) {
                return data.To_CSV__2D(tessellation);
            }
            

            string output = "";
            int len = data.Length;

            for (int i = 0; i < len; i++)
            {
                var coords = tessellation.Q__Cell__To__Coords(i);
                var next_coords = coords.Q__Deep_Copy();
                if (i < len - 1) next_coords = tessellation.Q__Cell__To__Coords(i + 1);
                T value = data[i];
                output += value.ToString();

                bool change_line = ChangeLine(coords, next_coords);
                if (change_line)
                {
                    output += "\n";
                }
                else
                {
                    output += ",";
                }
            }

            return output;
        }



        public static SKBitmap To_Image(
            this int[] data,
            BS_Ortho_Tessellation tessellation
            )
        {
            int num_bcs = tessellation.num_bcs;
            int[] num_cells__per__bc = tessellation.num_cells__per__bc.Q__Deep_Copy();
            int width = 1;
            int height = 1;
            for (int i = 0; i < num_bcs; i++)
            {
                if (i % 2 == 0)
                {
                    width *= num_cells__per__bc[i];
                }
                else
                {
                    height *= num_cells__per__bc[i];
                }
            }

            SKBitmap bitmap = new SKBitmap(width, height);

            for (int i = 0; i < data.Length; i++)
            {
                int v = data[i];
                int[] coords_nd = tessellation.Q__Cell__To__Coords(i);

                int[] coords_2d = To_2D_Coords(coords_nd, tessellation);

                SKColor c1 = SKColors.Black;
                SKColor c2 = SKColors.White;

                if (v > 0)
                {
                    bitmap.SetPixel(coords_2d[0], coords_2d[1], c2);
                }
                else
                {
                    bitmap.SetPixel(coords_2d[0], coords_2d[1], c1);
                }

            }

            return bitmap;
        }

        private static int[] To_2D_Coords(
            this int[] coords,
            BS_Ortho_Tessellation tessellation
            )
        {
            var num_cells__per__bc = tessellation.num_cells__per__bc;
            int x = coords[0];
            int y = 0;
            for (int c = 1; c < coords.Length; c++)
            {

                int multiplier = 1;
                for (int m = 1; m < c; m++)
                {
                    multiplier *= num_cells__per__bc[m];
                }
                y += coords[c] * multiplier;
            }
            return new int[] { x, y };
        }

        public static bool ChangeLine(int[] old_coords, int[] new_coords)
        {

            int len = old_coords.Length;
            for (int i = 1; i < len; i++)
            {
                int v1 = old_coords[i];
                int v2 = new_coords[i];
                if (v1 != v2) return true;
            }

            return false;
        }
    }
}
