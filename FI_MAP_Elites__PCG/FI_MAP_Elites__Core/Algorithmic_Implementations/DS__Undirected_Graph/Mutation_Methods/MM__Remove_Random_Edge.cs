using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Remove_Random_Edge : Mutation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new MM__Remove_Random_Edge();
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            List<Undirected_Edge> all_edges = individual.Q__Edges();
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
