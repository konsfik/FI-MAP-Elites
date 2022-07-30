using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static partial class Templates__Interactive_QD
    {
        public static Mutation_Method<DS__Architectural_Plan> Template__Mutation_Method(
            double area_error_margin
            )
        {
            MM__Sequence__Recalculations mutation_method =
                new MM__Sequence__Recalculations(
                    new List<Mutation_Method<DS__Architectural_Plan>>() {
                        Template__MM__Destruction(1,3),
                        Template__MM__Repair(area_error_margin)
                    },
                    recalculate_phenotype: false,
                    starting_level: 1
                );
            return mutation_method;
        }

        private static Mutation_Method<DS__Architectural_Plan> Template__MM__Destruction(
            int minimum_selections,
            int maximum_selections
            )
        {
            MM__Random_Selection__Recalculations destruction_L1 =
                new MM__Random_Selection__Recalculations(
                    mutation_methods_list: new List<Mutation_Method<DS__Architectural_Plan>>() {
                        new MM__Destruction__L1__Move_Voronoi_Points<DS__Architectural_Plan>(
                            cell_selection_probability: 0.01,
                            cell_mutation_intensity: 1.0,
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L1__Move_Voronoi_Points<DS__Architectural_Plan>(
                            cell_selection_probability: 0.01,
                            cell_mutation_intensity: 0.05,
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L1__Move_Voronoi_Points<DS__Architectural_Plan>(
                            cell_selection_probability: 0.5,
                            cell_mutation_intensity: 0.05,
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L1__Move_All_Voronoi_Points<DS__Architectural_Plan>(
                            cell_mutation_intensity: 0.25,
                            recalculate_phenotype: false
                            )
                        },
                    recalculate_phenotype: false,
                    starting_level: 1
                    );

            MM__Random_Selection__Recalculations destruction_L2 =
                new MM__Random_Selection__Recalculations(
                    mutation_methods_list: new List<Mutation_Method<DS__Architectural_Plan>>() {
                        new MM__Destruction__L2__Delete_Space_Unit<DS__Architectural_Plan>(
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L2__Blow_Up_Space_Unit<DS__Architectural_Plan>(
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L2__Blow_Up_Space_Unit__Safe<DS__Architectural_Plan>(
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L2__Erode_Space_Unit__Safe<DS__Architectural_Plan>(
                            recalculate_phenotype: false
                            )
                    },
                    recalculate_phenotype: false,
                    starting_level: 2
                    );

            MM__Random_Selection__Recalculations destruction_L3 =
                new MM__Random_Selection__Recalculations(
                    mutation_methods_list: new List<Mutation_Method<DS__Architectural_Plan>>()
                    {
                        new MM__Destruction__L3__Delete_Random_Openings(
                            0.05,
                            recalculate_phenotype: false
                            ),
                        new MM__Destruction__L3__Delete_Random_Openings(
                            0.5,
                            recalculate_phenotype: false
                            ),
                    },
                    recalculate_phenotype: false,
                    starting_level: 2
                    );

            MM__Random_Selections__Recalculations destruction_method =
                new MM__Random_Selections__Recalculations(
                    mutation_methods_list: new List<Mutation_Method<DS__Architectural_Plan>>() {
                        destruction_L1,
                        destruction_L2,
                        destruction_L3
                    },
                    minimum_selections: minimum_selections,
                    maximum_selections: maximum_selections,
                    recalculate_phenotype: true,
                    starting_level: 1
                    );

            return destruction_method;
        }

        private static Mutation_Method<DS__Architectural_Plan> Template__MM__Repair(
            double area_error_margin
            )
        {
            MM__Sequence__Recalculations repair_mutation_method =
                new MM__Sequence__Recalculations(
                    new List<Mutation_Method<DS__Architectural_Plan>>() {
                        new MM__Repair__L2__Add_Missing_Space_Units<DS__Architectural_Plan>(),
                        new MM__Repair__L2__Fix_Space_Units_Coherence<DS__Architectural_Plan>(),
                        new MM__Repair__L2__Fix_Connections<DS__Architectural_Plan>(),
                        new MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold<DS__Architectural_Plan>(area_error_margin),
                        new MM__Repair__L3__Openings()
                    },
                    recalculate_phenotype: false,
                    starting_level: 2
                    );

            return repair_mutation_method;
        }
    }
}
