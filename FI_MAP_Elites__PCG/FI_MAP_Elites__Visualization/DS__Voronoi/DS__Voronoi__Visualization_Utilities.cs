using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static class DS__Voronoi__Visualization_Utilities
    {
        public static List<SKPoint> Convert__To__SKPoint_List(
            this List<Vec2d> original_list,
            float offset_x,
            float offset_y,
            float scale
            )
        {
            List<SKPoint> converted_list = new List<SKPoint>();
            foreach (var op in original_list)
            {
                SKPoint p = new SKPoint(
                    (float)op.x * scale + offset_x,
                    (float)op.y * scale + offset_y
                    );
                converted_list.Add(p);
            }
            return converted_list;
        }

        public static SKPoint Convert__To__System_Drawing_PointF(
            this Vec2d vert,
            float offset_x,
            float offset_y, 
            float scale
            )
        {
            return new SKPoint(
                (float)vert.x * scale + offset_x,
                (float)vert.y * scale + offset_y
                );
        }
    }
}
