using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Crossover_Method<T>:I_Deep_Copyable
        where T : Data_Structure
    {
        public abstract T Crossover_Parents(
            I_PRNG rand,
            T parent_1,
            T parent_2
            );

        public abstract object Q__Deep_Copy();
    }
}
