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
    public class MM__Repair__L3__Openings : Mutation_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new MM__Repair__L3__Openings();
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Architectural_Plan individual)
        {
            // step 1: delete all the improper openings
            Remove_Improper_Openings(rand, individual);

            Add_Missing_Interior_doors(rand, individual);
            Add_Missing_Exterior_doors(rand, individual);
            Add_Missing_Windows(rand, individual);

            individual.M__Recalculate_Phenotype(starting_level: 3);
        }

        private void Remove_Improper_Openings(I_PRNG rand, DS__Architectural_Plan individual)
        {
            var improper_interior_doors = individual.Q__Improper_Connection_Doors();
            foreach (var interior_door in improper_interior_doors)
            {
                individual.connection_doors.Remove(interior_door);
            }

            var improper_exterior_doors = individual.Q__Improper_Entrance_Doors();
            foreach (var exterior_door in improper_exterior_doors)
            {
                individual.entrance_doors.Remove(exterior_door);
            }

            var improper_windows = individual.Q__Improper_Windows();
            foreach (var window in improper_windows)
            {
                individual.windows.Remove(window);
            }
        }

        private void Add_Missing_Interior_doors(I_PRNG rand, DS__Architectural_Plan individual)
        {
            var existing_interior_doors = individual.Q__Connection_Doors();
            var prescribed_room_connections = individual.Q__Prescribed_Connections();

            var existing_interior_doors__room_connections = new List<Undirected_Edge>();

            foreach (EG__Connection_Door door in existing_interior_doors)
            {
                existing_interior_doors__room_connections.Add(door.Q__Space_Units_Connection());
            }

            var missing_interior_doors__room_connections =
                prescribed_room_connections.FindAll(
                    x =>
                    existing_interior_doors__room_connections.Contains(x) == false
                );

            List<EG__Connection_Door> doors_to_add = new List<EG__Connection_Door>();

            foreach (var con in missing_interior_doors__room_connections)
            {
                // attempt to place door for this connection
                int room_1 = con.v1;
                int room_2 = con.v2;

                var possible_cell_edges =
                    individual.Q__Legal_Cell_Edges__Of__Space_Units_Pair(new Undirected_Edge(room_1, room_2));

                if (possible_cell_edges.Count < 1)
                    continue;

                List<Undirected_Edge> proper_edges = new List<Undirected_Edge>();
                foreach (var edge in possible_cell_edges)
                {
                    individual
                        .voronoi_tessellation
                        .Q__Shared_Line__Between_Neighbor_Cells(
                            edge.v1,
                            edge.v2,
                            out bool success,
                            out Line_Segment line_segment
                        );

                    if (success == false)
                    {
                        continue;
                    }

                    double line_length = line_segment.Q__Magnitude();
                    if (line_length < individual.prescription.openings__minimum_wall_length)
                    {
                        continue;
                    }

                    proper_edges.Add(edge);

                }

                if (proper_edges.Count < 1)
                    continue;

                // select one at random
                var selected_edge = proper_edges.Q__Random_Item(rand);

                EG__Connection_Door interior_door = new EG__Connection_Door(
                    selected_edge.v1,
                    selected_edge.v2,
                    room_1,
                    room_2
                    );

                doors_to_add.Add(interior_door);
            }

            foreach (var door in doors_to_add)
            {
                individual.M__Add__Connection_Door(door, false);
            }
        }

        private void Add_Missing_Exterior_doors(I_PRNG rand, DS__Architectural_Plan individual)
        {
            var existing_exterior_doors = individual.Q__Entrance_Doors();

            Dictionary<int, int> prescribed_num_doors__per__room =
                individual.prescription.num_entrance_doors__per__space_unit.Q__Deep_Copy();

            Dictionary<int, int> missing_num_doors__per__room = new Dictionary<int, int>();

            List<int> rooms = individual.Q__Prescribed_Space_Units();

            foreach (int room in rooms)
            {
                int existing_num_doors = existing_exterior_doors.Count(
                    x =>
                    x.space_unit == room
                    );

                int prescribed_num_doors = prescribed_num_doors__per__room[room];

                int num_missing = prescribed_num_doors - existing_num_doors;

                missing_num_doors__per__room.Add(room, num_missing);
            }

            List<EG__Entrance_Door> doors_to_add = new List<EG__Entrance_Door>();

            foreach (int room in rooms)
            {
                int num_doors_to_add = missing_num_doors__per__room[room];
                if (num_doors_to_add < 1)
                    continue;

                var possible_cell_edges =
                    individual.Q__Legal_Cell_Edges__Of__Space_Units_Pair(new Undirected_Edge(room, -1));

                if (possible_cell_edges.Count < 1)
                    continue;

                List<Undirected_Edge> proper_edges = new List<Undirected_Edge>();
                foreach (var edge in possible_cell_edges)
                {
                    individual
                        .voronoi_tessellation
                        .Q__Shared_Line__Between_Neighbor_Cells(
                            edge.v1,
                            edge.v2,
                            out bool success,
                            out Line_Segment line_segment
                        );

                    if (success == false)
                    {
                        continue;
                    }

                    double line_length = line_segment.Q__Magnitude();
                    if (line_length < individual.prescription.openings__minimum_wall_length)
                    {
                        continue;
                    }

                    proper_edges.Add(edge);
                }

                List<Undirected_Edge> selected_edges;

                if (proper_edges.Count == 0)
                    continue;
                else if (proper_edges.Count < num_doors_to_add)
                    selected_edges =
                        proper_edges.Q__Random_Items(rand, proper_edges.Count);
                else
                    selected_edges = proper_edges.Q__Random_Items(rand, num_doors_to_add);

                foreach (var edge in selected_edges)
                {
                    EG__Entrance_Door door = new EG__Entrance_Door(
                        room,
                        edge
                        );

                    doors_to_add.Add(door);
                }
            }

            foreach (var door in doors_to_add)
            {
                individual.M__Add__Entrance_Door(door, false);
            }

        }

        private void Add_Missing_Windows(I_PRNG rand, DS__Architectural_Plan individual)
        {
            var existing_windows = individual.Q__Windows();

            Dictionary<int, int> prescribed_num_windows__per__room =
                individual.prescription.num_windows__per__space_unit.Q__Deep_Copy();

            Dictionary<int, int> missing_num_windows__per__room = new Dictionary<int, int>();

            List<int> prescribed_space_units = individual.Q__Prescribed_Space_Units();

            foreach (int room in prescribed_space_units)
            {
                int existing_num_windows = existing_windows.Count(
                    x =>
                    x.space_unit == room
                    );

                int prescribed_num_windows = prescribed_num_windows__per__room[room];

                int num_missing = prescribed_num_windows - existing_num_windows;

                missing_num_windows__per__room.Add(room, num_missing);
            }

            List<EG__Window> windows_to_add = new List<EG__Window>();

            foreach (int room in prescribed_space_units)
            {
                int num_windows_to_add = missing_num_windows__per__room[room];
                if (num_windows_to_add < 1)
                    continue;

                List<Undirected_Edge> possible_cell_edges = new List<Undirected_Edge>();
                possible_cell_edges.AddRange(
                    individual.Q__Legal_Cell_Edges__Of__Space_Units_Pair(new Undirected_Edge(room, -1))
                    );

                // add connections to exterior rooms
                foreach (var space_unit in prescribed_space_units)
                {
                    bool room_is_exterior =
                        individual.prescription.type__per__space_unit[space_unit] == Space_Unit__Type.EXTERIOR;

                    if (room_is_exterior)
                    {
                        possible_cell_edges.AddRange(
                            individual.Q__Legal_Cell_Edges__Of__Space_Units_Pair(new Undirected_Edge(room, space_unit))
                        );
                    }
                }

                if (possible_cell_edges.Count < 1)
                    continue;

                // remove the ones that are occupied
                List<Undirected_Edge> non_occupied__cell_edges = new List<Undirected_Edge>();
                foreach (var edge in possible_cell_edges)
                {
                    int num_openings_on_edge = individual.Q__Num__Openings__On_Edge(edge);
                    if (num_openings_on_edge == 0)
                    {
                        non_occupied__cell_edges.Add(edge);
                    }
                }

                if (non_occupied__cell_edges.Count < 1)
                    continue;

                // remove the ones that are small
                List<Undirected_Edge> proper_length__non_occupied__cell_edges = new List<Undirected_Edge>();
                foreach (var edge in possible_cell_edges)
                {
                    individual
                    .voronoi_tessellation
                    .Q__Shared_Line__Between_Neighbor_Cells(
                        edge.v1,
                        edge.v2,
                        out bool success,
                        out Line_Segment line
                        );

                    if (success == false) continue;

                    double line_length = line.Q__Magnitude();
                    if (line_length >= individual.prescription.openings__minimum_wall_length)
                    {
                        proper_length__non_occupied__cell_edges.Add(edge);
                    }
                }

                if (proper_length__non_occupied__cell_edges.Count < 1)
                    continue;

                List<Undirected_Edge> selected_edges =
                    proper_length__non_occupied__cell_edges.Q__Random_Items(rand, num_windows_to_add);

                foreach (var edge in selected_edges)
                {
                    EG__Window window = new EG__Window(
                        room,
                        edge
                        );

                    windows_to_add.Add(window);
                }
            }

            foreach (var window in windows_to_add)
            {
                individual.M__Add__Window(window, false);
            }

        }



    }
}
