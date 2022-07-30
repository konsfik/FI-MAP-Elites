using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class RPM__Single_Cell_Near_Neighbors<T> : Single_Space_Unit_Placement_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new RPM__Single_Cell_Near_Neighbors<T>();
        }

        public override bool Place_Space_Unit(
            I_PRNG rand,
            T individual,
            int current_room,
            bool recalculate_phenotype
            )
        {
            List<int> existing_rooms = individual.Q__Existing_Space_Units();
            if (existing_rooms.Count == 0)
            {
                // placing the first room
                bool success = O__Place_Space_Unit_At_Random_Location(
                    individual,
                    rand,
                    current_room,
                    recalculate_phenotype
                    );

                return success;
            }
            else
            {
                // find the existing prescribed neighbors of the current room.
                List<int> existing_prescribed_neighbors =
                    individual
                    .Q__Space_Unit__Existing_Prescribed_Neighbors(current_room);

                if (existing_prescribed_neighbors.Count == 0)
                {
                    // If there are no existing prescribed neighbors, 
                    // place the room somewhere, at random.
                    bool success = O__Place_Space_Unit_At_Random_Location(
                        individual,
                        rand,
                        current_room,
                        recalculate_phenotype
                        );

                    return success;
                }
                else if (existing_prescribed_neighbors.Count == 1)
                {
                    // just start from a neighbor cell of that neighbor
                    int existing_prescribed_neighbor =
                        existing_prescribed_neighbors[0];

                    // attempt to place the current room adjacent to its prescribed neighbor
                    bool success = Place_Room_Next_To_Existing_Room(
                        rand,
                        individual,
                        current_room,
                        existing_prescribed_neighbor,
                        recalculate_phenotype
                        );

                    if (success == true)
                    {
                        // if the room was successfully placed next to its prescribed neighbor, 
                        // return true and terminate the process.
                        return true;
                    }
                    else
                    {
                        // if it was not possible to place the current room
                        // next to its existing prescribed neighbor,
                        // place it somewhere at random
                        success = O__Place_Space_Unit_At_Random_Location(
                            individual,
                            rand,
                            current_room,
                            recalculate_phenotype
                            );

                        return success;
                    }

                }
                else if (existing_prescribed_neighbors.Count > 1)
                {
                    // if there are more than one existing prescribed neighbors, 
                    // then the initial placement of the individual
                    // must be such that the selected cells touch on all of them.

                    // attempt to find single cells
                    // that is adjacent to all of the existing prescribed neighbors
                    List<int> common =
                        individual
                        .Q__Space_Units__Surrounding_Free_Cells__Common(
                            existing_prescribed_neighbors
                            );

                    if (common.Count > 0)
                    {
                        // if there is at least one cell that is a common neighbor to
                        // all the necessary adjacent rooms, 
                        // then select at random and move on.
                        int initial_cell = common.Q__Random_Item(rand);

                        individual.M__Assign_Cell_To_Space_Unit(
                            initial_cell, 
                            current_room,
                            recalculate_phenotype
                            );

                        return true;
                    }
                    else
                    {
                        // otherwise, find the smallest possible continuous area
                        // that connects as many of the neighbors as possible.

                        List<int> any =
                            individual
                            .Q__Space_Units__Surrounding_Cells__Free(
                                existing_prescribed_neighbors
                                );

                        if (any.Count > 0)
                        {
                            int initial_cell = any.Q__Random_Item(rand);
                            individual.M__Assign_Cell_To_Space_Unit(
                                initial_cell, 
                                current_room,
                                recalculate_phenotype
                                );

                            return true;
                        }
                        else
                        {
                            bool success = O__Place_Space_Unit_At_Random_Location(
                                individual,
                                rand,
                                current_room,
                                recalculate_phenotype
                                );

                            return success;
                        }
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        private bool Place_Room_Next_To_Existing_Room(
            I_PRNG rand,
            T individual,
            int current_room,
            int existing_room,
            bool recalculate_phenotype
            )
        {
            List<int> existing_room__surrounding_free_cells =
                individual
                .Q__Space_Unit__Surrounding_Cells__Free(existing_room);

            if (existing_room__surrounding_free_cells.Count > 0)
            {
                int placement_cell = existing_room__surrounding_free_cells.Q__Random_Item(rand);

                individual.M__Assign_Cell_To_Space_Unit(
                    placement_cell, 
                    current_room,
                    recalculate_phenotype
                    );

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool O__Place_Space_Unit_At_Random_Location(
            T individual,
            I_PRNG rand,
            int current_room,
            bool recalculate_phenotype
            )
        {
            // find the list of free, legal cells...
            List<int> free_legal_cells = individual.Q__Free_Active_Cells__List();

            if (free_legal_cells.Count == 0)
                return false;
            else
            {
                // select a random cell
                int initial_cell =
                    free_legal_cells
                    .Q__Random_Item(rand);

                // assign the random cell to the selected room
                individual.M__Assign_Cell_To_Space_Unit(
                    initial_cell, 
                    current_room,
                    recalculate_phenotype
                    );

                // notify that all is fine!
                return true;
            }
        }
    }
}
