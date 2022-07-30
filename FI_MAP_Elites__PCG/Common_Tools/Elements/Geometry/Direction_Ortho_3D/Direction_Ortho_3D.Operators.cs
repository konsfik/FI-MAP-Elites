using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Direction_Ortho_3D
    {
        public void M__Flip() {
            if (this.Q__Is_Right()) this._state = _left_state;
            else if (this.Q__Is_Left()) this._state = _right_state;
            else if (this.Q__Is_Up()) this._state = _down_state;
            else if (this.Q__Is_Down()) this._state = _up_state;
            else if (this.Q__Is_Forward()) this._state = _back_state;
            else if (this.Q__Is_Back()) this._state = _forward_state;
        }

        public void M__Flip_LR() {
            if (this.Q__Is_Right()) this._state = _left_state;
            else if (this.Q__Is_Left()) this._state = _right_state;
        }

        public void M__Flip_UD() {
            if (this.Q__Is_Up()) this._state = _down_state;
            else if (this.Q__Is_Down()) this._state = _up_state;
        }

        public void M__Flip_FB() {
            if (this.Q__Is_Forward()) this._state = _back_state;
            else if (this.Q__Is_Back()) this._state = _forward_state;
        }
    }
}
