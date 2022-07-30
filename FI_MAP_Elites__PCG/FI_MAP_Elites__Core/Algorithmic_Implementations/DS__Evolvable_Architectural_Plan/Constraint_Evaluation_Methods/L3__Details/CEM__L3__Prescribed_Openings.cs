using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CEM__L3__Prescribed_Openings : 
        Constraint_Evaluation_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new CEM__L3__Prescribed_Openings();
        }

        public override bool Q__Satisfied(DS__Architectural_Plan individual)
        {
            int num_existing_openings = individual.Q__Num__Existing_Openings();
            int num_prescribed_openings = individual.Q__Num__Prescribed_Openings();
            int num_satisfied_prescribed_openings = individual.Q__Num__Satisfied_Prescribed_Openings();

            bool condition =
                (num_existing_openings == num_prescribed_openings) && (num_prescribed_openings == num_satisfied_prescribed_openings);

            return condition;
        }
    }
}
