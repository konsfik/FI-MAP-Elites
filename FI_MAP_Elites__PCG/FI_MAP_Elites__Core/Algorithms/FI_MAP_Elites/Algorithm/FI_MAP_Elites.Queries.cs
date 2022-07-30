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
    using LocationsList = List<FIME__Location>;
    using CoordsList = List<int[]>;
    using IntList = List<int>;

    public partial class FI_MAP_Elites<T>
        where T : Data_Structure
    {

        /// <summary>
        /// Returns a list of all the occupied locations, 
        /// from within the feasible and infeasible state.
        /// </summary>
        /// <returns></returns>
        public LocationsList Q__Occupied_Locations()
        {
            LocationsList occupied_locations = new LocationsList();

            occupied_locations.AddRange(Q__Occupied_Feasible_Locations());
            occupied_locations.AddRange(Q__Occupied_Infeasible_Locations());

            return occupied_locations;
        }


        public LocationsList Q__Occupied_Feasible_Locations()
        {
            LocationsList occupied_feasible_locations = new LocationsList();

            IntList feasible_occupied_cells = feasible_state.Q__Occupied_Cells();
            foreach (var cell in feasible_occupied_cells)
            {
                occupied_feasible_locations.Add(
                    new FIME__Location(FIME__State_Type.Feasible, cell)
                    );
            }

            return occupied_feasible_locations;
        }

        public LocationsList Q__Occupied_Feasible_Locations__Within_Window(
            Selection_Window window
            )
        {
            var occupied_feasible_locations = Q__Occupied_Feasible_Locations();
            LocationsList occupied_feasible_locations__within_window = new LocationsList();
            foreach (var loc in occupied_feasible_locations)
            {
                var coords = Q__Cell_Coordinates_At_Location(loc);
                bool contained = window.Q__Contains_Coordinates(coords);
                if (contained)
                {
                    occupied_feasible_locations__within_window.Add(loc);
                }
            }

            return occupied_feasible_locations__within_window;
        }

        public CoordsList Q__Occupied_Feasible_Coords()
        {
            return feasible_state.Q__Occupied_Coords();
        }

        public CoordsList Q__Occupied_Feasible_Coords__Within_Window(Selection_Window window)
        {
            CoordsList occupied_feasible_coords = Q__Occupied_Feasible_Coords();
            CoordsList occupied_feasible_coords__within_window = new CoordsList();
            foreach (var coords in occupied_feasible_coords)
            {
                bool contained = window.Q__Contains_Coordinates(coords);
                if (contained)
                {
                    occupied_feasible_coords__within_window.Add(coords);
                }
            }

            return occupied_feasible_coords__within_window;
        }

        public CoordsList Q__Occupied_Infeasible_Coords()
        {
            return infeasible_state.Q__Occupied_Coords();
        }



        public LocationsList Q__Occupied_Infeasible_Locations()
        {
            LocationsList occupied_infeasible_locations = new LocationsList();

            IntList infeasible_occupied_cells = infeasible_state.Q__Occupied_Cells();
            foreach (var cell in infeasible_occupied_cells)
            {
                occupied_infeasible_locations.Add(
                    new FIME__Location(FIME__State_Type.Infeasible, cell)
                    );
            }

            return occupied_infeasible_locations;
        }

        public LocationsList Q__Occupied_Infeasible_Locations__Within_Window(
            Selection_Window window
            )
        {
            LocationsList occupied_infeasible_locations = Q__Occupied_Infeasible_Locations();
            LocationsList occupied_infeasible_locations__within_window = new LocationsList();
            foreach (var loc in occupied_infeasible_locations)
            {
                var coords = Q__Cell_Coordinates_At_Location(loc);
                bool contained = window.Q__Contains_Coordinates(coords);
                if (contained)
                {
                    occupied_infeasible_locations__within_window.Add(loc);
                }
            }

            return occupied_infeasible_locations__within_window;
        }

        public bool Q__Calculate_Feasibility(T individual)
        {
            return feasibility_discrimination_method.Q__Satisfied(individual);
        }

        public double Q__Calculate_Fitness(
            T individual,
            bool is_feasible
            )
        {
            if (is_feasible)
            {
                return feasible_state.Q__Calculate_Fitness(individual);
            }
            else
            {
                return infeasible_state.Q__Calculate_Fitness(individual);
            }
        }

        public double[] Q__Calculate_Feature_Vector(
            T individual,
            bool is_feasible
            )
        {
            if (is_feasible)
            {
                return feasible_state.Q__Calculate_Feature_Vector(individual);
            }
            else
            {
                return infeasible_state.Q__Calculate_Feature_Vector(individual);
            }
        }

        public int[] Q__Calculate_Cell_Coordinates(double[] feature_vector, bool is_feasible)
        {
            if (is_feasible)
            {
                return feasible_state.tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            }
            else
            {
                return infeasible_state.tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            }
        }

        public FIME__Location Q__Calculate_Location(
            double[] feature_vector,
            bool is_feasible
            )
        {
            if (is_feasible)
            {
                FIME__State_Type state_type = FIME__State_Type.Feasible;
                int cell = feasible_state.tessellation.Q__Feature_Vector__To__Cell(feature_vector);
                return new FIME__Location(state_type, cell);
            }
            else
            {
                FIME__State_Type state_type = FIME__State_Type.Infeasible;
                int cell = feasible_state.tessellation.Q__Feature_Vector__To__Cell(feature_vector);
                return new FIME__Location(state_type, cell);
            }

        }

        public T Q__Individual_At_Location(FIME__Location location)
        {
            if (location.state == FIME__State_Type.Feasible)
            {
                return feasible_state.individuals__archive[location.cell];
            }
            else if (location.state == FIME__State_Type.Infeasible)
            {
                return infeasible_state.individuals__archive[location.cell];
            }
            else
            {
                throw new Exception("improper state");
            }
        }

        public double Q__Fitness_At_Location(FIME__Location location)
        {
            if (location.state == FIME__State_Type.Feasible)
            {
                return feasible_state.fitness__archive[location.cell];
            }
            else if (location.state == FIME__State_Type.Infeasible)
            {
                return infeasible_state.fitness__archive[location.cell];
            }
            else
            {
                throw new Exception("improper state");
            }
        }

        public double[] Q__Feature_Vector_At_Location(FIME__Location location)
        {
            if (location.state == FIME__State_Type.Feasible)
            {
                return feasible_state.feature_vectors__archive[location.cell];
            }
            else if (location.state == FIME__State_Type.Infeasible)
            {
                return infeasible_state.feature_vectors__archive[location.cell];
            }
            else
            {
                throw new Exception("improper state");
            }
        }

        public int[] Q__Cell_Coordinates_At_Location(FIME__Location location)
        {
            if (location.state == FIME__State_Type.Feasible)
            {
                return feasible_state.tessellation.Q__Cell__To__Coords(location.cell);
            }
            else if (location.state == FIME__State_Type.Infeasible)
            {
                return infeasible_state.tessellation.Q__Cell__To__Coords(location.cell);
            }
            else
            {
                throw new System.Exception("incompatible state");
            }
        }

        public bool Q__Is_Location_Occupied(FIME__Location location)
        {
            switch (location.state)
            {
                case FIME__State_Type.Feasible:
                    return feasible_state.Q__Is_Cell_Occupied(location.cell);
                case FIME__State_Type.Infeasible:
                    return infeasible_state.Q__Is_Cell_Occupied(location.cell);
                case FIME__State_Type.None:
                    throw new Exception("undefined state");
                default:
                    throw new Exception("undefined state");
            }
        }

        public bool Q__Is_Feature_Vector_In_Range(
            double[] feature_vector,
            bool is_feasible
            )
        {
            if (is_feasible)
            {
                return feasible_state.Q__Is_Within_Range(feature_vector);
            }
            else
            {
                return infeasible_state.Q__Is_Within_Range(feature_vector);
            }
        }

        public T Q__Individual__Deep_Copy(FIME__Location location)
        {
            if (location.state == FIME__State_Type.Feasible)
            {
                return feasible_state.Q__Individual__Deep_Copy(location.cell);
            }
            else if (location.state == FIME__State_Type.Infeasible)
            {
                return infeasible_state.Q__Individual__Deep_Copy(location.cell);
            }
            else
            {
                throw new Exception("state not properly selected");
            }
        }

        public T Q__Best_Feasible_Individual__Deep_Copied(I_PRNG rand)
        {
            double best_fitness = Q__Best_Feasible_Fitness();
            IntList relevant_indices = Q__Feasible_Individual_Indices__Of__Specific_Fitness(best_fitness);
            if (relevant_indices.Count == 0)
            {
                return null;
            }
            else if (relevant_indices.Count == 1)
            {
                int individual_index = relevant_indices[0];
                return (T)feasible_state.individuals__archive[individual_index].Q__Deep_Copy();
            }
            else
            {
                int individual_index = relevant_indices.Q__Random_Item(rand);
                return (T)feasible_state.individuals__archive[individual_index].Q__Deep_Copy();
            }
        }

        public IntList Q__Feasible_Individual_Indices__Of__Specific_Fitness(double fitness)
        {
            IntList indices = new IntList();
            int num_indices = feasible_state.fitness__archive.Length;
            for (int i = 0; i < num_indices; i++)
            {
                if (feasible_state.fitness__archive[i] == fitness)
                    indices.Add(i);
            }
            return indices;
        }

        public double Q__Best_Feasible_Fitness()
        {
            if (feasible_state.fitness_goal == EA__Fitness_Goal.MAXIMIZATION)
                return feasible_state.fitness__archive.Max();
            else if (feasible_state.fitness_goal == EA__Fitness_Goal.MINIMIZATION)
                return feasible_state.fitness__archive.Min();
            else
                throw new Exception("goal must be minimization or maximization");
        }

        public double Q__Best_Infeasible_Fitness()
        {
            if (infeasible_state.fitness_goal == EA__Fitness_Goal.MAXIMIZATION)
                return infeasible_state.fitness__archive.Max();
            else if (infeasible_state.fitness_goal == EA__Fitness_Goal.MINIMIZATION)
                return infeasible_state.fitness__archive.Min();
            else
                throw new Exception("goal must be minimization or maximization");
        }

    }
}
