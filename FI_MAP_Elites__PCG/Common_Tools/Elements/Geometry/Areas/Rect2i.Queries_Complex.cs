using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Rect2i
    {
        public static HashSet<Vec2i> GeometricNeighbors_WithinRect(
            Rect2i rect,
            Vec2i point,
            Directions_Ortho_2D directions
            )
        {
            var neighbors = point.Geometric_Neighbors(directions);

            neighbors.RemoveWhere(
                x =>
                rect.Contains(x) == false
                );

            return neighbors;
        }

        public static HashSet<Vec2i> GeometricNeighbors_WithinRect(
            Rect2i rect,
            HashSet<Vec2i> points,
            Directions_Ortho_2D directions
            )
        {
            var neighbors = new HashSet<Vec2i>();

            foreach (var point in points)
            {
                neighbors.UnionWith(GeometricNeighbors_WithinRect(rect, point, directions));
            }

            neighbors.ExceptWith(points);

            return neighbors;
        }


        public Vec2i Up_Left_Corner()
        {
            return new Vec2i(Min_X(), Max_Y());
        }
        public Vec2i Up_Right_Corner()
        {
            return new Vec2i(Max_X(), Max_Y());
        }
        public Vec2i Down_Left_Corner()
        {
            return new Vec2i(Min_X(), Min_Y());
        }
        public Vec2i Down_Right_Corner()
        {
            return new Vec2i(Max_X(), Min_Y());
        }
        public HashSet<Vec2i> Corners()
        {
            return new HashSet<Vec2i>() {
                Up_Left_Corner(),
                Up_Right_Corner(),
                Down_Left_Corner(),
                Down_Right_Corner()
            };
        }


        public bool ContainsAll(
            HashSet<Vec2i> points
            )
        {
            foreach (var point in points)
            {
                if (this.Contains(point) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainsAny(
            HashSet<Vec2i> points
            )
        {
            foreach (var point in points)
            {
                if (this.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }


        public bool Intersects(Rect2i otherRect)
        {
            return ContainsAny(otherRect.Corners());
        }

        public Rect2i Random_ContainedRect(
            System.Random rand
            )
        {
            Vec2i p1 = Random_ContainedPoint(rand);
            Vec2i p2 = Random_ContainedPoint(rand);
            return new Rect2i(p1, p2);
        }

        public Rect2i Random_ContainedRect(
            I_PRNG rand
            )
        {
            Vec2i p1 = Random_ContainedPoint(rand);
            Vec2i p2 = Random_ContainedPoint(rand);
            return new Rect2i(p1, p2);
        }

        public Rect2i Random_ContainedRect(
            System.Random rand,
            int max_width,
            int max_height
            )
        {
            int x0 = rand.Next(Q__Num_Points_X()) + Min_X();
            int y0 = rand.Next(Q__Num_Points_Y()) + Min_Y();

            int x_remaining = Max_X() - x0;
            int y_remaining = Max_Y() - y0;

            int x1;
            int y1;
            if (x_remaining > max_width)
            {
                x1 = x0 + rand.Next(max_width);
            }
            else
            {
                x1 = x0 + rand.Next(x_remaining);
            }

            if (y_remaining > max_width)
            {
                y1 = y0 + rand.Next(max_height);
            }
            else
            {
                y1 = y0 + rand.Next(y_remaining);
            }

            Vec2i p1 = new Vec2i(x0, y0);
            Vec2i p2 = new Vec2i(x1, y1);
            return new Rect2i(p1, p2);
        }

        public Vec2i Random_ContainedPoint(System.Random rand)
        {
            return new Vec2i(
                rand.Next(Min_X(), Max_X() + 1),
                rand.Next(Min_Y(), Max_Y() + 1)
                );
        }

        public Vec2i Random_ContainedPoint(I_PRNG rand)
        {
            return new Vec2i(
                rand.Next(Min_X(), Max_X() + 1),
                rand.Next(Min_Y(), Max_Y() + 1)
                );
        }
    }
}
