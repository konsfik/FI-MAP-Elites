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
        public DS__Undirected_Weighted_Graph Q__Sub_Graph__Containing_Vertices(List<int> verts_to_keep)
        {

            DS__Undirected_Weighted_Graph sub_graph =
                (DS__Undirected_Weighted_Graph)this.Q__Deep_Copy();

            List<int> all_verts = sub_graph.weight_per_neighbor_per_vertex.Keys.ToList();

            List<int> verts_to_remove =
                all_verts.FindAll(
                    x => verts_to_keep.Contains(x) == false
                );

            foreach (var v in verts_to_remove)
            {
                sub_graph.M__Remove_Vertex(v);
            }

            return sub_graph;
        }

        public List<List<int>> Q__Islands__Verts()
        {
            List<int> all_verts = Q__Vertices();

            List<List<int>> islands_verts = new List<List<int>>();

            while (all_verts.Count > 0)
            {
                int root_vert = all_verts[0];
                var reachable = Q__Reachable_Vertices(root_vert);
                islands_verts.Add(reachable);
                all_verts.RemoveAll(
                    x =>
                    reachable.Contains(x)
                    );
            }

            return islands_verts;
        }

        public List<DS__Undirected_Weighted_Graph> Q__Islands__Graphs()
        {
            List<int> all_verts = Q__Vertices();


            List<DS__Undirected_Weighted_Graph> islands_graphs = new List<DS__Undirected_Weighted_Graph>();

            List<List<int>> islands_verts = Q__Islands__Verts();
            foreach (var set in islands_verts) {
                islands_graphs.Add(
                    Q__Sub_Graph__Containing_Vertices(set)
                    ) ;
            }

            return islands_graphs;
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
