using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L2__Erode_Space_Unit__Safe<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L2__Erode_Space_Unit__Safe(
            bool recalculate_phenotype
            )
        {
            this.recalculate_phenotype = recalculate_phenotype;
        }

        private MM__Destruction__L2__Erode_Space_Unit__Safe(
            MM__Destruction__L2__Erode_Space_Unit__Safe<T> mm_to_copy
            )
        {
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L2__Erode_Space_Unit__Safe<T>(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<int> existing_space_units = individual.Q__Existing_Space_Units();

            if (existing_space_units.Count == 0) return;

            int selected_room = existing_space_units.Q__Random_Item(rand);

            // perform stripping down of that room
            List<int> safely_deletable_cells_of_room = individual.Q__Safely_Destroyable_Cells(
                selected_room
                );

            while (safely_deletable_cells_of_room.Count > 0)
            {
                // select a cell, at random
                int selected_cell = safely_deletable_cells_of_room.Q__Random_Item(rand);

                // clear that cell
                individual.M__Clear_Cell(selected_cell, false);

                // refresh the list of safely deletable cells
                safely_deletable_cells_of_room = individual.Q__Safely_Destroyable_Cells(
                    selected_room
                    );
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 2);
            }
        }
    }
}
