using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.CMCE;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;

using SkiaSharp;

namespace FI_MAP_Elites__Experiments
{

    public static class FIME_Experiments_Utils
    {
        private static DSA__Architectural_Plan Prepare_Analyzer(
            double min_percent_active_cells,
            double area_error_margin,
            double min_bridge_line_magnitude
            )
        {
            DSA__Architectural_Plan analyzer = new DSA__Architectural_Plan(
                cem__l1__voronoi_connected: new CEM__L1__Voronoi_Connected<DS__Architectural_Plan>(),
                cem__l1__voronoi_min_percent_active_cells: new CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>(min_percent_active_cells),

                cem__l2__space_units_exist: new CEM__L2__Space_Units_Exist<DS__Architectural_Plan>(),
                cem__l2__space_units_are_coherent: new CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>(),
                cem__l2__prescribed_connections_exist: new CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>(),
                cem__l2__space_units_areas_within_margin: new CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>(area_error_margin),

                cem__l3__prescribed_openings: new CEM__L3__Prescribed_Openings(),
                cem__l3__not_narrow_passages: new CEM__L3__Not_Narrow_Passages(min_bridge_line_magnitude),

                em__cem__l1__voronoi_connected: new EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan>(),
                em__cem__l1__voronoi_min_percent_active_cells: new EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>(min_percent_active_cells),
                em__cem__l2__space_units_exist: new EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan>(),
                em__cem__l2__space_units_are_coherent: new EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>(),
                em__cem__l2__prescribed_connections_exist: new EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>(),
                em__cem__l2__space_units_areas_within_margin: new EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>(area_error_margin),
                em__cem__l3__prescribed_openings: new EM__CEM__L3__Prescribed_Openings(),
                em__cem__l3__not_narrow_passages: new EM__CEM__L3__Not_Narrow_Passages(min_bridge_line_magnitude),

                em__l2__compactness: new EM__L2__Compactness<DS__Architectural_Plan>(),
                em__l2__compactness_per_space_unit: new EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan>(),
                em__l2__used_cells_compactness_average: new EM__L2__Used_Cells_Compactness<DS__Architectural_Plan>(),
                em__l2__lines_orthogonality: new EM__L2__Lines_Orthogonality<DS__Architectural_Plan>(),
                em__l2__lines_orthogonality_weighted: new EM__L2__Lines_Orthogonality_Weighted<DS__Architectural_Plan>(),
                em__l2__angles_orthogonality: new EM__L2__Angles_Orthogonality<DS__Architectural_Plan>(),
                em__l2__angles_non_acute: new EM__L2__Angles_Non_Acute<DS__Architectural_Plan>(),
                em__l2__space_units_area_precision: new EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>(),

                em__l3__avg_dist_entrances_connection_doors_normalized: new EM__L3__Avg_Dist_Entrances_Connection_Doors_Normalized(),
                em__l3__avg_dist_connection_doors_normalized: new EM__L3__Avg_Dist_Connection_Doors_Normalized(),
                em__l3__avg_dist_windows_normalized: new EM__L3__Avg_Dist_Windows_Normalized(),
                em__l3__percent_circulation_area: new EM__L3__Percent_Circulation_Area()
                );

            return analyzer;
        }

