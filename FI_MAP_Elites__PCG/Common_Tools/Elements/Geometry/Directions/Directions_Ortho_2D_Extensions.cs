using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Directions_Ortho_2D_Extensions
    {

        public static bool HasFlag(
            this Directions_Ortho_2D directions,
            Directions_Ortho_2D flag
            )
        {
            return (directions & flag) == flag;
        }

        public static bool IsSingle(
            this Directions_Ortho_2D direction
            )
        {
            return
                direction == Directions_Ortho_2D.U
                ||
                direction == Directions_Ortho_2D.D
                ||
                direction == Directions_Ortho_2D.L
                ||
                direction == Directions_Ortho_2D.R;
        }

        public static List<Directions_Ortho_2D> AsList(
            this Directions_Ortho_2D directions
            )
        {
            List<Directions_Ortho_2D> directionsList = new List<Directions_Ortho_2D>();
            if (directions.HasFlag(Directions_Ortho_2D.U))
            {
                directionsList.Add(Directions_Ortho_2D.U);
            }
            if (directions.HasFlag(Directions_Ortho_2D.D))
            {
                directionsList.Add(Directions_Ortho_2D.D);
            }
            if (directions.HasFlag(Directions_Ortho_2D.L))
            {
                directionsList.Add(Directions_Ortho_2D.L);
            }
            if (directions.HasFlag(Directions_Ortho_2D.R))
            {
                directionsList.Add(Directions_Ortho_2D.R);
            }
            return directionsList;
        }

        public static Directions_Ortho_2D AddFlag(
            this Directions_Ortho_2D directions,
            Directions_Ortho_2D flag
            )
        {
            return directions | flag;
        }

        public static Directions_Ortho_2D RemoveFlag(
            this Directions_Ortho_2D directions,
            Directions_Ortho_2D flag
            )
        {
            return directions & ~flag;
        }
    }
}
