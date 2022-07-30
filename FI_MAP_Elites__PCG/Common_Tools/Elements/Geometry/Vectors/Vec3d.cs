using System;

namespace Common_Tools
{
    public struct Vec3d : IEquatable<Vec3d>
    {
        public double x;
        public double y;
        public double z;

        #region templates
        public static readonly Vec3d zero = new Vec3d(0, 0, 0);
        public static readonly Vec3d one = new Vec3d(1, 1, 1);

        public static readonly Vec3d right = new Vec3d(1, 0, 0);
        public static readonly Vec3d left = new Vec3d(-1, 0, 0);

        public static readonly Vec3d up = new Vec3d(0, 1, 0);
        public static readonly Vec3d down = new Vec3d(0, -1, 0);

        public static readonly Vec3d forward = new Vec3d(0, 0, 1);
        public static readonly Vec3d back = new Vec3d(0, 0, -1);
        #endregion

        #region constructors

        public Vec3d(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion


        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public void Normalize()
        {
            double magnitude = this.Magnitude();
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
        }

        public static Vec3d Normalize(Vec3d a)
        {
            double magnitude = a.Magnitude();
            return new Vec3d(a.x / magnitude, a.y / magnitude, a.z / magnitude);
        }

        public double DistanceSquare(Vec3d v)
        {
            return Vec3d.DistanceSquare(this, v);
        }

        public static double DistanceSquare(Vec3d a, Vec3d b)
        {
            double cx = b.x - a.x;
            double cy = b.y - a.y;
            double cz = b.z - a.z;
            return cx * cx + cy * cy + cz * cz;
        }

        public override string ToString()
        {
            return string.Format("(" + x + "," + y + ")");
        }



        #region equality_override
        public bool Equals(Vec3d other)
        {
            return
                this.x == other.x
                &&
                this.y == other.y
                &&
                this.z == other.z;
        }

        public override bool Equals(object other)
        {
            if (other is Vec3d o)
            {
                return this.Equals(o);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + x.GetHashCode();
            hash = hash * 31 + y.GetHashCode();
            hash = hash * 31 + z.GetHashCode();
            return hash;
        }

        public static bool operator ==(Vec3d a, Vec3d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec3d a, Vec3d b)
        {
            return !(a == b);
        }
        #endregion

        #region operator_overloading
        public static Vec3d operator -(Vec3d a, Vec3d b)
        {
            return new Vec3d(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vec3d operator /(Vec3d vec, double num)
        {
            return new Vec3d(vec.x / num, vec.y / num, vec.z / num);
        }

        public static Vec3d operator +(Vec3d a, Vec3d b)
        {
            return new Vec3d(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vec3d operator *(Vec3d a, double mult)
        {
            return new Vec3d(a.x * mult, a.y * mult, a.z * mult);
        }

        public static Vec3d Min(Vec3d a, Vec3d b)
        {
            return new Vec3d(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public static Vec3d Max(Vec3d a, Vec3d b)
        {
            return new Vec3d(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }
        #endregion

    }
}