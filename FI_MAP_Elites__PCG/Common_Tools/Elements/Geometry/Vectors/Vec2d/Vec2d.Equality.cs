using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Vec2d : IEquatable<Vec2d>
    {
        public bool Equals(Vec2d other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public override bool Equals(object other)
        {
            if (other is Vec2d o)
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
            return hash;
        }

        public static bool operator ==(Vec2d a, Vec2d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec2d a, Vec2d b)
        {
            return !(a == b);
        }
    }
}
