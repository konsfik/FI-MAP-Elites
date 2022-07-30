using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class CEM__Modular<T> : Constraint_Evaluation_Method<T>
        where T : Data_Structure
    {
        public List<Constraint_Evaluation_Method<T>> constraint_evaluation_methods;

        public CEM__Modular(List<Constraint_Evaluation_Method<T>> constraint_evaluation_methods)
        {
            this.constraint_evaluation_methods = constraint_evaluation_methods;
        }

        private CEM__Modular(CEM__Modular<T> cem_to_copy)
        {
            this.constraint_evaluation_methods = cem_to_copy.constraint_evaluation_methods.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new CEM__Modular<T>(this);
        }

        public override string Name()
        {
            string name = "Modular_Of_";
            foreach (var eval in constraint_evaluation_methods)
            {
                name += "_" + eval.Name();
            }
            return name;
        }

        public override bool Q__Satisfied(T individual)
        {
            foreach (var method in constraint_evaluation_methods)
            {
                if (method.Q__Satisfied(individual) == false)
                {
                    return false;
                }
            }

            // else
            return true;
        }
    }
}
