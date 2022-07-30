using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public struct UEdge_i : IEquatable<UEdge_i>
    {
        public int id_0;
        public int id_1;

        public UEdge_i(int id_1, int id_2)
        {
            this.id_0 = id_1;
            this.id_1 = id_2;
        }

        public bool Q__Is_Adjacent_To_Vertex(int vertex)
        {
            return
                vertex == id_0
                ||
                vertex == id_1;
        }

        public int Q__Other_Vertex(int vertex)
        {
            if (vertex == id_0) return id_1;
            else if (vertex == id_1) return id_0;
            else throw new Exception("non adjacent vertex");
        }

        public override string ToString()
        {
            return "{" + id_0.ToString() + ", " + id_1.ToString() + "}";
        }

        #region equality override
        public bool Equals(UEdge_i other)
        {
            return
            (
                (this.id_0 == other.id_0 && this.id_1 == other.id_1)
                ||
                (this.id_0 == other.id_1 && this.id_1 == other.id_0)
            );
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is UEdge_i other_edge)
            {
                return Equals(other_edge);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + id_0.GetHashCode() + id_1.GetHashCode();
            return hash;
        }

        public static bool operator ==(UEdge_i c1, UEdge_i c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(UEdge_i c1, UEdge_i c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
