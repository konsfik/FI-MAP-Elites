using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__Cells_Area__Simplistic_Variance__Normalized : Evaluation_Method<DS__Voronoi>
    {
        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            return individual.Q__Eval__Cells_Area__Simplistic_Variance__Normalized();
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Cells_Area__Simplistic_Variance__Normalized();
        }
    }
}
