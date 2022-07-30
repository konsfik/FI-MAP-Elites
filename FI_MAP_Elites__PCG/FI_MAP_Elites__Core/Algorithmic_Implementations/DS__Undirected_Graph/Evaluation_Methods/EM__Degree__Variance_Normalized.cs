using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class EM__Degree__Variance_Normalized : Evaluation_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new EM__Degree__Variance_Normalized();
        }

        public override double Evaluate_Individual(DS__Undirected_Graph individual)
        {
            List<int> all_verts = individual.Q__Vertices();
            List<double> degree_per_vertex = new List<double>();
            foreach (var v in all_verts)
            {
                degree_per_vertex.Add(individual.Q__Degree(v));
            }

            int num_verts = individual.Q__Num_Vertices();

            double max_var = (num_verts / 2.0) * (num_verts / 2.0);

            double variance = degree_per_vertex.Q__Variance(false);

            return variance / max_var;
        }
    }
}
