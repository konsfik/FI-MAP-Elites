using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public abstract class Parent_Selection_Method<T> :I_Deep_Copyable
        where T: Data_Structure
    {
        public abstract object Q__Deep_Copy();
        public abstract FIME__Location Select__Parent_Location(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm
            );
    }
}
