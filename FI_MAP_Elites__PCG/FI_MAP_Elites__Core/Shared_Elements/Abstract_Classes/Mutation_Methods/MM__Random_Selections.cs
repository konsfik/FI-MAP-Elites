using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class MM__Random_Selections<T> : Mutation_Method<T>
        where T : I_Deep_Copyable
    {
        public readonly List<Mutation_Method<T>> mutation_methods_list;
        public int minimum_selections;
        public int maximum_selections;

        public MM__Random_Selections(
            List<Mutation_Method<T>> mutation_methods_list,
            int minimum_selections,
            int maximum_selections
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
        }

        private MM__Random_Selections(MM__Random_Selections<T> mm_to_copy)
        {
            this.mutation_methods_list = mm_to_copy.mutation_methods_list.Q__Deep_Copy();
            this.minimum_selections = mm_to_copy.minimum_selections;
            this.maximum_selections = mm_to_copy.maximum_selections;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Random_Selections<T>(this);
        }

        public override void Mutate_Individual(I_PRNG rand, T individual)
        {
            int num_selections = rand.Next(minimum_selections, maximum_selections + 1);
            for (int i = 0; i < num_selections; i++)
            {
                mutation_methods_list.Q__Random_Item(rand).Mutate_Individual(rand, individual);
            }
        }
    }
}
