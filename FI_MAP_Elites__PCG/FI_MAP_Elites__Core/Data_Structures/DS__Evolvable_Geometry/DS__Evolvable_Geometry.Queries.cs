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
    public partial class DS__Evolvable_Geometry : Data_Structure
    {
        public List<int> Q__Highest_Degree_Space_Units()
        {
            return
                prescription
                .graph
                .Q__Highest_Degree_Vertices();
        }

        public List<int> Q__Highest_Degree_Space_Units(
            List<int> candidate_rooms
            )
        {
            return
                prescription
                .graph
                .Q__Highest_Degree_Vertices(candidate_rooms);
        }

        public bool Q__Space_Units_Connection_Exists(Undirected_Edge connection)
        {
            int unit_1 = connection.v1;
            List<int> unit_1_cells = Q__Space_Unit__Cells(unit_1);

            int unit_2 = connection.v2;
            List<int> unit_2_cells = Q__Space_Unit__Cells(unit_2);

            foreach (int unit_1_cell in unit_1_cells)
            {
                List<int> cell_neighbors = Q__Cell__Neighbors(unit_1_cell);
                foreach (int cell_neighbor in cell_neighbors)
                {
                    if (unit_2_cells.Contains(cell_neighbor))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<Undirected_Edge> Q__Prescribed_Connections()
        {
            return prescription.Q__Prescribed__Connections();
        }

        /// <summary>
        /// Safely destroyable cells are the ones that can be removed from an existing space unit
        /// without breaking this space unit's coherence AND without breaking this space unit's 
        /// connectivity with other existing space units.
        /// </summary>
        /// <param name="space_unit"></param>
        /// <returns></returns>
        public List<int> Q__Safely_Destroyable_Cells(int space_unit)
        {
            List<int> space_unit_cells
                = Q__Space_Unit__Cells(space_unit);

            List<int> space_unit_adjacent_occupied_cells
                = Q__Space_Unit__Surrounding_Cells__Occupied(space_unit);

            List<int> cells_that_must_remain = new List<int>();

            // find the existing, prescribed neighbors
            List<int> space_unit_existing_prescribed_neighbors =
                Q__Space_Unit__Existing_Prescribed_Neighbors(space_unit);

            // for each of them, detect the points of contact...
            foreach (int neighbor in space_unit_existing_prescribed_neighbors)
            {
                // find all the cells of the neighbor
                List<int> neighbor_adjacent_cells = Q__Space_Unit__Cells(neighbor);

                // remove all the cells that are not adjacent to the current room
                // this way we have a list of cells that belong to the neighbor 
                // but are also adjacent to the current room.
                neighbor_adjacent_cells.RemoveAll(
                    x =>
                    space_unit_adjacent_occupied_cells
                    .Contains(x) == false
                    );

                // find the contact cells between the current room and the neighbor
                List<int> contact_cells = new List<int>();

                foreach (int cell in space_unit_cells)
                {
                    List<int> room_cell_nei = Q__Cell__Neighbors(cell);

                    bool is_point_of_contact =
                        room_cell_nei
                        .Any(x => neighbor_adjacent_cells.Contains(x)
                            );

                    if (is_point_of_contact)
                        contact_cells.Add(cell);
                }

                if (contact_cells.Count == 1)
                    cells_that_must_remain.Add(contact_cells[0]);
            }


            DS__Undirected_Graph space_unit__sub_graph = Q__Space_Unit__Sub_Graph(space_unit);
            List<int> removable_vertices = space_unit__sub_graph.Q__Removable_Vertices();

            removable_vertices.RemoveAll(
                x => cells_that_must_remain.Contains(x)
                );

            return removable_vertices;
        }

        public List<int> Q__Space_Unit__Existing_Prescribed_Neighbors(int room_id)
        {
            List<int> room_adjacent_not_free_cells = Q__Space_Unit__Surrounding_Cells__Occupied(room_id);

            List<int> existing_rooms = Q__Existing_Space_Units();

            List<int> prescribed_neighbors = Q__Space_Unit__Prescribed_Neighbors(room_id);

            List<int> existing_prescribed_neighbors = prescribed_neighbors.FindAll(
                x => existing_rooms.Contains(x)
                );

            return existing_prescribed_neighbors;
        }

        public bool Q__All_Space_Units_Exist()
        {
            List<int> prescribed_rooms_ids = Q__Prescribed_Space_Units();

            foreach (int room in prescribed_rooms_ids)
            {
                if (Q__Space_Unit_Exists(room) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public List<int> Q__Prescribed_Space_Units()
        {
            return prescription.Q__Prescribed__Space_Units();
        }

        public int Q__Num_Prescribed_Space_Units()
        {
            return prescription.name__per__space_unit.Count;
        }

        public List<int> Q__Existing_Space_Units()
        {
            List<int> prescribed_rooms = Q__Prescribed_Space_Units();
            List<int> existing_rooms = new List<int>();

            foreach (int room in prescribed_rooms)
            {
                if (Q__Space_Unit_Exists(room))
                {
                    existing_rooms.Add(room);
                }
            }

            return existing_rooms;
        }

        public List<int> Q__Missing_Prescribed_Space_Units()
        {
            List<int> prescribed_rooms = Q__Prescribed_Space_Units();
            List<int> existing_rooms = Q__Existing_Space_Units();
            List<int> missing_rooms = prescribed_rooms.FindAll(
                x =>
                existing_rooms.Contains(x) == false
                );
            return missing_rooms;
        }

        public List<Undirected_Edge> Q__Missing_Prescribed_Connections()
        {
            List<Undirected_Edge> prescribed_connections = Q__Prescribed_Connections();

            List<Undirected_Edge> missing_prescribed_connections = new List<Undirected_Edge>();
            foreach (var pc in prescribed_connections)
            {
                if (Q__Space_Units_Connection_Exists(pc) == false)
                {
                    missing_prescribed_connections.Add(pc);
                }
            }
            return missing_prescribed_connections;
        }



        /// <summary>
        /// Returns the prescribed neighbors of a set of rooms, 
        /// excluding those rooms.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_ids"></param>
        /// <returns></returns>
        public List<int> Q_Space_Units__Prescribed_Neighbors(List<int> room_ids)
        {
            return prescription.graph.Q__Vertices_Neighbors_Exclusive(room_ids);
        }

        public List<int> Q__Active_Cells()
        {
            return voronoi_tessellation.Q__Active_Cells();
        }

        public List<int> Q__All_Cells()
        {
            return voronoi_tessellation.Q__All_Cells();
        }

        public List<int> Q__Inactive_Cells()
        {
            return voronoi_tessellation.Q__Inactive_Cells();
        }

        public List<int> Q__Free_Active_Cells__List()
        {
            List<int> active_cells = Q__Active_Cells();

            List<int> free_cells = new List<int>(capacity: active_cells.Count);
            foreach (int cell_id in active_cells)
            {
                if (Q__Is_Cell__Free(cell_id))
                {
                    free_cells.Add(cell_id);
                }
            }

            return free_cells;
        }

        public DS__Undirected_Graph Q__Free_Active_Cells__Sub_Graph()
        {
            List<int> free_legal_cells = Q__Free_Active_Cells__List();
            DS__Undirected_Graph sub_graph =
                voronoi_tessellation
                .connectivity_graph
                .Q__Sub_Graph__Containing_Vertices(free_legal_cells);
            return sub_graph;
        }


        public List<Line_Segment> Q__Common_Lines__Between_Space_Units(
            int space_unit_1,
            int space_unit_2
            )
        {
            List<Line_Segment> common_room_lines = new List<Line_Segment>();

            var room_1__perimeter_lines = Q__Space_Unit__Perimeter_Lines__Unordered(space_unit_1);
            var room_2__perimeter_lines = Q__Space_Unit__Perimeter_Lines__Unordered(space_unit_2);

            foreach (var line in room_1__perimeter_lines)
            {
                if (
                    room_2__perimeter_lines.Contains(line) == true
                    &&
                    common_room_lines.Contains(line) == false
                    )
                {
                    common_room_lines.Add(line);
                }
            }

            return common_room_lines;
        }

    }
}
