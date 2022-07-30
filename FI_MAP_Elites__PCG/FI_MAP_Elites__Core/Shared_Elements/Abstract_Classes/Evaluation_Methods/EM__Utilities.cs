using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public static class EM__Utilities
    {
        public static bool Is_Better_Than<T>(
            this T this_individual,
            T other_individual,
            Evaluation_Method<T> evaluation_method,
            EA__Fitness_Goal goal
            )
            where T : Data_Structure
        {
            double this_fitness = evaluation_method.Evaluate_Individual(this_individual);
            double other_fitness = evaluation_method.Evaluate_Individual(other_individual);

            if (goal == EA__Fitness_Goal.MAXIMIZATION)
            {
                return this_fitness > other_fitness;
            }
            else if (goal == EA__Fitness_Goal.MINIMIZATION)
            {
                return this_fitness < other_fitness;
            }

            return false;
        }

        public static bool Is_Better_Than(
            this double this_fitness,
            double other_fitness,
            EA__Fitness_Goal goal
            )
        {
            if (goal == EA__Fitness_Goal.MAXIMIZATION)
            {
                return this_fitness > other_fitness;
            }
            else if (goal == EA__Fitness_Goal.MINIMIZATION)
            {
                return this_fitness < other_fitness;
            }
            return false;
        }

    }
}
