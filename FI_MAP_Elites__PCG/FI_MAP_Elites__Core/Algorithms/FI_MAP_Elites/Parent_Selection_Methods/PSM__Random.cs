using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class PSM__Random<T> : Parent_Selection_Method<T>
        where T : Data_Structure
    {
        public FIME__State_Type last_selected_state;

        public PSM__Random()
        {
            last_selected_state = FIME__State_Type.None;
        }

        private PSM__Random(PSM__Random<T> psm_to_copy)
        {
            this.last_selected_state = psm_to_copy.last_selected_state;
        }

        public override object Q__Deep_Copy()
        {
            return new PSM__Random<T>(this);
        }

        public override FIME__Location Select__Parent_Location(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm
            )
        {
            switch (last_selected_state)
            {
                case FIME__State_Type.None:
                    if (algorithm.feasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Feasible);
                    }
                    else if (algorithm.infeasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Infeasible);
                    }
                    break;
                case FIME__State_Type.Infeasible:
                    if (algorithm.feasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Feasible);
                    }
                    else if (algorithm.infeasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Infeasible);
                    }
                    break;
                case FIME__State_Type.Feasible:
                    if (algorithm.infeasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Infeasible);
                    }
                    else if (algorithm.feasible_state.Q__Is_Empty() == false)
                    {
                        return Select_From_State(rand, algorithm, FIME__State_Type.Feasible);
                    }
                    break;
                default:
                    throw new System.Exception("something is very wrong here!");
            }
            return new FIME__Location(FIME__State_Type.None, -1);
        }

        private FIME__Location Select_From_State(
            I_PRNG rand,
            FI_MAP_Elites<T> algorithm,
            FIME__State_Type preferred_state
            )
        {
            last_selected_state = preferred_state;
            List<int> existing_individuals_locations = new List<int>();

            if (preferred_state == FIME__State_Type.Feasible)
            {
                existing_individuals_locations = 
                    algorithm
                    .feasible_state
                    .Q__Occupied_Cells();
            }
            else if (preferred_state == FIME__State_Type.Infeasible)
            {
                existing_individuals_locations = 
                    algorithm
                    .infeasible_state
                    .Q__Occupied_Cells();
            }

            if (existing_individuals_locations.Count == 0)
            {
                return new FIME__Location(preferred_state, -1);
            }
            else
            {
                return new FIME__Location(
                    preferred_state, 
                    existing_individuals_locations.Q__Random_Item(rand)
                    );
            }
        }
    }
}
