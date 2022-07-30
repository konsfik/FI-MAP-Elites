using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__Experiments
{
    public static partial class Templates__PCG_Workshop
    {
        private static double Q__Minimum_Space_Unit_Area() {
            return 4.0;
        }

        /// <summary>
        /// Returns a circular graph of a user-specified number of nodes
        /// </summary>
        /// <param name="num_space_units"></param>
        /// <returns></returns>
        public static DS__Layout_Constraints Q__LC__Cycle_Graph(
            int num_space_units
            )
        {
            if (num_space_units < 4) throw new ArgumentOutOfRangeException("num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "cycle_graph_" + num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                name: spec_name,
                openings__minimum_wall_length: openings_minimum_wall_length,
                connectivity_threshold: 0.01
                );

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add space units
            for (int i = 0; i < num_space_units; i++)
            {
                string room_name = "room_" + i.ToString();
                var color = all_colors[i % num_all_colors];
                spec.M__Add_Space_Unit(
                    id: i,
                    name: room_name,
                    color: color,
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // add connections
            for (int i = 0; i < num_space_units; i++)
            {
                int this_node_index = i;
                int next_node_index = (i + 1) % num_space_units;
                spec.M__Add_Connection(new Undirected_Edge(this_node_index, next_node_index));
            }

            // recalculate area per space unit, here
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Double_Cycle_Graph(
            int half_num_space_units
            )
        {
            if (half_num_space_units < 4) throw new ArgumentOutOfRangeException("half num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "double_cycle_graph_" + half_num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                name: spec_name,
                openings__minimum_wall_length: openings_minimum_wall_length,
                connectivity_threshold: 0.01
                );

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            int num_space_units = half_num_space_units * 2;

            // add space units
            for (int i = 0; i < num_space_units; i++)
            {
                string room_name = "room_" + i.ToString();
                var color = all_colors[i % num_all_colors];
                spec.M__Add_Space_Unit(
                    id: i,
                    name: room_name,
                    color: color,
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // connect first half
            for (int i = 0; i < half_num_space_units; i++)
            {
                int this_node_index = i;
                int next_node_index = (i + 1);
                if (next_node_index >= half_num_space_units) next_node_index = 0;
                spec.M__Add_Connection(new Undirected_Edge(this_node_index, next_node_index));
            }

            // connect second half
            for (int i = half_num_space_units; i < num_space_units; i++)
            {
                int this_node_index = i;
                int next_node_index = (i + 1);
                if (next_node_index >= num_space_units) next_node_index = half_num_space_units;
                spec.M__Add_Connection(new Undirected_Edge(this_node_index, next_node_index));
            }

            // add extra connection between two halves
            spec.M__Add_Connection(new Undirected_Edge(0, half_num_space_units));

            // recalculate area per space unit, here
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Star_Graph(
            int num_space_units
            )
        {
            if (num_space_units < 4) throw new ArgumentOutOfRangeException("num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "star_graph_" + num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                spec_name,
                openings_minimum_wall_length,
                0.01
                );

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add the center space unit
            string center_room_name = "room_0";
            var center_room_color = all_colors[0];
            spec.M__Add_Space_Unit(
                id: 0,
                name: center_room_name,
                color: center_room_color,
                area: minimum_space_unit_area,
                num_entrance_doors: 0,
                num_windows: 0
            );

            // add satellite space units
            for (int i = 1; i < num_space_units; i++)
            {
                string room_name = "room_" + i.ToString();
                var color = all_colors[i % num_all_colors];
                spec.M__Add_Space_Unit(
                    id: i,
                    name: room_name,
                    color: color,
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // add connections
            // i.e. connect the center to all satellites
            for (int i = 1; i < num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(0, i));
            }

            // recalculate space units areas
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Double_Star_Graph(
            int half_num_space_units
            )
        {
            if (half_num_space_units < 4) throw new ArgumentOutOfRangeException("half num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "double_star_graph_" + half_num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                spec_name,
                openings_minimum_wall_length,
                0.01
                );

            int num_space_units = half_num_space_units * 2;

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add all space units
            for (int i = 0; i < num_space_units; i++)
            {
                spec.M__Add_Space_Unit(
                    id: i,
                    name: "room_" + i.ToString(),
                    color: all_colors[i % num_all_colors],
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // add connections
            // i.e. connect the first center to the first set of satellites
            for (int i = 1; i < half_num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(0, i));
            }

            // i.e. connect the second center to the second set of satellites
            for (int i = half_num_space_units + 1; i < num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(half_num_space_units, i));
            }

            // add the final connection in between the graphs
            spec.M__Add_Connection(new Undirected_Edge(1, half_num_space_units + 1));

            // recalculate space units areas
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Wheel_Graph(
            int num_space_units
            )
        {
            if (num_space_units < 4) throw new ArgumentOutOfRangeException("num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "wheel_graph_" + num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                spec_name,
                openings_minimum_wall_length,
                0.01
                );

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add the center space unit
            string center_room_name = "room_0";
            var center_room_color = all_colors[0];
            spec.M__Add_Space_Unit(
                id: 0,
                name: center_room_name,
                color: center_room_color,
                area: minimum_space_unit_area,
                num_entrance_doors: 0,
                num_windows: 0
            );

            // add satellite space units
            for (int i = 1; i < num_space_units; i++)
            {
                string room_name = "room_" + i.ToString();
                var color = all_colors[i % num_all_colors];
                spec.M__Add_Space_Unit(
                    id: i,
                    name: room_name,
                    color: color,
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // connect the center to all satellites
            for (int i = 1; i < num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(0, i));
            }

            // connect satellites together (outer circle)
            for (int i = 1; i < num_space_units; i++)
            {
                int this_index = i;
                int next_index = i + 1;
                if (next_index > num_space_units - 1) next_index = 1;
                spec.M__Add_Connection(new Undirected_Edge(this_index, next_index));
            }

            // recalculate space units areas
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Double_Wheel_Graph(
            int half_num_space_units
            )
        {
            if (half_num_space_units < 4) throw new ArgumentOutOfRangeException("half num nodes must be >= 4");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "double_wheel_graph_" + half_num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                spec_name,
                openings_minimum_wall_length,
                0.01
                );

            int num_space_units = half_num_space_units * 2;

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add all the space units
            for (int i = 0; i < num_space_units; i++)
            {
                spec.M__Add_Space_Unit(
                    id: i,
                    name: "room_" + i.ToString(),
                    color: all_colors[i % num_all_colors],
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // add connections

            // connect the first center to the first set of satellites
            for (int i = 1; i < half_num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(0, i));
            }

            // connect the first set of satellites among them
            for (int i = 1; i < half_num_space_units; i++)
            {
                int this_index = i;
                int next_index = i + 1;
                if (next_index >= half_num_space_units) next_index = 1;
                spec.M__Add_Connection(new Undirected_Edge(this_index, next_index));
            }

            // connect the second center to the second set of satellites
            for (int i = half_num_space_units + 1; i < num_space_units; i++)
            {
                spec.M__Add_Connection(new Undirected_Edge(half_num_space_units, i));
            }

            // connect the second set of satellites among them
            for (int i = half_num_space_units + 1; i < num_space_units; i++)
            {
                int this_index = i;
                int next_index = i + 1;
                if (next_index >= num_space_units) next_index = half_num_space_units + 1;
                spec.M__Add_Connection(new Undirected_Edge(this_index, next_index));
            }

            // add the final connection in between the graphs
            spec.M__Add_Connection(new Undirected_Edge(1, half_num_space_units + 1));

            // recalculate space units areas
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }

        public static DS__Layout_Constraints Q__LC__Path_Graph(
            int num_space_units
            )
        {
            if (num_space_units < 3) throw new ArgumentOutOfRangeException("num nodes must be >= 3");

            double openings_minimum_wall_length = 0.5;
            double minimum_space_unit_area = Q__Minimum_Space_Unit_Area();

            string spec_name = "path_graph_" + num_space_units.ToString();
            DS__Layout_Constraints spec = new DS__Layout_Constraints(
                name: spec_name,
                openings__minimum_wall_length: openings_minimum_wall_length,
                connectivity_threshold: 0.01
                );

            var all_colors = RGB_Color.All_Colors();
            int num_all_colors = all_colors.Count;

            // add space units
            for (int i = 0; i < num_space_units; i++)
            {
                string room_name = "room_" + i.ToString();
                var color = all_colors[i % num_all_colors];
                spec.M__Add_Space_Unit(
                    id: i,
                    name: room_name,
                    color: color,
                    area: minimum_space_unit_area,
                    num_entrance_doors: 0,
                    num_windows: 0
                );
            }

            // add connections
            for (int i = 0; i < num_space_units - 1; i++)
            {
                int this_node_index = i;
                int next_node_index = (i + 1);
                spec.M__Add_Connection(new Undirected_Edge(this_node_index, next_node_index));
            }

            // recalculate area per space unit, here
            for (int i = 0; i < num_space_units; i++)
            {
                int num_neighbors = spec.graph.Q__Num_Neighbors(i);
                double new_area = minimum_space_unit_area + (double)num_neighbors;
                spec.area__per__space_unit[i] = new_area;
            }

            return spec;
        }
    }
}
