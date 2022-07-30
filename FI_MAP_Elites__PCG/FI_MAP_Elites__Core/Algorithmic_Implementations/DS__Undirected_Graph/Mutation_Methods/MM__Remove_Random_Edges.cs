using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Remove_Random_Edges : Mutation_Method<DS__Undirected_Graph>
    {
        public readonly double mutation_rate;
        public readonly bool at_least_one;

        public MM__Remove_Random_Edges(
            double mutation_rate,
            bool at_least_one
            )
        {
            if (mutation_rate < 0.0)
                throw new System.Exception("mutation rate must be > 0");
            if (mutation_rate > 1.0)
                throw new System.Exception("mutation rate must be < 1");

            this.mutation_rate = mutation_rate;
            this.at_least_one = at_least_one;
        }

        private MM__Remove_Random_Edges(MM__Remove_Random_Edges mm_to_copy)
        {
            this.mutation_rate = mm_to_copy.mutation_rate;
            this.at_least_one = mm_to_copy.at_least_one;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Remove_Random_Edges(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            List<Undirected_Edge> all_edges = individual.Q__Edges();

            bool at_least_one_removed = false;

            foreach (Undirected_Edge edge in all_edges)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll < mutation_rate)
                {
                    individual.M__Remove_Edge(edge);
                    at_least_one_removed = true;
                }
            }

            if (
                at_least_one_removed == false
                &&
                at_least_one == true
                )
            {
                if (all_edges.Count == 0)
                {
                    return;
                }
                else
                {
                    Undirected_Edge random_edge = all_edges.Q__Random_Item(rand);
                    individual.M__Remove_Edge(random_edge);
                }
            }
        }
    }
}
