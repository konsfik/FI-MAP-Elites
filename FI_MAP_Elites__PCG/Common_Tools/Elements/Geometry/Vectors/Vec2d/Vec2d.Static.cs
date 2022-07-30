using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Vec2d
    {
        public static readonly Vec2d zero = new Vec2d(0, 0);
        public static readonly Vec2d one = new Vec2d(1, 1);

        public static readonly Vec2d right = new Vec2d(1, 0);
        public static readonly Vec2d left = new Vec2d(-1, 0);

        public static readonly Vec2d up = new Vec2d(0, 1);
        public static readonly Vec2d down = new Vec2d(0, -1);

        public static Vec2d Normalize(Vec2d a)
        {
            double magnitude = a.Q__Magnitude();
            if (magnitude == 0.0) return a;
            return new Vec2d(a.x / magnitude, a.y / magnitude);
        }

        public static double Squared_Distance_Between(Vec2d a, Vec2d b)
        {
            double delta_x = b.x - a.x;
            double delta_y = b.y - a.y;
            return (delta_x * delta_x) + (delta_y * delta_y);
        }

        public static double Distance_Between(Vec2d a, Vec2d b)
        {
            double squared_distance = Squared_Distance_Between(a, b);
            return Math.Sqrt(squared_distance);
        }

        /// <summary>
        /// Returns the angle between a and b.
        /// The result is in radians, in the range between 0 and PI.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Angle_Between(Vec2d a, Vec2d b)
        {
            return Math.Abs(Signed_Angle_Between(a, b));
        }

        /// <summary>
        /// Measures the signed angle from "a" to "b" in a counter-clock-wise manner.
        /// The result is in radians in the range between (-PI and PI).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Signed_Angle_Between(Vec2d a, Vec2d b)
        {
            double signed_angle =
                Math.Atan2(a.x * b.y - a.y * b.x, a.x * b.x + a.y * b.y);

            return signed_angle;
        }

        public static Vec2d Min(Vec2d a, Vec2d b)
        {
            return new Vec2d(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        public static Vec2d Max(Vec2d a, Vec2d b)
        {
            return new Vec2d(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }
    }
}
