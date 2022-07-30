using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Array_2D_Extensions__Deep_Copy
    {
        public static double[][] Q__Deep_Copy(this double[][] original_array)
        {
            int len = original_array.Length;
            double[][] array_copy = new double[len][];

            for (int i = 0; i < len; i++)
                array_copy[i] = original_array[i].Q__Deep_Copy();

            return array_copy;
        }

        public static double[,] Q__Deep_Copy(this double[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            double[,] arrayCopy = new double[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static float[,] Q__Deep_Copy(this float[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            float[,] arrayCopy = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static int[,] Q__Deep_Copy(this int[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            int[,] arrayCopy = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static bool[,] Q__Deep_Copy(this bool[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            bool[,] arrayCopy = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static string[,] Q__Deep_Copy(this string[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            string[,] arrayCopy = new string[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Directions_Ortho_2D[,] Q__Deep_Copy(this Directions_Ortho_2D[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Directions_Ortho_2D[,] arrayCopy = new Directions_Ortho_2D[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Vec2i[,] Q__Deep_Copy(this Vec2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Vec2i[,] arrayCopy = new Vec2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Edge_2i[,] Q__Deep_Copy(this Edge_2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Edge_2i[,] arrayCopy = new Edge_2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static UEdge_2i[,] Q__Deep_Copy(this UEdge_2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            UEdge_2i[,] arrayCopy = new UEdge_2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

    }
}
