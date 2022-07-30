using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static partial class List_Extensions_Math
    {
        public static float Q__Mean(this List<float> values)
        {
            float sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }
            return sum / (float)values.Count;
        }

        public static float Q__Variance(this List<float> values, bool is_sample)
        {
            float mean = values.Q__Mean();
            float variance_sum = 0.0f;
            foreach (var value in values)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value) ^ 2
            }
            float divider = (float)values.Count;
            if (is_sample)
                divider -= 1.0f;
            float variance = variance_sum / divider;
            return variance;
        }
    }
}
