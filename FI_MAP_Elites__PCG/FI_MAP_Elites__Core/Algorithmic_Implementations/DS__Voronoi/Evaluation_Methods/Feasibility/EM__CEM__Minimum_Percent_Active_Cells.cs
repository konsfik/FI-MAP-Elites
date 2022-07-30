using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__CEM__Minimum_Percent_Active_Cells : Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_percent_active_cells;

        public EM__CEM__Minimum_Percent_Active_Cells(double min_line_length)
        {
            this.min_percent_active_cells = min_line_length;
        }

        private EM__CEM__Minimum_Percent_Active_Cells(
            EM__CEM__Minimum_Percent_Active_Cells em_to_copy
            )
        {
            this.min_percent_active_cells = em_to_copy.min_percent_active_cells;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__Minimum_Percent_Active_Cells(this);
        }

        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            int num_points = individual.Q__Num_Generator_Points();
            int num_active_cells = individual.Q__Num_Active_Cells();

            double percent = (double)num_active_cells / (double)num_points;

            if (percent >= min_percent_active_cells)
            {
                return 1.0;
            }
            else
            {
                return percent.Q__Mapped(0.0, min_percent_active_cells, 0.0, 1.0);
            }
        }
    }
}
