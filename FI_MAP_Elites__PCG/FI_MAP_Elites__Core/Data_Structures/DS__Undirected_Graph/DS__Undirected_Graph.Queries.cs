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
        #region graph_general_queries
        /// <summary>
        /// Returns a list of all the vertices that are contained in the graph.
        /// </summary>
        /// <returns></returns>
        public List<int> Q__Vertices()
        {
            return neighbors__per__vertex.Keys.ToList().Q__Deep_Copy();
        }

        /// <summary>
        /// Returns a list of all the edges that are contained in the graph.
        /// </summary>
        /// <returns></returns>
        public List<Undirected_Edge> Q__Edges()
        {
            List<Undirected_Edge> edges = new List<Undirected_Edge>();

            foreach (int key in neighbors__per__vertex.Keys)
            {
                foreach (int value in neighbors__per__vertex[key])
                {
                    Undirected_Edge edge = new Undirected_Edge(key, value);
                    if (edges.Contains(edge) == false)
                        edges.Add(edge);
                }
            }

            return edges;
        }

        /// <summary>
        /// Returns all the possible edges, given the current set of vertices in the graph. 
        /// </summary>
        /// <returns></returns>
        public List<Undirected_Edge> Q__All_Possible_Edges()
        {
            List<int> vertices = Q__Vertices();
            int num_vertices = vertices.Count;

            List<Undirected_Edge> all_possible_edges = new List<Undirected_Edge>();

            for (int i = 0; i < num_vertices - 1; i++)
            {
                for (int j = i + 1; j < num_vertices; j++)
                {
                    Undirected_Edge edge = new Undirected_Edge(
                        vertices[i],
                        vertices[j]
                        );
                    all_possible_edges.Add(edge);
                }
            }

            return all_possible_edges;
        }


        /// <summary>
        /// Returns the graph's order.
        /// A graph's order is the number of vertices included in the graph.
        /// Definition-source: https://en.wikipedia.org/wiki/Glossary_of_graph_theory
        /// </summary>
        /// <returns></returns>
        public int Q__Order()
        {
            return Q__Num_Vertices();
        }

        /// <summary>
        /// Returns the number of vertices that are contained in the graph.
        /// </summary>
        /// <returns></returns>
        public int Q__Num_Vertices()
        {
            return neighbors__per__vertex.Count;
        }

        /// <summary>
        /// Returns the number of edges that are contained in the graph.
        /// </summary>
        /// <returns></returns>
        public int Q__Num_Edges()
        {
            return Q__Degree_Sum() / 2;
        }

        /// <summary>
        /// Returns the maximum possible number of edges, given the number of vertices.
        /// TESTED: []
        /// </summary>
        /// <returns></returns>
        public int Q__Num_All_Possible_Edges()
        {
            int num_vertices = Q__Num_Vertices();

            int sum = 0;
            int init_val = num_vertices;
            for (int i = 0; i < num_vertices; i++)
            {
                init_val -= 1;
                sum += init_val;
            }
            return init_val;
        }

        /// <summary>
        /// Returns the sum of the degree of all vertices.
        /// TESTED: []
        /// </summary>
        /// <returns></returns>
        public int Q__Degree_Sum()
        {
            int degree_sum = 0;
            foreach (int vertex in neighbors__per__vertex.Keys)
            {
                degree_sum += Q__Degree(vertex);
            }
            return degree_sum;
        }

        public double Q__Degree_Average()
        {
            int num_verts = Q__Num_Vertices();
            if (num_verts == 0.0)
            {
                return 0.0;
            }
            else
            {
                int degree_sum = Q__Degree_Sum();
                return (double)degree_sum / (double)num_verts;
            }
        }

        /// <summary>
        /// Returns the sum of the degree of the selected vertices.
        /// TESTED: []
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public int Q__Degree_Sum(List<int> vertices)
        {
            int degree_sum = 0;

            foreach (int vertex in vertices)
            {
                degree_sum += Q__Degree(vertex);
            }

            return degree_sum;
        }

        /// <summary>
        /// Returns the vertices that can be removed 
        /// without breaking the graph's connectivity.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<int> Q__Removable_Vertices()
        {
            List<int> all_vertex_ids = Q__Vertices();

            List<int> removable_vertices = new List<int>();
            foreach (int vertex_id in all_vertex_ids)
            {
                if (Q__Is_Removable(vertex_id))
                    removable_vertices.Add(vertex_id);
            }

            return removable_vertices;
        }



        public List<int> Q__Highest_Degree_Vertices()
        {
            List<int> all_vertices = Q__Vertices();
            return Q__Highest_Degree_Vertices(all_vertices);
        }

        public List<int> Q__Highest_Degree_Vertices(List<int> candidate_vertices)
        {
            int highest_centrality = -1;
            foreach (var v in candidate_vertices)
            {
                int v_centrality = Q__Degree(v);
                if (v_centrality > highest_centrality)
                    highest_centrality = v_centrality;
            }

            List<int> highest_centrality_vertices = new List<int>();
            foreach (var v in candidate_vertices)
            {
                int v_centrality = Q__Degree(v);
                if (v_centrality == highest_centrality)
                    highest_centrality_vertices.Add(v);
            }

            return highest_centrality_vertices;
        }

        #endregion



        #region constraints
        /// <summary>
        /// Returns whether the graph is planar.
        /// I.e. whether the graph can be drawn on a plane without intersecting edges.
        /// </summary>
        /// <returns></returns>
        public bool Q__Is_Planar()
        {
            int num_edges = Q__Num_Edges();
            int num_vertices = Q__Num_Vertices();

            // euler's formula:
            // v - e + f = 2
            // => f = 2 + e - v
            int num_faces = 2 + num_edges - num_vertices;



            throw new NotImplementedException();

        }

        /// <summary>
        /// reference: https://www.geeksforgeeks.org/detect-cycle-undirected-graph/?ref=lbp
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public bool Q__Is_Cyclic()
        {
            List<int> vertices = Q__Vertices();
            Dictionary<int, bool> visited = new Dictionary<int, bool>();
            foreach (int vertex in vertices)
            {
                visited.Add(vertex, false);
            }

            foreach (int vertex in vertices)
            {
                if (!visited[vertex])
                {
                    bool is_cyclic =
                        Q__Is_Cyclic__Util(vertex, visited, -1);

                    if (is_cyclic)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Utility for Is_Cyclic
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private bool Q__Is_Cyclic__Util(int vertex, Dictionary<int, bool> visited, int parent)
        {
            // Mark the current vertex as visited 
            visited[vertex] = true;

            List<int> neighbors = Q__Neighbors(vertex);
            foreach (int neighbor in neighbors)
            {
                // If a neighbor is not visited,  
                // then recur for that adjacent 
                if (!visited[neighbor])
                {
                    bool is_cyclic = Q__Is_Cyclic__Util(neighbor, visited, vertex);
                    if (is_cyclic)
                    {
                        return true;
                    }
                }

                // If a neighbor is visited and  
                // not parent of current vertex, 
                // then there is a cycle. 
                else if (neighbor != parent)
                {
                    return true;
                }
            }
            return false;
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

        /// <summary>
        /// Return whether all vertices of the graph are connected to all other vertices of the graph.
        /// </summary>
        /// <returns></returns>
        public bool Q__Is_Fully_Connected()
        {
            int num_vertices = Q__Num_Vertices();

            foreach (int n in neighbors__per__vertex.Keys)
            {
                if (neighbors__per__vertex[n].Count < num_vertices - 1)
                    return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Returns the neighbors of a set of vertices,
        /// excluding those vertices.
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public List<int> Q__Vertices_Neighbors_Exclusive(List<int> vertices)
        {
            List<int> vertices_neighbors_exclusive = new List<int>();
            foreach (var vertex in vertices)
            {
                List<int> vertex_neighbors = this.Q__Neighbors(vertex);

                // exclude the list of vertices
                vertex_neighbors.RemoveAll(x => vertices.Contains(x));

                // exclude the ones already added to the list
                vertex_neighbors.RemoveAll(x => vertices_neighbors_exclusive.Contains(x));

                // add the remaining ones to the list...
                vertices_neighbors_exclusive.AddRange(vertex_neighbors);
            }
            return vertices_neighbors_exclusive;
        }

        public bool Q__Contains_Edge(Undirected_Edge edge)
        {
            if (!neighbors__per__vertex.ContainsKey(edge.v1))
                return false;
            else
                return neighbors__per__vertex[edge.v1].Contains(edge.v2);
        }

        public bool Q__Contains_Edge(int v1, int v2)
        {
            if (!neighbors__per__vertex.ContainsKey(v1))
                return false;
            else
                return neighbors__per__vertex[v1].Contains(v2);
        }

        public bool Q__Contains_Vertex(int vertex)
        {
            return neighbors__per__vertex.Keys.Contains(vertex);
        }

        public DS__Undirected_Graph Q__Sub_Graph__Containing_Vertices(List<int> seleted_vertices)
        {
            Dictionary<int, List<int>> selected__neighbors_per_vertex = new Dictionary<int, List<int>>();

            foreach (var kvp in neighbors__per__vertex)
            {
                int vertex = kvp.Key;

                if (seleted_vertices.Contains(vertex))
                {
                    List<int> neighbors = kvp.Value.Q__Deep_Copy();
                    List<int> selected_neighbors =
                        neighbors
                        .FindAll(
                            x => seleted_vertices.Contains(x)
                            );

                    selected__neighbors_per_vertex.Add(vertex, selected_neighbors);
                }
                else
                {
                    continue;
                }
            }

            DS__Undirected_Graph sub_graph = new DS__Undirected_Graph(selected__neighbors_per_vertex);

            return sub_graph;
        }

        public List<List<int>> Q__Cycles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the graph's islands as lists of vertices.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<List<int>> Q__Vertex_Islands()
        {
            List<int> all_vertices = Q__Vertices();

            List<List<int>> islands = new List<List<int>>();

            while (all_vertices.Count > 0)
            {
                // select a vertex (does not matter which one)
                int reference_vertex = all_vertices[0];

                // find the vertices reachable from that vertex (island)
                List<int> island = Q__Reachable_Vertices(reference_vertex);

                // remove this island from the reference list
                all_vertices.RemoveAll(
                    x =>
                    island.Contains(x)
                    );

                // add the island to the islands list
                islands.Add(island);
            }

            return islands;
        }

        /// <summary>
        /// Returns the graph's islands as independent graphs.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<DS__Undirected_Graph> Q__Graph_Islands()
        {
            List<List<int>> vertex_islands = Q__Vertex_Islands();

            List<DS__Undirected_Graph> graph_islands = new List<DS__Undirected_Graph>();
            foreach (List<int> vertex_island in vertex_islands)
            {
                DS__Undirected_Graph graph_island = Q__Sub_Graph__Containing_Vertices(vertex_island);
                graph_islands.Add(graph_island);
            }

            return graph_islands;
        }

        /// <summary>
        /// Returns one of the possible shortest paths from a vertex to another one, 
        /// through a graph.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="root"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public List<int> Q__Shortest_Path_BFS(int root, int destination)
        {
            if (root.Equals(destination))
            {
                throw new System.ArgumentException("root equals destination");
            }

            Dictionary<int, int> predecessors = new Dictionary<int, int>();

            var vertices = Q__Vertices();

            foreach (var vertex in vertices)
            {
                predecessors.Add(vertex, vertex);
            }

            List<int> visited_vertices = new List<int>();

            Queue<int> search_queue = new Queue<int>();

            search_queue.Enqueue(root);
            visited_vertices.Add(root);
            bool foundDestination = false;

            while (search_queue.Count > 0 && foundDestination == false)
            {
                var current = search_queue.Dequeue();
                var neighbors = Q__Neighbors(current);
                List<int> unvisitedNeighbors = neighbors.FindAll(x => visited_vertices.Contains(x) == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    predecessors[neighbor] = current;
                    search_queue.Enqueue(neighbor);
                    visited_vertices.Add(neighbor);

                    if (neighbor.Equals(destination))
                    {
                        foundDestination = true;
                        break;
                    }
                }
            }

            List<int> shortestPath = new List<int>();

            bool path_finished = false;
            var current_path_position = destination;
            shortestPath.Add(current_path_position);
            while (path_finished == false)
            {
                var predecessor = predecessors[current_path_position];
                if (predecessor.Equals(current_path_position) == false)
                {
                    shortestPath.Add(predecessor);
                    current_path_position = predecessor;
                }
                else
                {
                    path_finished = true;
                }
            }

            shortestPath.Reverse();

            if (
                shortestPath.Contains(root)
                &&
                shortestPath.Contains(destination)
                )
            {
                return shortestPath;
            }

            return new List<int>();
        }

        /// <summary>
        /// Returns one of the possible shortest paths from a root vertex
        /// to a list of possible destinations, through a graph.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="root"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public List<int> Q__Shortest_Path_BFS(int root, List<int> possible_destinations)
        {
            if (possible_destinations.Contains(root))
            {
                return new List<int>() { root };
            }

            Dictionary<int, int> predecessors = new Dictionary<int, int>();

            var all_vertex_ids = Q__Vertices();

            foreach (var vertex_id in all_vertex_ids)
            {
                predecessors.Add(vertex_id, vertex_id);
            }

            List<int> visited_vertices = new List<int>();

            Queue<int> search_queue = new Queue<int>();

            search_queue.Enqueue(root);
            visited_vertices.Add(root);
            bool destination_was_discovered = false;
            int discovered_destination = -1;
            while (search_queue.Count > 0 && destination_was_discovered == false)
            {
                var current = search_queue.Dequeue();
                var neighbors = Q__Neighbors(current);
                List<int> unvisitedNeighbors = neighbors.FindAll(x => visited_vertices.Contains(x) == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    predecessors[neighbor] = current;
                    search_queue.Enqueue(neighbor);
                    visited_vertices.Add(neighbor);

                    if (possible_destinations.Contains(neighbor))
                    {
                        discovered_destination = neighbor;
                        destination_was_discovered = true;
                        break;
                    }
                }
            }

            if (discovered_destination == -1)
            {
                return new List<int>();
            }

            List<int> shortest_path = new List<int>();

            bool pathFinished = false;
            var current_path_position = discovered_destination;
            shortest_path.Add(current_path_position);
            while (pathFinished == false)
            {
                var predecessor = predecessors[current_path_position];
                if (predecessor.Equals(current_path_position) == false)
                {
                    shortest_path.Add(predecessor);
                    current_path_position = predecessor;
                }
                else
                {
                    pathFinished = true;
                }
            }

            shortest_path.Reverse();

            if (
                shortest_path.Contains(root)
                &&
                shortest_path.Contains(discovered_destination)
                )
            {
                return shortest_path;
            }

            return new List<int>();
        }
    }
}
