using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class EM__Average<T> : Evaluation_Method<T>
        where T:Data_Structure
    {
        public List<Evaluation_Method<T>> evaluation_methods;

        public EM__Average(
            List<Evaluation_Method<T>> evaluation_methods
            )
        {
            this.evaluation_methods = evaluation_methods;
        }

        private EM__Average(EM__Average<T> em_to_copy)
        {
            this.evaluation_methods = em_to_copy.evaluation_methods.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Average<T>(this);
        }

        public override double Evaluate_Individual(T individual)
        {
            double score_sum = 0.0;

            foreach (var method in evaluation_methods)
                score_sum += method.Evaluate_Individual(individual);

            double average = score_sum / (double)(evaluation_methods.Count);

            return average;
        }
    }
}
