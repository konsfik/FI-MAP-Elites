using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__CEM__L2__Space_Units_Areas_Within_Margin<T> : Evaluation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly double maximum_permitted_fractional_error;

        public EM__CEM__L2__Space_Units_Areas_Within_Margin(
            double maximum_permitted_fractional_error
            )
        {
            if (
                maximum_permitted_fractional_error < 0.0
                ||
                maximum_permitted_fractional_error > 1.0
                )
                throw new Exception("maximum_permitted_fractional_error must be between 0 and 1");

            this.maximum_permitted_fractional_error = maximum_permitted_fractional_error;
        }

        private EM__CEM__L2__Space_Units_Areas_Within_Margin(
            EM__CEM__L2__Space_Units_Areas_Within_Margin<T> em_to_copy
            )
        {
            this.maximum_permitted_fractional_error = em_to_copy.maximum_permitted_fractional_error;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__CEM__L2__Space_Units_Areas_Within_Margin<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            List<int> prescribed_space_units = individual.Q__Prescribed_Space_Units();

            double area_score_sum = 0.0;

            foreach (var space_unit in prescribed_space_units)
            {
                if (individual.Q__Space_Unit_Exists(space_unit))
                {
                    double space_unit_area =
                        individual
                        .Q__Space_Unit__Area(space_unit);

                    double space_unit_prescribed_area =
                        individual
                        .Q__Space_Unit__Prescribed_Area(space_unit);

                    double fractional_error = space_unit_area.Q__Fractional_Error(space_unit_prescribed_area);

                    if (fractional_error <= maximum_permitted_fractional_error)
                    {
                        area_score_sum += 1.0;
                    }
                    else
                    {
                        // the fractional error is larger than the permitted error...
                        double minimum_permitted_similarity = 1.0 - maximum_permitted_fractional_error;
                        double current_similarity = 1.0 - fractional_error;
                        double current_score = current_similarity / minimum_permitted_similarity;
                        area_score_sum += current_score;
                    }
                }
            }

            double total_area_score = area_score_sum / (double)prescribed_space_units.Count;

            return total_area_score;
        }
    }
}
