using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    /// <summary>
    /// Expands an existing room, until it reaches its prescribed area.
    /// </summary>
    public abstract class Space_Unit_Expansion_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public abstract bool Expand_Space_Unit(
            I_PRNG rand,
            T individual,
            int room_id,
            bool recalculate_phenotype
            );
    }
}
