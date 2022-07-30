using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__Experiments
{
    public static partial class Templates__PCG_Workshop
    {
        public static Generation_Method<DS__Architectural_Plan> Template__Generation_Method(
            DS__Layout_Constraints layout_constraints,
            Tessellation_Type tessellation_type
            ) 
        {
            var pcgm = Q__Point_Cloud_Generation_Method(tessellation_type);

            var generation_method = new GM__Select_Place_Expand_Repeat(
                problem_specification: layout_constraints,
                point_cloud_generation_method: pcgm
                );

            return generation_method;
        }

        public static Point_Cloud_Generation_Method Q__Point_Cloud_Generation_Method(Tessellation_Type pcgm_type)
        {
            if (pcgm_type == Tessellation_Type.ORTHO)
            {
                Rect2d bounding_rectangle = new Rect2d(0, 0, 16, 16);
                return new PCGM__Ortho_Grid(
                    bounding_rectangle,
                    16,
                    16
                    );
            }
            else if (pcgm_type == Tessellation_Type.HEX)
            {
                Rect2d bounding_rectangle = new Rect2d(0, 0, 16.74, 14.03);
                return new PCGM__Hexagonal_Grid(
                    bounding_rectangle,
                    16
                    );
            }
            else if (pcgm_type == Tessellation_Type.RANDOM)
            {
                Rect2d bounding_rectangle = new Rect2d(0, 0, 16, 16);
                return new PCGM__Random_Points(
                    bounding_rectangle,
                    256
                    );
            }
            else
            {
                return null;
            }
        }
    }
}
