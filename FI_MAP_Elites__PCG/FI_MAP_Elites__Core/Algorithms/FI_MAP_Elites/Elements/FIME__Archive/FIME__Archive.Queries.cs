using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FIME__Archive<T> where T : Data_Structure
    {

        public double Q__Coverage()
        {
            int num_cells = tessellation.num_cells;
            int num_existing_individuals = Q__Num_Occupied_Cells();
            double coverage = (double)num_existing_individuals / (double)num_cells;
            return coverage;
        }

        public double Q__Calculate_Fitness(T individual)
        {
            return fitness_function.Evaluate_Individual(individual);
        }

        public double[] Q__Calculate_Feature_Vector(T individual)
        {
            // calculate the real coordinates of this individual...
            int num_features = Q__Num_Features();

            double[] feature_vector = new double[num_features];

            for (int ft = 0; ft < num_features; ft++)
            {
                feature_vector[ft] = evaluation_method__per__feature[ft].Evaluate_Individual(individual);
            }

            return feature_vector;
        }

        public T Q__Individual__Deep_Copy(int cell)
        {
            return (T)individuals__archive[cell].Q__Deep_Copy();
        }

        public List<T> Q__Existing_Individuals__Copied()
        {
            List<T> existing_individuals = new List<T>();
            foreach (var ind in individuals__archive)
            {
                if (ind != null)
                {
                    existing_individuals.Add((T)ind.Q__Deep_Copy());
                }
            }
            return existing_individuals;
        }

        public List<int> Q__Occupied_Cells()
        {
            // initialize a list with a size that can fit all individuals so as to reduce impact of adding new individuals
            List<int> existing_individuals__locations = new List<int>(tessellation.num_cells);

            for (int i = 0; i < tessellation.num_cells; i++)
            {
                if (individuals__archive[i] != null)
                {
                    existing_individuals__locations.Add(i);
                }
            }

            return existing_individuals__locations;
        }

        public List<int[]> Q__Occupied_Coords()
        {
            List<int> occupied_cells = Q__Occupied_Cells();
            List<int[]> occupied_coords = new List<int[]>();
            foreach (var c in occupied_cells)
            {
                int[] coords = tessellation.Q__Cell__To__Coords(c);
                occupied_coords.Add(coords);
            }
            return occupied_coords;
        }
        public int Q__Num_Occupied_Cells()
        {
            int num_occupied_cells = 0;
            for (int i = 0; i < tessellation.num_cells; i++)
            {
                if (individuals__archive[i] != null)
                {
                    num_occupied_cells++;
                }
            }
            return num_occupied_cells;
        }

        /// <summary>
        /// Returns whether the individual with fitness_1 should replace the individual with fitness_2,
        /// according to the type of individual comparison that has been selected for this state:
        /// |   >   |   >=  |   <   |   <=  |
        /// </summary>
        /// <param name="old_fitness"></param>
        /// <param name="new_fitness"></param>
        /// <returns></returns>
        public bool Q__Should_Replace(
            double old_fitness,
            double new_fitness
            )
        {
            return Replacement_Utilities.Should_Be_Replaced(
                old_fitness: old_fitness,
                new_fitness: new_fitness,
                fitness_goal: fitness_goal,
                replacement_rule: individual_comparison_type
                );
        }

        public int Q__Num_Existing_Individuals()
        {
            int num_cells = Q__Num_Cells();
            int num_existing_individuals = 0;
            for (int c = 0; c < num_cells; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    num_existing_individuals++;
                }
            }
            return num_existing_individuals;
        }

        public bool Q__Is_Cell_Occupied(int cell)
        {
            return Q__Individual_Exists(cell);
        }

        public bool Q__Individual_Exists(int cell)
        {
            return individuals__archive[cell] != null;
        }



        public bool Q__Is_Within_Range(double[] feature_vector)
        {
            int num_dims = tessellation.num_bcs;
            for (int d = 0; d < num_dims; d++)
            {
                double v = feature_vector[d];
                if (v < tessellation.min_value__per__bc[d])
                {
                    return false;
                }
                else if (v > tessellation.max_value__per__bc[d])
                {
                    return false;
                }
            }
            return true;
        }

        public int Q__Num_Cells()
        {
            return tessellation.num_cells;
        }

        public int Q__Num_Features()
        {
            return evaluation_method__per__feature.Length;
        }

        public double Q__Feature__Min_Value(int feature_index)
        {
            return tessellation.min_value__per__bc[feature_index];
        }

        public double Q__Feature__Max_Value(int feature_index)
        {
            return tessellation.max_value__per__bc[feature_index];
        }

        public int Q__Feature__Subdivisions(int feature_index)
        {
            return tessellation.num_cells__per__bc[feature_index];
        }

        public bool Q__Is_Empty()
        {
            int num_cells = Q__Num_Cells();
            for (int c = 0; c < num_cells; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    return false;
                }
            }
            return true;
        }


        public double Q__Average_Fitness()
        {
            double fitness_sum = 0;

            int num_cells = Q__Num_Cells();

            for (int c = 0; c < num_cells; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    fitness_sum += fitness__archive[c];
                }
            }

            double average_fitness = fitness_sum / (double)num_cells;

            // here ther is a problem: should we divide by num total cells"? or bby num occupied cells?

            throw new NotImplementedException();
        }

        public double Q__Best_Fitness()
        {
            if (fitness_goal == EA__Fitness_Goal.MAXIMIZATION)
                return Q__Max_Fitness();
            else if (fitness_goal == EA__Fitness_Goal.MINIMIZATION)
                return Q__Min_Fitness();
            else
                return double.NaN;
        }

        public double Q__Max_Fitness()
        {
            double max_fitness = Double.NegativeInfinity;

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    double cell_fitness = fitness__archive[c];
                    if (cell_fitness > max_fitness)
                    {
                        max_fitness = cell_fitness;
                    }
                }
            }

            return max_fitness;
        }

        public double Q__Min_Fitness()
        {
            double min_fitness = Double.PositiveInfinity;

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    double cell_fitness = fitness__archive[c];
                    if (cell_fitness < min_fitness)
                        min_fitness = cell_fitness;
                }
            }

            return min_fitness;
        }

        public double Q__Fitness_Sum()
        {
            double fitness_sum = 0.0;

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                if (Q__Individual_Exists(c))
                {
                    double cell_fitness = fitness__archive[c];
                    fitness_sum += cell_fitness;
                }
            }

            return fitness_sum;
        }
    }
}
