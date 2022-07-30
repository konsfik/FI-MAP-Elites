using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public interface I_DS_Stats<T>
        where T:Data_Structure
    {
        void M__Update(T individual);

        string Q__CSV_Header(string delimiter);

        string Q__CSV_Row(string delimiter);
    }
}
