using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph
{
    public partial class DS__Undirected_Weighted_Graph : Data_Structure
    {
        public Dictionary<int, Dictionary<int, double>> weight_per_neighbor_per_vertex;

        //public Dictionary<int, List<Half_Edge>> half_edges__per__node;

        public DS__Undirected_Weighted_Graph()
        {
            weight_per_neighbor_per_vertex = new Dictionary<int, Dictionary<int, double>>();
        }

        public DS__Undirected_Weighted_Graph(
            Dictionary<int, Dictionary<int, double>> weight_per_neighbor_per_vertex
            )
        {
            this.weight_per_neighbor_per_vertex = weight_per_neighbor_per_vertex.Q__Deep_Copy();
        }

        private DS__Undirected_Weighted_Graph(DS__Undirected_Weighted_Graph uwg_to_copy)
        {
            this.weight_per_neighbor_per_vertex = new  Dictionary<int, Dictionary<int, double>>();
            foreach (var entry in uwg_to_copy.weight_per_neighbor_per_vertex)
            {
                this.weight_per_neighbor_per_vertex.Add(
                    entry.Key,
                    new Dictionary<int, double>(entry.Value)
                    );
            }
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Undirected_Weighted_Graph(this);
        }

        

        
    }
}
