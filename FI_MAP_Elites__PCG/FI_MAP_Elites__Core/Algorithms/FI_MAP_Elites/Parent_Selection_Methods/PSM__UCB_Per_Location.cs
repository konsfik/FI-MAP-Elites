using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class PSM__UCB_Per_Location<T> : Parent_Selection_Method<T>,
        I_FIME__Offspring_Generated__Listener<T>,
        I_FIME__Individual_Generated__Listener<T>
        where T : Data_Structure
    {
        public FIME__Offspring_Survival__Score_Keeping_Type score_keeping_type;

        public double c_value;
        public BS_Ortho_Tessellation tessellation;

        public FIME__State_Type previous_state;


        public bool[] feasible_state__individual_exists__archive;
        public double[] feasible_state__selections__archive;
        public double[] feasible_state__successful_offspring__archive;
        public double[] feasible_state__ucb_values__archive;

        public bool[] infeasible_state__individual_exists__archive;
        public double[] infeasible_state__selections__archive;
        public double[] infeasible_state__successful_offspring__archive;
        public double[] infeasible_state__ucb_values__archive;


        public PSM__UCB_Per_Location(
            double c_value,
            FIME__Offspring_Survival__Score_Keeping_Type score_keeping_type,
            BS_Ortho_Tessellation tessellation
            )
        {
            this.score_keeping_type = score_keeping_type;

            this.c_value = c_value;
            this.tessellation = (BS_Ortho_Tessellation)tessellation.Q__Deep_Copy();

            this.previous_state = FIME__State_Type.None;

            this.feasible_state__individual_exists__archive = new bool[tessellation.num_cells];
            this.feasible_state__selections__archive = new double[tessellation.num_cells];
            this.feasible_state__successful_offspring__archive = new double[tessellation.num_cells];
            this.feasible_state__ucb_values__archive = new double[tessellation.num_cells];

            this.infeasible_state__individual_exists__archive = new bool[tessellation.num_cells];
            this.infeasible_state__selections__archive = new double[tessellation.num_cells];
            this.infeasible_state__successful_offspring__archive = new double[tessellation.num_cells];
            this.infeasible_state__ucb_values__archive = new double[tessellation.num_cells];

            for (int c = 0; c < tessellation.num_cells; c++)
            {
                feasible_state__individual_exists__archive[c] = false;
                feasible_state__selections__archive[c] = 0;
                feasible_state__successful_offspring__archive[c] = 0;
                feasible_state__ucb_values__archive[c] = double.NegativeInfinity;

                infeasible_state__individual_exists__archive[c] = false;
                infeasible_state__selections__archive[c] = 0;
                infeasible_state__successful_offspring__archive[c] = 0;
                infeasible_state__ucb_values__archive[c] = double.NegativeInfinity;
            }
        }

        private PSM__UCB_Per_Location(PSM__UCB_Per_Location<T> psm_to_copy)
        {
            this.score_keeping_type = psm_to_copy.score_keeping_type;

            this.c_value = psm_to_copy.c_value;
            this.tessellation = (BS_Ortho_Tessellation)psm_to_copy.tessellation.Q__Deep_Copy();

            this.feasible_state__individual_exists__archive = psm_to_copy.feasible_state__individual_exists__archive.Q__Deep_Copy();
            this.feasible_state__selections__archive = psm_to_copy.feasible_state__selections__archive.Q__Deep_Copy();
            this.feasible_state__successful_offspring__archive = psm_to_copy.feasible_state__successful_offspring__archive.Q__Deep_Copy();
            this.feasible_state__ucb_values__archive = psm_to_copy.feasible_state__ucb_values__archive.Q__Deep_Copy();

            this.infeasible_state__individual_exists__archive = psm_to_copy.infeasible_state__individual_exists__archive.Q__Deep_Copy();
            this.infeasible_state__selections__archive = psm_to_copy.infeasible_state__selections__archive.Q__Deep_Copy();
            this.infeasible_state__successful_offspring__archive = psm_to_copy.infeasible_state__successful_offspring__archive.Q__Deep_Copy();
            this.infeasible_state__ucb_values__archive = psm_to_copy.infeasible_state__ucb_values__archive.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new PSM__UCB_Per_Location<T>(this);
        }

        public override FIME__Location Select__Parent_Location(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm
            )
        {
            if (previous_state == FIME__State_Type.None)
            {
                previous_state = FIME__State_Type.Infeasible;
            }


            if (previous_state == FIME__State_Type.Feasible)
            {
                if (infeasible_state__individual_exists__archive.Any(x => x == true))
                {
                    // work on the infeasible state
                    previous_state = FIME__State_Type.Infeasible;
                    return Select__Parent_Location__From__Infeasible_State(rand, algorithm);
                }
                else if (feasible_state__individual_exists__archive.Any(x => x == true))
                {
                    // work on the feasible state
                    previous_state = FIME__State_Type.Feasible;
                    return Select__Parent_Location__From__Feasible_State(rand, algorithm);
                }
                else
                {
                    throw new Exception("no individuals found");
                }

            }
            else if (previous_state == FIME__State_Type.Infeasible)
            {
                if (feasible_state__individual_exists__archive.Any(x => x == true))
                {
                    // work on the feasible state
                    previous_state = FIME__State_Type.Feasible;
                    return Select__Parent_Location__From__Feasible_State(rand, algorithm);
                }
                else if (infeasible_state__individual_exists__archive.Any(x => x == true))
                {
                    // work on the infeasible state
                    previous_state = FIME__State_Type.Infeasible;
                    return Select__Parent_Location__From__Infeasible_State(rand, algorithm);
                }
                else
                {
                    throw new Exception("no individuals found");
                }
            }
            else
            {
                throw new Exception("incompatible state");
            }
        }

        private FIME__Location Select__Parent_Location__From__Feasible_State(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm
            )
        {
            double best_ucb_value =
                feasible_state__ucb_values__archive.Max();
            List<int> best_ucb_value__cells =
                feasible_state__ucb_values__archive.Indexes_Of_Value(best_ucb_value);
            int selected_cell =
                best_ucb_value__cells.Q__Random_Item(rand);
            return new FIME__Location(
                FIME__State_Type.Feasible,
                selected_cell
                );
        }

        private FIME__Location Select__Parent_Location__From__Infeasible_State(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm
            )
        {
            double best_ucb_value =
                infeasible_state__ucb_values__archive.Q__Max();
            List<int> best_ucb_value__cells =
                infeasible_state__ucb_values__archive.Indexes_Of_Value(best_ucb_value);
            int selected_cell =
                best_ucb_value__cells.Q__Random_Item(rand);
            return new FIME__Location(
                FIME__State_Type.Infeasible,
                selected_cell
                );
        }


        public void On__Offspring_Generated(
            object sender,
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            bool parent_is_feasible = (event_args.parent_location.state == FIME__State_Type.Feasible);
            int parent_cell = event_args.parent_location.cell;
            bool offspring_is_feasible = (event_args.offspring_location.state == FIME__State_Type.Feasible);
            int offspring_cell = event_args.offspring_location.cell;

            // increase parent selections
            if (parent_is_feasible)
            {
                feasible_state__selections__archive[parent_cell] += 1;
            }
            else
            {
                infeasible_state__selections__archive[parent_cell] += 1;
            }

            if (offspring_is_feasible)
            {
                // set offspring existence to true
                feasible_state__individual_exists__archive[offspring_cell] = true;
            }
            else
            {
                // set offspring existence to true
                infeasible_state__individual_exists__archive[offspring_cell] = true;
            }


            bool condition_1 =
                event_args.placement_result == FIME__Placement_Result.SUCCESS__DISCOVERY
                ||
                event_args.placement_result == FIME__Placement_Result.SUCCESS__REPLACEMENT;

            bool condition_2_a =
                score_keeping_type.HasFlag(FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_FEASIBLE)
                &&
                event_args.parent_location.state == FIME__State_Type.Feasible
                &&
                event_args.offspring_location.state == FIME__State_Type.Feasible;

            bool condition_2_b =
                score_keeping_type.HasFlag(FIME__Offspring_Survival__Score_Keeping_Type.FEASIBLE_TO_INFEASIBLE)
                &&
                event_args.parent_location.state == FIME__State_Type.Feasible
                &&
                event_args.offspring_location.state == FIME__State_Type.Infeasible;

            bool condition_2_c =
                score_keeping_type.HasFlag(FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_FEASIBLE)
                &&
                event_args.parent_location.state == FIME__State_Type.Infeasible
                &&
                event_args.offspring_location.state == FIME__State_Type.Feasible;

            bool condition_2_d =
                score_keeping_type.HasFlag(FIME__Offspring_Survival__Score_Keeping_Type.INFEASIBLE_TO_INFEASIBLE)
                &&
                event_args.parent_location.state == FIME__State_Type.Infeasible
                &&
                event_args.offspring_location.state == FIME__State_Type.Infeasible;

            bool score_increase =
                condition_1
                &&
                (condition_2_a || condition_2_b || condition_2_c || condition_2_d);

            // update score
            if (score_increase)
            {
                if (event_args.offspring_location.state == FIME__State_Type.Feasible)
                {
                    feasible_state__successful_offspring__archive[offspring_cell] += 1;
                }
                else
                {
                    infeasible_state__successful_offspring__archive[offspring_cell] += 1;
                }
            }

            // update the ucb value
            Update_UCB_Values();
        }

        public void On__Individual_Generated(
            object sender,
            FIME__Individual_Generated__EventArgs<T> event_args)
        {
            if (
                event_args.placement_result == FIME__Placement_Result.SUCCESS__DISCOVERY
                ||
                event_args.placement_result == FIME__Placement_Result.SUCCESS__REPLACEMENT
                )
            {
                if (event_args.location.state == FIME__State_Type.Feasible)
                {
                    feasible_state__individual_exists__archive[event_args.location.cell] = true;
                    // update the ucb value
                    Update_UCB_Values();
                }
                else if (event_args.location.state == FIME__State_Type.Infeasible)
                {
                    infeasible_state__individual_exists__archive[event_args.location.cell] = true;
                    // update the ucb value
                    Update_UCB_Values();
                }
            }
            
        }

        public void Update_UCB_Values() {
            UCB_Utilities.Update_UCB_Values(
                ucb_values: feasible_state__ucb_values__archive,
                selections__archive: feasible_state__selections__archive,
                rewards__archive: feasible_state__successful_offspring__archive,
                exist: feasible_state__individual_exists__archive,
                c_value: c_value
                );

            UCB_Utilities.Update_UCB_Values(
                ucb_values: infeasible_state__ucb_values__archive,
                selections__archive: infeasible_state__selections__archive,
                rewards__archive: infeasible_state__successful_offspring__archive,
                exist: infeasible_state__individual_exists__archive,
                c_value: c_value
                );
        }
    }
}
