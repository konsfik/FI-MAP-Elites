using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class EM__Percent_Leaf_Vertices : Evaluation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__Percent_Leaf_Vertices();
        }

        public override double Evaluate_Individual(DS__Undirected_Graph undirected_graph)
        {
            int num_verts = undirected_graph.Q__Num_Vertices();

            if (num_verts == 0)
            {
                return 0.0;
            }
            else
            {
                int num_leaf_verts = 0;
                foreach (int v in undirected_graph.neighbors__per__vertex.Keys)
                {
                    if (undirected_graph.Q__Is_Leaf(v))
                        num_leaf_verts++;
                }
                return (double)num_leaf_verts / (double)num_verts;
            }
        }
    }
}
