using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Directions_Ortho_3d
    {
        [TestMethod]
        public void Test__Constructors()
        {
            Direction_Ortho_3D dir = Direction_Ortho_3D.None();
            Assert.IsTrue(dir.Q__State() == 0b00_00_00_00);
            Assert.IsTrue(dir.Q__Is_None());
            Assert.IsFalse(dir.Q__Uses_X_Axis());
            Assert.IsFalse(dir.Q__Uses_Y_Axis());
            Assert.IsFalse(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Right();
            Assert.IsTrue(dir.Q__State() == 0b00_00_00_01);
            Assert.IsTrue(dir.Q__Is_Right());
            Assert.IsTrue(dir.Q__Uses_X_Axis());
            Assert.IsFalse(dir.Q__Uses_Y_Axis());
            Assert.IsFalse(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Left();
            Assert.IsTrue(dir.Q__State() == 0b00_00_00_10);
            Assert.IsTrue(dir.Q__Is_Left());
            Assert.IsTrue(dir.Q__Uses_X_Axis());
            Assert.IsFalse(dir.Q__Uses_Y_Axis());
            Assert.IsFalse(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Up();
            Assert.IsTrue(dir.Q__State() == 0b00_00_01_00);
            Assert.IsTrue(dir.Q__Is_Up());
            Assert.IsFalse(dir.Q__Uses_X_Axis());
            Assert.IsTrue(dir.Q__Uses_Y_Axis());
            Assert.IsFalse(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Down();
            Assert.IsTrue(dir.Q__State() == 0b00_00_10_00);
            Assert.IsTrue(dir.Q__Is_Down());
            Assert.IsFalse(dir.Q__Uses_X_Axis());
            Assert.IsTrue(dir.Q__Uses_Y_Axis());
            Assert.IsFalse(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Forward();
            Assert.IsTrue(dir.Q__State() == 0b00_01_00_00);
            Assert.IsTrue(dir.Q__Is_Forward());
            Assert.IsFalse(dir.Q__Uses_X_Axis());
            Assert.IsFalse(dir.Q__Uses_Y_Axis());
            Assert.IsTrue(dir.Q__Uses_Z_Axis());

            dir = Direction_Ortho_3D.Back();
            Assert.IsTrue(dir.Q__State() == 0b00_10_00_00);
            Assert.IsTrue(dir.Q__Is_Back());
            Assert.IsFalse(dir.Q__Uses_X_Axis());
            Assert.IsFalse(dir.Q__Uses_Y_Axis());
            Assert.IsTrue(dir.Q__Uses_Z_Axis());


            Direction_Ortho_3D dir_1 = Direction_Ortho_3D.Right();
            Direction_Ortho_3D dir_2 = Direction_Ortho_3D.Right();
            Assert.IsTrue(dir_1.Equals(dir_2));
            Assert.IsTrue(dir_1 == dir_2);
        }
    }
}
