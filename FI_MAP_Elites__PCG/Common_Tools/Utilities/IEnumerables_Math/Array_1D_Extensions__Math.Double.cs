using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public static partial class Array_1D_Extensions__Math
    {
        public static double Q__Max(this double[] values)
        {
            double max_value = double.NegativeInfinity;

            foreach (var v in values) {
                if (v > max_value) {
                    max_value = v;
                }
            }

            return max_value;
        }

        /// <summary>
        /// Returns the sum of all numbers in the array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Q__Sum(this double[] values)
        {
            int len = values.Length;

            double sum = 0.0d;
            for (int i = 0; i < len; i++)
            {
                sum += values[i];
            }

            return sum;
        }

        /// <summary>
        /// Calculates the mean value (average) of an array of doubles.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Q__Mean(this double[] values)
        {
            return values.Q__Sum() / (double)values.Length;
        }

        /// <summary>
        /// Calculates the variance of an array of doubles.
        /// Also offers the option of treating the values as a population or as a sample.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="is_sample"></param>
        /// <returns></returns>
        public static double Q__Variance(this double[] values, bool is_sample)
        {
            double mean = values.Q__Mean();
            double variance_sum = 0;

            foreach (var value in values)
                variance_sum += (value - mean) * (value - mean);

            double divider = (double)values.Length;

            if (is_sample) divider -= 1.0;

            return variance_sum / divider;
        }

        /// <summary>
        /// Calculates the standard deviation of an array of doubles.
        /// Also offers the option of treating the values as a population or as a sample.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="is_sample"></param>
        /// <returns></returns>
        public static double Q__Standard_Deviation(this double[] values, bool is_sample)
        {
            return Math.Sqrt(Q__Variance(values, is_sample));
        }


    }
}
