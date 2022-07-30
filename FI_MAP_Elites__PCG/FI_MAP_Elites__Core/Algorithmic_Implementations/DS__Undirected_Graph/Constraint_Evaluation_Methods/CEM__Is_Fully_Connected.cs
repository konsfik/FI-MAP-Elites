using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class CEM__Is_Fully_Connected : Constraint_Evaluation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new CEM__Is_Fully_Connected();
        }

        public override bool Q__Satisfied(DS__Undirected_Graph undirected_graph)
        {
            return undirected_graph.Q__Is_Fully_Connected();
        }
    }
}
