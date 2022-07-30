using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Add_New_Vertices__Disconnected : Mutation_Method<DS__Undirected_Graph>
    {
        public readonly int absolute_max_verts;
        public readonly int min_verts_to_add;
        public readonly int max_verts_to_add;

        public MM__Add_New_Vertices__Disconnected(
            int absolute_max_verts,
            int min_verts_to_add,
            int max_verts_to_add
            )
        {
            if (absolute_max_verts < 1)
                throw new System.Exception("absolute max vertices must be >= 1");
            if (min_verts_to_add <= 0)
                throw new System.Exception("min_num_nodes must be at least 1");
            if (max_verts_to_add <= min_verts_to_add)
                throw new System.Exception("max_num_nodes must be greater than min_num_nodes");

            this.absolute_max_verts = absolute_max_verts;
            this.min_verts_to_add = min_verts_to_add;
            this.max_verts_to_add = max_verts_to_add;
        }

        private MM__Add_New_Vertices__Disconnected(MM__Add_New_Vertices__Disconnected mm_to_copy)
        {
            this.absolute_max_verts = mm_to_copy.absolute_max_verts;
            this.min_verts_to_add = mm_to_copy.min_verts_to_add;
            this.max_verts_to_add = mm_to_copy.max_verts_to_add;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Add_New_Vertices__Disconnected(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            int current_num_verts = individual.Q__Num_Vertices();
            int num_verts_to_add = rand.Next(min_verts_to_add, max_verts_to_add + 1);

            int remaining_additions = max_verts_to_add - current_num_verts;

            if (remaining_additions <= 0)
            {
                return;
            }
            else if (remaining_additions < num_verts_to_add)
            {
                num_verts_to_add = remaining_additions;
            }

            for (int i = 0; i < num_verts_to_add; i++)
            {
                int max_node_id = individual.neighbors__per__vertex.Keys.Max();
                int new_node_id = max_node_id + 1;
                individual.M__Add_Vertex(new_node_id);
            }
        }
    }
}
