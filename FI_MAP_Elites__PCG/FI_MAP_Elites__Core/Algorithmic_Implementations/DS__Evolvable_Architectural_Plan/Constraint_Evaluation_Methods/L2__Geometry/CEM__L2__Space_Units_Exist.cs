using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CEM__L2__Space_Units_Exist<T> : Constraint_Evaluation_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new CEM__L2__Space_Units_Exist<T>();
        }

        public override bool Q__Satisfied(T individual)
        {
            List<int> prescribed_space_units = individual.Q__Prescribed_Space_Units();

            foreach (int space_unit in prescribed_space_units)
            {
                if (individual.Q__Space_Unit_Exists(space_unit) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
