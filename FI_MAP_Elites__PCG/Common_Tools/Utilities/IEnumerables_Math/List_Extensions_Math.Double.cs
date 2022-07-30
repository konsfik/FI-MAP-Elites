using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static partial class List_Extensions_Math
    {

        public static void M__Divide_By(
            this List<double> list,
            double divisor
            )
        {
            int num_elements = list.Count;
            for (int i = 0; i < num_elements; i++)
            {
                list[i] /= divisor;
            }
        }

        public static void M__Multiply_By(
            this List<double> list,
            double multiplier
            )
        {
            int num_elements = list.Count;
            for (int i = 0; i < num_elements; i++)
            {
                list[i] *= multiplier;
            }
        }

        public static double Q__Sum(
            this IEnumerable<double> values
            )
        {
            double sum = 0.0;
            foreach (var value in values)
            {
                sum += value;
            }
            return sum;
        }

        public static int Q__Sum(
            this IEnumerable<bool> values
            )
        {
            int sum = 0;
            foreach (var value in values)
            {
                if (value) sum++;
            }
            return sum;
        }

        /// <summary>
        /// Returns the average of a list (or other ienumerable) of double values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Q__Mean(
            this IEnumerable<double> values
            )
        {
            double sum = 0;
            int cnt = 0;
            foreach (var value in values)
            {
                sum += value;
                cnt++;
            }
            return sum / cnt;
        }

        public static double Q__Variance(
            this IEnumerable<double> values,
            bool is_sample
            )
        {
            double mean = values.Q__Mean();
            double variance_sum = 0;
            int cnt = 0;
            foreach (var value in values)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value) ^ 2
                cnt++;
            }
            double divider = (double)cnt;
            if (is_sample)
                divider -= 1.0;
            double variance = variance_sum / divider;
            return variance;
        }

        /// <summary>
        /// Returns the standard deviation of a list (or other ienumerable) of double values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="is_sample"></param>
        /// <returns></returns>
        public static double Q__Standard_Deviation(
            this List<double> values,
            bool is_sample
            )
        {
            double variance = values.Q__Variance(is_sample);
            return Math.Sqrt(variance);
        }

        public static double Q__Simplistic_Variance(this List<double> values)
        {
            double mean = values.Q__Mean();
            double variance_sum = 0;
            foreach (var value in values)
            {
                variance_sum += Math.Abs(mean - value);
            }
            double divider = (double)values.Count;
            double variance = variance_sum / divider;
            return variance;
        }

        /// <summary>
        /// Returns the minimum difference among a list of double numbers.
        /// This metric is quite similar to how Diversity is calculated, but 
        /// instead of calculating the average of nearest differences,
        /// it returns the minimum local difference that can be found.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Q__Minimum_Difference(this List<double> values)
        {
            List<double> sorted = values.Q__Deep_Copy();

            sorted.Sort();

            int num_values = sorted.Count;

            double min_diff = double.PositiveInfinity;

            for (int i = 0; i < num_values; i++)
            {
                if (i == 0)
                {
                    double min_delta = Math.Abs(sorted[i] - sorted[i + 1]);
                    if (min_delta < min_diff) min_diff = min_delta;
                }
                else if (i == num_values - 1)
                {
                    double min_delta = Math.Abs(sorted[i] - sorted[i - 1]);
                    if (min_delta < min_diff) min_diff = min_delta;
                }
                else
                {
                    double this_value = sorted[i];
                    double left_value = sorted[i - 1];
                    double right_value = sorted[i + 1];
                    double left_delta = Math.Abs(this_value - left_value);
                    double right_delta = Math.Abs(this_value - right_value);
                    double min_delta = (left_delta < right_delta) ? left_delta : right_delta;
                    if (min_delta < min_diff) min_diff = min_delta;
                }
            }

            return min_diff;
        }

        public static double Q__Diversity(this List<double> values)
        {
            List<double> sorted = values.Q__Deep_Copy();

            sorted.Sort();

            int num_values = sorted.Count;

            double min_delta_sum = 0.0;

            for (int i = 0; i < num_values; i++)
            {
                if (i == 0)
                {
                    double min_delta = Math.Abs(sorted[i] - sorted[i + 1]);
                    min_delta_sum += min_delta;
                }
                else if (i == num_values - 1)
                {
                    double min_delta = Math.Abs(sorted[i] - sorted[i - 1]);
                    min_delta_sum += min_delta;

                }
                else
                {
                    double this_value = sorted[i];
                    double left_value = sorted[i - 1];
                    double right_value = sorted[i + 1];
                    double left_delta = Math.Abs(this_value - left_value);
                    double right_delta = Math.Abs(this_value - right_value);
                    double min_delta = (left_delta < right_delta) ? left_delta : right_delta;
                    min_delta_sum += min_delta;
                }
            }

            double diversity = min_delta_sum / num_values;

            return diversity;
        }

    }
}
