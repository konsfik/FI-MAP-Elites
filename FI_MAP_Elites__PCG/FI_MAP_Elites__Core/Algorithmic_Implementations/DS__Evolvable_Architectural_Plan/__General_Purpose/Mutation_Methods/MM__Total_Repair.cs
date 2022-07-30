using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Total_Repair : Mutation_Method<DS__Architectural_Plan>
    {
        List<Mutation_Method<DS__Architectural_Plan>> repair_methods;

        public MM__Total_Repair()
        {
            repair_methods = new List<Mutation_Method<DS__Architectural_Plan>>() {
                new MM__Repair__L2__Add_Missing_Space_Units<DS__Architectural_Plan>(),
                new MM__Repair__L2__Fix_Space_Units_Coherence<DS__Architectural_Plan>(),
                new MM__Repair__L2__Fix_Connections<DS__Architectural_Plan>(),
                new MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold<DS__Architectural_Plan>(0.1),
                new MM__Repair__L3__Openings()
            };
        }

        private MM__Total_Repair(MM__Total_Repair mm_to_copy)
        {
            this.repair_methods = mm_to_copy.repair_methods.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Total_Repair(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            DS__Architectural_Plan solution
            )
        {
            foreach (var method in repair_methods)
            {
                method.Mutate_Individual(rand, solution);
            }
        }
    }
}
