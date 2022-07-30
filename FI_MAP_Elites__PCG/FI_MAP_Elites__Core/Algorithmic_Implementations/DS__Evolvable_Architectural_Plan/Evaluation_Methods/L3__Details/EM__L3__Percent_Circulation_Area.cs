using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__L3__Percent_Circulation_Area : Evaluation_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__L3__Percent_Circulation_Area();
        }

        public override double Evaluate_Individual(DS__Architectural_Plan individual)
        {
            return individual.Q__BC__Percent_Circulation_Area();
        }
    }
}
