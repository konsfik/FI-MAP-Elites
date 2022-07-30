using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CEM__L1__Voronoi_Min_Percent_Active_Cells<T> : Constraint_Evaluation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly double min_percent_active_cells;

        public CEM__L1__Voronoi_Min_Percent_Active_Cells(
            double min_percent_active_cells
            )
        {
            this.min_percent_active_cells = min_percent_active_cells;
        }

        private CEM__L1__Voronoi_Min_Percent_Active_Cells(
            CEM__L1__Voronoi_Min_Percent_Active_Cells<T> cem_to_copy
            )
        {
            this.min_percent_active_cells = cem_to_copy.min_percent_active_cells;
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__L1__Voronoi_Min_Percent_Active_Cells<T>(this);
        }

        public override bool Q__Satisfied(T individual)
        {
            return individual.Q__CEM__L1__Voronoi_Min_Percent_Active_Cells(min_percent_active_cells);
        }
    }
}
