using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class EM__Constraints_Percentage<T> : Evaluation_Method<T>
        where T : Data_Structure
    {
        public List<Constraint_Evaluation_Method<T>> constraint_evaluation_methods;

        public EM__Constraints_Percentage(
            List<Constraint_Evaluation_Method<T>> constraint_evaluation_methods
            )
        {
            this.constraint_evaluation_methods = constraint_evaluation_methods;
        }

        private EM__Constraints_Percentage(EM__Constraints_Percentage<T> cem_to_copy)
        {
            this.constraint_evaluation_methods = cem_to_copy.constraint_evaluation_methods.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Constraints_Percentage<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            int num_constraints = constraint_evaluation_methods.Count;

            int num_satisfied_constraints = 0;
            foreach (var method in constraint_evaluation_methods)
            {
                if (method.Q__Satisfied(individual))
                {
                    num_satisfied_constraints++;
                }
            }

            double score = (double)num_satisfied_constraints / (double)num_constraints;

            return score;
        }

        public override string Name()
        {
            string name = "Constraints_Percentage_Of_";
            foreach (var eval in constraint_evaluation_methods)
            {
                name += "_" + eval.Name();
            }
            return name;
        }
    }
}
