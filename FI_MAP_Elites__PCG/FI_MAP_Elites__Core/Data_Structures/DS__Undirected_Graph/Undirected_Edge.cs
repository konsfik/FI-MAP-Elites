using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public struct Undirected_Edge : IEquatable<Undirected_Edge>
    {
        public readonly int v1;
        public readonly int v2;

        // json constructor
        public Undirected_Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public bool Q__Contains_Index(int index) {
            return
                (v1 == index)
                ||
                (v2 == index);
        }

        public override string ToString()
        {
            return "{" + v1.ToString() + ", " + v2.ToString() + "}";
        }

        #region equality override
        public bool Equals(Undirected_Edge other)
        {
            return
            (
                (this.v1 == other.v1 && this.v2 == other.v2)
                ||
                (this.v1 == other.v2 && this.v2 == other.v1)
            );
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is Undirected_Edge other_edge)
            {
                return Equals(other_edge);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + v1.GetHashCode() + v2.GetHashCode();
            return hash;
        }

        public static bool operator ==(Undirected_Edge c1, Undirected_Edge c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(Undirected_Edge c1, Undirected_Edge c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
