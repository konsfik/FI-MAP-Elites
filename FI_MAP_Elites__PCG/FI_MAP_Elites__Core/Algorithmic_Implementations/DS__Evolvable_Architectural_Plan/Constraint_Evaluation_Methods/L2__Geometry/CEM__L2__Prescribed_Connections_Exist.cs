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
    /// <summary>
    /// A constraint evaluation method that is satisfied when all the prescribed connections exist. 
    /// More precisely, when both of the following are true: 
    /// 1) All the prescribed space units exist.
    /// 2) The existing space units are adjacent to the ones that they are supposed to.
    /// </summary>
    public class CEM__L2__Prescribed_Connections_Exist<T> : Constraint_Evaluation_Method<T>
        where T: DS__Evolvable_Geometry
    {
        public override object Q__Deep_Copy()
        {
            return new CEM__L2__Prescribed_Connections_Exist<T>();
        }

        public override bool Q__Satisfied(T individual)
        {
            List<Undirected_Edge> prescribed_connections = 
                individual
                .Q__Prescribed_Connections();

            foreach (var connection in prescribed_connections)
            {
                bool connection_exists =
                    individual
                    .Q__Space_Units_Connection_Exists(connection);

                if (connection_exists == false)
                    return false;
            }

            return true;
        }
    }
}
