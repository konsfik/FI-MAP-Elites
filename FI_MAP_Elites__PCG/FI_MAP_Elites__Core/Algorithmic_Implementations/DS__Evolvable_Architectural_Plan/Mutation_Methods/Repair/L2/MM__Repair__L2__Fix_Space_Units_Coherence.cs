using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    /// <summary>
    /// Repairs the coherence of all space units.
    /// Operation:
    /// Iterates over all the space units.
    /// Checkes whether a space unit is coherent.
    /// If a space unit is not coherent, it keeps one of its parts, at random, and deletes the rest.
    /// </summary>
    public class MM__Repair__L2__Fix_Space_Units_Coherence<T> : Mutation_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new MM__Repair__L2__Fix_Space_Units_Coherence<T>();
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<int> existing_rooms = individual.Q__Existing_Space_Units();

            foreach (var room_id in existing_rooms)
            {
                DS__Undirected_Graph room_sub_graph = individual.Q__Space_Unit__Sub_Graph(room_id);

                if (room_sub_graph.Q__Is_Connected()) continue;

                List<DS__Undirected_Graph> room_graph_islands = room_sub_graph.Q__Graph_Islands();

                if (room_graph_islands.Count > 1)
                {
                    var selected_island = room_graph_islands.Q__Random_Item(rand);

                    var room_cells = individual.Q__Space_Unit__Cells(room_id);

                    foreach (int c in room_cells)
                    {
                        if (selected_island.Q__Contains_Vertex(c)) continue;
                        individual.M__Clear_Cell(c, false);
                    }
                }
            }

            individual.M__Recalculate_Phenotype(starting_level: 2);
        }
    }
}
