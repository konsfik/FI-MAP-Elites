using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public static class FIME__Score_Keeping_Types
    {
        public static FIME__Offspring_Survival__Score_Keeping_Type Type_1__ALL()
        {
            return
                FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_FEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_INFEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_FEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_INFEASIBLE;
        }

        public static FIME__Offspring_Survival__Score_Keeping_Type Type_2__NO_FI()
        {
            return
                FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_FEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_FEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_INFEASIBLE;
        }

        public static FIME__Offspring_Survival__Score_Keeping_Type Type_3__ONLY_TO_F()
        {
            return
                FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_FEASIBLE
                |
                FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_FEASIBLE;
        }
    }
}
