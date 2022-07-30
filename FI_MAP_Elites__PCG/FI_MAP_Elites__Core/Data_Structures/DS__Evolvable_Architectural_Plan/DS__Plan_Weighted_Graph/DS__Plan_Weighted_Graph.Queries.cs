using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Plan_Weighted_Graph
    {
        /// <summary>
        /// Returns the maximum distance between all pairs of vertices that are found in the graph.
        /// </summary>
        /// <returns></returns>
        public double Q__Maximum__Shortest_Path__Length()
        {
            return graph.Q__Maximum_Shortest_Path_Length();
        }

        /// <summary>
        /// Returns the vertices of all entrance doors that are found in the graph.
        /// </summary>
        /// <returns></returns>
        public List<int> Q__Entrance_Doors__Verts()
        {
            List<int> entrance_doors_verts = new List<int>();
            foreach (var kvp in use__per__vertex)
            {
                int vertex = kvp.Key;
                PWG__Vertex_Use use = kvp.Value;
                if (use == PWG__Vertex_Use.ENTRANCE_DOOR)
                {
                    entrance_doors_verts.Add(vertex);
                }
            }
            return entrance_doors_verts;
        }

        /// <summary>
        /// Returns the vertices of all entrance doors that are found in the graph.
        /// This includes interior and exterior connection doors.
        /// </summary>
        /// <returns></returns>
        public List<int> Q__Connection_Doors__Verts()
        {
            List<int> connection_doors_verts = new List<int>();
            foreach (var kvp in use__per__vertex)
            {
                int vertex = kvp.Key;
                PWG__Vertex_Use vertex_use = kvp.Value;
                if (
                    vertex_use == PWG__Vertex_Use.INTERIOR_CONNECTION_DOOR
                    ||
                    vertex_use == PWG__Vertex_Use.EXTERIOR_CONNECTION_DOOR
                    )
                {
                    connection_doors_verts.Add(vertex);
                }
            }
            return connection_doors_verts;
        }

        public List<int> Q__Windows__Verts()
        {
            List<int> windows_verts = new List<int>();
            foreach (var kvp in use__per__vertex)
            {
                int vertex = kvp.Key;
                PWG__Vertex_Use use = kvp.Value;
                if (use == PWG__Vertex_Use.WINDOW)
                {
                    windows_verts.Add(vertex);
                }
            }
            return windows_verts;
        }

        public List<List<int>> Q__Circulation_Paths()
        {
            var entrance_doors_verts = Q__Entrance_Doors__Verts();
            var connection_doors_verts = Q__Connection_Doors__Verts();

            List<List<int>> circulation_paths = new List<List<int>>();
            foreach (var entrance_door_vertex in entrance_doors_verts)
            {
                foreach (var connection_door_vertex in connection_doors_verts)
                {
                    graph.Q__Shortest_Path(
                        entrance_door_vertex,
                        connection_door_vertex,
                        out bool success,
                        out List<int> path
                        );

                    if (success == false)
                    {
                        continue;
                    }
                    else
                    {
                        circulation_paths.Add(path);
                    }
                }
            }

            return circulation_paths;
        }

        public List<Undirected_Weighted_Edge> Q__Circulation_Paths__Unique_Edges()
        {
            var circulation_paths = Q__Circulation_Paths();

            List<Undirected_Weighted_Edge> unique_edges = new List<Undirected_Weighted_Edge>();
            foreach (var path in circulation_paths)
            {
                int num_verts = path.Count;
                for (int i = 0; i < num_verts - 1; i++)
                {
                    int v1 = path[i];
                    int v2 = path[i + 1];
                    double weight = graph.Q__Connection_Weight(v1, v2);
                    Undirected_Weighted_Edge edge = new Undirected_Weighted_Edge(v1, v2, weight);
                    bool existing =
                        unique_edges
                        .Any(
                            x =>
                            (x.v1 == edge.v1 && x.v2 == edge.v2)
                            ||
                            (x.v1 == edge.v2 && x.v2 == edge.v1)
                            );
                    if (existing == false)
                    {
                        unique_edges.Add(edge);
                    }
                }
            }
            return unique_edges;
        }


        public List<int> Q__Circulation_verts()
        {
            List<List<int>> circulation_paths = Q__Circulation_Paths();

            HashSet<int> circulation_verts = new HashSet<int>();
            foreach (List<int> group in circulation_paths)
            {
                foreach (int vert in group)
                {
                    circulation_verts.Add(vert);
                }
            }

            return circulation_verts.ToList();
        }
    }
}
