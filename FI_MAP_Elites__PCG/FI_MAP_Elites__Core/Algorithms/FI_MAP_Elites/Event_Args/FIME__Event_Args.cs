using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public class FIME__Event_Args<T> : EventArgs
        where T : Data_Structure
    {
        public FI_MAP_Elites<T> cmce_algorithm;
        public string message;

        public FIME__Event_Args(
            FI_MAP_Elites<T> cmce_algorithm,
            string message
            )
        {
            this.cmce_algorithm = cmce_algorithm;
            this.message = message;
        }
    }
}
