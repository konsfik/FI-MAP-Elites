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
    public class EM__CEM__L3__Not_Narrow_Passages: Evaluation_Method<DS__Architectural_Plan>
    {
        public double min_bridge_line_magnitude;

        public EM__CEM__L3__Not_Narrow_Passages(double min_bridge_line_magnitude)
        {
            this.min_bridge_line_magnitude = min_bridge_line_magnitude;
        }

        private EM__CEM__L3__Not_Narrow_Passages(EM__CEM__L3__Not_Narrow_Passages cem_to_copy)
        {
            this.min_bridge_line_magnitude = cem_to_copy.min_bridge_line_magnitude;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__L3__Not_Narrow_Passages(this);
        }

        public override double Evaluate_Individual(DS__Architectural_Plan individual)
        {
            var bridge_edges =
                individual
                .Q__Plan__Sub_Graph()
                .Q__Local_Bridges();

            if (bridge_edges.Count == 0)
            {
                return 1.0;
            }

            List<double> scores = new List<double>();
            foreach (var edge in bridge_edges)
            {
                individual.voronoi_tessellation.Q__Shared_Line__Between_Neighbor_Cells(
                    edge.v1,
                    edge.v2,
                    out bool success,
                    out Line_Segment bridge_line
                    );
                if (success == false) scores.Add(0.0);

                double bridge_line_length = bridge_line.Q__Magnitude();

                if (bridge_line_length >= min_bridge_line_magnitude)
                {
                    scores.Add(1.0);
                }
                else {
                    double similarity = bridge_line_length.Q__Fractional_Similarity(min_bridge_line_magnitude);
                    similarity = similarity * similarity;
                    scores.Add(similarity);
                }
            }

            return scores.Q__Mean();
        }
    }
}
