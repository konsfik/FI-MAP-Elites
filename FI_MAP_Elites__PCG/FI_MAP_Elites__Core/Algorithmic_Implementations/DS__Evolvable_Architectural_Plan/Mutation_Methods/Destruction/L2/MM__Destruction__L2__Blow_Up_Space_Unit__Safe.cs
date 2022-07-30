using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L2__Blow_Up_Space_Unit__Safe<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L2__Blow_Up_Space_Unit__Safe(
            bool recalculate_phenotype
            )
        {
            this.recalculate_phenotype = recalculate_phenotype;
        }

        public MM__Destruction__L2__Blow_Up_Space_Unit__Safe(
            MM__Destruction__L2__Blow_Up_Space_Unit__Safe<T> mm_to_copy
            )
        {
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L2__Blow_Up_Space_Unit__Safe<T>(this);
        }

        public override void Mutate_Individual(I_PRNG rand, T individual)
        {
            List<int> existing_space_units = individual.Q__Existing_Space_Units();

            if (existing_space_units.Count == 0) return;

            // select an existing space unit, at random.
            int selected_space_unit = existing_space_units.Q__Random_Item(rand);

            // find the free surrounding cells
            List<int> free_surrounding_cells =
                individual.Q__Space_Unit__Surrounding_Cells__Free(
                    selected_space_unit
                    );

            // assign all the free surrounding cells to that space unit
            individual.M__Assign_Cells_To_Space_Unit(
                free_surrounding_cells,
                selected_space_unit,
                true
                );

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 2);
            }
        }
    }
}
