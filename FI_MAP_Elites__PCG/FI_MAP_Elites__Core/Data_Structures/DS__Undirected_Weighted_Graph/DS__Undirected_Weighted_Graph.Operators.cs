using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph
{
    public partial class DS__Undirected_Weighted_Graph : Data_Structure
    {
        public void M__Add_Vertex(int v)
        {
            if (weight_per_neighbor_per_vertex.Keys.Contains(v) == false)
                weight_per_neighbor_per_vertex.Add(v, new Dictionary<int, double>());
        }

        public void M__Add_Vertex__Fast(int v) {
            weight_per_neighbor_per_vertex.Add(v, new Dictionary<int, double>());
        }

        public void M__Remove_Vertex(int vertex)
        {
            weight_per_neighbor_per_vertex.Remove(vertex);

            foreach (int n in weight_per_neighbor_per_vertex.Keys)
                weight_per_neighbor_per_vertex[n].Remove(vertex);
        }

        public void M__Add_Edge(Undirected_Weighted_Edge edge)
        {
            M__Add_Edge(edge.v1, edge.v2, edge.weight);
        }

        public void M__Add_Edge(int v1, int v2, double weight)
        {
            if (v1 == v2)
                throw new System.Exception("vertex cannot be connected to itself");

            if (Q__Connection_Exists(v1, v2))
            {
                return;
            }


            M__Add_Vertex(v1);
            M__Add_Vertex(v2);
            if (weight_per_neighbor_per_vertex[v1].ContainsKey(v2) == false)
            {
                weight_per_neighbor_per_vertex[v1].Add(v2, weight);
            }
            if (weight_per_neighbor_per_vertex[v2].ContainsKey(v1) == false)
            {
                weight_per_neighbor_per_vertex[v2].Add(v1, weight);
            }
        }

        public void M__Remove_Edge(Undirected_Weighted_Edge edge)
        {
            weight_per_neighbor_per_vertex[edge.v1].Remove(edge.v2);
            weight_per_neighbor_per_vertex[edge.v2].Remove(edge.v1);
        }
    }
}
