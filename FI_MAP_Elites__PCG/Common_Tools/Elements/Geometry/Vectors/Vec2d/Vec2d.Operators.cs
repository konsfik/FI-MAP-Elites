using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Vec2d
    {
        public void M__Normalize()
        {
            double magnitude = this.Q__Magnitude();
            if (magnitude == 0.0) return;
            x /= magnitude;
            y /= magnitude;
        }


    }
}
