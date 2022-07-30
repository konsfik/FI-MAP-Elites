using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public static class UCB_Utilities
    {
        public static void Update_UCB_Values(
            double[] ucb_values,
            double[] selections__archive,
            double[] rewards__archive,
            bool[] exist,
            double c_value
            )
        {
            double total_selections = selections__archive.Q__Sum();

            int num_cells = exist.Length;


            for (int c = 0; c < num_cells; c++)
            {
                bool exists = exist[c];
                if (exists)
                {
                    double selections = selections__archive[c];
                    double reward = rewards__archive[c];

                    double ucb_value = UCB_Value_Calculation(
                        selections,
                        reward,
                        total_selections,
                        c_value
                        );

                    ucb_values[c] = ucb_value;
                }
                else
                {
                    ucb_values[c] = Double.NegativeInfinity;
                }
            }
        }

        public static void Update_UCB_Values(
            double[] ucb_values,
            double[] selections__archive,
            double[][] rewards__archive,
            bool[] exist,
            double c_value
            )
        {
            double total_selections = selections__archive.Q__Sum();

            int num_cells = exist.Length;


            for (int c = 0; c < num_cells; c++)
            {
                bool exists = exist[c];
                if (exists)
                {
                    double selections = selections__archive[c];
                    double reward = rewards__archive[c].Sum();

                    double ucb_value = UCB_Value_Calculation(
                        selections,
                        reward,
                        total_selections,
                        c_value
                        );

                    ucb_values[c] = ucb_value;
                }
                else
                {
                    ucb_values[c] = Double.NegativeInfinity;
                }
            }
        }

        public static double UCB_Value_Calculation(
            double times_selected,
            double reward_sum,
            double parent_times_selected,
            double c_value
            )
        {
            if (times_selected <= 0)
            {
                return Double.PositiveInfinity;
            }
            else
            {
                double ni = times_selected;
                double wi = reward_sum;
                double Ni = parent_times_selected;

                double ucb_1_value = (wi / ni) + c_value * Math.Sqrt(Math.Log(Ni) / ni);

                return ucb_1_value;
            }
        }
    }
}
