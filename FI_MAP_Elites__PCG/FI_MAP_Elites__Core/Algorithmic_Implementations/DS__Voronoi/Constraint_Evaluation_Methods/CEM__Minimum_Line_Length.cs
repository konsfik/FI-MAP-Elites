using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class CEM__Minimum_Line_Length : Constraint_Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_line_length;

        public CEM__Minimum_Line_Length(double min_line_length)
        {
            this.min_line_length = min_line_length;
        }

        private CEM__Minimum_Line_Length(CEM__Minimum_Line_Length cem_to_copy)
        {
            this.min_line_length = cem_to_copy.min_line_length;
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__Minimum_Line_Length(this);
        }

        public override bool Q__Satisfied(DS__Voronoi individual)
        {
            if (individual.voronoi_lines.Count == 0) return false;

            foreach (var line in individual.voronoi_lines) {
                double len = line.Q__Magnitude();
                if (len < min_line_length)
                    return false;
            }

            return true;
        }
    }
}
