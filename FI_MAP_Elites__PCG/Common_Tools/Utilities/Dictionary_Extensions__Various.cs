using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Dictionary_Extensions__Various
    {

        public static int Value_Sum(
            this Dictionary<int, bool> dictionary
            )
        {
            int sum = 0;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == true)
                {
                    sum++;
                }
            }
            return sum;
        }

        public static int Value_Sum(
            this Dictionary<int, int> dictionary
            )
        {
            int sum = 0;
            foreach (var kvp in dictionary)
            {
                sum += kvp.Value;
            }
            return sum;
        }

        public static float Value_Sum(
            this Dictionary<int, float> dictionary
            )
        {
            float sum = 0;
            foreach (var kvp in dictionary)
            {
                if(kvp.Value!= float.NaN)
                    sum += kvp.Value;
            }
            return sum;
        }

        public static double Value_Sum(
            this Dictionary<int, double> dictionary
            )
        {
            double sum = 0;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value != double.NaN)
                    sum += kvp.Value;
            }
            return sum;
        }




        public static int Value_Max(
            this Dictionary<int, int> dictionary
            )
        {
            int max = int.MinValue;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                }
            }
            return max;
        }

        public static float Value_Max(
            this Dictionary<int, float> dictionary
            )
        {
            float max = float.NegativeInfinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                }
            }
            return max;
        }

        public static double Value_Max(
            this Dictionary<int, double> dictionary
            )
        {
            double max = double.NegativeInfinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                }
            }
            return max;
        }


        public static int Value_Min(
            this Dictionary<int, int> dictionary
            )
        {
            int min = int.MaxValue;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value < min)
                {
                    min = kvp.Value;
                }
            }
            return min;
        }

        public static float Value_Min(
            this Dictionary<int, float> dictionary
            )
        {
            float min = float.PositiveInfinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value < min)
                {
                    min = kvp.Value;
                }
            }
            return min;
        }

        public static double Value_Min(
            this Dictionary<int, double> dictionary
            )
        {
            double min = double.PositiveInfinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value < min)
                {
                    min = kvp.Value;
                }
            }
            return min;
        }


        public static List<int> Indexes_Of_Value(
            this Dictionary<int, int> dictionary,
            int value
            )
        {
            List<int> indexes_of_value = new List<int>();
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == value)
                {
                    indexes_of_value.Add(kvp.Key);
                }
            }
            return indexes_of_value;
        }

        public static List<int> Indexes_Of_Value(
            this Dictionary<int, float> dictionary,
            float value
            )
        {
            List<int> indexes_of_value = new List<int>();
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == value)
                {
                    indexes_of_value.Add(kvp.Key);
                }
            }
            return indexes_of_value;
        }

        public static List<int> Indexes_Of_Value(
            this Dictionary<int, double> dictionary,
            double value
            )
        {
            List<int> indexes_of_value = new List<int>();
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == value)
                {
                    indexes_of_value.Add(kvp.Key);
                }
            }
            return indexes_of_value;
        }


    }
}
