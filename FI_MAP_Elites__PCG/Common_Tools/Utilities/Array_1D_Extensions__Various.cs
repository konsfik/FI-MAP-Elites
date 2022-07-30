using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public static class Array_1D_Extensions__Various
    {
        public static List<int> Indexes_Of_Value(
            this double[] array,
            double value
            )
        {
            List<int> indexes_of_value = new List<int>();

            int array_length = array.Length;

            for (int i = 0; i < array_length; i++)
            {
                if (array[i] == value)
                {
                    indexes_of_value.Add(i);
                }
            }
            return indexes_of_value;
        }

        public static List<int> Indexes_Of_Value(
            this float[] array,
            float value
            )
        {
            List<int> indexes_of_value = new List<int>();

            int array_length = array.Length;

            for (int i = 0; i < array_length; i++)
            {
                if (array[i] == value)
                {
                    indexes_of_value.Add(i);
                }
            }
            return indexes_of_value;
        }

        public static List<int> Indexes_Of_Value(
            this int[] array,
            int value
            )
        {
            List<int> indexes_of_value = new List<int>();

            int array_length = array.Length;

            for (int i = 0; i < array_length; i++)
            {
                if (array[i] == value)
                {
                    indexes_of_value.Add(i);
                }
            }
            return indexes_of_value;
        }

        public static List<int> Indexes_Of_Value(
            this bool[] array,
            bool value
            )
        {
            List<int> indexes_of_value = new List<int>();

            int array_length = array.Length;

            for (int i = 0; i < array_length; i++)
            {
                if (array[i] == value)
                {
                    indexes_of_value.Add(i);
                }
            }
            return indexes_of_value;
        }
    }
}
