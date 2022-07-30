using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class GM__Graph_Generator : Generation_Method<DS__Undirected_Graph>
    {
        public readonly int min_num_verts;
        public readonly int max_num_verts;
        public readonly double edge_percentage;

        public GM__Graph_Generator(
            int min_num_verts,
            int max_num_verts,
            double active_edges_probability
            )
        {
            if (min_num_verts < 1)
                throw new System.Exception("minimum_number_of_nodes must be greater than zero");

            if (max_num_verts <= min_num_verts)
                throw new System.Exception("maximum_number_of_nodes must be greater than minimum_number_of_nodes");

            if (active_edges_probability < 0.0)
                throw new System.Exception("edge_percentage must be >= 0.0");

            if (active_edges_probability > 1.0)
                throw new System.Exception("edge_percentage must be <= 1.0");

            this.min_num_verts = min_num_verts;
            this.max_num_verts = max_num_verts;
            this.edge_percentage = active_edges_probability;
        }

        private GM__Graph_Generator(GM__Graph_Generator gm_to_copy)
        {
            this.min_num_verts = gm_to_copy.min_num_verts;
            this.max_num_verts = gm_to_copy.max_num_verts;
            this.edge_percentage = gm_to_copy.edge_percentage;
        }

        public override object Q__Deep_Copy()
        {
            return new GM__Graph_Generator(this);
        }

        public override DS__Undirected_Graph Generate_Individual(I_PRNG rand)
        {
            DS__Undirected_Graph graph = new DS__Undirected_Graph();

            int number_of_nodes = rand.Next(min_num_verts, max_num_verts + 1);
            for (int n = 0; n < number_of_nodes; n++)
                graph.M__Add_Vertex(n);

            List<Undirected_Edge> all_possible_edges = graph.Q__All_Possible_Edges();
            foreach (Undirected_Edge edge in all_possible_edges)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll <= edge_percentage)
                    graph.M__Add_Edge(edge);
            }

            return graph;
        }
    }
}
