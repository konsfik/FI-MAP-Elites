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
    public partial class DS__Layout_Constraints : Data_Structure
    {
        public int Q__Space_Unit_ID(string space_unit_name)
        {
            foreach (var kvp in name__per__space_unit)
            {
                if (kvp.Value == space_unit_name)
                    return kvp.Key;
            }
            throw new System.Exception("Space unit name does not exist!");
        }

        public RGB_Color Q__Prescribed__Space_Unit__Color(int space_unit_id)
        {
            return color__per__space_unit[space_unit_id];
        }

        public double Q__Prescribed__Space_Unit_Area(int space_unit_id)
        {
            return area__per__space_unit[space_unit_id];
        }

        public List<int> Q__Prescribed__Space_Units()
        {
            return name__per__space_unit.Keys.ToList().Q__Deep_Copy();
        }

        public Dictionary<int, double> Q__Prescribed_Area__Per__Space_Unit_ID__Deep_Copied()
        {
            return area__per__space_unit.Q__Deep_Copy();
        }

        public List<Undirected_Edge> Q__Prescribed__Connections()
        {
            return graph.Q__Edges();
        }

        public double Q__Area_Sum() {
            double area_sum = 0.0;
            foreach (var kvp in area__per__space_unit) { 
                area_sum += kvp.Value;
            }
            return area_sum;
        }
    }
}
