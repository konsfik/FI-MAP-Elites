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

        public static Evaluation_Method<DS__Architectural_Plan> Template__Feasible_Fitness_Function() {
            return new EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>();
        }

        public static Evaluation_Method<DS__Architectural_Plan> Template__Infeasible_Fitness_Function(
            double min_percent_active_cells,
            double area_error_margin
            )
        {
            EM__Average__Hierarchical<DS__Architectural_Plan> infeasible_fitness_function =
                new EM__Average__Hierarchical<DS__Architectural_Plan>(
                    evaluation_methods: new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        Template__EM__CEM__H1__Voronoi(min_percent_active_cells),
                        Template__EM__CEM__H2__Geometry(area_error_margin),
                        Template__EM__CEM__H3__Details(area_error_margin)
                    },
                    pass_values: new List<double>() {
                        1.0,
                        1.0,
                        1.0
                    },
                    epsilon: 0.0000_0001
                    );

            //EM__Average<DS__Architectural_Plan> infeasible_fitness_function =
            //    new EM__Average<DS__Architectural_Plan>(
            //        evaluation_methods: new List<Evaluation_Method<DS__Architectural_Plan>>() {
            //            Template__EM__CEM__H1__Voronoi(min_percent_active_cells),
            //            Template__EM__CEM__H2__Geometry(area_error_margin),
            //            Template__EM__CEM__H3__Details(area_error_margin)
            //        }
            //        );

            return infeasible_fitness_function;
        }

        private static EM__Average<DS__Architectural_Plan> Template__EM__CEM__H1__Voronoi(
            double min_percent_active_cells
            )
        {
            EM__Average<DS__Architectural_Plan> basic_voronoi_constraints =
                new EM__Average<DS__Architectural_Plan>(
                    new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        new EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan>(),
                        new EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>(min_percent_active_cells),
                    }
                    );

            return basic_voronoi_constraints;
        }

        private static EM__Average<DS__Architectural_Plan> Template__EM__CEM__H2__Geometry(
            double area_error_margin
            )
        {
            EM__Average<DS__Architectural_Plan> basic_geometry_constraints =
                new EM__Average<DS__Architectural_Plan>(
                    new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        new EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>(area_error_margin),
                    }
                    );

            return basic_geometry_constraints;
        }

        private static EM__Average<DS__Architectural_Plan> Template__EM__CEM__H3__Details(double area_error_margin)
        {
            EM__Average<DS__Architectural_Plan> basic_geometry_constraints =
                new EM__Average<DS__Architectural_Plan>(
                    new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        new EM__CEM__L3__Not_Narrow_Passages(1.0),
                        new EM__CEM__L3__Prescribed_Openings()
                    }
                    );

            return basic_geometry_constraints;
        }
    }
}
