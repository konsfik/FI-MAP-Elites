using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;


namespace Common_Tools
{
    public struct Vec2i : IEquatable<Vec2i>
    {
        public readonly int x;
        public readonly int y;

        public Vec2i(
            int x,
            int y
            )
        {
            this.x = x;
            this.y = y;
        }

        public HashSet<Vec2i> Geometric_Neighbors(Directions_Ortho_2D directions)
        {
            HashSet<Vec2i> geometric_neighbors = new HashSet<Vec2i>();
            if (directions.HasFlag(Directions_Ortho_2D.L)) geometric_neighbors.Add(To_Left());
            if (directions.HasFlag(Directions_Ortho_2D.R)) geometric_neighbors.Add(To_Right());
            if (directions.HasFlag(Directions_Ortho_2D.U)) geometric_neighbors.Add(To_Up());
            if (directions.HasFlag(Directions_Ortho_2D.D)) geometric_neighbors.Add(To_Down());
            return geometric_neighbors;
        }

        #region basic_directions
        public static readonly Vec2i left = new Vec2i(-1, 0);
        public static readonly Vec2i right = new Vec2i(1, 0);
        public static readonly Vec2i up = new Vec2i(0, 1);
        public static readonly Vec2i down = new Vec2i(0, -1);

        #endregion

        #region directions_towards
        public Vec2i To_Left()
        {
            return new Vec2i(x - 1, y);
        }
        public Vec2i To_Right()
        {
            return new Vec2i(x + 1, y);
        }
        public Vec2i To_Up()
        {
            return new Vec2i(x, y + 1);
        }
        public Vec2i To_Down()
        {
            return new Vec2i(x, y - 1);
        }
        #endregion

        public static Vec2i MinCoords(Vec2i p1, Vec2i p2)
        {
            return new Vec2i(
                Math.Min(p1.x, p2.x),
                Math.Min(p1.y, p2.y)
                );
        }

        public static Vec2i MinCoords(HashSet<Vec2i> points)
        {
            int min_X = points.ElementAt(0).x;
            int min_Y = points.ElementAt(0).y;

            foreach (var point in points)
            {
                min_X = Math.Min(min_X, point.x);
                min_Y = Math.Min(min_Y, point.y);
            }

            return new Vec2i(
                min_X,
                min_Y
                );
        }

        public static Vec2i MaxCoords(Vec2i p1, Vec2i p2)
        {
            return new Vec2i(
                Math.Max(p1.x, p2.x),
                Math.Max(p1.y, p2.y)
                );
        }

        public static Vec2i MaxCoords(HashSet<Vec2i> points)
        {
            int max_X = points.ElementAt(0).x;
            int max_Y = points.ElementAt(0).y;

            foreach (var point in points)
            {
                max_X = Math.Max(max_X, point.x);
                max_Y = Math.Max(max_Y, point.y);
            }

            return new Vec2i(
                max_X,
                max_Y
                );
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public static int Manhattan_Distance(Vec2i p1, Vec2i p2)
        {
            return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
        }

        public int Manhattan_Distance_From(Vec2i other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y);
        }

        public static int Max_Axis_Distance(Vec2i p1, Vec2i p2)
        {
            int x_dist = Math.Abs(p1.x - p2.x);
            int y_dist = Math.Abs(p1.y - p2.y);
            int max_axis_distance = x_dist;
            if (y_dist > max_axis_distance) max_axis_distance = y_dist;
            return max_axis_distance;
        }

        #region equality overrides && operator overloading
        public override bool Equals(object otherObject)
        {
            if (otherObject is Vec2i other_vector)
            {
                return Equals(other_vector);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Vec2i otherObject)
        {
            return
            (
                this.x == otherObject.x
                &&
                this.y == otherObject.y
            );
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + x;
            hash = hash * 31 + y;
            return hash;
        }



        public static bool operator ==(Vec2i c1, Vec2i c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(Vec2i c1, Vec2i c2)
        {
            return !(c1 == c2);
        }

        public static Vec2i operator +(Vec2i left, Vec2i right)
        {
            return new Vec2i(
                left.x + right.x,
                left.y + right.y
                );
        }

        public static Vec2i operator +(Vec2i left, int right)
        {
            return new Vec2i(
                left.x + right,
                left.y + right
                );
        }

        public static Vec2i operator -(Vec2i left, Vec2i right)
        {
            return new Vec2i(
                left.x - right.x,
                left.y - right.y
                );
        }

        public static Vec2i operator -(Vec2i left, int right)
        {
            return new Vec2i(
                left.x - right,
                left.y - right
                );
        }

        public static Vec2i operator *(Vec2i left, Vec2i right)
        {
            return new Vec2i(
                left.x * right.x,
                left.y * right.y
                );
        }

        public static Vec2i operator *(Vec2i left, int right)
        {
            return new Vec2i(
                left.x * right,
                left.y * right
                );
        }

        public static Vec2i operator /(Vec2i left, Vec2i right)
        {
            return new Vec2i(
                left.x / right.x,
                left.y / right.y
                );
        }

        public static Vec2i operator /(Vec2i left, int right)
        {
            return new Vec2i(
                left.x / right,
                left.y / right
                );
        }
        #endregion
    }
}
