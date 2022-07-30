using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public abstract class Space_Unit_Selection_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public abstract int Select_Space_Unit(
            I_PRNG rand,
            T individual,
            List<int> visited_rooms
            );
    }
}
