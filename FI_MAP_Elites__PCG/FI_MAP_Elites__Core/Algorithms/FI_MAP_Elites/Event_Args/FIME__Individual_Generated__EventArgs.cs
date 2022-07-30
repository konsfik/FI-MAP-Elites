using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class FIME__Individual_Generated__EventArgs<T> : FIME__Event_Args<T>
        where T : Data_Structure
    {
        public T individual;
        public FIME__Location location;
        public FIME__Placement_Result placement_result;
        public double[] feature_vector;
        public double fitness;

        public FIME__Individual_Generated__EventArgs(
            FI_MAP_Elites<T> cmce_algorithm,
            string message,

            T individual,
            FIME__Location location,
            FIME__Placement_Result placement_result,
            double[] feature_vector,
            double fitness
            ):base(
                cmce_algorithm, 
                message
                )
        {
            this.individual = (T)individual.Q__Deep_Copy();
            this.location = location;
            this.placement_result = placement_result;
            this.feature_vector = feature_vector.Q__Deep_Copy();
            this.fitness = fitness;
        }
    }
}
