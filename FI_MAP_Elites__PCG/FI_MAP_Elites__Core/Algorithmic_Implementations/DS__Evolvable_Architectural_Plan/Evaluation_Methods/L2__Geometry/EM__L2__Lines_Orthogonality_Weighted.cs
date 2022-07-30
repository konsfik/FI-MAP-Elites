using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__L2__Lines_Orthogonality_Weighted<T> : Evaluation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new EM__L2__Lines_Orthogonality_Weighted<T>();
        }

        public override double Evaluate_Individual(T individual)
        {
            return individual.Q__BC__Lines_Orthogonality_Weighted();
        }
    }
}
