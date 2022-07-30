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
using System.Text.Json;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class CMCE__Experiment_Runner<T>
        where T : Data_Structure
    {
        public void Util__Process_Data(int num_evals)
        {
            if (console_out_evals.Contains(num_evals))
            {
                Util__Console_Out(num_evals);
            }

            if (save_png_evals.Contains(num_evals))
            {
                Util__Save_PNGs(num_evals);
            }

            if (save_population_evals.Contains(num_evals))
            {
                Util__Save_Populations(num_evals);
            }
        }

        public void Util__Console_Out(int eval)
        {
            Console.WriteLine(
                    "Evaluation: " + eval.ToString()
                    + " | Best feasible fitness: " + algorithm.feasible_state.Q__Best_Fitness()
                    + " | Best infeasible fitness: " + algorithm.infeasible_state.Q__Best_Fitness()
                    );
        }

        public void Util__Save_Populations(int eval)
        {
            string populations_output_folder =
                output_folder
                + "\\population_eval_"
                + eval.ToString();
            IO_Utilities.CreateFolder(populations_output_folder);

            string feasible_population_output_folder =
                populations_output_folder
                + "\\feasible_pop";
            IO_Utilities.CreateFolder(feasible_population_output_folder);

            Util__Save_Population(
                feasible_population_output_folder,
                algorithm.feasible_state
                );

            string infeasible_population_output_folder =
                populations_output_folder
                + "\\infeasible_pop";
            IO_Utilities.CreateFolder(infeasible_population_output_folder);

            Util__Save_Population(
                infeasible_population_output_folder,
                algorithm.infeasible_state
                );

        }

        private void Util__Save_Population(
            string this_population_output_folder,
            FIME__Archive<T> state
            )
        {
            string stats_file_path =
                this_population_output_folder
                + "\\stats.csv";

            string stats_header =
                "cell_id,";

            int num_bcs = state.tessellation.num_bcs;
            for (int i = 0; i < num_bcs; i++)
            {
                stats_header += "cell_coord_" + i.ToString() + ",";
            }
            for (int i = 0; i < num_bcs; i++)
            {
                stats_header += "feature_value_" + i.ToString() + ",";
            }
            stats_header += "fitness";

            if (ds_analyzer != null)
            {
                stats_header += ",";
                stats_header += ds_analyzer.Q__CSV__Data_Header(",", "");
            }

            stats_header += "\n";

            IO_Utilities.Append_To_File(
                stats_file_path,
                stats_header
                );

            // save the state's population
            int num_cells = state.tessellation.num_cells;
            for (int c = 0; c < num_cells; c++)
            {
                T individual = state.individuals__archive[c];
                if (individual == null) continue;


                double[] feature_vector =
                    state
                    .feature_vectors__archive[c];

                int[] cell_coordinates =
                    state
                    .tessellation
                    .Q__Cell__To__Coords(c);

                double fitness = 
                    state
                    .fitness__archive[c];

                string data_row = "";
                data_row += (c.ToString() + ",");
                for (int i = 0; i < num_bcs; i++)
                {
                    data_row += cell_coordinates[i] + ",";
                }
                for (int i = 0; i < num_bcs; i++)
                {
                    data_row += feature_vector[i] + ",";
                }
                data_row += fitness.ToString();

                if (ds_analyzer != null)
                {
                    ds_analyzer.Update(individual);
                    data_row += ",";
                    data_row += ds_analyzer.Q__CSV__Data_Row(",", "");
                }

                data_row += "\n";

                IO_Utilities.Append_To_File(
                    stats_file_path,
                    data_row
                );

                // save json
                string serialized_individual = serialization_method.Serialize_To_String(individual);

                string ind_json_path =
                    this_population_output_folder
                    + "\\ind_"
                    + c.ToString()
                    + ".json";

                IO_Utilities.Append_To_File(
                    ind_json_path,
                    serialized_individual
                    );

                // save png
                using (SKBitmap img = visualization_method__high_res.Generate_Bitmap(individual))
                {
                    string ind_png_path =
                        this_population_output_folder
                        + "\\ind_"
                        + c.ToString()
                        + ".png";

                    img.Save_To_Disk(ind_png_path);
                }
            }
        }


        private void Util__Save_PNGs(int num_evals) {
            using (
                    SKBitmap feasible_state_bitmap =
                    algorithm
                    .feasible_state
                    .Draw(
                        solution_visualizer: visualization_method__low_res,
                        gap_size: 2,
                        selection_window: null
                        )
                    )
            {
                feasible_state_bitmap.Save_To_Disk(
                    output_folder
                    + "//feasible_state__evaluation_"
                    + num_evals.ToString()
                    + ".png");
            }

            using (
                SKBitmap infeasible_state_bitmap =
                algorithm.infeasible_state.Draw(
                    solution_visualizer: visualization_method__low_res,
                    gap_size: 2,
                    selection_window: null
                    )
                )
            {
                infeasible_state_bitmap.Save_To_Disk(
                    output_folder
                    + "//infeasible_state__evaluation_"
                    + num_evals.ToString()
                    + ".png");
            }
        }
    }
}
