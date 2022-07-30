using System;
using System.Collections;

namespace Common_Tools
{
    public struct Circle : IEquatable<Circle>
    {
        public Vec2d center;
        public double radius;

        public Circle(double centerX, double centerY, double radius)
        {
            this.center = new Vec2d(centerX, centerY);
            this.radius = radius;
        }

        #region equality_override
        public override bool Equals(object other_object)
        {
            if (other_object is Line_Segment other_vector)
            {
                return Equals(other_vector);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Circle other)
        {
            return this.center == other.center && this.radius == other.radius;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + radius.GetHashCode();
            hash = hash * 31 + center.GetHashCode();
            return hash;
        }

        public static bool operator ==(Circle a, Circle b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Circle a, Circle b)
        {
            return !(a == b);
        }
        #endregion

        public override string ToString()
        {
            return "Circle (center: " + center + "; radius: " + radius + ")";
        }
    }
}