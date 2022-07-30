using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Rect_2i__Extensions
    {
        public static HashSet<Vec2i> Contained_Points__Set(
            this Rect2i rect
            )
        {
            HashSet<Vec2i> contained_points = new HashSet<Vec2i>();
            for (int x = rect.min_coords.x; x <= rect.max_coords.x; x++)
            {
                for (int y = rect.min_coords.y; y <= rect.max_coords.y; y++)
                {
                    contained_points.Add(
                        new Vec2i(x, y)
                        );
                }
            }
            return contained_points;
        }

        public static List<Vec2i> Contained_Points__List(
            this Rect2i rect
            )
        {
            List<Vec2i> contained_points = new List<Vec2i>();
            for (int x = rect.min_coords.x; x <= rect.max_coords.x; x++)
            {
                for (int y = rect.min_coords.y; y <= rect.max_coords.y; y++)
                {
                    contained_points.Add(
                        new Vec2i(x, y)
                        );
                }
            }
            return contained_points;
        }

        public static HashSet<UEdge_2i> Contained_Ortho_UEdges__Set(
            this Rect2i rect
            )
        {
            int min_x = rect.min_coords.x;
            int max_x = rect.max_coords.x;
            int min_y = rect.min_coords.y;
            int max_y = rect.max_coords.y;

            HashSet<UEdge_2i> contained_u_edges = new HashSet<UEdge_2i>();
            for (int x = min_x; x <= max_x; x++)
            {
                for (int y = min_y; y <= max_y; y++)
                {
                    // right - edge
                    if (x < max_x)
                    {
                        Vec2i origin = new Vec2i(x, y);
                        Vec2i exit = new Vec2i(x + 1, y);
                        UEdge_2i edge = new UEdge_2i(origin, exit);
                        contained_u_edges.Add(edge);
                    }
                    // down - edge
                    if (y < max_y)
                    {
                        Vec2i origin = new Vec2i(x, y);
                        Vec2i exit = new Vec2i(x, y + 1);
                        UEdge_2i edge = new UEdge_2i(origin, exit);
                        contained_u_edges.Add(edge);
                    }
                }
            }
            return contained_u_edges;
        }

        public static List<UEdge_2i> Contained_Ortho_UEdges__List(
            this Rect2i rect
            )
        {
            int min_x = rect.min_coords.x;
            int max_x = rect.max_coords.x;
            int min_y = rect.min_coords.y;
            int max_y = rect.max_coords.y;

            List<UEdge_2i> contained_u_edges = new List<UEdge_2i>();
            for (int x = min_x; x <= max_x; x++)
            {
                for (int y = min_y; y <= max_y; y++)
                {
                    // right - edge
                    if (x < max_x)
                    {
                        Vec2i origin = new Vec2i(x, y);
                        Vec2i exit = new Vec2i(x + 1, y);
                        UEdge_2i edge = new UEdge_2i(origin, exit);
                        contained_u_edges.Add(edge);
                    }
                    // down - edge
                    if (y < max_y)
                    {
                        Vec2i origin = new Vec2i(x, y);
                        Vec2i exit = new Vec2i(x, y + 1);
                        UEdge_2i edge = new UEdge_2i(origin, exit);
                        contained_u_edges.Add(edge);
                    }
                }
            }
            return contained_u_edges;
        }

        public static bool Contains(
            this in Rect2i rect,
            in Vec2i point
            )
        {
            return
                point.x.Q__Is_In_Range(rect.min_coords.x, rect.max_coords.x)
                &&
                point.y.Q__Is_In_Range(rect.min_coords.y, rect.max_coords.y);
        }

        public static bool Contains(
            this in Rect2i this_rect,
            in Rect2i otherRect
            )
        {
            return
                this_rect.Contains(otherRect.min_coords)
                &&
                this_rect.Contains(otherRect.max_coords);
        }


        public static Rect2i RandomContainedRect(
            Random rand,
            Rect2i rect)
        {
            Vec2i p1 = rect.Random_ContainedPoint(rand);
            Vec2i p2 = rect.Random_ContainedPoint(rand);

            Rect2i newRect = new Rect2i(p1, p2);

            return newRect;
        }
    }
}
