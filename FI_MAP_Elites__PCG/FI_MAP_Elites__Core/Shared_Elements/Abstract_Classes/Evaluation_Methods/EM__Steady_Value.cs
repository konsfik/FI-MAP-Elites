using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public class EM__Steady_Value<T> : Evaluation_Method<T>
        where T:Data_Structure
    {
        public readonly double value;

        public EM__Steady_Value(double value)
        {
            this.value = value;
        }

        public override object Q__Deep_Copy()
        {
            return new EM__Steady_Value<T>(this.value);
        }

        public override double Evaluate_Individual(T individual)
        {
            return value;
        }
    }
}