        public static void Run__CMCE__Experiment(
            DS__Layout_Constraints layout_constraints,
            Tessellation_Type tessellation_type,
            string output_folder
            )
        {
            PRNG_Basic rand = new PRNG_Basic();

            double min_percent_active_cells = 0.5;
            double area_error_margin = 0.4;
            double min_bridge_line_magnitude = 0.5;

            int archive_size = 16;

            Generation_Method<DS__Architectural_Plan> generation_method =
                Templates__PCG_Workshop
                .Template__Generation_Method(
                    layout_constraints,
                    tessellation_type
                    );

            Mutation_Method<DS__Architectural_Plan> mutation_method;
            var bc1 = Templates__PCG_Workshop.Q__BC1();
            var bc2 = Templates__PCG_Workshop.Q__BC2();

            if (tessellation_type == Tessellation_Type.ORTHO)
            {
                mutation_method =
                    Templates__PCG_Workshop
                    .Template__Mutation_Method__Ortho_Hex_Grid(
                        area_error_margin
                        );

            }
            else if (tessellation_type == Tessellation_Type.HEX)
            {
                mutation_method =
                    Templates__PCG_Workshop
                    .Template__Mutation_Method__Ortho_Hex_Grid(
                        area_error_margin
                        );

            }
            else if (tessellation_type == Tessellation_Type.RANDOM)
            {
                mutation_method =
                    Templates__PCG_Workshop
                    .Template__Mutation_Method__Free_Style(
                        area_error_margin
                        );

            }
            else
            {
                throw new Exception("error here!");
            }

            var feasible_state__fitness_function = new EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>();
            var feasible_state__fitness_goal = EA__Fitness_Goal.MAXIMIZATION;
            var feasible_state__replacement_rule = EA__Individual_Replacement_Rule.REPLACE_IF_BETTER_OR_EQUAL;

            var infeasible_state__fitness_function =
                Templates__PCG_Workshop
                .Template__Infeasible_Fitness_Function(
                    min_percent_active_cells: min_percent_active_cells,
                    area_error_margin: area_error_margin,
                    min_bridge_line_magnitude: min_bridge_line_magnitude
                    );
            var infeasible_state_fitness_goal = EA__Fitness_Goal.MAXIMIZATION;
            var infeasible_state__replacement_rule = EA__Individual_Replacement_Rule.REPLACE_IF_BETTER_OR_EQUAL;

            Evaluation_Method<DS__Architectural_Plan>[] evaluation_method__per__feature =
                new Evaluation_Method<DS__Architectural_Plan>[] {
                    (Evaluation_Method<DS__Architectural_Plan>)bc1.Q__Deep_Copy(),
                    (Evaluation_Method<DS__Architectural_Plan>)bc2.Q__Deep_Copy()
                };
            BS_Ortho_Tessellation tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { archive_size, archive_size },
                min_value__per__bc: new double[] { 0, 0 },
                max_value__per__bc: new double[] { 1, 1 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.DISCARD
                );

            var feasibility_discrimination_method =
                Templates__PCG_Workshop.Template__Feasibility_Discrimination_Method(
                    min_percent_active_cells: min_percent_active_cells,
                    area_error_margin: area_error_margin,
                    min_bridge_line_magnitude: min_bridge_line_magnitude
                    );

            var parent_selection_method = new PSM__UCB_Per_Location__Discovery<DS__Architectural_Plan>(
                1.0 / Math.Sqrt(2.0),
                tessellation
                );

            FI_MAP_Elites__Settings<DS__Architectural_Plan> settings =
                new FI_MAP_Elites__Settings<DS__Architectural_Plan>(
                    generation_method,
                    feasible_state__fitness_function: feasible_state__fitness_function,
                    feasible_state__fitness_goal: feasible_state__fitness_goal,
                    feasible_state__replacement_rule: feasible_state__replacement_rule,
                    feasible_state_mutation_method: mutation_method,
                    infeasible_state__fitness_function: infeasible_state__fitness_function,
                    infeasible_state__fitness_goal: infeasible_state_fitness_goal,
                    infeasible_state__replacement_rule: infeasible_state__replacement_rule,
                    infeasible_state_mutation_method: mutation_method,
                    evaluation_method__per__feature: evaluation_method__per__feature,
                    tessellation: tessellation,
                    feasibility_discrimination_method: feasibility_discrimination_method,
                    parent_selection_method: parent_selection_method
                    );

            int num_generations = 524_288;
            int initial_population_size = 100;
            List<int> save_csv_gens = new List<int>() {
                0,
                    1,
                2,
                    3,
                4,
                    (4 + 1), (4 + 2), (4 + 2 + 1),
                8,
                    (8 + 2), (8 + 4), (8 + 4 + 2),
                16,
                    (16 + 4), (16 + 8), (16 + 8 + 4),
                32,
                    (32 + 8), (32 + 16), (32 + 16 + 8),
                64,
                    (64 + 16), (64 + 32), (64 + 32 + 16),
                128,
                    (128 + 32), (128 + 64), (128 + 64 + 32),
                256,
                    (256 + 64), (256 + 128), (256 + 128 + 64),
                512,
                    (512 + 128), (512 + 256), (512 + 256 + 128),
                1_024,
                    (1024 + 256), (1024 + 512), (1024 + 512 + 256),
                2_048,
                    (2_048 + 512), (2_048 + 1024), (2_048 + 1024 + 512),
                4_096,
                    (4_096 + 1024), (4_096 + 2_048), (4_096 + 2_048 + 1024),
                8_192,
                    (8_192 + 2_048), (8_192 + 4_096), (8_192 + 4_096 + 2_048),
                16_384,
                    (16_384 + 4_096), (16_384 + 8_192), (16_384 + 8_192 + 4_096),
                32_768,
                    (32_768 + 8_192), (32_768 + 16_384), (32_768 + 16_384 + 8_192),
                65_536,
                    (65_536 + 16_384), (65_536 + 32_768), (65_536 + 32_768 + 16_384),
                131_072,
                    (131_072 + 32_768), (131_072 + 65_536), (131_072 + 65_536 + 32_768),
                262_144,
                    (262_144 + 65_536), (262_144 + 131_072), (262_144 + 131_072 + 65_536),
                524_288
            };

            List<int> save_png_gens = new List<int>() {
                0,
                2,
                4,
                8,
                16,
                32,
                64,
                128,
                256,
                512,
                1_024,
                2_048,
                4_096,
                8_192,
                16_384,
                32_768,
                65_536,
                131_072,
                262_144,
                524_288
            };

            foreach (var gen in save_csv_gens)
            {
                Console.WriteLine(gen);
            }

            List<int> save_pop_gens = new List<int>() {
                524_288
            };

            DSA__Architectural_Plan analyzer = Prepare_Analyzer(
                min_percent_active_cells: min_percent_active_cells,
                area_error_margin: area_error_margin,
                min_bridge_line_magnitude: min_bridge_line_magnitude
                );

            CMCE__Experiment_Runner<DS__Architectural_Plan> experiment_runner =
                new CMCE__Experiment_Runner<DS__Architectural_Plan>(
                    output_folder: output_folder,
                    max_num_evals: num_generations,
                    initial_population_size: initial_population_size,
                    cmce_settings: settings,
                    console_out_evals: save_csv_gens,
                    save_csv_evals: save_csv_gens,
                    save_png_evals: save_png_gens,
                    save_population_evals: save_pop_gens,
                    save_all_individuals__stats: false,
                    ds_analyzer: analyzer,
                    visualization_method__low_res: Templates__PCG_Workshop.Prepare_Visualization_Method_Low_Res(),
                    visualization_method__high_res: Templates__PCG_Workshop.Prepare_Visualization_Method_High_Res(),
                    new CSM__Architectural_Plan_Json()
                    );

            VM__Layout_Constraints layout_constraints_visualization =
                new VM__Layout_Constraints();

            using (var bmp = layout_constraints_visualization.Generate_Bitmap(layout_constraints))
            {
                bmp.Save_To_Disk(output_folder + "\\layout_constraints.png");
            }

            experiment_runner.Run_Experiment(rand);
        }

        public static int Q__Num_Repetitions() 
        {
            return 1;
        }
    }
}
