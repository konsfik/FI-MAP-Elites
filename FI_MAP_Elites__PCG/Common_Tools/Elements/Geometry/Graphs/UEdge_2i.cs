using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public struct UEdge_2i : IEquatable<UEdge_2i>
    {
        public readonly Vec2i origin;
        public readonly Vec2i exit;

        public UEdge_2i(Vec2i origin, Vec2i exit)
        {
            this.origin = origin;
            this.exit = exit;
        }

        public Edge_2i Reverse()
        {
            return new Edge_2i(exit, origin);
        }

        public static UEdge_2i FromDirection(Vec2i origin, Directions_Ortho_2D direction)
        {
            if (direction.IsSingle() == false)
            {
                throw new System.Exception("direction is not single!");
            }
            else if (direction == Directions_Ortho_2D.None)
            {
                throw new System.Exception("direction is None!");
            }
            if (direction == Directions_Ortho_2D.U)
            {
                return new UEdge_2i(origin, origin.To_Up());
            }
            if (direction == Directions_Ortho_2D.D)
            {
                return new UEdge_2i(origin, origin.To_Down());
            }
            if (direction == Directions_Ortho_2D.L)
            {
                return new UEdge_2i(origin, origin.To_Left());
            }
            if (direction == Directions_Ortho_2D.R)
            {
                return new UEdge_2i(origin, origin.To_Right());
            }
            return new UEdge_2i();
        }

        public Directions_Ortho_2D To_Direction()
        {
            Vec2i diff = exit - origin;
            if (diff == Vec2i.up)
            {
                return Directions_Ortho_2D.U;
            }
            if (diff == Vec2i.down)
            {
                return Directions_Ortho_2D.D;
            }
            if (diff == Vec2i.left)
            {
                return Directions_Ortho_2D.L;
            }
            if (diff == Vec2i.right)
            {
                return Directions_Ortho_2D.R;
            }
            return Directions_Ortho_2D.None;
        }

        public override string ToString()
        {
            return "[" + origin.ToString() + "," + exit.ToString() + "]";
        }

        #region equality override
        public bool Equals(UEdge_2i other)
        {
            return
            (
                (this.origin.Equals(other.origin) && this.exit.Equals(other.exit))
                ||
                (this.origin.Equals(other.exit) && this.exit.Equals(other.origin))
            );
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is UEdge_2i other_edge)
            {
                return Equals(other_edge);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + origin.GetHashCode() + exit.GetHashCode();
            return hash;
        }

        public static bool operator ==(UEdge_2i c1, UEdge_2i c2)
        {
            //if (Object.ReferenceEquals(c1, null))
            //{
            //    if (Object.ReferenceEquals(c2, null))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else // c1 is not null
            //{
            //    if (Object.ReferenceEquals(c2, null)) // c2 is null
            //    {
            //        return false;
            //    }
            //}
            return c1.Equals(c2);
        }
        public static bool operator !=(UEdge_2i c1, UEdge_2i c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
