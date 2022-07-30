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
        // DFS based function to find all bridges. It uses recursive
        // function bridgeUtil()
        public List<Undirected_Edge> Q__Bridges()
        {
            // Mark all the vertices as not visited
            Dictionary<int, bool> visited = new Dictionary<int, bool>(capacity: Q__Num_Vertices());
            Dictionary<int, int> disc = new Dictionary<int, int>(capacity: Q__Num_Vertices());
            Dictionary<int, int> low = new Dictionary<int, int>(capacity: Q__Num_Vertices());
            Dictionary<int, int> parent = new Dictionary<int, int>(capacity: Q__Num_Vertices());
            int time = 0;

            List<Undirected_Edge> bridges = new List<Undirected_Edge>();

            // Initialize parent and visited, 
            // and ap(articulation point) arrays
            foreach (var kvp in neighbors__per__vertex)
            {
                int vertex = kvp.Key;
                visited[vertex] = false;
                disc[vertex] = 0;
                low[vertex] = 0;
                parent[vertex] = -1;
            }

            // Call the recursive helper function to find Bridges
            // in DFS tree rooted with vertex 'i'
            foreach (var kvp in neighbors__per__vertex)
            {
                int vertex = kvp.Key;
                if (visited[vertex] == false)
                {
                    Q__Bridges__Util(
                        vertex,
                        visited,
                        disc,
                        low,
                        parent,
                        ref time,
                        bridges
                        );
                }
            }

            return bridges;
        }

        // A recursive function that finds and prints bridges
        // using DFS traversal
        // u --> The vertex to be visited next
        // visited[] --> keeps track of visited vertices
        // disc[] --> Stores discovery times of visited vertices
        // parent[] --> Stores parent vertices in DFS tree
        private void Q__Bridges__Util(
            int vertex_next,
            Dictionary<int, bool> visited,
            Dictionary<int, int> disc,
            Dictionary<int, int> low,
            Dictionary<int, int> parent,
            ref int time,
            List<Undirected_Edge> bridges
            )
        {

            // Mark the current node as visited
            visited[vertex_next] = true;

            // Initialize discovery time and low value
            disc[vertex_next] = low[vertex_next] = ++time;

            // Go through all vertices adjacent to this
            foreach (int neighbor in neighbors__per__vertex[vertex_next])
            {
                // neighbor is current adjacent of u

                // If v is not visited yet, then make it a child
                // of u in DFS tree and recur for it.
                // If v is not visited yet, then recur for it
                if (!visited[neighbor])
                {
                    parent[neighbor] = vertex_next;
                    Q__Bridges__Util(neighbor, visited, disc, low, parent, ref time, bridges);

                    // Check if the subtree rooted with v has a
                    // connection to one of the ancestors of u
                    low[vertex_next] = Math.Min(low[vertex_next], low[neighbor]);

                    // If the lowest vertex reachable from subtree
                    // under v is below u in DFS tree, then u-v is
                    // a bridge
                    if (low[neighbor] > disc[vertex_next])
                        bridges.Add(new Undirected_Edge(vertex_next,neighbor));
                        //Console.WriteLine(vertex_next + " " + neighbor);
                }

                // Update low value of u for parent function calls.
                else if (neighbor != parent[vertex_next])
                    low[vertex_next] = Math.Min(low[vertex_next], disc[neighbor]);
            }
        }

        /// <summary>
        /// Returns all the edges, for which the following is true:
        /// Their vertices do not have any common neighbors
        /// </summary>
        /// <returns></returns>
        public List<Undirected_Edge> Q__Local_Bridges() {
            List<Undirected_Edge> all_edges = Q__Edges();

            List<Undirected_Edge> local_bridges = new List<Undirected_Edge>();
            foreach (var edge in all_edges) {
                var v1_neighbors = Q__Neighbors(edge.v1);
                var v2_neighbors = Q__Neighbors(edge.v2);

                bool common = v1_neighbors.Any(
                    x=> v2_neighbors.Contains(x)
                    );

                if (common == false) {
                    local_bridges.Add(edge);
                }
            }

            return local_bridges;
        }
    }
}
