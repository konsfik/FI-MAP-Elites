using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction_Repair : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly MM__Random_Destruction destruction_method;
        public readonly MM__Total_Repair repair_method;

        public MM__Destruction_Repair()
        {
            destruction_method = new MM__Random_Destruction();
            repair_method = new MM__Total_Repair();
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction_Repair();
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Architectural_Plan individual)
        {
            destruction_method.Mutate_Individual(rand, individual);

            individual.M__Recalculate_Phenotype(starting_level: 1);

            repair_method.Mutate_Individual(rand, individual);

            individual.M__Recalculate_Phenotype(starting_level:1);
        }
    }
}
