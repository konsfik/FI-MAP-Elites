using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__Experiments
{
    public static partial class Templates__PCG_Workshop
    {
        #region fitness
        public static Evaluation_Method<DS__Architectural_Plan> Template__Feasible_Fitness_Function()
        {
            return new EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>();
        }
        #endregion

        #region hard constraints
        public static Constraint_Evaluation_Method<DS__Architectural_Plan> Template__Feasibility_Discrimination_Method(
            double min_percent_active_cells,
            double area_error_margin, 
            double min_bridge_line_magnitude
            )
        {
            CEM__Modular<DS__Architectural_Plan> feasibility_discrimination_method =
                new CEM__Modular<DS__Architectural_Plan>(
                    new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                        Template__CEM__H1__Voronoi(min_percent_active_cells),
                        Template__CEM__H2__Geometry(area_error_margin),
                        Template__CEM__H3__Details(min_bridge_line_magnitude)
                    }
                );

            return feasibility_discrimination_method;
        }

        private static CEM__Modular<DS__Architectural_Plan> Template__CEM__H1__Voronoi(
            double min_percent_active_cells
            )
        {
            CEM__Modular<DS__Architectural_Plan> basic_voronoi_constraints =
                new CEM__Modular<DS__Architectural_Plan>(
                    new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                        new CEM__L1__Voronoi_Connected<DS__Architectural_Plan>(),
                        new CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>(min_percent_active_cells),
                    }
                    );

            return basic_voronoi_constraints;
        }

        private static CEM__Modular<DS__Architectural_Plan> Template__CEM__H2__Geometry(
            double area_error_margin
            )
        {
            CEM__Modular<DS__Architectural_Plan> basic_geometry_constraints =
                new CEM__Modular<DS__Architectural_Plan>(
                    new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                        new CEM__L2__Space_Units_Exist<DS__Architectural_Plan>(),
                        new CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>(),
                        new CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>(),
                        new CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>(area_error_margin),
                    }
                    );

            return basic_geometry_constraints;
        }

        private static CEM__Modular<DS__Architectural_Plan> Template__CEM__H3__Details(double min_bridge_line_magnitude)
        {
            return new CEM__Modular<DS__Architectural_Plan>(
                new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                    new CEM__L3__Not_Narrow_Passages(min_bridge_line_magnitude),
                    new CEM__L3__Prescribed_Openings()
                }
                );
        }
        #endregion

        #region soft constraints
        public static Evaluation_Method<DS__Architectural_Plan> Template__Infeasible_Fitness_Function(
            double min_percent_active_cells,
            double area_error_margin,
            double min_bridge_line_magnitude
            )
        {
            EM__Average__Hierarchical<DS__Architectural_Plan> infeasible_fitness_function =
                new EM__Average__Hierarchical<DS__Architectural_Plan>(
                    evaluation_methods: new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        Template__EM__CEM__H1__Voronoi(min_percent_active_cells),
                        Template__EM__CEM__H2__Geometry(area_error_margin),
                        Template__EM__CEM__H3__Details(min_bridge_line_magnitude)
                    },
                    pass_values: new List<double>() {
                        1.0,
                        1.0,
                        1.0
                    },
                    epsilon: 0.0000_0001
                    );

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

        private static EM__Average<DS__Architectural_Plan> Template__EM__CEM__H3__Details(
            double min_bridge_line_magnitude
            )
        {
            EM__Average<DS__Architectural_Plan> basic_geometry_constraints =
                new EM__Average<DS__Architectural_Plan>(
                    new List<Evaluation_Method<DS__Architectural_Plan>>() {
                        new EM__CEM__L3__Not_Narrow_Passages(min_bridge_line_magnitude),
                        new EM__CEM__L3__Prescribed_Openings()
                    }
                    );

            return basic_geometry_constraints;
        }
        #endregion
    }
}
