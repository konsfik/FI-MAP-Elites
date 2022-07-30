using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public abstract class Evolvable_Geometry__Generation_Method : Generation_Method<DS__Architectural_Plan>
    {
        public readonly DS__Layout_Constraints prescription;
        public readonly Point_Cloud_Generation_Method point_cloud_generation_method;

        public Evolvable_Geometry__Generation_Method(
            DS__Layout_Constraints prescription,
            Point_Cloud_Generation_Method point_cloud_generation_method
            )
        {
            this.prescription = prescription;
            this.point_cloud_generation_method = point_cloud_generation_method;
        }

    }
}
