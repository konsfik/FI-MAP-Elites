using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Number_Series_Generator
    {

        public static List<int> Multiplied_Series__Predefined_Steps(
            int initial_value,
            int multiplier,
            int num_values
            )
        {
            if (initial_value < 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (multiplier < 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (num_values < 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            List<int> result = new List<int>();
            int current_value = initial_value;
            for (int i = 0; i < num_values; i++)
            {
                result.Add(current_value);
                current_value *= multiplier;
            }

            return result;
        }

        public static List<int> Multiplied_Series__Max_Value(
            int initial_value,
            int multiplier,
            int max_value
            )
        {
            if (initial_value < 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (multiplier < 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (max_value < initial_value)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            List<int> result = new List<int>();
            int current_value = initial_value;
            while (current_value <= max_value)
            {
                result.Add(current_value);
                current_value *= multiplier;
            }

            return result;
        }
    }
}
