using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Constraint_Evaluation_Method<T>:I_Deep_Copyable
        where T : Data_Structure
    {
        public abstract bool Q__Satisfied(T individual);

        public virtual string Name()
        {
            return this.GetType().Name;
        }

        public abstract object Q__Deep_Copy();
    }
}
