using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class MM__Random_Selection__Recalculations : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly List<Mutation_Method<DS__Architectural_Plan>> mutation_methods_list;
        public readonly bool recalculate_phenotype;
        public readonly int starting_level;

        public MM__Random_Selection__Recalculations(
            List<Mutation_Method<DS__Architectural_Plan>> mutation_methods_list,
            bool recalculate_phenotype,
            int starting_level
            )
        {
            this.mutation_methods_list = mutation_methods_list.Q__Deep_Copy();
            this.recalculate_phenotype = recalculate_phenotype;
            this.starting_level = starting_level;
        }

        private MM__Random_Selection__Recalculations(
            MM__Random_Selection__Recalculations mm_to_copy)
        {
            this.mutation_methods_list = mm_to_copy.mutation_methods_list.Q__Deep_Copy();
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
            this.starting_level = mm_to_copy.starting_level;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Random_Selection__Recalculations(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Architectural_Plan individual)
        {
            mutation_methods_list.Q__Random_Item(rand).Mutate_Individual(rand, individual);

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: starting_level);
            }
        }
    }
}
