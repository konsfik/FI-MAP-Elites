using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class MM__Sequence<T> : Mutation_Method<T>
        where T : I_Deep_Copyable
    {
        public readonly List<Mutation_Method<T>> mutation_methods_list;

        public MM__Sequence(List<Mutation_Method<T>> mutation_methods_list)
        {
            this.mutation_methods_list = mutation_methods_list.Q__Deep_Copy();
        }

        private MM__Sequence(MM__Sequence<T> mm_to_copy)
        {
            this.mutation_methods_list = mm_to_copy.mutation_methods_list.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Sequence<T>(this);
        }

        public override void Mutate_Individual(I_PRNG rand, T individual)
        {
            foreach (var method in mutation_methods_list)
                method.Mutate_Individual(rand, individual);
        }

        
    }
}
