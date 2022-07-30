using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L2__Delete_Space_Unit<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L2__Delete_Space_Unit(
            bool recalculate_phenotype
            )
        {
            this.recalculate_phenotype = recalculate_phenotype;
        }

        public MM__Destruction__L2__Delete_Space_Unit(
            MM__Destruction__L2__Delete_Space_Unit<T> mm_to_copy
            )
        {
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L2__Delete_Space_Unit<T>(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<int> existing_space_units = individual.Q__Existing_Space_Units();

            if (existing_space_units.Count == 0) return;

            int selected_space_unit = existing_space_units.Q__Random_Item(rand);

            individual.M__Delete_Space_Unit(selected_space_unit, true);

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 2);
            }
        }
    }
}
