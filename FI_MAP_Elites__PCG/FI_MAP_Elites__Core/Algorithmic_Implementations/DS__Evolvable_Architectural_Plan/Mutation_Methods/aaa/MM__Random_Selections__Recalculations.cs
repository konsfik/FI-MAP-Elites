using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class MM__Random_Selections__Recalculations : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly List<Mutation_Method<DS__Architectural_Plan>> mutation_methods_list;
        public int minimum_selections;
        public int maximum_selections;

        public bool recalculate_phenotype;
        public int starting_level;

        public MM__Random_Selections__Recalculations(
            List<Mutation_Method<DS__Architectural_Plan>> mutation_methods_list,
            int minimum_selections,
            int maximum_selections,

            bool recalculate_phenotype,
            int starting_level
            )
        {
            if (minimum_selections < 1)
            {
                throw new Exception("minimum_selections must be >= 1");
            }
            if (maximum_selections < minimum_selections)
            {
                throw new Exception("maximum_selections must be >= minimum_selections");
            }
            this.mutation_methods_list = mutation_methods_list.Q__Deep_Copy();
            this.minimum_selections = minimum_selections;
            this.maximum_selections = maximum_selections;

            this.recalculate_phenotype = recalculate_phenotype;
            this.starting_level = starting_level;
        }

        private MM__Random_Selections__Recalculations(
            MM__Random_Selections__Recalculations mm_to_copy)
        {
            this.mutation_methods_list = mm_to_copy.mutation_methods_list.Q__Deep_Copy();
            this.minimum_selections = mm_to_copy.minimum_selections;
            this.maximum_selections = mm_to_copy.maximum_selections;

            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
            this.starting_level = mm_to_copy.starting_level;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Random_Selections__Recalculations(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Architectural_Plan individual)
        {
            int num_selections = rand.Next(minimum_selections, maximum_selections + 1);
            for (int i = 0; i < num_selections; i++)
            {
                mutation_methods_list.Q__Random_Item(rand).Mutate_Individual(rand, individual);
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level);
            }
        }
    }
}
