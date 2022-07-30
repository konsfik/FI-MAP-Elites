using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__CEM__Is_Connected : Evaluation_Method<DS__Voronoi>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__CEM__Is_Connected();
        }

        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            int num_active_cells = 
                individual
                .Q__Num_Active_Cells();

            if (num_active_cells == 0)
            {
                return 0.0;
            }
            else
            {
                int num_islands = 
                    individual
                    .connectivity_graph
                    .Q__Graph_Islands()
                    .Count;

                double percent_disconnection = (double)num_islands / (double)num_active_cells;
                double score = 1.0 - percent_disconnection;

                return score;
            }
        }


    }
}
