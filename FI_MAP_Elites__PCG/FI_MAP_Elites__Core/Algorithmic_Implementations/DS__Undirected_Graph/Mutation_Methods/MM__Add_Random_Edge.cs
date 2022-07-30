using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Add_Random_Edge : Mutation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new MM__Add_Random_Edge();
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            List<Undirected_Edge> all_possible_edges = individual.Q__All_Possible_Edges();
            List<Undirected_Edge> existing_edges = individual.Q__Edges();

            List<Undirected_Edge> non_existent_edges = new List<Undirected_Edge>(all_possible_edges);
            non_existent_edges
            .RemoveAll(
                x =>
                existing_edges.Contains(x)
                );

            Undirected_Edge random_edge = non_existent_edges.Q__Random_Item(rand);
            individual.M__Add_Edge(random_edge);
        }
    }
}
