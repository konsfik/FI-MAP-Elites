using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Visualization;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

using SkiaSharp;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class CMCE_Stats__Basic<T>
        where T : Data_Structure
    {
        public void Calculate_And_Save_Summary()
        {
            bool first_time = num_evals <= 1;

            string output_file_path =
                output_folder
                + "\\summary.csv";

            if (first_time) {
                IO_Utilities.Append_To_File(
                    output_file_path,
                    Summary__Header() + "\n"
                    );
            }

            IO_Utilities.Append_To_File(
                    output_file_path,
                    Summary__Row(tessellation) + "\n"
                    );
        }

        public string Summary__Row(
            BS_Ortho_Tessellation tessellation
            )
        {
            string row = "";

            // 1. date_time
            // 2. date_time_ticks
            row += DateTime.UtcNow.ToString() + ",";
            row += DateTime.UtcNow.Ticks.ToString() + ",";

            // 3. num_evals
            // 4. num_cells
            row += num_evals.ToString() + ",";
            int num_cells = tessellation.num_cells;
            row += tessellation.num_cells.ToString() + ",";

            // 5. individual_exists_on_FS__sum
            // 6. individual_exists_on_IS__sum
            int individual_exists_on_FS__sum = individual_exists__on_FS.Q__Sum();
            row += individual_exists_on_FS__sum.ToString() + ",";
            int individual_exists_on_IS__sum = individual_exists__on_IS.Q__Sum();
            row += individual_exists_on_IS__sum.ToString() + ",";

            // 7. fitness_on_FS__sum
            // 8. fitness_on_IS__sum
            double fitness_on_FS__sum = Fitness_Sum__On_FS(num_cells);
            row += fitness_on_FS__sum.ToString() + ",";
            double fitness_on_IS__sum = Fitness_Sum__On_IS(num_cells);
            row += fitness_on_IS__sum.ToString() + ",";


            // 9. num_landings_on_FS__sum
            // 10. num_landings_on_IS__sum
            row += num_landings__on_FS.Q__Sum().ToString() + ",";
            row += num_landings__on_IS.Q__Sum().ToString() + ",";

            // 11. num_survivals__on_FS__sum
            // 12. num_survivals__on_IS__sum
            row += num_survivals__on_FS.Q__Sum().ToString() + ",";
            row += num_survivals__on_IS.Q__Sum().ToString() + ",";

            // 13. num_selections__on_FS__sum
            // 14. num_selections__on_IS__sum
            row += num_selections__on_FS.Q__Sum().ToString() + ",";
            row += num_selections__on_IS.Q__Sum().ToString() + ",";

            // 15. coverage_on_FS
            // 16. coverage_on_IS
            double coverage_on_FS = (double)individual_exists_on_FS__sum / (double)num_cells;
            row += coverage_on_FS.ToString() + ",";
            double coverage_on_IS = (double)individual_exists_on_IS__sum / (double)num_cells;
            row += coverage_on_IS.ToString() + ",";

            // 17. highest_fitness_on_FS
            // 18. highest_fitness_on_IS
            double highest_fitness_on_FS = Highest_Fitness__On_FS(num_cells);
            row += highest_fitness_on_FS.ToString() + ",";
            double highest_fitness_on_IS = Highest_Fitness__On_IS(num_cells);
            row += highest_fitness_on_IS.ToString() + ",";

            // 19. fitness_sum_over_num_individuals_on_FS
            // 20. fitness_sum_over_num_individuals_on_IS
            double fitness_sum_over_num_individuals_on_FS = fitness_on_FS__sum / (double)individual_exists_on_FS__sum;
            row += fitness_sum_over_num_individuals_on_FS.ToString() + ",";
            double fitness_sum_over_num_individuals_on_IS = fitness_on_IS__sum / (double)individual_exists_on_IS__sum;
            row += fitness_sum_over_num_individuals_on_IS.ToString() + ",";

            // 21. fitness_sum_over_num_cells_on_FS
            // 22. fitness_sum_over_num_cells_on_IS
            double fitness_sum_over_num_cells_on_FS = fitness_on_FS__sum / (double)num_cells;
            row += fitness_sum_over_num_cells_on_FS.ToString() + ",";
            double fitness_sum_over_num_cells_on_IS = fitness_on_IS__sum / (double)num_cells;
            row += fitness_sum_over_num_cells_on_IS.ToString() + ",";

            return row;
        }

        public string Summary__Header()
        {
            string header = "";

            // 1. date_time
            // 2. date_time_ticks
            header += "date_time,";
            header += "date_time_ticks,";

            // 3. num_evals
            // 4. num_cells
            header += "num_evals,";
            header += "num_cells,";

            // 5. individual_exists_on_FS__sum
            // 6. individual_exists_on_IS__sum
            header += "individual_exists_on_FS__sum,";
            header += "individual_exists_on_IS__sum,";

            // 7. fitness_on_FS__sum
            // 8. fitness_on_IS__sum
            header += "fitness_on_FS__sum,";
            header += "fitness_on_IS__sum,";

            // 9. num_landings_on_FS__sum
            // 10. num_landings_on_IS__sum
            header += "num_landings_on_FS__sum,";
            header += "num_landings_on_IS__sum,";

            // 11. num_survivals__on_FS
            // 12. num_survivals__on_IS
            header += "num_survivals_on_FS__sum,";
            header += "num_survivals_on_IS__sum,";

            // 13. num_selections__on_FS
            // 14. num_selections__on_IS
            header += "num_selections_on_FS__sum,";
            header += "num_selections_on_IS__sum,";

            // 15. coverage_on_FS
            // 16. coverage_on_IS
            header += "coverage_on_FS,";
            header += "coverage_on_IS,";

            // 17. highest_fitness_on_FS
            // 18. highest_fitness_on_IS
            header += "highest_fitness_on_FS,"; // global performance
            header += "highest_fitness_on_IS,"; // global performance

            // 19. fitness_sum_over_num_individuals_on_FS
            // 20. fitness_sum_over_num_individuals_on_IS
            header += "fitness_sum_over_num_individuals_on_FS,"; // ~ precision
            header += "fitness_sum_over_num_individuals_on_IS,"; // ~ precision

            // 21. fitness_sum_over_num_cells_on_FS
            // 22. fitness_sum_over_num_cells_on_IS
            header += "fitness_sum_over_num_cells_on_FS,"; // ~ global reliability
            header += "fitness_sum_over_num_cells_on_IS,"; // ~ global reliability

            return header;
        }

        private double Highest_Fitness__On_FS(int num_cells)
        {
            double max = double.NegativeInfinity;
            for (int c = 0; c < num_cells; c++)
            {
                bool exists = individual_exists__on_FS[c] == 1;
                if (!exists)
                {
                    continue;
                }
                else
                {
                    double fit = fitness__on_FS[c];
                    if (fit > max) max = fit;
                }
            }
            return max;
        }

        private double Highest_Fitness__On_IS(int num_cells)
        {
            double max = double.NegativeInfinity;
            for (int c = 0; c < num_cells; c++)
            {
                bool exists = individual_exists__on_IS[c] == 1;
                if (!exists)
                {
                    continue;
                }
                else
                {
                    double fit = fitness__on_IS[c];
                    if (fit > max) max = fit;
                }
            }
            return max;
        }

        private double Fitness_Sum__On_FS(int num_cells)
        {
            double sum = 0.0;
            for (int c = 0; c < num_cells; c++)
            {
                bool exists = individual_exists__on_FS[c] == 1;
                if (!exists)
                {
                    continue;
                }
                else
                {
                    double fit = fitness__on_FS[c];
                    sum += fit;
                }
            }
            return sum;
        }

        private double Fitness_Sum__On_IS(int num_cells)
        {
            double sum = 0.0;
            for (int c = 0; c < num_cells; c++)
            {
                bool exists = individual_exists__on_IS[c] == 1;
                if (!exists)
                {
                    continue;
                }
                else
                {
                    double fit = fitness__on_IS[c];
                    sum += fit;
                }
            }
            return sum;
        }
    }
}
