using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Direction_Ortho_3D
    {
        public byte Q__State() {
            return _state;
        }

        public bool Q__Is_None()
        {
            return (_state & _none_state) == _none_state;
        }

        public bool Q__Is_Right()
        {
            return (_state & _right_state) == _right_state;
        }

        public bool Q__Is_Left()
        {
            return (_state & _left_state) == _left_state;
        }

        public bool Q__Is_Up()
        {
            return (_state & _up_state) == _up_state;
        }

        public bool Q__Is_Down()
        {
            return (_state & _down_state) == _down_state;
        }

        public bool Q__Is_Forward()
        {
            return (_state & _forward_state) == _forward_state;
        }

        public bool Q__Is_Back()
        {
            return (_state & _back_state) == _back_state;
        }

        public bool Q__Uses_X_Axis()
        {
            return Q__Is_Left() || Q__Is_Right();
        }

        public bool Q__Uses_Y_Axis()
        {
            return Q__Is_Up() || Q__Is_Down();
        }

        public bool Q__Uses_Z_Axis()
        {
            return Q__Is_Forward() || Q__Is_Back();
        }
    }
}
