using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.CMCE;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;

using SkiaSharp;

namespace FI_MAP_Elites__Experiments
{
    public static partial class Templates__PCG_Workshop
    {
        public static Visualization_Method<DS__Architectural_Plan> Prepare_Visualization_Method_Low_Res()
        {
            VM__Architectural_Plan__Simple visualization_method =
                new VM__Architectural_Plan__Simple(
                    room_perimeter__color: SKColors.Black,
                    room_perimeter__pen_width: 1.0f,
                    scale: 6.0f
                    );

            return visualization_method;
        }

        public static Visualization_Method<DS__Architectural_Plan> Prepare_Visualization_Method_High_Res()
        {
            VM__Architectural_Plan__Detailed visualization_method = new VM__Architectural_Plan__Detailed(
                room_perimeter__color: SKColors.Black,
                room_perimeter__pen_width: 2.0f,
                scale: 40.0f,
                plan_graph: true,
                plan_graph__stroke_width: 1.0f,
                plan_graph__dash_size: 4.0f,
                plan_graph__points_radius: 4.0f,
                plan_graph__color: SKColors.Black
                );

            return visualization_method;
        }
    }
}
