using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public struct FIME__Location
    {
        public FIME__State_Type state;
        public int cell;

        public FIME__Location(FIME__State_Type state, int cell)
        {
            this.state = state;
            this.cell = cell;
        }
    }
}
