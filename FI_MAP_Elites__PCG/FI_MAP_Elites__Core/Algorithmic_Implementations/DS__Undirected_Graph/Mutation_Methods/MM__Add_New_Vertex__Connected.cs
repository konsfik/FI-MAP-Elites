﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class MM__Add_New_Vertex__Connected : Mutation_Method<DS__Undirected_Graph>
    {
        public readonly int absolute_max_verts;

        public MM__Add_New_Vertex__Connected(
            int absolute_max_verts
            )
        {
            this.absolute_max_verts = absolute_max_verts;
        }

        public MM__Add_New_Vertex__Connected(MM__Add_New_Vertex__Connected mm_to_copy)
        {
            this.absolute_max_verts = mm_to_copy.absolute_max_verts;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Add_New_Vertex__Connected(this);
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Undirected_Graph individual)
        {
            int current_num_verts = individual.Q__Num_Vertices();

            if (current_num_verts >= absolute_max_verts)
                return;

            List<int> existing_verts = individual.Q__Vertices();
            int selected_vertex = existing_verts.Q__Random_Item(rand);

            int max_vertex_id = individual.neighbors__per__vertex.Keys.Max();
            int new_vertex_id = max_vertex_id + 1;

            individual.M__Add_Edge(selected_vertex, new_vertex_id);
        }
    }
}
