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
        public double Q__Space_Unit_Compactness(int space_unit)
        {
            double area = Q__Space_Unit__Area(space_unit);
            double perimeter = Q__Space_Unit__Perimeter(space_unit);
            double compactness =
                4.0 * Math.PI * area / (perimeter * perimeter);
            return compactness;
        }

        public List<Undirected_Edge> Q__Legal_Cell_Edges__Of__Space_Units_Pair(
            Undirected_Edge room_pair
            )
        {
            List<Undirected_Edge> legal_cell_edges_of_room_pair = new List<Undirected_Edge>();

            var legal_cell_connections =
                voronoi_tessellation
                .connectivity_graph
                .Q__Edges();

            foreach (var lcc in legal_cell_connections)
            {
                int room_1 = space_unit__per__cell[lcc.v1];
                int room_2 = space_unit__per__cell[lcc.v2];

                Undirected_Edge this_room_pair = new Undirected_Edge(room_1, room_2);

                if (this_room_pair == room_pair)
                {
                    legal_cell_edges_of_room_pair.Add(lcc);
                }
            }

            return legal_cell_edges_of_room_pair;
        }

        public bool Q__Space_Unit_Exists(int room_id)
        {
            foreach (var kvp in space_unit__per__cell)
            {
                if (kvp.Value == room_id)
                    return true;
            }

            return false;
        }

        public bool Q__Space_Unit_Is_Coherent(int room)
        {
            return Q__Space_Unit__Sub_Graph(room).Q__Is_Connected();
        }

        public double Q__Space_Unit__Area(int space_unit)
        {
            double room_area = 0;
            foreach (var kvp in space_unit__per__cell)
            {
                int cell = kvp.Key;
                int s_unit = kvp.Value;
                if (s_unit == space_unit)
                {
                    double cell_area =
                        voronoi_tessellation
                        .area__per__cell[cell];
                    room_area += cell_area;
                }
            }
            return room_area;
        }

        public double Q__Space_Unit__Prescribed_Area(int room_id)
        {
            return prescription.Q__Prescribed__Space_Unit_Area(room_id);
        }

        /// <summary>
        /// Returns the perimeter length of a room.
        /// If the room contains holes, the perimeter of these holes will be included.
        /// If the room is split in many parts, the perimeter will be the sum of all of them.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public double Q__Space_Unit__Perimeter(int room_id)
        {
            List<Line_Segment> room_perimeter_lines = Q__Space_Unit__Perimeter_Lines__Unordered(room_id);

            double perimeter = 0.0;

            foreach (var perimeter_line in room_perimeter_lines)
                perimeter += perimeter_line.Q__Magnitude();

            return perimeter;
        }

        public List<List<Line_Segment>> Q__Space_Unit__Perimeter_Lines__Groupped__Ordered(
            int space_unit
            )
        {
            List<Line_Segment> available_lines =
                Q__Space_Unit__Perimeter_Lines__Unordered(space_unit);

            List<List<Line_Segment>> groupped_ordered =
                new List<List<Line_Segment>>();

            while (available_lines.Count > 0)
            {

                List<Line_Segment> current_sequence = new List<Line_Segment>();
                // select the first line sement
                Line_Segment ls1 = available_lines[0];
                // add it to the current sequence
                current_sequence.Add(ls1);
                // remove it from the available lines
                available_lines.Remove(ls1);

                bool sequence_over = false;
                int room_lines_count = available_lines.Count;
                while (sequence_over == false && room_lines_count > 0)
                {
                    room_lines_count = available_lines.Count;
                    for (int i = 0; i < room_lines_count; i++)
                    {
                        Line_Segment ls2 = available_lines[i];
                        if (ls1.Q__Is_Connected_To(ls2))
                        {
                            // found pair!
                            current_sequence.Add(ls2);
                            available_lines.Remove(ls2);
                            ls1 = ls2;
                            break;
                        }
                        else
                        {
                            // if we have reached the end of the loop and not found a pair,
                            // then the sequence is over.
                            if (i == available_lines.Count - 1)
                            {
                                sequence_over = true;
                            }
                        }
                    }

                }
                groupped_ordered.Add(current_sequence);
            }

            return groupped_ordered;
        }

        public List<Line_Segment> Q__Space_Unit__Perimeter_Lines__Unordered(
            int room_id
            )
        {
            List<int> room_cells = Q__Space_Unit__Cells(room_id);

            List<Line_Segment> all_line_segments = new List<Line_Segment>();
            foreach (var cell in room_cells)
            {
                List<Line_Segment> cell_perimeter_lines = Q__Cell__Perimeter_Lines(cell);

                all_line_segments.AddRange(cell_perimeter_lines);
            }

            List<Line_Segment> line_segments_to_remove = new List<Line_Segment>();
            int num_all_segments = all_line_segments.Count;
            for (int i = 0; i < num_all_segments - 1; i++)
            {
                for (int j = i + 1; j < num_all_segments; j++)
                {
                    Line_Segment line_segment_1 = all_line_segments[i];
                    Line_Segment line_segment_2 = all_line_segments[j];
                    if (line_segment_1 == line_segment_2)
                    {
                        if (line_segments_to_remove.Contains(line_segment_1) == false)
                        {
                            line_segments_to_remove.Add(line_segment_1);
                        }
                    }
                }
            }

            List<Line_Segment> remaining_line_segments = all_line_segments.Q__Deep_Copy();
            remaining_line_segments.RemoveAll(
                x =>
                line_segments_to_remove.Contains(x)
                );

            return remaining_line_segments;
        }

        public List<int> Q__Space_Unit__Prescribed_Neighbors(int room_id)
        {
            return prescription.graph.Q__Neighbors(room_id);
        }



        /// <summary>
        /// Returns the list of rooms that are actually neighbors of this room.
        /// I.e. the rooms that have adjaccent cells to this room.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="space_unit_id"></param>
        /// <returns></returns>
        public List<int> Q__Space_Unit__Neighbors(int space_unit_id)
        {
            List<int> room_adjacent_not_free_cells =
                Q__Space_Unit__Surrounding_Cells__Occupied(space_unit_id);

            List<int> existing_neighbors = new List<int>();

            foreach (int cell in room_adjacent_not_free_cells)
            {
                int other_room = space_unit__per__cell[cell];

                if (
                    existing_neighbors.Contains(other_room) == false
                    &&
                    other_room != -1
                    )
                {
                    existing_neighbors.Add(other_room);
                }
            }

            return existing_neighbors;
        }


        /// <summary>
        /// Returns a graph that represents the room's own conectivity,
        /// i.e. the way that the room's cells are connected to each other.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public DS__Undirected_Graph Q__Space_Unit__Sub_Graph(int room_id)
        {
            List<int> room_cells = Q__Space_Unit__Cells(room_id);

            return
                voronoi_tessellation
                .connectivity_graph
                .Q__Sub_Graph__Containing_Vertices(room_cells);
        }

        /// <summary>
        /// Returns the list of cells that are occupied by this room.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public List<int> Q__Space_Unit__Cells(int room_id)
        {
            List<int> room_cells = new List<int>();
            foreach (var kvp in space_unit__per__cell)
            {
                int k_cell_id = kvp.Key;
                int v_room_id = kvp.Value;
                if (v_room_id == room_id)
                {
                    room_cells.Add(k_cell_id);
                }
            }
            return room_cells;
        }

        /// <summary>
        /// Returns the list of cells that surround the cells of this room.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="space_unit"></param>
        /// <returns></returns>
        public List<int> Q__Space_Unit__Surrounding_Cells(
            int space_unit
            )
        {
            List<int> space_unit_cells = Q__Space_Unit__Cells(space_unit);

            List<int> space_unit_adjacent_cells = new List<int>();

            foreach (int c in space_unit_cells)
            {
                List<int> nei =
                    Q__Cell__Neighbors(c).FindAll(
                        x =>
                        space_unit_adjacent_cells.Contains(x) == false
                        &&
                        space_unit_cells.Contains(x) == false
                        );

                space_unit_adjacent_cells.AddRange(nei);
            }

            return space_unit_adjacent_cells;
        }

        /// <summary>
        /// Returns the cells that surround this room
        /// and are free (i.e. not occupied by any room).
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public List<int> Q__Space_Unit__Surrounding_Cells__Free(
            int room_id
            )
        {
            List<int> room_cells = Q__Space_Unit__Cells(room_id);

            List<int> room_adjacent_free_cells = new List<int>();

            foreach (int rc in room_cells)
            {
                List<int> nei = Q__Cell__Neighbors(rc).FindAll(
                    x =>
                    room_adjacent_free_cells.Contains(x) == false
                    &&
                    room_cells.Contains(x) == false
                    &&
                    Q__Is_Cell__Free_And_Active(x)
                    );

                room_adjacent_free_cells.AddRange(nei);
            }

            return room_adjacent_free_cells;
        }

        /// <summary>
        /// Returns the list of cells that surround this room
        /// and are occupied by some other room.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public List<int> Q__Space_Unit__Surrounding_Cells__Occupied(
            int room_id
            )
        {
            List<int> room_cells = Q__Space_Unit__Cells(room_id);

            List<int> room_adjacent_not_free_cells = new List<int>();

            foreach (int rc in room_cells)
            {
                List<int> nei =
                    Q__Cell__Neighbors(rc)
                    .FindAll(
                        x =>
                        room_adjacent_not_free_cells.Contains(x) == false
                        &&
                        room_cells.Contains(x) == false
                        &&
                        Q__Is_Cell__Free_And_Active(x) == false
                        );

                room_adjacent_not_free_cells.AddRange(nei);
            }

            return room_adjacent_not_free_cells;
        }


        public List<int> Q__Space_Units__Cells(
            List<int> space_units
            )
        {
            List<int> space_units_cells = new List<int>();
            foreach (int unit in space_units)
            {
                space_units_cells.AddRange(Q__Space_Unit__Cells(unit));
            }
            return space_units_cells;
        }

        /// <summary>
        /// Returns the free cells that surround a list of rooms.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="room_ids"></param>
        /// <returns></returns>
        public List<int> Q__Space_Units__Surrounding_Cells__Free(
            List<int> room_ids
            )
        {
            List<int> rooms_cells = Q__Space_Units__Cells(room_ids);

            List<int> rooms_adjacent_free_cells = new List<int>();

            foreach (int rc in rooms_cells)
            {
                List<int> nei = Q__Cell__Neighbors(rc).FindAll(
                    x =>
                    rooms_adjacent_free_cells.Contains(x) == false
                    &&
                    rooms_cells.Contains(x) == false
                    &&
                    Q__Is_Cell__Free_And_Active(x)
                    );

                rooms_adjacent_free_cells.AddRange(nei);
            }

            return rooms_adjacent_free_cells;
        }

        public List<int> Q__Space_Units__Surrounding_Free_Cells__Common(
            List<int> room_ids
            )
        {
            List<int> rooms_adjacent_free_cells = Q__Space_Units__Surrounding_Cells__Free(room_ids);

            List<int> rooms_common_adjacent_free_cells = new List<int>();

            foreach (int afc in rooms_adjacent_free_cells)
            {
                Dictionary<int, bool> adjacency_check = new Dictionary<int, bool>();
                foreach (var room_id in room_ids)
                {
                    adjacency_check.Add(room_id, false);
                }
                // if this cell is neighbor to all the room ids...
                List<int> afc_neighbor_cells = Q__Cell__Neighbors(afc);
                foreach (int afc_neighbor in afc_neighbor_cells)
                {
                    // where does that cell belong?
                    int owner = space_unit__per__cell[afc_neighbor];
                    if (room_ids.Contains(owner))
                    {
                        adjacency_check[owner] = true;
                    }
                }

                bool all_true = true;
                foreach (int room in room_ids)
                {
                    if (adjacency_check[room] == false)
                    {
                        all_true = false;
                        break;
                    }
                }

                if (all_true)
                {
                    rooms_common_adjacent_free_cells.Add(afc);
                }
            }

            return rooms_common_adjacent_free_cells;
        }
    }
}
