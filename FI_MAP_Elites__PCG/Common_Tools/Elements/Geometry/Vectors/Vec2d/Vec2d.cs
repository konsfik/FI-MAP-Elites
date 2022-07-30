using System;

namespace Common_Tools
{
    public partial struct Vec2d
    {
        public double x;
        public double y;

        public static Vec2d From__Polar_Coordinates(
            double angle,
            double length
            )
        {
            double x = Math.Cos(angle) * length;
            double y = Math.Sin(angle) * length;

            return new Vec2d(x, y);
        }

        public Vec2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vec2d(float x, float y)
        {
            this.x = (double)x;
            this.y = (double)y;
        }

        public override string ToString()
        {
            return string.Format("(" + x + "," + y + ")");
        }

    }
}