using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class EM__Average__Hierarchical<T> : Evaluation_Method<T>
        where T : Data_Structure
    {
        public List<Evaluation_Method<T>> evaluation_methods;
        public List<double> pass_values;
        public double epsilon;

        public EM__Average__Hierarchical(
            List<Evaluation_Method<T>> evaluation_methods,
            List<double> pass_values,
            double epsilon
            )
        {
            if (evaluation_methods.Count != pass_values.Count) {
                throw new Exception("pass values count must be equal to evaluation methods count");
            }

            this.evaluation_methods = evaluation_methods.Q__Deep_Copy();
            this.pass_values = pass_values.Q__Deep_Copy();
            this.epsilon = epsilon;
        }

        private EM__Average__Hierarchical(EM__Average__Hierarchical<T> em_to_copy)
        {
            this.evaluation_methods = em_to_copy.evaluation_methods.Q__Deep_Copy();
            this.pass_values = em_to_copy.pass_values.Q__Deep_Copy();
            this.epsilon = em_to_copy.epsilon;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Average__Hierarchical<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            double score_sum = 0.0;

            int num_values = evaluation_methods.Count;

            for (int i = 0; i < num_values; i++) {
                double this_value = evaluation_methods[i].Evaluate_Individual(individual);
                double pass_value = pass_values[i];
                if (this_value.Q__Approximately_Equal(pass_value, epsilon))
                {
                    score_sum += pass_value;
                }
                else {
                    score_sum += this_value;
                    break;
                }
            }

            double score_average = score_sum / (double)num_values;

            return score_average;
        }
    }
}
