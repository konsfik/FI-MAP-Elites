using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public partial class DS__Undirected_Graph
    {
        /// <summary>
        /// Returns the degree of the current vertex.
        /// The degree is the number of edges that are adjacent to this vertex.
        /// definition - source: https://en.wikipedia.org/wiki/Glossary_of_graph_theory
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public int Q__Degree(int vertex)
        {
            return neighbors__per__vertex[vertex].Count;
        }

        /// <summary>
        /// Returns whether the current vertex is a leaf vertex.
        /// A leaf vertex is a vertex of degree 1.
        /// definition - source: https://en.wikipedia.org/wiki/Glossary_of_graph_theory
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Q__Is_Leaf(int vertex)
        {
            return Q__Degree(vertex) == 1;
        }

        /// <summary>
        /// Returns whether the vertex is of degree zero (0).
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Q__Is_Isolated(int vertex)
        {
            return Q__Degree(vertex) == 0;
        }


        /// <summary>
        /// Returns whether the selected vertex is an articulation point.
        /// An articulation point is a vertex in a connected graph whose removal would disconnect the graph.
        /// source: https://en.wikipedia.org/wiki/Glossary_of_graph_theory
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Q__Is_Articulation_Point(int vertex)
        {
            // find the remaining vertices
            List<int> other_vertices = Q__Vertices();
            other_vertices.Remove(vertex);

            // get the sub-graph of the remaining vertices
            DS__Undirected_Graph sub_graph = Q__Sub_Graph__Containing_Vertices(other_vertices);

            return sub_graph.Q__Is_Connected() == false;
        }

        public bool Q__Is_Removable(int vertex)
        {
            // find the remaining vertices
            List<int> remaining_vertices = Q__Vertices();
            remaining_vertices.Remove(vertex);

            // get the sub-graph of the remaining vertices
            DS__Undirected_Graph sub_graph = Q__Sub_Graph__Containing_Vertices(remaining_vertices);

            // check if the sub graph is fully conneccted
            return sub_graph.Q__Is_Connected();
        }

        public List<int> Q__Neighbors(int vertex)
        {
            return neighbors__per__vertex[vertex].Q__Deep_Copy();
        }

        public List<Undirected_Edge> Q__Edges(int vertex)
        {
            List<Undirected_Edge> vertex_edges = new List<Undirected_Edge>();

            foreach (int neighbor in neighbors__per__vertex[vertex])
            {
                vertex_edges.Add(
                    new Undirected_Edge(vertex, neighbor)
                    );
            }

            return vertex_edges;
        }

        public int Q__Num_Neighbors(int vertex)
        {
            return neighbors__per__vertex[vertex].Count;
        }

        /// <summary>
        /// Returns the set of vertices that are reachable from the current vertex, in a list.
        /// </summary>
        /// <param name="root_vertex"></param>
        /// <returns></returns>
        public List<int> Q__Reachable_Vertices(int root_vertex)
        {
            List<int> visited_vertices = new List<int>() { root_vertex };

            return Q__Reachable_Vertices__Util(
                root_vertex,
                visited_vertices
                );
        }

        private List<int> Q__Reachable_Vertices__Util(int vertex, List<int> visited_vertices)
        {
            if (visited_vertices == null || visited_vertices.Count == 0)
                visited_vertices = new List<int>() { vertex };

            List<int> neighbors = Q__Neighbors(vertex);

            foreach (var neighbor in neighbors)
            {
                if (visited_vertices.Contains(neighbor) == false)
                {
                    visited_vertices.Add(neighbor);
                    Q__Reachable_Vertices__Util(neighbor, visited_vertices);
                }
            }

            return visited_vertices;
        }
    }
}
