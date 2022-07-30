using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class GM__Random_Placement : Generation_Method<DS__Voronoi>
    {
        public readonly int num_points;
        public readonly Rect2d bounding_rectangle;
        public readonly double connectivity_threshold;

        public GM__Random_Placement(
            int num_points,
            Rect2d bounding_rectangle,
            double connectivity_threshold
            )
        {
            this.num_points = num_points;
            this.bounding_rectangle = bounding_rectangle;
            this.connectivity_threshold = connectivity_threshold;
        }

        private GM__Random_Placement(GM__Random_Placement gm_to_copy)
        {
            this.num_points = gm_to_copy.num_points;
            this.bounding_rectangle = gm_to_copy.bounding_rectangle;
            this.connectivity_threshold = gm_to_copy.connectivity_threshold;
        }

        public override DS__Voronoi Generate_Individual(I_PRNG rand)
        {
            List<Vec2d> generated_points = new List<Vec2d>(capacity: num_points);
            for (int i = 0; i < num_points; i++)
            {
                double x = 
                    rand.NextDouble() * bounding_rectangle.width 
                    + bounding_rectangle.Min_X;
                double y = 
                    rand.NextDouble() * bounding_rectangle.height 
                    + bounding_rectangle.Min_Y;
                generated_points.Add(new Vec2d(x, y));
            }

            DS__Voronoi voronoi =
                new DS__Voronoi(
                    generated_points,
                    bounding_rectangle,
                    connectivity_threshold
                    );

            return voronoi;
        }

        public override object Q__Deep_Copy()
        {
            return new GM__Random_Placement(this);
        }
    }
}
