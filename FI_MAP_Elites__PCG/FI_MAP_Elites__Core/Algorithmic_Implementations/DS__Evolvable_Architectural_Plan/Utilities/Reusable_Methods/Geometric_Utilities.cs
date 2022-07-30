using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static class Geometric_Utilities
    {
        /// <summary>
        /// Calculates the minimum possible perimeter, for a 2D area.
        /// It does so by finding the perimeter of a circle that has that area.
        /// (assuming that a circle is the shape with the smallest possible perimeter per area)
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static double Minimum_Possible_Perimeter(
            double area
            )
        {
            // the radius of a circle with an area equal to the room 
            // E = PI * r^2
            // => E / PI = r^2
            // => r = sqrt(E / PI)
            double circle_radius = Math.Sqrt(area / Math.PI);

            // the minimum possible perimeter is equal to the perimeter of a circle
            // P = 2 * pi * r
            double circle_perimeter = 2.0 * Math.PI * circle_radius;

            return circle_perimeter;
        }

        /// <summary>
        /// Evaluates the difference between a value and a target value.
        /// If the value is smaller than the target value, the
        /// </summary>
        /// <param name="target_value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Evaluate_Similarity(
            double value,
            double target_value
            )
        {
            if (value < 0 || target_value < 0) {
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
    }
}
