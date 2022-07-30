using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Rect2i
    {
        public int Min_X()
        {
            return min_coords.x;
        }

        public int Max_X()
        {
            return max_coords.x;
        }

        public int Min_Y()
        {
            return min_coords.y;
        }

        public int Max_Y()
        {
            return max_coords.y;
        }

        public int Q__Inner_Width()
        {
            return Max_X() - Min_X();
        }

        public int Q__Outer_Width()
        {
            return Q__Inner_Width() + 1;
        }

        public int Q__Inner_Height()
        {
            return Max_Y() - Min_Y();
        }

        public int Q__Outer_Height()
        {
            return Q__Inner_Height() + 1;
        }
        public int Q__Num_Points_X()
        {
            return Q__Outer_Width();
        }

        public int Q__Num_Points_Y()
        {
            return Q__Outer_Height();
        }

        public int Q__Num_Points()
        {
            return Q__Num_Points_X() * Q__Num_Points_Y();
        }

    }
}
