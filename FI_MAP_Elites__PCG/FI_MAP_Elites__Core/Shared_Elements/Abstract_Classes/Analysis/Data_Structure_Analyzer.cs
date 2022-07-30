using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Data_Structure_Analyzer<T> : Data_Structure
        where T : Data_Structure
    {

        public abstract void Update(T individual);

        public abstract string Q__CSV__Data_Header(string delimiter, string end_character);

        public abstract string Q__CSV__Data_Row(string delimiter, string end_character);
    }
}
