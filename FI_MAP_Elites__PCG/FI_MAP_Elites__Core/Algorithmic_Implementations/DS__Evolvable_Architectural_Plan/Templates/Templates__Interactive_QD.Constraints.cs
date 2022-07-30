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
        public static Constraint_Evaluation_Method<DS__Architectural_Plan> Template__Feasibility_Discrimination_Method(
            double min_percent_active_cells,
            double area_error_margin
            )
        {
            CEM__Modular<DS__Architectural_Plan> feasibility_discrimination_method =
                new CEM__Modular<DS__Architectural_Plan>(
                    new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                        Template__CEM__H1__Voronoi(min_percent_active_cells),
                        Template__CEM__H2__Geometry(area_error_margin),
                        Template__CEM__H3__Details()
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

        private static CEM__Modular<DS__Architectural_Plan> Template__CEM__H3__Details()
        {
            return new CEM__Modular<DS__Architectural_Plan>(
                new List<Constraint_Evaluation_Method<DS__Architectural_Plan>>() {
                    new CEM__L3__Not_Narrow_Passages(1.0),
                    new CEM__L3__Prescribed_Openings()
                }
                );
        }
    }
}
