using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class CEM__Minimum_Cell_Area : Constraint_Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_cell_area;

        public CEM__Minimum_Cell_Area(double min_cell_area)
        {
            this.min_cell_area = min_cell_area;
        }

        private CEM__Minimum_Cell_Area(CEM__Minimum_Cell_Area cem_to_copy)
        {
            this.min_cell_area = cem_to_copy.min_cell_area;
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__Minimum_Cell_Area(this);
        }

        public override bool Q__Satisfied(DS__Voronoi individual)
        {
            int num_active_cells = individual.Q__Num_Active_Cells();
            if (num_active_cells == 0)
            {
                return false;
            }

            int num_cells = individual.Q__Num_Generator_Points();
            for (int c = 0; c < num_cells; c++)
            {
                if (individual.Q__Cell__Is_Active(c))
                {
                    double cell_area = individual.Q__Cell_Area(c);
                    if (cell_area < min_cell_area)
                        return false;
                }
            }

            return true;
        }
    }
}
