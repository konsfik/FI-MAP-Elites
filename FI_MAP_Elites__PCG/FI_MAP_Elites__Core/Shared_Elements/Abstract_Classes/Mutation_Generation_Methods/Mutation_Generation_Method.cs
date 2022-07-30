using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Mutation_Generation_Method<T>
        where T: Data_Structure
    {
        public abstract Mutation<T> Generate_Mutation(
            I_PRNG rand, 
            T parent
            );
    }
}
