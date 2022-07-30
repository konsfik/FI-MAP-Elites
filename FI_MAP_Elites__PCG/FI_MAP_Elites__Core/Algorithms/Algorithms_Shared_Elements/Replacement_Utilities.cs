using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.Shared_Elements
{
    public static class Replacement_Utilities
    {
        /// <summary>
        /// Returns whether the new fitness should replace the old fitness
        /// </summary>
        /// <param name="old_fitness"></param>
        /// <param name="new_fitness"></param>
        /// <param name="fitness_goal"></param>
        /// <param name="replacement_rule"></param>
        /// <returns></returns>
        public static bool Should_Be_Replaced(
            double old_fitness,
            double new_fitness,
            EA__Fitness_Goal fitness_goal,
            EA__Individual_Replacement_Rule replacement_rule
            )
        {
            switch (replacement_rule)
            {
                case EA__Individual_Replacement_Rule.REPLACE_ALWAYS:
                    return true;
                case EA__Individual_Replacement_Rule.REPLACE_IF_BETTER_OR_EQUAL:
                    switch (fitness_goal)
                    {
                        case EA__Fitness_Goal.MAXIMIZATION:
                            bool old_nan = double.IsNaN(old_fitness);
                            bool new_nan = double.IsNaN(new_fitness);
                            bool both_nan = (old_nan == true) && (new_nan == true);
                            bool only_old_nan = (old_nan == true) && (new_nan == false);
                            bool only_new_nan = (old_nan == false) && (new_nan == true);
                            bool none_nan = (old_nan == false) && (new_nan == false);

                            if (both_nan) return true;
                            else if (only_old_nan) return true;
                            else if (only_new_nan) return false;
                            else if (none_nan)
                            {
                                if (new_fitness >= old_fitness) return true;
                                else return false;
                            }
                            return false;

                        case EA__Fitness_Goal.MINIMIZATION:
                            old_nan = double.IsNaN(old_fitness);
                            new_nan = double.IsNaN(new_fitness);
                            both_nan = (old_nan == true) && (new_nan == true);
                            only_old_nan = (old_nan == true) && (new_nan == false);
                            only_new_nan = (old_nan == false) && (new_nan == true);
                            none_nan = (old_nan == false) && (new_nan == false);
                            if (both_nan) return true;
                            else if (only_old_nan) return true;
                            else if (only_new_nan) return false;
                            else if (none_nan) 
                            {
                                if (new_fitness <= old_fitness) return true;
                                else return false;
                            }
                            return false;

                        default:
                            return false;
                    }

                case EA__Individual_Replacement_Rule.REPLACE_IF_BETTER:
                    switch (fitness_goal)
                    {
                        case EA__Fitness_Goal.MAXIMIZATION:
                            bool old_nan = double.IsNaN(old_fitness);
                            bool new_nan = double.IsNaN(new_fitness);
                            bool both_nan = (old_nan == true) && (new_nan == true);
                            bool only_old_nan = (old_nan == true) && (new_nan == false);
                            bool only_new_nan = (old_nan == false) && (new_nan == true);
                            bool none_nan = (old_nan == false) && (new_nan == false);

                            if (both_nan) return false;
                            else if (only_old_nan) return true;
                            else if (only_new_nan) return false;
                            else if (none_nan)
                            {
                                if (new_fitness > old_fitness) return true;
                                else return false;
                            }
                            return false;

                        case EA__Fitness_Goal.MINIMIZATION:
                            old_nan = double.IsNaN(old_fitness);
                            new_nan = double.IsNaN(new_fitness);
                            both_nan = (old_nan == true) && (new_nan == true);
                            only_old_nan = (old_nan == true) && (new_nan == false);
                            only_new_nan = (old_nan == false) && (new_nan == true);
                            none_nan = (old_nan == false) && (new_nan == false);

                            if (both_nan) return false;
                            else if (only_old_nan) return true;
                            else if (only_new_nan) return false;
                            else if (none_nan) // none nan
                            {
                                if (new_fitness < old_fitness) return true;
                                else return false;
                            }
                            return false;

                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }
    }
}
