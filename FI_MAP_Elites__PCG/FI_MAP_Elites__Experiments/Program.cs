using System;
using System.Collections.Generic;
using Common_Tools;

namespace FI_MAP_Elites__Experiments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments),
            };
            delegator.Run(types, "Program_");
        }
    }
}
