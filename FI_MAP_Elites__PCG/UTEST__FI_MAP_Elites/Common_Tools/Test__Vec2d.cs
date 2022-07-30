using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Vec2d
    {
        [TestMethod]
        public void Test__Q__Angle_To()
        {
            double epsilon = 0.0000_0001;

            Vec2d v1 = new Vec2d(0.0, 1.0);
            Vec2d v2 = new Vec2d(0.0, 1.0);
            double angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(0.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, 1.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, 0.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 2.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, -1.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(3.0 * Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(0.0, -1.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, -1.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(3.0 * Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, 0.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 2.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, 1.0);
            angle = v1.Q__Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 4.0, epsilon));
        }


        [TestMethod]
        public void Test__Q__Signed_Angle_To()
        {
            double epsilon = 0.0000_0001;

            Vec2d v1 = new Vec2d(0.0, 1.0);
            Vec2d v2 = new Vec2d(0.0, 1.0);
            double angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(0.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, 1.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(-Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, 0.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(-Math.PI / 2.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(1.0, -1.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(-3.0 * Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(0.0, -1.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(
                angle.Q__Approximately_Equal(Math.PI, epsilon)
                ||
                angle.Q__Approximately_Equal(-Math.PI, epsilon)
                );

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, -1.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(3.0 * Math.PI / 4.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, 0.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 2.0, epsilon));

            v1 = new Vec2d(0.0, 1.0);
            v2 = new Vec2d(-1.0, 1.0);
            angle = v1.Q__Signed_Angle_To(v2);
            Assert.IsTrue(angle.Q__Approximately_Equal(Math.PI / 4.0, epsilon));
        }

    }
}
