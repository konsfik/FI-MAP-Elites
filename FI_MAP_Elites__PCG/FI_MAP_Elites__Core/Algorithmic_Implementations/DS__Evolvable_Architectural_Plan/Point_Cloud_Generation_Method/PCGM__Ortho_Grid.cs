using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class PCGM__Ortho_Grid : Point_Cloud_Generation_Method
    {
        public readonly int num_points_x;
        public readonly int num_points_y;

        public PCGM__Ortho_Grid(
            Rect2d bounding_rectangle,
            int num_points_x,
            int num_points_y
            ) : base(bounding_rectangle)
        {
            this.num_points_x = num_points_x;
            this.num_points_y = num_points_y;
        }

        /// <summary>
        /// -----------
        /// |.|.|.|.|.|
        /// -----------
        /// |.|*|*|*|.|
        /// -----------
        /// |.|*|*|*|.|
        /// -----------
        /// |.|*|*|*|.|
        /// -----------
        /// |.|.|.|.|.|
        /// -----------
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public override List<Vec2d> Generate_Point_Cloud(I_PRNG rand)
        {
            double dist_between_points__x_axis = bounding_rectangle.width / (double)num_points_x;
            double dist_between_points__y_axis = bounding_rectangle.height / (double)num_points_y;

            double min_x_value = dist_between_points__x_axis / 2.0;
            double min_y_value = dist_between_points__y_axis / 2.0;

            List<Vec2d> point__per__cell_id = new List<Vec2d>();
            for (int xi = 0; xi < num_points_x; xi++)
            {
                for (int yi = 0; yi < num_points_y; yi++)
                {
                    double x = min_x_value + xi * dist_between_points__x_axis;
                    double y = min_y_value + yi * dist_between_points__y_axis;

                    Vec2d v = new Vec2d(x, y);
                    point__per__cell_id.Add(v);
                }
            }

            return point__per__cell_id;
        }
    }
}
