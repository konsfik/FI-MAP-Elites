using System;
using System.Collections;
using System.Collections.Generic;

namespace Common_Tools
{
    public partial struct Line_Segment : IEquatable<Line_Segment>
    {
        public Vec2d p0;
        public Vec2d p1;

        public Line_Segment(Vec2d p0, Vec2d p1)
        {
            this.p0 = p0;
            this.p1 = p1;
        }

        public Line_Segment(double x0, double y0, double x1, double y1)
        {
            this.p0 = new Vec2d(x0, y0);
            this.p1 = new Vec2d(x1, y1);
        }

        public override string ToString()
        {
            return "[" + p0.ToString() + "," + p1.ToString() + "]";
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

        public bool Equals(Line_Segment other)
        {
            bool same = this.p0 == other.p0 && this.p1 == other.p1;
            bool reverse = this.p0 == other.p1 && this.p1 == other.p0;

            return same || reverse;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = (hash * 31) + p0.GetHashCode() + p1.GetHashCode();
            return hash;
        }

        public static bool operator ==(Line_Segment a, Line_Segment b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Line_Segment a, Line_Segment b)
        {
            return !(a == b);
        }
        #endregion
    }
}