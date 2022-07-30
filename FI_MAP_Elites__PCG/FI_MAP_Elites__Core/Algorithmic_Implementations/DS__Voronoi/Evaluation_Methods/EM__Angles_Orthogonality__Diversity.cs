using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__Angles_Orthogonality__Diversity : Evaluation_Method<DS__Voronoi>
    {
        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            return individual.Q__Eval__Angles_Orthogonality__Diversity();
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Angles_Orthogonality__Diversity();
        }
    }
}
