using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FI_MAP_Elites<T>
        where T : Data_Structure
    {
        public event EventHandler<FIME__Preexisting_Individual_Placed__EventArgs<T>> event__preexisting_individual_placed;
        public event EventHandler<FIME__Individual_Generated__EventArgs<T>> event__individual_generated;
        public event EventHandler<FIME__Offspring_Generated__EventArgs<T>> event__offspring_generated;

        private void Invoke_Event__Preexisting_Individual_Placed(
            FIME__Location individual_location,
            double[] feature_vector,
            FIME__Placement_Result placement_result
            )
        {
            if (event__preexisting_individual_placed != null)
            {
                FIME__Preexisting_Individual_Placed__EventArgs<T> event_args = 
                    new FIME__Preexisting_Individual_Placed__EventArgs<T>(
                        cmce_algorithm: this,
                        message: "preexisting_individual_placed",
                        preexisting_individual_location: individual_location,
                        placement_result: placement_result,
                        feature_vector: feature_vector
                        );
                event__preexisting_individual_placed.Invoke(this, event_args);
            }
        }

        private void Invoke_Event__Individual_Generated(
            T individual,
            FIME__Location individual_location,
            double[] individual_feature_vector,
            double individual_fitness,
            FIME__Placement_Result individual_placement_result
            )
        {
            if (event__individual_generated != null)
            {
                FIME__Individual_Generated__EventArgs<T> event_args =
                    new FIME__Individual_Generated__EventArgs<T>(
                        cmce_algorithm: this,
                        message: "individual_generated",
                        individual: individual,
                        location: individual_location,
                        placement_result: individual_placement_result,
                        feature_vector: individual_feature_vector,
                        fitness: individual_fitness
                        );
                event__individual_generated.Invoke(this, event_args);
            }
        }

        private void Invoke_Event__Offspring_Generated(
            T individual,

            FIME__Location parent_location,
            double[] parent_feature_vector,
            double parent_fitness,

            FIME__Location offspring_location,
            double[] offspring_feature_vector,
            double offspring_fitness,

            FIME__Placement_Result placement_result
            )
        {
            if (event__offspring_generated != null)
            {
                FIME__Offspring_Generated__EventArgs<T> event_args =
                    new FIME__Offspring_Generated__EventArgs<T>(
                        cmce_algorithm: this,
                        message: "individual_generated",

                        offspring: individual,
                        parent_location: parent_location,
                        parent_feature_vector: parent_feature_vector,
                        parent_fitness: parent_fitness,
                        offspring_location: offspring_location,
                        offspring_feature_vector: offspring_feature_vector,
                        offspring_fitness: offspring_fitness,
                        placement_result: placement_result
                        );

                event__offspring_generated.Invoke(this, event_args);
            }
        }

    }
}
