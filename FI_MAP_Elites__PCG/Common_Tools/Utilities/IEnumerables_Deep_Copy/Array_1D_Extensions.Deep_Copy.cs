using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common_Tools
{
    public static partial class Array_1D_Extensions
    {
        public static T[] Q__Deep_Copy<T>(this T[] original_array)
            where T : I_Deep_Copyable
        {
            if (original_array == null)
                return null;

            int num_items = original_array.Length;
            T[] copied_array = new T[num_items]; // initialize the list's capacity
            for (int i = 0; i < num_items; i++)
            {
                copied_array[i] = (T)original_array[i].Q__Deep_Copy();
            }
            return copied_array;
        }

        /// <summary>
        /// Returns a deep copy of a 1-dimensional array of "double".
        /// </summary>
        /// <param name="original_array"></param>
        /// <returns></returns>
        public static double[] Q__Deep_Copy(this double[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            double[] copy_array = new double[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        /// <summary>
        /// Returns a deep copy of a 1-dimensional array of "double?".
        /// The keyword "double?" ("double" with a questionmark at the end) represents a nullable double.
        /// </summary>
        /// <param name="original_array"></param>
        /// <returns></returns>
        public static double?[] Q__Deep_Copy(this double?[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            double?[] copy_array = new double?[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static float[] Q__Deep_Copy(this float[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            float[] copy_array = new float[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static int[] Q__Deep_Copy(this int[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            int[] copy_array = new int[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static bool[] Q__Deep_Copy(this bool[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            bool[] copy_array = new bool[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static string[] Q__Deep_Copy(this string[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            string[] copy_array = new string[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static Vec2i[] Q__Deep_Copy(this Vec2i[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            Vec2i[] copy_array = new Vec2i[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static List<Line_Segment>[] Q__Deep_Copy(this List<Line_Segment>[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            List<Line_Segment>[] copy_array = new List<Line_Segment>[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i].Q__Deep_Copy();
            }

            return copy_array;
        }

        public static Directions_Ortho_2D[] Q__Deep_Copy(this Directions_Ortho_2D[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            Directions_Ortho_2D[] copy_array = new Directions_Ortho_2D[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }


        

        

        

    }
}
