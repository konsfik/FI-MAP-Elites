using Common_Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class PCGM__Hexagonal_Grid : Point_Cloud_Generation_Method
    {
        public readonly int num_points_x;

        public PCGM__Hexagonal_Grid(
            Rect2d bounding_rectangle,
            int num_points_x
            ) : base(bounding_rectangle)
        {
            this.num_points_x = num_points_x;
        }

        public override List<Vec2d> Generate_Point_Cloud(I_PRNG rand)
        {
            int num_subdivisions = num_points_x * 2 - 1;

            double half_x_dist = bounding_rectangle.width / (double)num_subdivisions;
            double x_dist = half_x_dist * 2;

            double ang = 2.0 * Math.PI / 6.0; // 60 degrees
            double y_dist = x_dist * Math.Sin(ang);

            int num_points_y = (int)(bounding_rectangle.height / y_dist) + 1;

            List<Vec2d> point__per__cell_id = new List<Vec2d>();
            int cnt = 0;
            for (int xi = 0; xi < num_points_x; xi++)
            {
                for (int yi = 0; yi < num_points_y; yi++)
                {
                    double x = xi * x_dist;

                    if (yi % 2 == 1) x += half_x_dist;

                    double y = yi * y_dist;

                    Vec2d v = new Vec2d(x, y);
                    point__per__cell_id.Add(v);

                    cnt++;
                }
            }

            return point__per__cell_id;
        }
    }
}
