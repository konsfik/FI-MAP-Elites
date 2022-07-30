using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public partial class DS__Undirected_Graph : Data_Structure
    {
        // state
        public Dictionary<int, List<int>> neighbors__per__vertex;

        // constructors
        public DS__Undirected_Graph()
        {
            neighbors__per__vertex = new Dictionary<int, List<int>>();
        }

        public DS__Undirected_Graph(
            Dictionary<int, List<int>> neighbors__per__node
            )
        {
            this.neighbors__per__vertex = neighbors__per__node.Q__Deep_Copy();
        }

        private DS__Undirected_Graph(
            DS__Undirected_Graph graph_to_copy
            )
        {
            this.neighbors__per__vertex = graph_to_copy.neighbors__per__vertex.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Undirected_Graph(this);
        }
    }
}
