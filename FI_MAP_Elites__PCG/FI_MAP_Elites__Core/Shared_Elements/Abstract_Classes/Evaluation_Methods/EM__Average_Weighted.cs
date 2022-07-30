using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class EM__Average_Weighted<T> : Evaluation_Method<T>
        where T:Data_Structure
    {
        public List<Evaluation_Method<T>> evaluation_methods;
        public List<double> weights;

        public EM__Average_Weighted(
            List<Evaluation_Method<T>> evaluation_methods,
            List<double> weights
            )
        {
            this.evaluation_methods = evaluation_methods;
            this.weights = weights;
        }

        private EM__Average_Weighted(EM__Average_Weighted<T> em_to_copy)
        {
            this.evaluation_methods = em_to_copy.evaluation_methods.Q__Deep_Copy();
            this.weights = em_to_copy.weights;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Average_Weighted<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            double weighted_score_sum = 0.0;

            double weights_sum = weights.Sum();

            int num_methods = evaluation_methods.Count;

            for (int i = 0; i < num_methods; i++)
            {
                double this_weight_part = weights[i] / weights_sum;
                weighted_score_sum +=
                    evaluation_methods[i]
                    .Evaluate_Individual(individual)
                    * this_weight_part;
            }

            return weighted_score_sum;
        }
    }
}
