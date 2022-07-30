using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Vec2d
    {
        public static Vec2d operator -(Vec2d a, Vec2d b)
        {
            return new Vec2d(a.x - b.x, a.y - b.y);
        }

        public static Vec2d operator /(Vec2d vec, double num)
        {
            return new Vec2d(vec.x / num, vec.y / num);
        }

        public static Vec2d operator +(Vec2d a, Vec2d b)
        {
            return new Vec2d(a.x + b.x, a.y + b.y);
        }

        public static Vec2d operator *(Vec2d a, int i)
        {
            return new Vec2d(a.x * i, a.y * i);
        }

        public static Vec2d operator *(Vec2d a, double i)
        {
            return new Vec2d(a.x * i, a.y * i);
        }
    }
}
