using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CEM__L1__Voronoi_Connected<T> : Constraint_Evaluation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new CEM__L1__Voronoi_Connected<T>();
        }

        public override bool Q__Satisfied(T individual)
        {
            return individual.Q__CEM__L1__Voronoi_Connected();
        }
    }
}
