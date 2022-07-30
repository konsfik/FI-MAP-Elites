using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FIME__Archive<T> where T : Data_Structure
    {
        public T[,] Q__Individuals__Table_2D() {
            if (Q__Num_Features() != 2)
                throw new System.Exception("this only works with 2d states");

            int width = tessellation.num_cells__per__bc[0];
            int height = tessellation.num_cells__per__bc[1];

            T[,] individuals_table = new T[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    individuals_table[x, y] = null;
                }
            }

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                if (Q__Is_Cell_Occupied(c))
                {
                    int[] cell_coordinates = tessellation.Q__Cell__To__Coords(c);
                    if (cell_coordinates != null)
                    {
                        T individual = individuals__archive[c];
                        individuals_table[cell_coordinates[0], cell_coordinates[1]] = individual;
                    }
                }
            }

            return individuals_table;
        }

        public double[,] Q__Fitness__Table_2D()
        {
            if (Q__Num_Features() != 2)
                throw new System.Exception("this only works with 2d states");

            int width = tessellation.num_cells__per__bc[0];
            int height = tessellation.num_cells__per__bc[1];

            double[,] fitness_table = new double[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    fitness_table[x, y] = Double.NaN;
                }
            }

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                if (Q__Is_Cell_Occupied(c))
                {
                    int[] cell_coordinates = tessellation.Q__Cell__To__Coords(c);
                    if (cell_coordinates != null)
                    {
                        double cell_fitness = fitness__archive[c];
                        fitness_table[cell_coordinates[0], cell_coordinates[1]] = cell_fitness;
                    }
                }
            }

            return fitness_table;
        }

        //public int[,] Q__Selections_Per_Location__Table_2D()
        //{
        //    if (Q__Num_Features() != 2)
        //        throw new System.Exception("this only works with 2d states");

        //    int width = tessellation.num_cells__per__bc[0];
        //    int height = tessellation.num_cells__per__bc[1];

        //    int[,] selections_per_location = new int[width, height];

        //    int nc = Q__Num_Cells();

        //    for (int c = 0; c < nc; c++)
        //    {
        //        int[] cell_coordinates = tessellation.Q__Cell_Coordinates__From__Cell_ID(c);
        //        if (cell_coordinates != null)
        //        {
        //            int x = cell_coordinates[0];
        //            int y = cell_coordinates[1];
        //            int selections = selections__per__cell[c];
        //            selections_per_location[x, y] = selections;
        //        }
        //    }

        //    return selections_per_location;
        //}

        public bool[,] Q__Individual_Exists__Table_2D()
        {
            if (Q__Num_Features() != 2)
                throw new System.Exception("this only works with 2d states");

            int width = tessellation.num_cells__per__bc[0];
            int height = tessellation.num_cells__per__bc[1];

            bool[,] individual_exists_table = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    individual_exists_table[x, y] = false;
                }
            }

            int nc = Q__Num_Cells();

            for (int c = 0; c < nc; c++)
            {
                int[] cell_coordinates = tessellation.Q__Cell__To__Coords(c);
                if (cell_coordinates != null)
                {
                    bool exists = individuals__archive[c] != null;
                    individual_exists_table[cell_coordinates[0], cell_coordinates[1]] = exists;
                }
            }

            return individual_exists_table;
        }


        //public int[,] Q__Offspring_Survivals_Per_Location__Table_2D()
        //{
        //    if (Q__Num_Features() != 2)
        //        throw new System.Exception("this only works with 2d states");

        //    int nc = Q__Num_Cells();

        //    int width = tessellation.num_cells__per__bc[0];
        //    int height = tessellation.num_cells__per__bc[1];

        //    int[,] offspring_survivals_per_location = new int[width, height];

        //    for (int c = 0; c < nc; c++)
        //    {
        //        int[] cell_coordinates = tessellation.Q__Cell_Coordinates__From__Cell_ID(c);
        //        if (cell_coordinates != null)
        //        {
        //            int x = cell_coordinates[0];
        //            int y = cell_coordinates[1];
        //            int offspring_survivals = offspring_survivals__per__cell[c];
        //            offspring_survivals_per_location[x, y] = offspring_survivals;
        //        }
        //    }

        //    return offspring_survivals_per_location;
        //}

        //public int[,] Q__Offspring_Survivals_Per_Individual__Table_2D()
        //{
        //    if (Q__Num_Features() != 2)
        //        throw new System.Exception("this only works with 2d states");

        //    int nc = Q__Num_Cells();

        //    int width = tessellation.num_cells__per__bc[0];
        //    int height = tessellation.num_cells__per__bc[1];

        //    int[,] offspring_survivals_per_individual = new int[width, height];

        //    for (int c = 0; c < nc; c++)
        //    {
        //        int[] cell_coordinates = tessellation.Q__Cell_Coordinates__From__Cell_ID(c);
        //        if (cell_coordinates != null)
        //        {
        //            int x = cell_coordinates[0];
        //            int y = cell_coordinates[1];
        //            int offspring_survivals = offspring_survivals__per__individual[c];
        //            offspring_survivals_per_individual[x, y] = offspring_survivals;
        //        }
        //    }

        //    return offspring_survivals_per_individual;
        //}
    }
}
