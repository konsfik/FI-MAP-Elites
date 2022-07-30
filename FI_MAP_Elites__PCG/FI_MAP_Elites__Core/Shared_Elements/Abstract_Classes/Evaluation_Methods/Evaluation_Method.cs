using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Evaluation_Method<T> : I_Deep_Copyable
        where T : Data_Structure
    {
        public abstract object Q__Deep_Copy();

        public abstract double Evaluate_Individual(T individual);

        public virtual string Name()
        {
            return this.GetType().Name;
        }
    }
}
