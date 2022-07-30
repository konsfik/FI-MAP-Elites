using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    /// <summary>
    /// Evaluates an individual based on how many of the prescribed rooms
    /// actually exist in the solution.
    /// The value range of this evaluation score is between 0 and 1.
    /// </summary>
    public class EM__CEM__L2__Space_Units_Exist<T> : Evaluation_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new EM__CEM__L2__Space_Units_Exist<T>();
        }

        public override double Evaluate_Individual(T individual)
        {
            return individual.Q__EM__CEM__Space_Units_Exist();
        }
    }
}
