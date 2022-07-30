using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    [Flags]
    public enum Directions_Ortho_2D
    {
        None = 0b0000, // 0
        R = 0b0001, // 1
        U = 0b0010, // 2
        L = 0b0100, // 4
        D = 0b1000, // 8

        X = L | R,
        Y = U | D,

        ORTHO = X | Y
    }
}
