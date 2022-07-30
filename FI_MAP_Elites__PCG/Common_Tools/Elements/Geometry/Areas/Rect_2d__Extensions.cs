using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Rect_2d__Extensions
    {
        public static bool Contains_Point(
            this Rect2d bounding_rectangle,
            Vec2d point,
            bool include_edges
            )
        {
            if (include_edges)
            {

                return
                    point.x >= bounding_rectangle.Min_X
                    &&
                    point.x <= bounding_rectangle.Max_X
                    &&
                    point.y >= bounding_rectangle.Min_Y
                    &&
                    point.y <= bounding_rectangle.Max_Y;
            }
            else
            {
                return
                    point.x > bounding_rectangle.Min_X
                    &&
                    point.x < bounding_rectangle.Max_X
                    &&
                    point.y > bounding_rectangle.Min_Y
                    &&
                    point.y < bounding_rectangle.Max_Y;
            }
        }

        public static bool Contains_Point(
            this Rect2d bounding_rectangle,
            double x,
            double y,
            bool include_edges
            )
        {
            if (include_edges)
            {

                return
                    x >= bounding_rectangle.Min_X
                    &&
                    x <= bounding_rectangle.Max_X
                    &&
                    y >= bounding_rectangle.Min_Y
                    &&
                    y <= bounding_rectangle.Max_Y;
            }
            else
            {
                return
                    x > bounding_rectangle.Min_X
                    &&
                    x < bounding_rectangle.Max_X
                    &&
                    y > bounding_rectangle.Min_Y
                    &&
                    y < bounding_rectangle.Max_Y;
            }
        }

    }
}
