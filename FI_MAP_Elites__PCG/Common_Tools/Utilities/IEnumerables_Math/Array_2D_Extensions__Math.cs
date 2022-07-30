using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Array_2D_Extensions__Math
    {
        public static int Sum(this bool[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            int sum = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (array[x, y])
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        public static int Sum(this int[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            int sum = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    sum += array[x, y];
                }
            }
            return sum;
        }

        public static float Sum(this float[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            float sum = 0.0f;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    sum += array[x, y];
                }
            }
            return sum;
        }

        public static double Sum(this double[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            double sum = 0.0d;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    sum += array[x, y];
                }
            }
            return sum;
        }

        public static int Min(this int[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            int min = int.MaxValue;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (array[x, y] < min)
                    {
                        min = array[x, y];
                    }
                }
            }
            return min;
        }

        public static float Min(this float[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            float min = float.PositiveInfinity;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (float.IsNaN(array[x, y]))
                    {
                        continue;
                    }
                    else if (array[x, y] < min)
                    {
                        min = array[x, y];
                    }
                }
            }
            return min;
        }

        public static double Min(this double[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            double min = double.PositiveInfinity;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (Double.IsNaN(array[x, y]))
                    {
                        continue;
                    }
                    else if (array[x, y] < min)
                    {
                        min = array[x, y];
                    }
                }
            }

            return min;
        }

        public static int Max(this int[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            int max = int.MinValue;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (array[x, y] > max)
                    {
                        max = array[x, y];
                    }
                }
            }

            return max;
        }

        public static float Max(this float[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            float max = float.NegativeInfinity;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (float.IsNaN(array[x, y]))
                    {
                        continue;
                    }
                    else if (array[x, y] > max)
                    {
                        max = array[x, y];
                    }
                }
            }

            return max;
        }

        public static double Max(this double[,] array)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            double max = double.NegativeInfinity;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (Double.IsNaN(array[x, y]))
                    {
                        continue;
                    }
                    else if (array[x, y] > max)
                    {
                        max = array[x, y];
                    }
                }
            }

            return max;
        }

        public static double Average(this double[,] array)
        {
            return array.Mean();
        }

        public static float Average(this float[,] array)
        {
            return array.Mean();
        }

        public static double Mean(this double[,] array)
        {
            return array.Sum() / (double)array.NumValues();
        }

        public static float Mean(this float[,] array)
        {
            return array.Sum() / (float)array.NumValues();
        }

        public static double Variance(this double[,] array)
        {
            double mean = array.Mean();
            double variance_sum = 0;
            foreach (var value in array)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value)^2;
            }
            double variance = variance_sum / (double)array.NumValues();
            return variance;
        }

        public static float Variance(this float[,] array)
        {
            float mean = array.Mean();
            float variance_sum = 0;
            foreach (var value in array)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value)^2;
            }
            float variance = variance_sum / (float)array.NumValues();
            return variance;
        }

        public static double Standard_Deviation(this double[,] array)
        {
            return Math.Sqrt(array.Variance());
        }

        public static int NumValues(this double[,] array)
        {
            return array.GetLength(0) * array.GetLength(1);
        }

        public static int NumValues(this float[,] array)
        {
            return array.GetLength(0) * array.GetLength(1);
        }

        public static double[,] Divided_By(this double[,] array, double divisor)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);

            double[,] dividedTable = new double[w, h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    dividedTable[x, y] = array[x, y] / divisor;
                }
            }

            return dividedTable;
        }

        public static double[,] Multiplied_By(this double[,] array, double multiplier)
        {
            double[,] multipliedTable = new double[array.GetLength(0), array.GetLength(1)];
            for (int x = 0; x < multipliedTable.GetLength(0); x++)
            {
                for (int y = 0; y < multipliedTable.GetLength(1); y++)
                {
                    multipliedTable[x, y] = array[x, y] * multiplier;
                }
            }
            return multipliedTable;
        }

    }
}
