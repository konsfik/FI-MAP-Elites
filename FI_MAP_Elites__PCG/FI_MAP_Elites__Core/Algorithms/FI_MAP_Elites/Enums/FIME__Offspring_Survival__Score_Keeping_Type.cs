using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    [Flags]
    public enum FIME__Offspring_Survival__Score_Keeping_Type
    {
        NONE = 0,
        FEASIBLE_TO_FEASIBLE = 1,
        FEASIBLE_TO_INFEASIBLE = 2,
        INFEASIBLE_TO_INFEASIBLE = 4,
        INFEASIBLE_TO_FEASIBLE = 8
    }
}
