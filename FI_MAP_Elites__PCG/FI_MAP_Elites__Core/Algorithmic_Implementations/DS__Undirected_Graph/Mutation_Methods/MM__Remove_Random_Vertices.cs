using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Remove_Random_Vertices : Mutation_Method<DS__Undirected_Graph>
    {
        public readonly int absolute_min_verts;
        public readonly int min_verts_to_remove;
        public readonly int max_verts_to_remove;

        public MM__Remove_Random_Vertices(
            int absolute_min_verts,
            int min_verts_to_remove,
            int max_verts_to_remove
            )
        {
            if (absolute_min_verts < 0)
                throw new Exception("absolute_min_verts must be >= 0");
            if (min_verts_to_remove <= 0)
                throw new System.Exception("min_verts_to_remove must be >=1");
            if (max_verts_to_remove <= min_verts_to_remove)
                throw new System.Exception("max_verts_to_remove must be > min_verts_to_remove");

            this.absolute_min_verts = absolute_min_verts;
            this.min_verts_to_remove = min_verts_to_remove;
            this.max_verts_to_remove = max_verts_to_remove;
        }

        private MM__Remove_Random_Vertices(MM__Remove_Random_Vertices mm_to_copy)
        {
            this.absolute_min_verts = mm_to_copy.absolute_min_verts;
            this.min_verts_to_remove = mm_to_copy.min_verts_to_remove;
            this.max_verts_to_remove = mm_to_copy.max_verts_to_remove;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Remove_Random_Vertices(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            int current_num_verts = individual.Q__Num_Vertices();
            int num_verts_to_remove = rand.Next(min_verts_to_remove, max_verts_to_remove + 1);
            int remaining_removals = current_num_verts - absolute_min_verts;

            if (remaining_removals <= 0)
            {
                return;
            }
            else if (remaining_removals < num_verts_to_remove)
            {
                num_verts_to_remove = remaining_removals;
            }

            for (int i = 0; i < num_verts_to_remove; i++)
            {
                List<int> existing_nodes = individual.Q__Vertices();
                int selected_vertex_id = existing_nodes.Q__Random_Item(rand);
                individual.M__Remove_Vertex(selected_vertex_id);
            }
        }
    }
}
