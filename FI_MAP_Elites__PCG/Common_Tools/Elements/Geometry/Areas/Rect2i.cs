using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Common_Tools
{
    public partial struct Rect2i
    {
        // state
        public readonly Vec2i min_coords;
        public readonly Vec2i max_coords;

        // constructors

        // json constructor
        public Rect2i(
            Vec2i min_coords,
            Vec2i max_coords
            )
        {
            // just in case...
            Vec2i min = Vec2i.MinCoords(min_coords, max_coords);
            Vec2i max = Vec2i.MaxCoords(min_coords, max_coords);
            this.min_coords = min;
            this.max_coords = max;
        }

        public static Rect2i From__Inner_Size(int inner_size)
        {
            Vec2i min_coords = new Vec2i(0, 0);
            Vec2i max_coords = new Vec2i(inner_size, inner_size);
            return new Rect2i(min_coords, max_coords);
        }

        public static Rect2i From__Outer_Size(int outer_size)
        {
            return From__Inner_Size(outer_size - 1);
        }

        public static Rect2i From__Inner_Dimensions(int inner_width, int inner_height)
        {
            Vec2i min_coords = new Vec2i(0, 0);
            Vec2i max_coords = new Vec2i(inner_width, inner_height);
            return new Rect2i(min_coords, max_coords);
        }

        public static Rect2i From__Outer_Dimensions(int outer_width, int outer_height)
        {
            return From__Inner_Dimensions(outer_width - 1, outer_height - 1);
        }

        public static Rect2i From__Num_Points(int num_points_x, int num_points_y)
        {
            return From__Outer_Dimensions(num_points_x, num_points_y);
        }

        


        // to string
        public override string ToString()
        {
            return "[" + min_coords.ToString() + "," + max_coords.ToString() + "]";
        }

        // equality overrides && operator overloadings
        public override bool Equals(object otherObject)
        {
            if (otherObject.GetType() != this.GetType())
            {
                return false;
            }
            Rect2i other = (Rect2i)otherObject;
            if (
                this.min_coords == other.min_coords
                &&
                this.max_coords == other.max_coords
                )
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + min_coords.GetHashCode();
            hash = hash * 31 + max_coords.GetHashCode();
            return hash;
        }
        public static bool operator ==(Rect2i b1, Rect2i b2)
        {
            return b1.Equals(b2);
        }
        public static bool operator !=(Rect2i b1, Rect2i b2)
        {
            return !(b1 == b2);
        }

    }
}
