using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Prescription<T>:Data_Structure
        where T:Data_Structure
    {
        public abstract List<Constraint_Evaluation_Method<T>> Q__Constraints();
    }
}
