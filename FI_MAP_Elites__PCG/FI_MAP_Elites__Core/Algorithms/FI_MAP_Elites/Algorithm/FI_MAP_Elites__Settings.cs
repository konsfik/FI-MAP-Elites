using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class FI_MAP_Elites__Settings<T>: I_Deep_Copyable
        where T : Data_Structure
    {
        public Generation_Method<T> generation_method;

        public Evaluation_Method<T>[] evaluation_method__per__feature;
        public BS_Ortho_Tessellation tessellation;

        public Constraint_Evaluation_Method<T> feasibility_discrimination_method;

        public Evaluation_Method<T> feasible_state__fitness_function;
        public EA__Fitness_Goal feasible_state__fitness_goal;
        public EA__Individual_Replacement_Rule feasible_state__replacement_rule;
        public Mutation_Method<T> feasible_state_mutation_method;

        public Evaluation_Method<T> infeasible_state__fitness_function;
        public EA__Fitness_Goal infeasible_state__fitness_goal;
        public EA__Individual_Replacement_Rule infeasible_state__replacement_rule;
        public Mutation_Method<T> infeasible_state_mutation_method;

        public Parent_Selection_Method<T> parent_selection_method;

        public FI_MAP_Elites__Settings(
            Generation_Method<T> generation_method,

            Evaluation_Method<T> feasible_state__fitness_function,
            EA__Fitness_Goal feasible_state__fitness_goal,
            EA__Individual_Replacement_Rule feasible_state__replacement_rule,
            Mutation_Method<T> feasible_state_mutation_method,

            Evaluation_Method<T> infeasible_state__fitness_function,
            EA__Fitness_Goal infeasible_state__fitness_goal,
            EA__Individual_Replacement_Rule infeasible_state__replacement_rule,
            Mutation_Method<T> infeasible_state_mutation_method,

            Evaluation_Method<T>[] evaluation_method__per__feature,
            BS_Ortho_Tessellation tessellation,

            Constraint_Evaluation_Method<T> feasibility_discrimination_method,
            Parent_Selection_Method<T> parent_selection_method
            )
        {
            this.generation_method = generation_method;

            this.feasible_state__fitness_function = 
                (Evaluation_Method<T>)feasible_state__fitness_function.Q__Deep_Copy();
            this.feasible_state__fitness_goal = 
                feasible_state__fitness_goal;
            this.feasible_state__replacement_rule = 
                feasible_state__replacement_rule;
            this.feasible_state_mutation_method = 
                (Mutation_Method<T>)feasible_state_mutation_method.Q__Deep_Copy();

            this.infeasible_state__fitness_function = 
                (Evaluation_Method<T>)infeasible_state__fitness_function.Q__Deep_Copy();
            this.infeasible_state__fitness_goal = 
                infeasible_state__fitness_goal;
            this.infeasible_state__replacement_rule = 
                infeasible_state__replacement_rule;
            this.infeasible_state_mutation_method = 
                (Mutation_Method<T>)infeasible_state_mutation_method.Q__Deep_Copy();

            this.evaluation_method__per__feature = 
                evaluation_method__per__feature;
            this.tessellation = 
                (BS_Ortho_Tessellation)tessellation.Q__Deep_Copy();

            this.feasibility_discrimination_method = 
                (Constraint_Evaluation_Method<T>)feasibility_discrimination_method.Q__Deep_Copy();
            this.parent_selection_method = 
                parent_selection_method;
        }

        private FI_MAP_Elites__Settings(FI_MAP_Elites__Settings<T> settings_to_copy)
        {
            this.generation_method = 
                (Generation_Method<T>)settings_to_copy.generation_method.Q__Deep_Copy();
            
            this.evaluation_method__per__feature =
                settings_to_copy.evaluation_method__per__feature.Q__Deep_Copy();
            this.tessellation =
                (BS_Ortho_Tessellation)settings_to_copy.tessellation.Q__Deep_Copy();
            
            this.feasibility_discrimination_method =
                (Constraint_Evaluation_Method<T>)settings_to_copy.feasibility_discrimination_method.Q__Deep_Copy();

            this.feasible_state__fitness_function =
                (Evaluation_Method<T>)settings_to_copy.feasible_state__fitness_function.Q__Deep_Copy();
            this.feasible_state__fitness_goal =
                settings_to_copy.feasible_state__fitness_goal;
            this.feasible_state__replacement_rule =
                settings_to_copy.feasible_state__replacement_rule;
            this.feasible_state_mutation_method =
                (Mutation_Method<T>)settings_to_copy.feasible_state_mutation_method.Q__Deep_Copy();

            this.infeasible_state__fitness_function =
                (Evaluation_Method<T>)settings_to_copy.infeasible_state__fitness_function.Q__Deep_Copy();
            this.infeasible_state__fitness_goal =
                settings_to_copy.infeasible_state__fitness_goal;
            this.infeasible_state__replacement_rule =
                settings_to_copy.infeasible_state__replacement_rule;
            this.infeasible_state_mutation_method =
                (Mutation_Method<T>)settings_to_copy.infeasible_state_mutation_method.Q__Deep_Copy();

            this.parent_selection_method =
                (Parent_Selection_Method<T>)settings_to_copy.parent_selection_method.Q__Deep_Copy();

        }

        public object Q__Deep_Copy()
        {
            return new FI_MAP_Elites__Settings<T>(this);
        }
    }
}
