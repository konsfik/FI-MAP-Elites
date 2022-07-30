using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common_Tools
{
    public static partial class Array_1D_Extensions__Math
    {
        

        public static float Q__Sum(this float[] array)
        {
            int len = array.Length;

            float sum = 0.0f;
            for (int i = 0; i < len; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static int Q__Sum(this int[] array)
        {
            int len = array.Length;

            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static int Q__Sum(this bool[] array)
        {
            int len = array.Length;

            int sum = 0;

            for (int i = 0; i < len; i++)
            {
                if (array[i])
                {
                    sum++;
                }
            }

            return sum;
        }


        public static double Q__Product(this double[] array)
        {
            double product = 1.0;
            foreach (var v in array)
                product *= v;
            return product;
        }

        public static double Q__Product(this double[] array, int last_index)
        {
            double product = 1.0;
            for (int i = 0; i <= last_index; i++)
                product *= array[i];
            return product;
        }

        public static float Q__Product(this float[] array)
        {
            float product = 1.0f;
            foreach (var v in array)
                product *= v;
            return product;
        }

        public static float Q__Product(this float[] array, int last_index)
        {
            float product = 1.0f;
            for (int i = 0; i <= last_index; i++)
                product *= array[i];
            return product;
        }

        public static int Q__Product(this int[] array)
        {
            int product = 1;
            foreach (var v in array)
                product *= v;
            return product;
        }

        public static int Q__Product(this int[] array, int last_index)
        {
            int product = 1;
            for (int i = 0; i <= last_index; i++)
            {
                product = product * array[i];
            }
            return product;
        }
    }
}
