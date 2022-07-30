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
        public BS_Ortho_Tessellation tessellation;
        public int num_evaluations;

        public List<int> console_out_evals;
        public List<int> save_csv_evals;
        public List<int> save_png_evals;
        public List<int> save_population_evals;

        public bool save_all_individuals__stats;

        public string output_folder;
        public string all_feasible_individuals_stats_file_path;
        public string all_infeasible_individuals_stats_file_path;

        public int initial_population_size;
        public int max_num_evals;

        public FI_MAP_Elites<T> algorithm;

        public Visualization_Method<T> visualization_method__low_res;
        public Visualization_Method<T> visualization_method__high_res;

        public Custom_Serialization_Method<T> serialization_method;

        public CMCE_Stats__Basic<T> algorithm_operation_stats;

        public Data_Structure_Analyzer<T> ds_analyzer;

        public CMCE__Experiment_Runner(
            string output_folder,

            int max_num_evals,
            int initial_population_size,

            FI_MAP_Elites__Settings<T> cmce_settings,

            List<int> console_out_evals,
            List<int> save_csv_evals,
            List<int> save_png_evals,
            List<int> save_population_evals,

            bool save_all_individuals__stats,

            Data_Structure_Analyzer<T> ds_analyzer,
            Visualization_Method<T> visualization_method__low_res,
            Visualization_Method<T> visualization_method__high_res,
            Custom_Serialization_Method<T> serialization_method
            )
        {
            this.output_folder = output_folder;
            this.max_num_evals = max_num_evals;
            this.initial_population_size = initial_population_size;

            this.tessellation = (BS_Ortho_Tessellation)cmce_settings.tessellation.Q__Deep_Copy();

            int num_cells = tessellation.num_cells;

            algorithm = new FI_MAP_Elites<T>(cmce_settings);

            this.console_out_evals = console_out_evals.Q__Deep_Copy();
            this.save_csv_evals = save_csv_evals.Q__Deep_Copy();
            this.save_png_evals = save_png_evals.Q__Deep_Copy();
            this.save_population_evals = save_population_evals.Q__Deep_Copy();

            this.ds_analyzer = (Data_Structure_Analyzer<T>)ds_analyzer.Q__Deep_Copy();
            this.visualization_method__low_res = visualization_method__low_res;
            this.visualization_method__high_res = visualization_method__high_res;
            this.serialization_method = serialization_method;

            // subscribe to algorithm events...
            algorithm_operation_stats = new CMCE_Stats__Basic<T>(save_csv_evals, this.tessellation, output_folder);
            algorithm_operation_stats.M__Subscribe_To_CMCE_Algorithm(algorithm);

            M__Subscribe_To_CMCE_Algorithm();

        }

        private void M__Subscribe_To_CMCE_Algorithm() {
            algorithm.event__individual_generated += On_Individual_Generated;
            algorithm.event__offspring_generated += On_Offspring_Generated;
        }

        public void Run_Experiment(
            I_PRNG rand
            )
        {
            algorithm.M__Generate_Initial_Population(rand, initial_population_size);

            //Util__Process_Data(0);

            while (algorithm.num_evaluations <= max_num_evals)
            {
                algorithm.M__Advance__One_Step(rand);
            }
        }

        

    }
}
