using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class CEM__Is_Connected : Constraint_Evaluation_Method<DS__Voronoi>
    {

        public override object Q__Deep_Copy()
        {
            return new CEM__Is_Connected();
        }

        public override bool Q__Satisfied(DS__Voronoi individual)
        {
            return individual.connectivity_graph.Q__Is_Connected();
        }
    }
}
