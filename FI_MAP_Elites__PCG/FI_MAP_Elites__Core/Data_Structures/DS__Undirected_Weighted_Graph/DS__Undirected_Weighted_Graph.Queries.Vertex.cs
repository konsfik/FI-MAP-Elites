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
        /// <summary>
        /// Returns the degree of the current vertex.
        /// The degree is the number of edges that are adjacent to this vertex.
        /// definition - source: https://en.wikipedia.org/wiki/Glossary_of_graph_theory
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public int Q__Degree(int vertex)
        {
            return weight_per_neighbor_per_vertex[vertex].Count;
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
            DS__Undirected_Weighted_Graph sub_graph = 
                Q__Sub_Graph__Containing_Vertices(other_vertices);

            return sub_graph.Q__Is_Connected() == false;
        }

        public bool Q__Is_Removable(int vertex)
        {
            // find the remaining vertices
            List<int> remaining_vertices = Q__Vertices();
            remaining_vertices.Remove(vertex);

            // get the sub-graph of the remaining vertices
            DS__Undirected_Weighted_Graph sub_graph = 
                Q__Sub_Graph__Containing_Vertices(remaining_vertices);

            // check if the sub graph is fully conneccted
            return sub_graph.Q__Is_Connected();
        }

        public List<int> Q__Neighbors(int vertex)
        {
            return weight_per_neighbor_per_vertex[vertex].Keys.ToList().Q__Deep_Copy();
        }
    }
}
