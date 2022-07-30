using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Mutation<T>
        where T : Data_Structure
    {
        public abstract T Generate_Mutated_Individual(T parent);
    }
}
