using Common_Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Repair__L2__Fix_Connections<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new MM__Repair__L2__Fix_Connections<T>();
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<Undirected_Edge> missing_prescribed_connections =
                individual.Q__Missing_Prescribed_Connections();
            missing_prescribed_connections.M__Shuffle(rand);

            foreach (var connection in missing_prescribed_connections)
            {
                int room_0 = connection.v1;
                int room_1 = connection.v2;

                int dice_roll = rand.Next(0, 2);
                if (dice_roll == 0)
                {
                    room_0 = connection.v2;
                    room_1 = connection.v1;
                }

                if (
                    individual.Q__Space_Unit_Exists(room_0) == false
                    ||
                    individual.Q__Space_Unit_Exists(room_1) == false
                    )
                {
                    continue;
                }
                else
                {
                    List<int> room_0_cells = individual.Q__Space_Unit__Cells(room_0);
                    List<int> room_1_cells = individual.Q__Space_Unit__Cells(room_1);

                    // find a path from the cells of room 0 to the cells of room 1
                    List<int> free_cells = individual.Q__Free_Active_Cells__List();

                    List<int> pathway_cells = free_cells.Q__Deep_Copy();
                    pathway_cells.AddRange(room_0_cells);
                    pathway_cells.AddRange(room_1_cells);

                    DS__Undirected_Graph pathway_graph =
                        individual
                        .voronoi_tessellation
                        .connectivity_graph
                        .Q__Sub_Graph__Containing_Vertices(
                            pathway_cells
                        );

                    // find the shortest path from any cell of room 0 to any cell of room 1,
                    // moving through the pathway graph...
                    List<List<int>> all_possible_connection_paths = new List<List<int>>();
                    foreach (var cell in room_0_cells)
                    {
                        List<int> path = pathway_graph.Q__Shortest_Path_BFS(
                            cell,
                            room_1_cells
                            );
                        if (path.Count > 0)
                        {
                            all_possible_connection_paths.Add(path);
                        }
                    }

                    if (all_possible_connection_paths.Count == 0)
                    {
                        continue;
                    }

                    int shortest_path_length = int.MaxValue;
                    foreach (var path in all_possible_connection_paths)
                    {
                        int pl = path.Count;
                        if (pl < shortest_path_length)
                        {
                            shortest_path_length = pl;
                        }
                    }

                    List<List<int>> shortest_paths = new List<List<int>>();
                    foreach (var path in all_possible_connection_paths)
                    {
                        if (path.Count == shortest_path_length)
                        {
                            shortest_paths.Add(path);
                        }
                    }

                    List<int> selected_path = shortest_paths.Q__Random_Item(rand);

                    int path_length = selected_path.Count;

                    int division_point = rand.Next(0, path_length - 2);

                    for (int i = 0; i < path_length; i++)
                    {
                        int cell = selected_path[i];
                        if (i < division_point)
                        {
                            individual.M__Assign_Cell_To_Space_Unit(
                                cell,
                                room_0,
                                false
                                );
                        }
                        else
                        {
                            individual.M__Assign_Cell_To_Space_Unit(
                                cell,
                                room_1,
                                false
                                );
                        }
                    }
                }
            }

            individual.M__Recalculate_Phenotype(starting_level: 2);

        }
    }
}
