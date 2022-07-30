using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Vec2d
    {
        /// <summary>
        /// reference: https://forum.unity.com/threads/whats-the-best-way-to-rotate-a-vector2-in-unity.729605/
        /// input must be in radians
        /// </summary>
        /// <param name="rotation_angle"></param>
        /// <returns></returns>
        public Vec2d Q__Rotated(double rotation_angle)
        {
            return new Vec2d(
               x * Math.Cos(rotation_angle) - y * Math.Sin(rotation_angle),
               x * Math.Sin(rotation_angle) + y * Math.Cos(rotation_angle)
            );
        }

        public Vec2d Q__Inversed()
        {
            return new Vec2d(-x, -y);
        }

        public double Q__Magnitude()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public Vec2d Q__Normalized()
        {
            double magnitude = this.Q__Magnitude();
            if (magnitude == 0.0) return new Vec2d(x, y);

            return new Vec2d(
                this.x / magnitude,
                this.y / magnitude
                );
        }

        public double Q__Squared_Distance_To(Vec2d other)
        {
            return Vec2d.Squared_Distance_Between(this, other);
        }

        public double Q__Distance_To(Vec2d other)
        {
            return Vec2d.Distance_Between(this, other);
        }

        /// <summary>
        /// Measures the signed angle from "this" to "other" in a counter-clock-wise manner.
        /// The result is in radians in the range between (-PI and PI).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Q__Signed_Angle_To(Vec2d other)
        {
            return Vec2d.Signed_Angle_Between(this, other);
        }

        /// <summary>
        /// Returns the angle between this vector and another vector.
        /// The result is in radians, in the range between 0 and PI.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Q__Angle_To(Vec2d other) {
            return Vec2d.Angle_Between(this, other);
        }

        public bool Q__Approximately_Equal_To(Vec2d other, double epsilon)
        {
            double x_dif = Math.Abs(this.x - other.x);
            double y_dif = Math.Abs(this.y - other.y);

            if (x_dif <= epsilon && y_dif <= epsilon)
                return true;
            else
                return false;
        }
    }
}
