using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

using SkiaSharp;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class CMCE_Stats__Basic<T>
        where T : Data_Structure
    {
        public void M__Subscribe_To_CMCE_Algorithm(FI_MAP_Elites<T> cmce_algorithm) 
        {
            cmce_algorithm.event__individual_generated += On_Individual_Generated; 
            cmce_algorithm.event__offspring_generated += On_Offspring_Generated; 
        }

        #region actual event responses
        private void On_Individual_Generated(
            object sender,
            FIME__Individual_Generated__EventArgs<T> event_args)
        {
            num_evals++;

            On_Individual_Generated__Update__Individual_Exists(event_args);
            On_Individual_Generated__Update__Fitness(event_args);
            On_Individual_Generated__Update__Num_Landings(event_args);
            On_Individual_Generated__Update__Num_Survivals(event_args);

            bool is_feasible = event_args.location.state == FIME__State_Type.Feasible;

            if (
                is_feasible== true
                &&
                first_feasible_found== false
                ) 
            {
                Save_Data_Tables_And_Summary();
                first_feasible_found = true;
            }
            else if (save_stats_evals.Contains(num_evals))
            {
                Save_Data_Tables_And_Summary();
            }
            
        }

        private void On_Offspring_Generated(
            object sender,
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            num_evals++;

            On_Offspring_Generated__Update__Individual_Exists(event_args);
            On_Offspring_Generated__Update__Fitness(event_args);
            On_Offspring_Generated__Update__Num_Landings(event_args);
            On_Offspring_Generated__Update__Num_Survivals(event_args);

            On_Offspring_Generated__Update__Selections(event_args);

            bool offspring_is_feasible = event_args.offspring_location.state == FIME__State_Type.Feasible;

            if (
                offspring_is_feasible == true
                &&
                first_feasible_found == false
                )
            {
                Save_Data_Tables_And_Summary();
                first_feasible_found = true;
            }
            else if (save_stats_evals.Contains(num_evals))
            {
                Save_Data_Tables_And_Summary();
            }
        }
        #endregion

        #region table updates
        private void On_Individual_Generated__Update__Individual_Exists(
            FIME__Individual_Generated__EventArgs<T> event_args
            )
        {
            var result = event_args.placement_result;
            if (result == FIME__Placement_Result.FAILURE__OUT_OF_RANGE) return;

            var to_state = event_args.location.state;
            int to_cell = event_args.location.cell;

            if (to_state == FIME__State_Type.Feasible)
            {
                individual_exists__on_FS[to_cell] = 1;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                individual_exists__on_IS[to_cell] = 1;
            }
        }
        private void On_Individual_Generated__Update__Fitness(
            FIME__Individual_Generated__EventArgs<T> event_args
            )
        {
            var result = event_args.placement_result;
            if (
                result == FIME__Placement_Result.FAILURE__OUT_OF_RANGE
                ||
                result == FIME__Placement_Result.FAILURE__REPLACEMENT
                )
            {
                return;
            }

            var to_state = event_args.location.state;
            int to_cell = event_args.location.cell;
            double fitness = event_args.fitness;

            if (to_state == FIME__State_Type.Feasible)
            {
                fitness__on_FS[to_cell] = fitness;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                fitness__on_IS[to_cell] = fitness;
            }
        }

        private void On_Individual_Generated__Update__Num_Landings(
            FIME__Individual_Generated__EventArgs<T> event_args
            )
        {
            var to_state = event_args.location.state;
            int to_cell = event_args.location.cell;
            if (to_state == FIME__State_Type.Feasible)
            {
                num_landings__on_FS[to_cell]++;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                num_landings__on_IS[to_cell]++;
            }
        }

        private void On_Individual_Generated__Update__Num_Survivals(
            FIME__Individual_Generated__EventArgs<T> event_args
            )
        {
            bool success =
                event_args.placement_result == FIME__Placement_Result.SUCCESS__DISCOVERY
                ||
                event_args.placement_result == FIME__Placement_Result.SUCCESS__REPLACEMENT;

            if (success == false) return;


            var to_state = event_args.location.state;
            int to_cell = event_args.location.cell;

            if (to_state == FIME__State_Type.Feasible)
            {
                num_survivals__on_FS[to_cell]++;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                num_survivals__on_IS[to_cell]++;
            }
        }



        private void On_Offspring_Generated__Update__Individual_Exists(
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            var result = event_args.placement_result;
            if (result == FIME__Placement_Result.FAILURE__OUT_OF_RANGE) return;

            var to_state = event_args.offspring_location.state;
            int to_cell = event_args.offspring_location.cell;

            if (to_state == FIME__State_Type.Feasible)
            {
                individual_exists__on_FS[to_cell] = 1;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                individual_exists__on_IS[to_cell] = 1;
            }
        }

        private void On_Offspring_Generated__Update__Fitness(
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            var result = event_args.placement_result;
            if (
                result == FIME__Placement_Result.FAILURE__OUT_OF_RANGE
                ||
                result == FIME__Placement_Result.FAILURE__REPLACEMENT
                )
            {
                return;
            }

            var to_state = event_args.offspring_location.state;
            int to_cell = event_args.offspring_location.cell;
            double offspring_fitness = event_args.offspring_fitness;

            if (to_state == FIME__State_Type.Feasible)
            {
                fitness__on_FS[to_cell] = offspring_fitness;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                fitness__on_IS[to_cell] = offspring_fitness;
            }
        }

        private void On_Offspring_Generated__Update__Num_Landings(
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            var result = event_args.placement_result;
            if (result == FIME__Placement_Result.FAILURE__OUT_OF_RANGE) return;

            var to_state = event_args.offspring_location.state;
            int to_cell = event_args.offspring_location.cell;
            if (to_state == FIME__State_Type.Feasible)
            {
                num_landings__on_FS[to_cell]++;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                num_landings__on_IS[to_cell]++;
            }
        }

        private void On_Offspring_Generated__Update__Num_Survivals(
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            bool success =
                event_args.placement_result == FIME__Placement_Result.SUCCESS__DISCOVERY
                ||
                event_args.placement_result == FIME__Placement_Result.SUCCESS__REPLACEMENT;

            if (success == false) return;


            var to_state = event_args.offspring_location.state;
            int to_cell = event_args.offspring_location.cell;

            if (to_state == FIME__State_Type.Feasible)
            {
                num_survivals__on_FS[to_cell]++;
            }
            else if (to_state == FIME__State_Type.Infeasible)
            {
                num_survivals__on_IS[to_cell]++;
            }
        }

        private void On_Offspring_Generated__Update__Selections(
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            var from_state = event_args.parent_location.state;
            int from_cell = event_args.parent_location.cell;

            if (from_state == FIME__State_Type.Feasible)
            {
                num_selections__on_FS[from_cell]++;
            }
            else if (from_state == FIME__State_Type.Infeasible)
            {
                num_selections__on_IS[from_cell]++;
            }
        }

        #endregion
    }
}
