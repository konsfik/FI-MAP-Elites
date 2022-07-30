using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public abstract class Single_Space_Unit_Placement_Method<T>:I_Deep_Copyable
        where T: DS__Evolvable_Geometry
    {
        public abstract bool Place_Space_Unit(
            I_PRNG rand,
            T individual,
            int room_id,
            bool recalculate_phenotype
            );

        public abstract object Q__Deep_Copy();
    }
}
