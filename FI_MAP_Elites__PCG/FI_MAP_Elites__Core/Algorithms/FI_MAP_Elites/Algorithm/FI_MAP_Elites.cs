using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FI_MAP_Elites<T>: Data_Structure
        where T : Data_Structure
    {
        // state
        public Generation_Method<T> generation_method;

        public Constraint_Evaluation_Method<T> feasibility_discrimination_method;
        public Parent_Selection_Method<T> parent_selection_method;

        public FIME__Archive<T> feasible_state;
        public FIME__Archive<T> infeasible_state;

        // counters
        public int num_evaluations;

        // constructor
        public FI_MAP_Elites(
            FI_MAP_Elites__Settings<T> settings
            )
        {
            this.generation_method = 
                (Generation_Method<T>)settings.generation_method.Q__Deep_Copy();
            this.feasibility_discrimination_method = 
                (Constraint_Evaluation_Method<T>)settings.feasibility_discrimination_method.Q__Deep_Copy();
            this.parent_selection_method = 
                (Parent_Selection_Method<T>)settings.parent_selection_method.Q__Deep_Copy();

            feasible_state = new FIME__Archive<T>(
                fitness_function: settings.feasible_state__fitness_function,
                fitness_goal: settings.feasible_state__fitness_goal,
                individual_comparison_type: settings.feasible_state__replacement_rule,
                evaluation_method__per__feature: settings.evaluation_method__per__feature,
                tessellation: settings.tessellation,
                mutation_method: settings.feasible_state_mutation_method
                );

            infeasible_state = new FIME__Archive<T>(
                fitness_function: settings.infeasible_state__fitness_function,
                fitness_goal: settings.infeasible_state__fitness_goal,
                individual_comparison_type: settings.infeasible_state__replacement_rule,
                evaluation_method__per__feature: settings.evaluation_method__per__feature,
                tessellation: settings.tessellation,
                mutation_method: settings.infeasible_state_mutation_method
                );

            // subscribe the parent selection method to the proper events...
            if (this.parent_selection_method is I_FIME__Individual_Generated__Listener<T> individual_generated_listener)
            {
                event__individual_generated += individual_generated_listener.On__Individual_Generated;
            }
            if (this.parent_selection_method is I_FIME__Offspring_Generated__Listener<T> offspring_generated_listener)
            {
                event__offspring_generated += offspring_generated_listener.On__Offspring_Generated;
            }

            this.num_evaluations = 0;
        }


        private FI_MAP_Elites(FI_MAP_Elites<T> cmce_to_copy) {
            this.generation_method = 
                (Generation_Method<T>)cmce_to_copy.generation_method.Q__Deep_Copy();
            this.feasibility_discrimination_method =
                (Constraint_Evaluation_Method<T>)cmce_to_copy.feasibility_discrimination_method.Q__Deep_Copy();
            this.parent_selection_method =
                (Parent_Selection_Method<T>)cmce_to_copy.parent_selection_method.Q__Deep_Copy();
            this.feasible_state =
                (FIME__Archive<T>)cmce_to_copy.feasible_state.Q__Deep_Copy();
            this.infeasible_state =
                (FIME__Archive<T>)cmce_to_copy.infeasible_state.Q__Deep_Copy();

            // subscribe the parent selection method to the proper events...
            if (this.parent_selection_method is I_FIME__Individual_Generated__Listener<T> individual_generated_listener)
            {
                event__individual_generated += individual_generated_listener.On__Individual_Generated;
            }
            if (this.parent_selection_method is I_FIME__Offspring_Generated__Listener<T> offspring_generated_listener)
            {
                event__offspring_generated += offspring_generated_listener.On__Offspring_Generated;
            }
        }

        public override object Q__Deep_Copy()
        {
            return new FI_MAP_Elites<T>(this);
        }
    }
}
