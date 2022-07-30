using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public partial class DS__Undirected_Graph
    {
        public void M__Add_Vertex(int vertex_id)
        {
            if (neighbors__per__vertex.Keys.Contains(vertex_id) == false)
                neighbors__per__vertex.Add(vertex_id, new List<int>());
        }

        public void M__Remove_Vertex(int vertex)
        {
            neighbors__per__vertex.Remove(vertex);

            foreach (int n in neighbors__per__vertex.Keys)
                neighbors__per__vertex[n].Remove(vertex);
        }

        public void M__Add_Edge(Undirected_Edge edge)
        {
            if (edge.v1 == edge.v2)
                throw new System.Exception("vertex cannot be connected to itself");

            M__Add_Edge(edge.v1, edge.v2);
        }

        public void M__Add_Edge(int v1, int v2)
        {
            if (v1 == v2)
                throw new System.Exception("vertex cannot be connected to itself");

            if (neighbors__per__vertex.ContainsKey(v1))
            {
                if (neighbors__per__vertex[v1].Contains(v2) == false)
                    neighbors__per__vertex[v1].Add(v2);
            }
            else
            {
                neighbors__per__vertex.Add(v1, new List<int>() { v2 });
            }

            if (neighbors__per__vertex.ContainsKey(v2))
            {
                if (neighbors__per__vertex[v2].Contains(v1) == false)
                    neighbors__per__vertex[v2].Add(v1);
            }
            else
            {
                neighbors__per__vertex.Add(v2, new List<int>() { v1 });
            }
        }

        public void M__Remove_Edge(Undirected_Edge edge)
        {
            neighbors__per__vertex[edge.v1].Remove(edge.v2);
            neighbors__per__vertex[edge.v2].Remove(edge.v1);
        }

        public void M__Remove_Isolated_Vertices()
        {
            List<int> isolated_vertices = new List<int>();

            foreach (int n in neighbors__per__vertex.Keys)
            {
                if (Q__Is_Isolated(n))
                    isolated_vertices.Add(n);
            }

            foreach (var vertex in isolated_vertices)
                neighbors__per__vertex.Remove(vertex);
        }
    }
}
