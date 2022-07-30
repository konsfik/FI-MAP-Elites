using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__Experiments
{
    public static partial class Templates__PCG_Workshop
    {
        public static Evaluation_Method<DS__Architectural_Plan> Q__BC1()
        {
            return new EM__L2__Compactness<DS__Architectural_Plan>();
        }
        public static Evaluation_Method<DS__Architectural_Plan> Q__BC2()
        {
            return new EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan>();
        }
    }
}
