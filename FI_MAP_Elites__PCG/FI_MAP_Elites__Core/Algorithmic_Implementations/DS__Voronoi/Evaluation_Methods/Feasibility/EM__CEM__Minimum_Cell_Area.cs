using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__CEM__Minimum_Cell_Area : Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_cell_area;

        public EM__CEM__Minimum_Cell_Area(double min_cell_area)
        {
            this.min_cell_area = min_cell_area;
        }

        private EM__CEM__Minimum_Cell_Area(EM__CEM__Minimum_Cell_Area em_to_copy)
        {
            this.min_cell_area = em_to_copy.min_cell_area;
        }

        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            int num_active_cells = individual.Q__Num_Active_Cells();
            if (num_active_cells == 0)
            {
                return 0.0;
            }

            int num_cells = individual.Q__Num_Generator_Points();
            double score_sum = 0;
            for (int c = 0; c < num_cells; c++)
            {
                if (individual.Q__Cell__Is_Active(c))
                {
                    double cell_area = individual.Q__Cell_Area(c);
                    if (cell_area < min_cell_area)
                    {
                        double fractional_similarity =
                            cell_area.Q__Fractional_Similarity(min_cell_area);
                        score_sum += fractional_similarity;
                    }
                    else
                    {
                        score_sum += 1.0;
                    }
                }
            }

            return score_sum / (double)num_active_cells;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__Minimum_Cell_Area(this);
        }
    }
}
