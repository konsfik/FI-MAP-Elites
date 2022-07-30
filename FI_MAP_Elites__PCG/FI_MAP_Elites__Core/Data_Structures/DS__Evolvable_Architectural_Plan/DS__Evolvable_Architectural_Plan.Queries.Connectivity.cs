using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Architectural_Plan : DS__Evolvable_Geometry
    {
        public DS__Undirected_Graph Q__Plan__Sub_Graph()
        {

            List<int> prescribed_space_units = Q__Prescribed_Space_Units();

            List<DS__Undirected_Graph> unit_graphs =
                new List<DS__Undirected_Graph>();

            foreach (var space_unit in prescribed_space_units)
            {
                List<int> unit_cells = Q__Space_Unit__Cells(space_unit);

                DS__Undirected_Graph unit_graph =
                    voronoi_tessellation
                    .connectivity_graph
                    .Q__Sub_Graph__Containing_Vertices(unit_cells);

                unit_graphs.Add(unit_graph);
            }

            DS__Undirected_Graph plan_graph = new DS__Undirected_Graph();
            foreach (var unit_graph in unit_graphs)
            {
                var vertices = unit_graph.Q__Vertices();
                foreach (var vertex in vertices)
                    plan_graph.M__Add_Vertex(vertex);

                var edges = unit_graph.Q__Edges();
                foreach (var edge in edges)
                    plan_graph.M__Add_Edge(edge);
            }

            // add the interior doors' connections
            foreach (var interior_door in connection_doors)
            {
                bool interior_door__is_proper = Q__Is_Connection_Door__Properly_Placed(interior_door);
                if (interior_door__is_proper == false)
                {
                    continue;
                }

                bool door_connection_exists =
                    voronoi_tessellation
                    .weighted_connectivity_graph
                    .Q__Connection_Exists(interior_door.Q__Cell_1(), interior_door.Q__Cell_2());

                if (door_connection_exists == false)
                {
                    continue;
                }

                plan_graph.M__Add_Edge(
                    interior_door.Q__Cell_1(),
                    interior_door.Q__Cell_2()
                    );
            }

            return plan_graph;
        }

    }
}
