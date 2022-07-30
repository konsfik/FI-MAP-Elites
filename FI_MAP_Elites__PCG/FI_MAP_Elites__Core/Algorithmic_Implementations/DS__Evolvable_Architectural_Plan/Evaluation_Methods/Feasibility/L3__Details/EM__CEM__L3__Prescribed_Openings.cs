using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__CEM__L3__Prescribed_Openings : Evaluation_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__CEM__L3__Prescribed_Openings();
        }

        public override double Evaluate_Individual(DS__Architectural_Plan individual)
        {
            return individual.Q__Eval__Openings_Placement_Score();
        }
    }
}
