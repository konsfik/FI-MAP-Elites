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
        /// Returns the shortest path between root and destination vertices.
        /// If destination is unreachable from root, it will return an empty list and success will be false.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="destination"></param>
        /// <param name="success"></param>
        /// <param name="shortest_path"></param>
        public void Q__Shortest_Path(
            int root,
            int destination,
            out bool success,
            out List<int> shortest_path
            )
        {
            Q__Prims_Minimum_Spanning_Tree(
                root,
                out Dictionary<int, int> parent_per_vertex,
                out Dictionary<int, double> min_distance_per_vertex
                );

            if (parent_per_vertex.Keys.Contains(root) == false)
            {
                success = false;
                shortest_path = new List<int>();
                return;
            }
            if (parent_per_vertex.Keys.Contains(destination) == false)
            {
                success = false;
                shortest_path = new List<int>();
                return;
            }

            shortest_path = new List<int>() { destination };
            bool done = false;
            while (!done)
            {
                int parent = parent_per_vertex[shortest_path.Last()];
                if (parent == root)
                {
                    shortest_path.Add(parent);
                    done = true;
                }
                else if (parent == -1)
                {
                    success = false;
                    shortest_path = new List<int>();
                    return;
                }
                else
                {
                    shortest_path.Add(parent);
                }
            }
            success = true;
            shortest_path.Reverse();
        }

        public void Q__Shortest_Path__Distance(
            int root,
            int destination,
            out bool success,
            out double distance
            )
        {
            Q__Prims_Minimum_Spanning_Tree(
                root,
                out Dictionary<int, int> parent_per_vertex,
                out Dictionary<int, double> min_distance_per_vertex
                );

            double dist = min_distance_per_vertex[destination];

            if (double.IsInfinity(dist))
            {
                success = false;
                distance = double.NaN;
            }
            else
            {
                success = true;
                distance = dist;
            }
        }

        /// <summary>
        /// Returns the maximum shortest path that can be found in the graph.
        /// this is also known as the graph's Diameter.
        /// </summary>
        /// <returns></returns>
        public double Q__Maximum_Shortest_Path_Length()
        {
            bool is_connected = Q__Is_Connected();

            if (is_connected)
            {
                return Q__Maximum_Shortest_Path_Length__Unsafe();
            }
            else
            {
                double max = double.NegativeInfinity;
                var islands = Q__Islands__Graphs();
                foreach (var island in islands)
                {
                    double this_max =
                        Q__Maximum_Shortest_Path_Length__Unsafe();

                    if (this_max > max)
                    {
                        max = this_max;
                    }
                }
                return max;
            }
        }

        /// <summary>
        /// This method assumes that the graph is connected!
        /// Make sure to make this check beforehand.
        /// </summary>
        /// <returns></returns>
        private double Q__Maximum_Shortest_Path_Length__Unsafe()
        {
            List<int> verts = Q__Vertices();

            double max_dist = double.NegativeInfinity;

            foreach (int v in verts)
            {
                Q__Maximum_Shortest_Path_Length_From_Root(
                    root: v,
                    out bool success,
                    out double max_len,
                    out int max_len_vert
                    );

                if (success == false) continue;

                if (max_len > max_dist)
                {
                    max_dist = max_len;
                }
            }

            return max_dist;
        }

        public double Q__Maximum_Shortest_Path_Length__Fast_Approximation(double epsilon)
        {
            bool is_connected = Q__Is_Connected();

            if (is_connected)
            {
                return Q__Maximum_Shortest_Path_Length__Unsafe__Fast_Approximation(epsilon);
            }
            else
            {
                double max = double.NegativeInfinity;
                var islands = Q__Islands__Graphs();
                foreach (var island in islands)
                {
                    double this_max =
                        Q__Maximum_Shortest_Path_Length__Unsafe__Fast_Approximation(epsilon);

                    if (this_max > max)
                    {
                        max = this_max;
                    }
                }
                return max;
            }
        }

        /// <summary>
        /// This method assumes that the graph is connected!
        /// Make sure to make this check beforehand.
        /// </summary>
        /// <returns></returns>
        private double Q__Maximum_Shortest_Path_Length__Unsafe__Fast_Approximation(double epsilon)
        {
            List<int> verts = Q__Vertices();

            double previous_max_distance = double.NegativeInfinity;
            int root = verts[0];
            for (int i = 0; i < 100; i++)
            {
                Q__Maximum_Shortest_Path_Length_From_Root(
                    root,
                    out bool success,
                    out double this_maximum_distance,
                    out int maximul_length_vertex
                    );

                if (success == false)
                {
                    throw new Exception("disconnected graph... this should not happen");
                }

                if (this_maximum_distance.Q__Approximately_Equal(previous_max_distance, epsilon))
                {
                    return this_maximum_distance;
                }
                else if (this_maximum_distance > previous_max_distance)
                {
                    previous_max_distance = this_maximum_distance;
                    root = maximul_length_vertex;
                }
                else
                {
                    throw new Exception("bug.. this should not happen");
                }
            }

            return previous_max_distance;
        }

        /// <summary>
        /// Returns the maximum possible shortest path length from a root node.
        /// It also returns the index of the furthest vertex.
        /// The method completely ignores unreachable vertices.
        /// If there is no vertex connected to the root vertex, the method will return NaN and -1.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="success"></param>
        /// <param name="maximum_length"></param>
        /// <param name="maximum_length_vertex"></param>
        public void Q__Maximum_Shortest_Path_Length_From_Root(
            int root,
            out bool success,
            out double maximum_length,
            out int maximum_length_vertex
            )
        {
            Q__Prims_Minimum_Spanning_Tree(
                root,
                out Dictionary<int, int> parent_per_vertex,
                out Dictionary<int, double> min_distance__per__vertex
                );

            List<double> valid_distances = new List<double>();
            foreach (var kvp in min_distance__per__vertex)
            {
                double dist = kvp.Value;
                if (double.IsNaN(dist))
                    continue;
                if (double.IsPositiveInfinity(dist))
                    continue;
                if (double.IsNegativeInfinity(dist))
                    continue;
                valid_distances.Add(dist);
            }

            if (valid_distances.Count == 0)
            {
                success = false;
                maximum_length = double.NaN;
                maximum_length_vertex = -1;
            }
            else
            {
                success = true;
                double max_len = valid_distances.Max();
                maximum_length = max_len;
                maximum_length_vertex = min_distance__per__vertex.First(x => x.Value == max_len).Key;
            }

        }

        /// <summary>
        /// Given a root vertex, the algorithm returns two dictionaries:
        /// The parent__per__vertex dictionary stores the parent per vertex, 
        /// while the min_distance__per__vertex stores the minimum distance per vertex, fromt the root vertex.
        /// If any vertex is unreachable from the root, then its parent will be -1 and its min_distance will be infinity.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="parent__per__vertex"></param>
        /// <param name="min_distance__per__vertex"></param>
        public void Q__Prims_Minimum_Spanning_Tree(
            int root,
            out Dictionary<int, int> parent__per__vertex,
            out Dictionary<int, double> min_distance__per__vertex
            )
        {
            List<int> all_verts = Q__Vertices();

            int num_verts = all_verts.Count;

            parent__per__vertex = new Dictionary<int, int>();
            min_distance__per__vertex = new Dictionary<int, double>(); // key

            Dictionary<int, bool> mst_set = new Dictionary<int, bool>();

            foreach (var v in all_verts)
            {
                min_distance__per__vertex.Add(v, double.PositiveInfinity);
                mst_set.Add(v, false);
                parent__per__vertex.Add(v, -1);
            }

            min_distance__per__vertex[root] = 0.0;
            parent__per__vertex[root] = -1;

            // Find shortest path for all vertices
            for (int count = 0; count < num_verts - 1; count++)
            {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minKey(
                    min_distance__per__vertex,
                    mst_set,
                    all_verts
                    );

                if (u == -1) continue;

                // Mark the picked vertex as processed
                mst_set[u] = true;


                // Update dist value of the adjacent
                // vertices of the picked vertex.
                foreach (int v in all_verts)
                {

                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]

                    bool not_in_spt = (mst_set[v] == false);
                    bool connection_exists = Q__Connection_Exists__Fast(u, v);

                    if (
                        not_in_spt
                        &&
                        connection_exists
                        &&
                        min_distance__per__vertex[u] != double.PositiveInfinity
                        )
                    {
                        double cwf = Q__Connection_Weight__Fast(u, v);

                        if (min_distance__per__vertex[u] + cwf < min_distance__per__vertex[v])
                        {
                            parent__per__vertex[v] = u;
                            min_distance__per__vertex[v] = min_distance__per__vertex[u] + cwf;
                        }

                    }
                }
            }
        }

        static int minKey(
            Dictionary<int, double> key,
            Dictionary<int, bool> mstSet,
            List<int> all_verts
            )
        {

            // Initialize min value
            double min = double.PositiveInfinity;
            int min_index = -1;

            foreach (int v in all_verts)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }
            }

            return min_index;
        }

        private bool Q__Connection_Exists__Fast(int v1, int v2)
        {
            //if (Q__Vertex_Exists(v1) == false)
            //{
            //    return false;
            //}
            //else
            //{
            //    return weight_per_neighbor_per_vertex[v1].ContainsKey(v2);
            //}
            return weight_per_neighbor_per_vertex[v1].ContainsKey(v2);
        }

        public double Q__Connection_Weight__Fast(int v1, int v2)
        {
            return weight_per_neighbor_per_vertex[v1][v2];
        }
    }
}
