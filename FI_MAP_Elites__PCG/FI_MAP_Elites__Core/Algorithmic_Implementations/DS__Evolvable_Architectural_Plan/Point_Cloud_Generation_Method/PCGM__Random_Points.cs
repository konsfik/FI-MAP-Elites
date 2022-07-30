using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class PCGM__Random_Points : Point_Cloud_Generation_Method
    {
        public readonly int num_points;

        public PCGM__Random_Points(
            Rect2d bounding_rectangle,
            int num_points
            ) : base(bounding_rectangle)
        {
            this.num_points = num_points;
        }

        public override List<Vec2d> Generate_Point_Cloud(I_PRNG rand)
        {
            List<Vec2d> point__per__cell_id = new List<Vec2d>(capacity: num_points);

            for (int i = 0; i < num_points; i++)
            {
                double x = rand.NextDouble() * bounding_rectangle.width + bounding_rectangle.x;
                double y = rand.NextDouble() * bounding_rectangle.height + bounding_rectangle.y;

                Vec2d v = new Vec2d(x, y);
                point__per__cell_id.Add(v);
            }
            return point__per__cell_id;
        }
    }
}
