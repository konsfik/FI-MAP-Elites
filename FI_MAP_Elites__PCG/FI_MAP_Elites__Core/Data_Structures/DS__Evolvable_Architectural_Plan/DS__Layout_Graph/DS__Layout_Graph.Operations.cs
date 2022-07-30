using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Layout_Constraints : Data_Structure
    {
        public void M__Add_Space_Unit(
            int id,
            string name,
            RGB_Color color,
            Space_Unit__Type type,
            double area,
            int num_entrance_doors,
            int num_windows
            )
        {
            name__per__space_unit[id] = name;
            color__per__space_unit[id] = color;
            type__per__space_unit[id] = type;
            area__per__space_unit[id] = area;
            num_entrance_doors__per__space_unit[id] = num_entrance_doors;
            num_windows__per__space_unit[id] = num_windows;
            graph.M__Add_Vertex(id);
        }

        public void M__Add_Space_Unit(
            int id,
            string name,
            RGB_Color color,
            double area,
            int num_entrance_doors,
            int num_windows
            )
        {
            name__per__space_unit[id] = name;
            color__per__space_unit[id] = color;
            type__per__space_unit[id] = Space_Unit__Type.INTERIOR;
            area__per__space_unit[id] = area;
            num_entrance_doors__per__space_unit[id] = num_entrance_doors;
            num_windows__per__space_unit[id] = num_windows;
            graph.M__Add_Vertex(id);
        }

        public void M__Add_Connection(Undirected_Edge connection)
        {
            graph.M__Add_Edge(connection);
        }

        public void M__Add_Connection(int v1, int v2)
        {
            graph.M__Add_Edge(v1,v2);
        }
    }
}
