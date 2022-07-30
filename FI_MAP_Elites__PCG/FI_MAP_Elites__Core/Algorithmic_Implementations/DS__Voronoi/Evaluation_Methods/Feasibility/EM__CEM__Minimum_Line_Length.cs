using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class EM__CEM__Minimum_Line_Length : Evaluation_Method<DS__Voronoi>
    {
        public readonly double min_line_length;

        public EM__CEM__Minimum_Line_Length(double min_line_length)
        {
            this.min_line_length = min_line_length;
        }

        private EM__CEM__Minimum_Line_Length(
            EM__CEM__Minimum_Line_Length em_to_copy
            )
        {
            this.min_line_length = em_to_copy.min_line_length;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__Minimum_Line_Length(this);
        }

        public override double Evaluate_Individual(
            DS__Voronoi individual
            )
        {
            int num_lines = individual.voronoi_lines.Count;
            if (num_lines == 0) return 0.0;

            double score_sum = 0.0;

            foreach (var line in individual.voronoi_lines)
            {
                double line_length = line.Q__Magnitude();
                if (line_length < min_line_length)
                {
                    //double fractional_similarity = line_length.Q__Fractional_Similarity(min_line_length);
                    //score_sum += fractional_similarity;
                }
                else
                {
                    score_sum += 1.0;
                }
            }

            return score_sum / (double)num_lines;
        }


    }
}
