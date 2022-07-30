using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Math_Utilities
    {
        public static double Q__Max(
            this double v1, 
            double v2
            ) 
        {
            if (v1 > v2) return v1;
            else return v2;
        }

        public static bool Q__Approximately_Equal(
            this double n1,
            double n2,
            double epsilon
            )
        {
            return Math.Abs(n1 - n2) <= epsilon;
        }


        /// <summary>
        /// Evaluates the similarity between a value and a target value.
        /// If value < target_value, then similarity = value / target_value.
        /// If value > target_value, then similarity = target_value / value.
        /// If value == target_value, then similarity = 1.
        /// </summary>
        /// <param name="target_value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Q__Fractional_Similarity(
            this double value,
            double target_value
            )
        {
            if (value < 0 || target_value < 0)
            {
                throw new System.Exception("values must be positive!");
            }

            if (value == target_value)
            {
                return 1;
            }
            else if (value < target_value)
            {
                return value / target_value;
            }
            else // actual value > target value
            {
                // the reverse calculation happens here...
                return target_value / value;
            }
        }

        /// <summary>
        /// Calculates the fractional error between a value and a target value as fractional_error = (1 - fractional_similarity).
        /// If value < target_value, then fractional_error = 1 - (value / target_value).
        /// If value > target_value, then fractional_error = 1 - (target_value / value).
        /// If value == target_value, then fractional_error = 1 - 1 = 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target_value"></param>
        /// <returns></returns>
        public static double Q__Fractional_Error(
            this double value,
            double target_value
            )
        {
            double fractional_similarity = 
                value.Q__Fractional_Similarity(target_value);

            return 1.0 - fractional_similarity;
        }

        public static bool Q__Is_In_Range(
            this double value,
            double range_min,
            double range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static bool Q__Is_In_Range(
            this float value,
            float range_min,
            float range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static bool Q__Is_In_Range(
            this int value,
            int range_min,
            int range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static double Q__Constrained(
            this double value,
            double cap_1,
            double cap_2
            )
        {
            if (cap_1 < cap_2)
            {
                if (value > cap_2) return cap_2;
                else if (value < cap_1) return cap_1;
                else return value;
            }
            else if (cap_1 > cap_2)
            {
                if (value < cap_2) return cap_2;
                else if (value > cap_1) return cap_1;
                else return value;
            }
            else if (cap_1 == cap_2)
            {
                return cap_1;
            }
            else return value;
        }

        public static float Q__Constrained(
            this float value,
            in float range_min,
            in float range_max
            )
        {
            if (range_min < range_max)
            {
                if (value > range_max) return range_max;
                else if (value < range_min) return range_min;
                else return value;
            }
            else if (range_min > range_max)
            {
                if (value < range_max) return range_max;
                else if (value > range_min) return range_min;
                else return value;
            }
            else if (range_min == range_max)
            {
                return range_min;
            }
            else return value;
        }

        public static double Q__Mapped(
            this double value,
            double from_min,
            double from_max,
            double to_min,
            double to_max
            )
        {
            double value_dif = value - from_min;
            double from_range = from_max - from_min;
            double to_range = to_max - to_min;

            double mapped_value = value_dif * to_range / from_range + to_min;

            return mapped_value;
        }

        public static float Q__Mapped(
            this float value,
            float from_min,
            float from_max,
            float to_min,
            float to_max
            )
        {
            float value_dif = value - from_min;
            float from_range = from_max - from_min;
            float to_range = to_max - to_min;

            float mapped_value = to_min + value_dif * to_range / from_range;

            return mapped_value;
        }

        public static double Q__Mapped_Constrained(
            this double value,
            double from_min,
            double from_max,
            double to_min,
            double to_max
            )
        {
            return
                Q__Mapped(value, from_min, from_max, to_min, to_max)
                .Q__Constrained(to_min, to_max);
        }


        public static float Q__Mapped_Constrained(
            this float value,
            float from_min,
            float from_max,
            float to_min,
            float to_max
            )
        {
            return
                Q__Mapped(value, from_min, from_max, to_min, to_max)
                .Q__Constrained(to_min, to_max);
        }


    }
}
