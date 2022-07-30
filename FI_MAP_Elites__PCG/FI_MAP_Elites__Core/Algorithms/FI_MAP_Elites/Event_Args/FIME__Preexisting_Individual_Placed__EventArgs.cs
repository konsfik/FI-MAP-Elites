using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class FIME__Preexisting_Individual_Placed__EventArgs<T> : FIME__Event_Args<T>
        where T : Data_Structure
    {
        public FIME__Location preexisting_individual_location;
        public FIME__Placement_Result placement_result;
        public double[] feature_vector;

        public FIME__Preexisting_Individual_Placed__EventArgs(
            FI_MAP_Elites<T> cmce_algorithm,
            string message,

            FIME__Location preexisting_individual_location,
            FIME__Placement_Result placement_result,
            double[] feature_vector
            ) : base(
                cmce_algorithm,
                message
                )
        {
            this.preexisting_individual_location = preexisting_individual_location;
            this.placement_result = placement_result;
            this.feature_vector = feature_vector.Q__Deep_Copy();
        }
    }
}
