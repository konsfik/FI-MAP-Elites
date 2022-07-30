using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph
{
    public struct Half_Edge : IEquatable<Half_Edge>
    {
        public readonly int vertex;
        public readonly double weight;

        public Half_Edge(int v2, double weight)
        {
            this.vertex = v2;
            this.weight = weight;
        }

        public bool Q__Contains_Index(int index)
        {
            return vertex == index;
        }

        #region equality override
        public bool Equals(Half_Edge other)
        {
            return this.vertex == other.vertex && this.weight == other.weight;
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is Half_Edge other_edge)
            {
                return Equals(other_edge);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + vertex.GetHashCode();
            hash = hash * 31 + weight.GetHashCode();
            return hash;
        }

        public static bool operator ==(
            Half_Edge c1, 
            Half_Edge c2
            )
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(
            Half_Edge c1, 
            Half_Edge c2
            )
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
