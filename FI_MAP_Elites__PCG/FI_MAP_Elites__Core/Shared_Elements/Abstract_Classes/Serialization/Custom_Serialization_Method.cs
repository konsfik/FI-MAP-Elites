using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Custom_Serialization_Method<T> : I_Deep_Copyable
        where T : Data_Structure
    {
        public abstract string Serialize_To_String(T individual);

        public abstract T Deserialize_From_String(string serialized_individual);
        public abstract object Q__Deep_Copy();

        public abstract string Q__File__Dot_Extension();
    }
}
