using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class PSM__Windowed__Random<T> : PSM__Windowed<T>
        where T : Data_Structure
    {
        public FIME__State_Type last_selected_state;

        public PSM__Windowed__Random() : base()
        {

        }

        public PSM__Windowed__Random(Selection_Window selection_window)
            : base(selection_window)
        {

        }

        private PSM__Windowed__Random(PSM__Windowed__Random<T> psm_to_copy)
        {
            if (psm_to_copy.selection_window == null)
                this.selection_window = null;
            else
                this.selection_window =
                    (Selection_Window)psm_to_copy.selection_window.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new PSM__Windowed__Random<T>(this);
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

            List<FIME__Location> locations = new List<FIME__Location>();

            if (preferred_state == FIME__State_Type.Feasible)
            {
                locations = Q__Occupied_Feasible_Locations__In_Window(algorithm);
            }
            else if (preferred_state == FIME__State_Type.Infeasible)
            {
                locations = Q__Occupied_Infeasible_Locations__In_Window(algorithm);
            }

            if (locations.Count == 0)
            {
                return new FIME__Location(preferred_state, -1);
            }
            else
            {
                return locations.Q__Random_Item(rand);
            }

        }

        private List<FIME__Location> Q__Occupied_Feasible_Locations__In_Window(FI_MAP_Elites<T> cmce_algorithm)
        {
            var occupied_feasible_locations = cmce_algorithm.Q__Occupied_Feasible_Locations();

            List<FIME__Location> occupied_feasible_locations_in_window =
                new List<FIME__Location>();

            foreach (var loc in occupied_feasible_locations)
            {
                var coords = cmce_algorithm.Q__Cell_Coordinates_At_Location(loc);
                bool contains_coordinates = selection_window.Q__Contains_Coordinates(coords);
                if (contains_coordinates)
                {
                    occupied_feasible_locations_in_window.Add(loc);
                }
            }

            return occupied_feasible_locations_in_window;
        }

        private List<FIME__Location> Q__Occupied_Infeasible_Locations__In_Window(FI_MAP_Elites<T> cmce_algorithm)
        {
            var occupied_infeasible_locations = cmce_algorithm.Q__Occupied_Infeasible_Locations();

            List<FIME__Location> occupied_infeasible_locations_in_window =
                new List<FIME__Location>();

            foreach (var loc in occupied_infeasible_locations)
            {
                var coords = cmce_algorithm.Q__Cell_Coordinates_At_Location(loc);
                bool contains_coordinates = selection_window.Q__Contains_Coordinates(coords);
                if (contains_coordinates)
                {
                    occupied_infeasible_locations_in_window.Add(loc);
                }
            }

            return occupied_infeasible_locations_in_window;
        }

        
    }
}
