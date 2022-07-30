using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    /// <summary>
    /// Room expansion method that keeps adding adjacent cells to an existing room,
    /// until that room reaches the desired area.
    /// </summary>
    public class REM__Free_Adjacent_Cells__Random<T> : Space_Unit_Expansion_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public override bool Expand_Space_Unit(
            I_PRNG rand,
            T individual,
            int room_id, 
            bool recalculate_phenotype
            )
        {
            if (individual.Q__Space_Unit_Exists(room_id) == false)
            {
                throw new System.Exception("Room does not exist!");
            }

            while (
                    individual.Q__Space_Unit__Area(room_id)
                    <
                    individual.Q__Space_Unit__Prescribed_Area(room_id)
                    )
            {
                // utilize adjacent cells, until the room has reached the assigned area...
                List<int> free_adjacent_cells =
                    individual
                    .Q__Space_Unit__Surrounding_Cells__Free(room_id);

                if (free_adjacent_cells.Count > 0)
                {
                    // select one of the expansion cells, at random
                    int expansion_cell = free_adjacent_cells.Q__Random_Item(rand);

                    // add it to the room's cells
                    individual.M__Assign_Cell_To_Space_Unit(expansion_cell, room_id, false);
                }
                else
                {
                    if (recalculate_phenotype)
                    {
                        individual.M__Recalculate_Phenotype(starting_level: 2);
                    }
                    // if there are no remaining expansion cells, 
                    // the process fails / stops (return false)
                    return false;
                }
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 2);
            }

            return true;
        }
    }
}
