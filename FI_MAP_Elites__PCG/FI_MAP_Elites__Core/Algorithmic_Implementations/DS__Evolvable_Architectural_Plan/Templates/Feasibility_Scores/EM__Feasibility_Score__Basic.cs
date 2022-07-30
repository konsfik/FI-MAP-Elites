using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class EM__Feasibility_Score__Basic : EM__Average<DS__Architectural_Plan>
    {
        public EM__Feasibility_Score__Basic()
            : base(
                 evaluation_methods: new List<Evaluation_Method<DS__Architectural_Plan>>()
                    {
                        new EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>(),
                        new EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>(0.4)
                    }
                 )
        {

        }

        public override object Q__Deep_Copy()
        {
            return new EM__Feasibility_Score__Basic();
        }
    }
}
