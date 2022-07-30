using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public interface I_FIME__Individual_Generated__Listener<T>
        where T : Data_Structure
    {
        void On__Individual_Generated(object sender, FIME__Individual_Generated__EventArgs<T> event_args);
    }
}
