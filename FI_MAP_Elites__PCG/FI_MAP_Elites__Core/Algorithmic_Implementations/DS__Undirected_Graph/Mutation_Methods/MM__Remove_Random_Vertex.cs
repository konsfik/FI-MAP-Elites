using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Remove_Random_Vertex : Mutation_Method<DS__Undirected_Graph>
    {
        public readonly int absolute_min_verts;

        public MM__Remove_Random_Vertex(
            int absolute_min_verts
            )
        {
            if (absolute_min_verts < 0)
                throw new Exception("absolute_min_verts must be >= 0");

            this.absolute_min_verts = absolute_min_verts;
        }

        private MM__Remove_Random_Vertex(MM__Remove_Random_Vertex mm_to_copy)
        {
            this.absolute_min_verts = mm_to_copy.absolute_min_verts;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Remove_Random_Vertex(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            int current_num_verts = individual.Q__Num_Vertices();

            if (current_num_verts <= absolute_min_verts)
                return;

            int max_vertex_id = individual.neighbors__per__vertex.Keys.Max();
            int new_vertex_id = max_vertex_id + 1;
            individual.M__Add_Vertex(new_vertex_id);
        }


    }
}
