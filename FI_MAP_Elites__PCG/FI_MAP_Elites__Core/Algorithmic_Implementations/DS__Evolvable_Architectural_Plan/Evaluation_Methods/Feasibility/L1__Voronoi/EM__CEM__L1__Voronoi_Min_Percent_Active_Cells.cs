using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<T> : Evaluation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly double min_percent_active_cells;

        public EM__CEM__L1__Voronoi_Min_Percent_Active_Cells(
            double min_percent_active_cells
            )
        {
            this.min_percent_active_cells = min_percent_active_cells;
        }

        private EM__CEM__L1__Voronoi_Min_Percent_Active_Cells(
            EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<T> em_to_copy
            )
        {
            this.min_percent_active_cells = em_to_copy.min_percent_active_cells;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            int num_generator_points = individual.voronoi_tessellation.Q__Num_Generator_Points();
            int num_active_cells = individual.voronoi_tessellation.Q__Num_Active_Cells();

            double percent_active_cells = (double)num_active_cells / (double)num_generator_points;

            if (percent_active_cells >= min_percent_active_cells)
            {
                return 1.0;
            }
            else
            {
                return percent_active_cells / min_percent_active_cells;
            }
        }
    }
}
