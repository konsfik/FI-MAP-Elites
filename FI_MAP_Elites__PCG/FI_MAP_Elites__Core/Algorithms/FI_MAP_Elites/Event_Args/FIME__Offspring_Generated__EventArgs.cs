using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class FIME__Offspring_Generated__EventArgs<T> : FIME__Event_Args<T>
        where T : Data_Structure
    {
        public T offspring;

        public FIME__Location parent_location;
        public double[] parent_feature_vector;
        public double parent_fitness;

        public FIME__Placement_Result placement_result;

        public FIME__Location offspring_location;
        public double[] offspring_feature_vector;
        public double offspring_fitness;

        public FIME__Offspring_Generated__EventArgs(
            FI_MAP_Elites<T> cmce_algorithm,
            string message,

            T offspring,
            FIME__Location parent_location,
            double[] parent_feature_vector,
            double parent_fitness,
            FIME__Placement_Result placement_result,
            FIME__Location offspring_location,
            double[] offspring_feature_vector,
            double offspring_fitness
            ):base(
                cmce_algorithm,
                message
                )
        {
            this.offspring = (T)offspring.Q__Deep_Copy();
            this.parent_location = parent_location;
            this.parent_feature_vector = parent_feature_vector.Q__Deep_Copy();
            this.parent_fitness = parent_fitness;
            this.placement_result = placement_result;
            this.offspring_location = offspring_location;
            this.offspring_feature_vector = offspring_feature_vector.Q__Deep_Copy();
            this.offspring_fitness = offspring_fitness;
        }
    }
}
