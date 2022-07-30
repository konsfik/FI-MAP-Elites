using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Direction_Ortho_3D : IEquatable<Direction_Ortho_3D>
    {
        private byte _state;

        private const byte _none_state = 0b_00_00_00_00;
        private const byte _right_state = 0b_00_00_00_01;
        private const byte _left_state = 0b_00_00_00_10;
        private const byte _up_state = 0b_00_00_01_00;
        private const byte _down_state = 0b_00_00_10_00;
        private const byte _forward_state = 0b_00_01_00_00;
        private const byte _back_state = 0b_00_10_00_00;
        private const byte _all_state = 0b_00_11_11_11;

        public Direction_Ortho_3D(byte state)
        {
            switch (state)
            {
                case _none_state:
                    _state = state;
                    break;
                case _right_state:
                    _state = state;
                    break;
                case _left_state:
                    _state = state;
                    break;
                case _up_state:
                    _state = state;
                    break;
                case _down_state:
                    _state = state;
                    break;
                case _forward_state:
                    _state = state;
                    break;
                case _back_state:
                    _state = state;
                    break;
                default:
                    throw new Exception("illegal state");
            }
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other"></param>
        public Direction_Ortho_3D(Direction_Ortho_3D other)
            :
            this(other._state)
        {

        }

        public static Direction_Ortho_3D None()
        {
            return new Direction_Ortho_3D(_none_state);
        }

        public static Direction_Ortho_3D Right()
        {
            return new Direction_Ortho_3D(_right_state);
        }

        public static Direction_Ortho_3D Left()
        {
            return new Direction_Ortho_3D(_left_state);
        }

        public static Direction_Ortho_3D Up()
        {
            return new Direction_Ortho_3D(_up_state);
        }

        public static Direction_Ortho_3D Down()
        {
            return new Direction_Ortho_3D(_down_state);
        }

        public static Direction_Ortho_3D Forward()
        {
            return new Direction_Ortho_3D(_forward_state);
        }

        public static Direction_Ortho_3D Back()
        {
            return new Direction_Ortho_3D(_back_state);
        }

        #region equality override
        public override bool Equals(object other_obj)
        {
            if (other_obj is Direction_Ortho_3D other_dir)
            {
                return this.Equals(other_dir);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Direction_Ortho_3D other)
        {
            return this._state == other._state;
        }

        public override int GetHashCode()
        {
            return _state.GetHashCode();
        }

        public static bool operator ==(Direction_Ortho_3D a, Direction_Ortho_3D b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Direction_Ortho_3D a, Direction_Ortho_3D b)
        {
            return !(a == b);
        }
        #endregion
    }
}
