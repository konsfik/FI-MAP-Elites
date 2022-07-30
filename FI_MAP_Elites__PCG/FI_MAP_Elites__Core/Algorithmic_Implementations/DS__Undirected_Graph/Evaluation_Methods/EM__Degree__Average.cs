using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class EM__Degree__Average : Evaluation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__Degree__Average();
        }

        public override double Evaluate_Individual(DS__Undirected_Graph individual)
        {
            return individual.Q__Degree_Average();
        }
    }
}
