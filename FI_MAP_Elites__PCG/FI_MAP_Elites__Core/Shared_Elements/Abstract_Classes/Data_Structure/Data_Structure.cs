using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Data_Structure : I_Deep_Copyable
    {
        public abstract object Q__Deep_Copy();
    }
}
