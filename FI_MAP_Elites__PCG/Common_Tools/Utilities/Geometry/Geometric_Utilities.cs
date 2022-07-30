using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public static class Geometric_Utilities
    {
        public static double Q__Angle__Bounded__NegPI_To_PI(
            this double angle)
        {
            return angle.Q__Angle__Bounded__2PI() - Math.PI;
        }

        /// <summary>
        /// Treats the input variable (angle) as an angle in radians.
        /// Returns its equivalent angle in the range of 0 to 2PI.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double Q__Angle__Bounded__2PI(
            this double angle)
        {
            double bounded_angle = angle;
            if (angle > (2.0 * Math.PI))
            {
                bounded_angle = bounded_angle % (2.0 * Math.PI);
                return bounded_angle;
            }
            else if (angle < 0)
            {
                bounded_angle = bounded_angle % (2.0 * Math.PI);
                bounded_angle = 2.0 * Math.PI - bounded_angle;
                return bounded_angle;
            }
            else
            {
                return bounded_angle;
            }
        }


        public static Vec2d[] Q__Mirrored_LR(this Vec2d[] points, double y_axis_location)
        {
            int num_points = points.Length;
            Vec2d[] mirrored_points = new Vec2d[num_points];
            for (int i = 0; i < num_points; i++)
            {
                double x_dif = points[i].x - y_axis_location;
                mirrored_points[i] = new Vec2d(
                    points[i].x - x_dif * 2.0,
                    points[i].y
                    );
            }
            return mirrored_points;
        }

        public static Vec2d[] Q__Mirrored_UD(this Vec2d[] points, double x_axis_location)
        {
            int num_points = points.Length;
            Vec2d[] mirrored_points = new Vec2d[num_points];
            for (int i = 0; i < num_points; i++)
            {
                double y_dif = points[i].y - x_axis_location;
                mirrored_points[i] = new Vec2d(
                    points[i].x,
                    points[i].y - y_dif * 2.0
                    );
            }
            return mirrored_points;
        }

        /// <summary>
        /// Calculates the area of a triangle, 
        /// given the length of its three sides (a,b,c),
        /// based on Heron's formula.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double Q__Triangle_Area(
            double a,
            double b,
            double c
            )
        {
            double p = (a + b + c) / 2;
            double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            return area;
        }

        /// <summary>
        /// Calculates the area of a triangle, 
        /// given the length of its three sides (a,b,c),
        /// based on Heron's formula.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double Q__Triangle_Area(
            Vec2d p1,
            Vec2d p2,
            Vec2d p3
            )
        {
            double a = p1.Q__Distance_To(p2);
            double b = p2.Q__Distance_To(p3);
            double c = p3.Q__Distance_To(p1);
            return Q__Triangle_Area(a, b, c);
        }

        public static Vec2d Q__Triangle_Centroid(
            Vec2d p1,
            Vec2d p2,
            Vec2d p3
            )
        {
            return (p1 + p2 + p3) / 3.0;
        }

        public static double Q__Angle__Between_Connected_Lines(
            this Line_Segment line_1,
            Line_Segment line_2
            )
        {
            if (line_1.p0 == line_2.p0)
            {
                Vec2d v1 = line_1.p1 - line_1.p0; // from p0 to p1
                Vec2d v2 = line_2.p1 - line_2.p0; // from p0 to p1
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p0 == line_2.p1)
            {
                Vec2d v1 = line_1.p1 - line_1.p0; // from p0 to p1
                Vec2d v2 = line_2.p0 - line_2.p1; // from p1 to p0
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p1 == line_2.p0)
            {
                Vec2d v1 = line_1.p0 - line_1.p1; // from p1 to p0
                Vec2d v2 = line_2.p1 - line_2.p0; // from p0 to p1
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p1 == line_2.p1)
            {
                Vec2d v1 = line_1.p0 - line_1.p1; // from p1 to p0
                Vec2d v2 = line_2.p0 - line_2.p1; // from p1 to p0
                return v1.Q__Angle_To(v2);
            }
            else
            {
                throw new System.Exception("lines do not share a common point");
            }
        }

        public static double Q__Orthogonality__Between_Connected_Lines(
            this Line_Segment line_1,
            Line_Segment line_2
            ) 
        {
            double angle = line_1.Q__Angle__Between_Connected_Lines(line_2);

            double orthogonality;

            if (angle < 0) orthogonality = 0.0;
            else if (angle < Math.PI / 2.0)
                orthogonality = angle.Q__Mapped(
                    0.0,
                    Math.PI / 2.0,
                    0.0,
                    1.0
                    );
            else if (angle < 3.0 * Math.PI / 4.0)
                orthogonality = angle.Q__Mapped(
                    Math.PI / 2.0,
                    3.0 * Math.PI / 4.0,
                    1.0,
                    0.5
                    );
            else if (angle <= Math.PI)
                orthogonality = angle.Q__Mapped(
                    3.0 * Math.PI / 4.0,
                    Math.PI,
                    0.5,
                    1.0
                    );
            else orthogonality = 0.0;

            return orthogonality;
        }

        public static double Q__Angle__Between_Approximately_Connected_Lines(
            this Line_Segment line_1,
            Line_Segment line_2,
            double connection_epsilon
            )
        {
            if (line_1.p0.Q__Approximately_Equal_To(line_2.p0, connection_epsilon))
            {
                Vec2d v1 = line_1.p1 - line_1.p0; // from p0 to p1
                Vec2d v2 = line_2.p1 - line_2.p0; // from p0 to p1
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p0.Q__Approximately_Equal_To(line_2.p1, connection_epsilon))
            {
                Vec2d v1 = line_1.p1 - line_1.p0; // from p0 to p1
                Vec2d v2 = line_2.p0 - line_2.p1; // from p1 to p0
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p1.Q__Approximately_Equal_To(line_2.p0, connection_epsilon))
            {
                Vec2d v1 = line_1.p0 - line_1.p1; // from p1 to p0
                Vec2d v2 = line_2.p1 - line_2.p0; // from p0 to p1
                return v1.Q__Angle_To(v2);
            }
            else if (line_1.p1.Q__Approximately_Equal_To(line_2.p1, connection_epsilon))
            {
                Vec2d v1 = line_1.p0 - line_1.p1; // from p1 to p0
                Vec2d v2 = line_2.p0 - line_2.p1; // from p1 to p0
                return v1.Q__Angle_To(v2);
            }
            else
            {
                throw new System.Exception("lines do not share a common point");
            }
        }


        /// <summary>
        /// Implementation Source: https://web.archive.org/web/20100405070507/http://valis.cs.uiuc.edu/~sariel/research/CG/compgeom/msg00831.html
        /// Alternative implementation source: https://mathopenref.com/coordpolygonarea2.html
        /// </summary>
        /// <returns></returns>
        public static double Q__Signed_Double_Area(
            this List<Vec2d> region_points
            )
        {
            int num_verts = region_points.Count;

            double signedDoubleArea = 0;

            for (int index = 0; index < num_verts; index++)
            {
                int next_index = (index + 1) % num_verts;
                Vec2d point = region_points[index];
                Vec2d next_point = region_points[next_index];
                signedDoubleArea += point.x * next_point.y - point.y * next_point.x;
            }

            return signedDoubleArea;
        }

        public static double Q__Area(
            this List<Vec2d> region_points
            )
        {
            return Math.Abs(region_points.Q__Signed_Double_Area() / 2.0);
        }
    }
}
