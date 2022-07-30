using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;


namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    /// <summary>
    /// An entrance door.
    /// This door connects a space unit (interior or exterior) to the "outside". 
    /// </summary>
    public struct EG__Entrance_Door
    {
        public readonly int space_unit;
        public readonly Undirected_Edge cells_connection;

        // json contructor
        public EG__Entrance_Door(
            int space_unit,
            Undirected_Edge cells_connection
            )
        {
            this.space_unit = space_unit;
            this.cells_connection = cells_connection;
        }

        public int Q__Cell_1()
        {
            return cells_connection.v1;
        }

        public int Q__Cell_2()
        {
            return cells_connection.v2;
        }

        public Undirected_Edge Q__Cells_Connection() {
            return cells_connection;
        }

        public int Q__Space_Unit() {
            return space_unit;
        }

        #region equality override
        public bool Equals(EG__Entrance_Door other)
        {
            return
            (
            this.cells_connection == other.cells_connection
            &&
            this.space_unit == other.space_unit
            );
        }

        public override bool Equals(object other_object)
        {
            if (other_object is EG__Entrance_Door other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + cells_connection.GetHashCode();
            hash = hash * 31 + space_unit.GetHashCode();
            return hash;
        }

        public static bool operator ==(EG__Entrance_Door c1, EG__Entrance_Door c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(EG__Entrance_Door c1, EG__Entrance_Door c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
