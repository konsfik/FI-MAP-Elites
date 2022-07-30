using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;
using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public enum PWG__Vertex_Type
    {
        CENTROID,
        MID_POINT
    }

    public enum PWG__Vertex_Use
    {
        NONE,
        INTERIOR_CONNECTION_DOOR,
        EXTERIOR_CONNECTION_DOOR,
        ENTRANCE_DOOR,
        WINDOW
    }

    public partial class DS__Plan_Weighted_Graph:Data_Structure
    {
        public DS__Undirected_Weighted_Graph graph;
        public Dictionary<int, Vec2d> location__per__vertex;
        public Dictionary<int, PWG__Vertex_Type> type__per__vertex;
        public Dictionary<int, PWG__Vertex_Use> use__per__vertex;

        public DS__Plan_Weighted_Graph()
        {
            this.graph = new DS__Undirected_Weighted_Graph();
            this.location__per__vertex = new Dictionary<int, Vec2d>();
            this.type__per__vertex = new Dictionary<int, PWG__Vertex_Type>();
            this.use__per__vertex = new Dictionary<int, PWG__Vertex_Use>();
        }

        public DS__Plan_Weighted_Graph(
            DS__Architectural_Plan individual
            )
        {
            Update(individual);
        }


        private DS__Plan_Weighted_Graph(DS__Plan_Weighted_Graph ds_to_copy)
        {
            this.graph = (DS__Undirected_Weighted_Graph)ds_to_copy.graph.Q__Deep_Copy();
            this.location__per__vertex = ds_to_copy.location__per__vertex.Q__Deep_Copy();
            this.type__per__vertex = ds_to_copy.type__per__vertex.Q__Deep_Copy();
            this.use__per__vertex = ds_to_copy.use__per__vertex.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Plan_Weighted_Graph(this);
        }

        public void Update(DS__Architectural_Plan individual) {
            graph = new DS__Undirected_Weighted_Graph();
            location__per__vertex = new Dictionary<int, Vec2d>();
            type__per__vertex = new Dictionary<int, PWG__Vertex_Type>();
            use__per__vertex = new Dictionary<int, PWG__Vertex_Use>();

            int num_points = individual.voronoi_tessellation.Q__Num_Generator_Points();

            int mid_point_vertex_counter = num_points;

            var prescribed_space_units = individual.Q__Prescribed_Space_Units();
            List<DS__Undirected_Weighted_Graph> units_weighted_graphs = new List<DS__Undirected_Weighted_Graph>();
            foreach (int space_unit in prescribed_space_units)
            {

                var unit_vertices =
                    individual
                    .Q__Space_Unit__Cells(space_unit);

                var unit_graph =
                    individual
                    .voronoi_tessellation
                    .connectivity_graph
                    .Q__Sub_Graph__Containing_Vertices(unit_vertices);

                var edges = unit_graph.Q__Edges();

                DS__Undirected_Weighted_Graph unit_weighted_graph = new DS__Undirected_Weighted_Graph();
                foreach (var edge in edges)
                {
                    individual
                        .voronoi_tessellation
                        .Q__Shared_Line__Between_Neighbor_Cells(
                            edge.v1,
                            edge.v2,
                            out bool success,
                            out Line_Segment line
                        );

                    if (success == false)
                    {
                        continue;
                    }
                    else
                    {
                        int v_start = edge.v1;
                        var p_start = individual.voronoi_tessellation.centroids[edge.v1];

                        int v_mid = mid_point_vertex_counter;
                        mid_point_vertex_counter++;
                        var p_mid = line.Q__Mid_Point();

                        int v_end = edge.v2;
                        var p_end = individual.voronoi_tessellation.centroids[edge.v2];

                        location__per__vertex[v_start] = p_start;
                        location__per__vertex[v_mid] = p_mid;
                        location__per__vertex[v_end] = p_end;

                        type__per__vertex[v_start] = PWG__Vertex_Type.CENTROID;
                        type__per__vertex[v_mid] = PWG__Vertex_Type.MID_POINT;
                        type__per__vertex[v_end] = PWG__Vertex_Type.CENTROID;

                        use__per__vertex[v_start] = PWG__Vertex_Use.NONE;
                        use__per__vertex[v_mid] = PWG__Vertex_Use.NONE;
                        use__per__vertex[v_end] = PWG__Vertex_Use.NONE;

                        var weighted_edge_1 =
                            new Undirected_Weighted_Edge(
                                v_start,
                                v_mid,
                                p_start.Q__Distance_To(p_mid)
                                );

                        var weighted_edge_2 =
                            new Undirected_Weighted_Edge(
                                v_mid,
                                v_end,
                                p_mid.Q__Distance_To(p_end)
                                );

                        unit_weighted_graph.M__Add_Edge(weighted_edge_1);
                        unit_weighted_graph.M__Add_Edge(weighted_edge_2);
                    }
                }
                units_weighted_graphs.Add(unit_weighted_graph);
            }

            // merge
            foreach (var unit_graph in units_weighted_graphs)
            {
                var vertices = unit_graph.Q__Vertices();
                foreach (var vertex in vertices)
                    graph.M__Add_Vertex(vertex);

                var edges = unit_graph.Q__Edges();
                foreach (var edge in edges)
                    graph.M__Add_Edge(edge);
            }


            // add the connection doors' connections
            foreach (var connection_door in individual.connection_doors)
            {
                bool connection_door__is_proper = individual.Q__Is_Connection_Door__Proper(connection_door);
                if (connection_door__is_proper == false)
                {
                    continue;
                }

                individual
                    .voronoi_tessellation
                    .Q__Shared_Line__Between_Neighbor_Cells(
                        connection_door.cells_connection.v1,
                        connection_door.cells_connection.v2,
                        out bool success,
                        out Line_Segment line
                    );

                if (success == false)
                {
                    continue;
                }

                var v_start = connection_door.Q__Cell_1();
                var p_start = individual.voronoi_tessellation.centroids[v_start];

                int v_mid = mid_point_vertex_counter;
                mid_point_vertex_counter++;
                var p_mid = line.Q__Mid_Point();

                var v_end = connection_door.Q__Cell_2();
                var p_end = individual.voronoi_tessellation.centroids[v_end];

                int space_unit_1 = connection_door.space_units_connection.v1;
                Space_Unit__Type space_unit_1__type = individual.prescription.type__per__space_unit[space_unit_1];
                int space_unit_2 = connection_door.space_units_connection.v2;
                Space_Unit__Type space_unit_2__type = individual.prescription.type__per__space_unit[space_unit_2];

                location__per__vertex[v_start] = p_start;
                location__per__vertex[v_mid] = p_mid;
                location__per__vertex[v_end] = p_end;

                type__per__vertex[v_start] = PWG__Vertex_Type.CENTROID;
                type__per__vertex[v_mid] = PWG__Vertex_Type.MID_POINT;
                type__per__vertex[v_end] = PWG__Vertex_Type.CENTROID;

                use__per__vertex[v_start] = PWG__Vertex_Use.NONE;
                if (
                    space_unit_1__type == Space_Unit__Type.INTERIOR
                    &&
                    space_unit_2__type == Space_Unit__Type.INTERIOR
                    )
                {
                    use__per__vertex[v_mid] = PWG__Vertex_Use.INTERIOR_CONNECTION_DOOR;
                }
                else
                {
                    use__per__vertex[v_mid] = PWG__Vertex_Use.EXTERIOR_CONNECTION_DOOR;
                }
                use__per__vertex[v_end] = PWG__Vertex_Use.NONE;

                graph.M__Add_Edge(
                    v_start,
                    v_mid,
                    p_start.Q__Distance_To(p_mid)
                    );

                graph.M__Add_Edge(
                    v_mid,
                    v_end,
                    p_mid.Q__Distance_To(p_end)
                    );

            }

            // add the exterior doors' connections
            foreach (var entrance_door in individual.entrance_doors)
            {
                bool exterior_door__is_proper = individual.Q__Is_Entrance_Door__Proper(entrance_door);
                if (exterior_door__is_proper == false)
                {
                    continue;
                }

                individual
                    .voronoi_tessellation
                    .Q__Shared_Line__Between_Neighbor_Cells(
                        entrance_door.cells_connection.v1,
                        entrance_door.cells_connection.v2,
                        out bool success,
                        out Line_Segment line
                    );

                if (success == false)
                {
                    continue;
                }

                int cell_1 = entrance_door.Q__Cell_1();
                int cell_2 = entrance_door.Q__Cell_2();
                int space_unit = entrance_door.Q__Space_Unit();
                int main_cell = cell_2;
                if (individual.space_unit__per__cell[cell_1] == space_unit)
                {
                    main_cell = cell_1;
                }

                var v_main = main_cell;
                var p_main = individual.voronoi_tessellation.centroids[v_main];

                int v_mid = mid_point_vertex_counter;
                mid_point_vertex_counter++;
                var p_mid = line.Q__Mid_Point();

                location__per__vertex[v_main] = p_main;
                location__per__vertex[v_mid] = p_mid;

                type__per__vertex[v_main] = PWG__Vertex_Type.CENTROID;
                type__per__vertex[v_mid] = PWG__Vertex_Type.MID_POINT;

                use__per__vertex[v_main] = PWG__Vertex_Use.NONE;
                use__per__vertex[v_mid] = PWG__Vertex_Use.ENTRANCE_DOOR;

                graph.M__Add_Edge(
                    v_main,
                    v_mid,
                    p_main.Q__Distance_To(p_mid)
                    );
            }

            // add the windows' connections
            foreach (var window in individual.windows)
            {
                bool window__is_proper = individual.Q__Is_Window__Proper(window);
                if (window__is_proper == false)
                {
                    continue;
                }

                individual
                    .voronoi_tessellation
                    .Q__Shared_Line__Between_Neighbor_Cells(
                        window.cells_connection.v1,
                        window.cells_connection.v2,
                        out bool success,
                        out Line_Segment line
                    );

                if (success == false)
                {
                    continue;
                }

                int cell_1 = window.Q__Cell_1();
                int cell_2 = window.Q__Cell_2();
                int space_unit = window.Q__Space_Unit();
                int main_cell = cell_2;
                if (individual.space_unit__per__cell[cell_1] == space_unit)
                {
                    main_cell = cell_1;
                }

                var v_main = main_cell;
                var p_main = individual.voronoi_tessellation.centroids[v_main];

                int v_mid = mid_point_vertex_counter;
                mid_point_vertex_counter++;
                var p_mid = line.Q__Mid_Point();

                location__per__vertex[v_main] = p_main;
                location__per__vertex[v_mid] = p_mid;

                type__per__vertex[v_main] = PWG__Vertex_Type.CENTROID;
                type__per__vertex[v_mid] = PWG__Vertex_Type.MID_POINT;

                use__per__vertex[v_main] = PWG__Vertex_Use.NONE;
                use__per__vertex[v_mid] = PWG__Vertex_Use.WINDOW;

                graph.M__Add_Edge(
                    v_main,
                    v_mid,
                    p_main.Q__Distance_To(p_mid)
                    );
            }
        }

        
        
    }
}
