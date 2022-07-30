using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public abstract class Point_Cloud_Generation_Method
    {
        public Rect2d bounding_rectangle;

        public Point_Cloud_Generation_Method()
        {

        }

        public Point_Cloud_Generation_Method(Rect2d bounding_rectangle)
        {
            this.bounding_rectangle = bounding_rectangle;
        }

        public abstract List<Vec2d> Generate_Point_Cloud(I_PRNG rand);
    }
}
