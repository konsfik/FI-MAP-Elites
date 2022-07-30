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
    /// A door that connects two space units.
    /// Those space units can be either interior or exterior.
    /// If either (or both) of those space units is an exterior space, 
    /// then the door itself is considered to be an exterior door.
    /// If, on the other hand, both space units are interior, then the door is 
    /// considered to be an interior door.
    /// </summary>
    public struct EG__Connection_Door : IEquatable<EG__Connection_Door>
    {
        public readonly Undirected_Edge cells_connection;
        public readonly Undirected_Edge space_units_connection;

        // json constructor
        public EG__Connection_Door(
            Undirected_Edge cells_connection,
            Undirected_Edge space_units_connection
            )
        {
            this.cells_connection = cells_connection;
            this.space_units_connection = space_units_connection;
        }

        public EG__Connection_Door(
            int cell_1, 
            int cell_2,
            int space_unit_1,
            int space_unit_2
            )
        {
            if (cell_1 == cell_2)
                throw new Exception("cell_1 must be different from cell_2");
            if (space_unit_1 == space_unit_2)
                throw new Exception("space_unit_1 must be different from space_unit_2");

            this.cells_connection = new Undirected_Edge(cell_1, cell_2);
            this.space_units_connection = new Undirected_Edge(space_unit_1, space_unit_2);
        }

        public int Q__Space_Unit_1()
        {
            return space_units_connection.v1;
        }

        public int Q__Space_Unit_2()
        {
            return space_units_connection.v2;
        }

        public Undirected_Edge Q__Space_Units_Connection() {
            return space_units_connection;
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

        #region equality override
        public bool Equals(EG__Connection_Door other)
        {
            return
            (
            this.cells_connection == other.cells_connection
            &&
            this.space_units_connection == other.space_units_connection
            );
        }

        public override bool Equals(object other_object)
        {
            if (other_object is EG__Connection_Door other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + cells_connection.GetHashCode();
            hash = hash * 31 + space_units_connection.GetHashCode();
            return hash;
        }

        public static bool operator ==(EG__Connection_Door c1, EG__Connection_Door c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(EG__Connection_Door c1, EG__Connection_Door c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
