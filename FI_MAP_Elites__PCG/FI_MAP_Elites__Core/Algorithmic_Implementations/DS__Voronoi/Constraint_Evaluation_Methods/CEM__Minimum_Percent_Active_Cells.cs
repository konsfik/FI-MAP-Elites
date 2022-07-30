using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class CEM__Minimum_Percent_Active_Cells : Constraint_Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_percent_active_cells;

        public CEM__Minimum_Percent_Active_Cells(double min_line_length)
        {
            this.min_percent_active_cells = min_line_length;
        }

        private CEM__Minimum_Percent_Active_Cells(CEM__Minimum_Percent_Active_Cells cem_to_copy)
        {
            this.min_percent_active_cells = cem_to_copy.min_percent_active_cells;
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__Minimum_Percent_Active_Cells(this);
        }

        public override bool Q__Satisfied(DS__Voronoi individual)
        {
            int num_points = individual.Q__Num_Generator_Points();
            int num_active_cells = individual.Q__Num_Active_Cells();

            double percent = (double)num_active_cells / (double)num_points;

            if (percent >= min_percent_active_cells)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
