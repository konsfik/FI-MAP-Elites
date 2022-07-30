using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph
{
    public partial class DS__Undirected_Weighted_Graph : Data_Structure
    {
        public double Q__Connection_Weights_Sum()
        {
            double sum = 0;
            var edges = Q__Edges();
            foreach (var edge in edges) {
                sum += edge.weight;
            }
            return sum;
        }

        public List<int> Q__Vertices()
        {
            return weight_per_neighbor_per_vertex.Keys.ToList();
        }

        public int Q__Num_Verts()
        {
            return weight_per_neighbor_per_vertex.Count;
        }

        public bool Q__Vertex_Exists(int v)
        {
            return weight_per_neighbor_per_vertex.ContainsKey(v);
        }

        public bool Q__Connection_Exists(int v1, int v2)
        {
            if (Q__Vertex_Exists(v1) == false) return false;
            if (Q__Vertex_Exists(v2) == false) return false;

            bool dir_1 = weight_per_neighbor_per_vertex[v1].ContainsKey(v2);
            bool dir_2 = weight_per_neighbor_per_vertex[v2].ContainsKey(v1);
            if (dir_1 && dir_2)
            {
                // both directions exist
                return true;
            }
            else if (dir_1 ^ dir_2)
            {
                // only one direction exists
                throw new System.Exception("improper connection");
            }
            else
            {
                // none of the directions exists
                return false;
            }
        }



        public double Q__Connection_Weight(int v1, int v2)
        {
            bool connection_exists = Q__Connection_Exists(v1, v2);
            if (connection_exists == false)
            {
                return double.NaN;
            }
            double weight = weight_per_neighbor_per_vertex[v1][v2];
            return weight;
        }



        public List<Undirected_Weighted_Edge> Q__Edges()
        {
            HashSet<Undirected_Weighted_Edge> edges = new HashSet<Undirected_Weighted_Edge>();
            foreach (var kvp_1 in weight_per_neighbor_per_vertex)
            {
                int n1 = kvp_1.Key;
                foreach (var kvp_2 in kvp_1.Value)
                {
                    int n2 = kvp_2.Key;
                    double weight = kvp_2.Value;
                    Undirected_Weighted_Edge edge = new Undirected_Weighted_Edge(
                        n1,
                        n2,
                        weight
                        );
                    edges.Add(edge);
                }
            }
            return edges.ToList();
        }

        /// <summary>
        /// Returns true if every vertex of the graph is visitable 
        /// from any other vertex of the graph.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public bool Q__Is_Connected()
        {
            List<int> graph_vertices = Q__Vertices();
            if (graph_vertices == null)
            {
                return false;
            }
            if (graph_vertices.Count == 0)
            {
                return false;
            }
            int initial_vertex = graph_vertices[0];

            var visitable = Q__Reachable_Vertices(initial_vertex);

            return visitable.Count == Q__Num_Vertices();
        }

        public int Q__Num_Vertices()
        {
            return weight_per_neighbor_per_vertex.Count;
        }
    }
}
