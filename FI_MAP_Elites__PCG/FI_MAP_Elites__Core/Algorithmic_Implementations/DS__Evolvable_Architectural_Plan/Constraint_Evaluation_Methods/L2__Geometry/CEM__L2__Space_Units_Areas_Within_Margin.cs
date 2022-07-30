using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CEM__L2__Space_Units_Areas_Within_Margin<T> : Constraint_Evaluation_Method<T>
        where T: DS__Evolvable_Geometry
    {
        public double maximum_permitted_fractional_error;

        public CEM__L2__Space_Units_Areas_Within_Margin(double maximum_permitted_fractional_error)
        {
            if (
                maximum_permitted_fractional_error < 0.0 
                || 
                maximum_permitted_fractional_error > 1.0
                )
                throw new Exception("maximum_permitted_fractional_error must be between 0 and 1");

            this.maximum_permitted_fractional_error = maximum_permitted_fractional_error;
        }

        private CEM__L2__Space_Units_Areas_Within_Margin(CEM__L2__Space_Units_Areas_Within_Margin<T> cem_to_copy)
        {
            this.maximum_permitted_fractional_error = cem_to_copy.maximum_permitted_fractional_error;
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__L2__Space_Units_Areas_Within_Margin<T>(this);
        }

        public override bool Q__Satisfied(T individual)
        {
            List<int> prescribed_space_units = 
                individual
                .Q__Prescribed_Space_Units();

            foreach (var space_unit in prescribed_space_units)
            {
                double prescribed_area = 
                    individual
                    .Q__Space_Unit__Prescribed_Area(space_unit);

                double area = 
                    individual
                    .Q__Space_Unit__Area(space_unit);

                double fractional_similarity = 
                    area
                    .Q__Fractional_Similarity(
                        prescribed_area
                        );
                
                double fractional_error = 1.0 - fractional_similarity;

                if (fractional_error > maximum_permitted_fractional_error)
                    return false;
            }

            return true;
        }
    }
}
