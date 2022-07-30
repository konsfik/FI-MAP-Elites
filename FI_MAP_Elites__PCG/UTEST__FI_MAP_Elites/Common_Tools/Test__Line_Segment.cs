using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Line_Segment
    {
        [TestMethod]
        public void Test__Q__Inclination()
        {
            double epsilon = 0.0000_0001;

            Line_Segment line = new Line_Segment(0, 0, 0, 1); // up
            double inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(1.0, epsilon));

            line = new Line_Segment(0, 0, 1, 1); // up-right
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(0, 0, 1, 0); // right
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.0, epsilon));

            line = new Line_Segment(0, 0, 1, -1); // right-down
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(0, 0, 0, -1); // down
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(1.0, epsilon));

            line = new Line_Segment(0, 0, -1, -1); // down-left
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(0, 0, -1, 0); // left
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.0, epsilon));

            line = new Line_Segment(0, 0, -1, 1); // left-up
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));




            line = new Line_Segment(1, 1, 1, 2); // up
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(1.0, epsilon));

            line = new Line_Segment(1, 1, 2, 2); // up-right
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(1, 1, 2, 1); // right
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.0, epsilon));

            line = new Line_Segment(1, 1, 2, 0); // right-down
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(1, 1, 1, 0); // down
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(1.0, epsilon));

            line = new Line_Segment(1, 1, 0, 0); // down-left
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));

            line = new Line_Segment(1, 1, 0, 1); // left
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.0, epsilon));

            line = new Line_Segment(1, 1, 0, 0); // left-up
            inclination = line.Q__Inclination();
            Assert.IsTrue(inclination.Q__Approximately_Equal(0.5, epsilon));
        }

    }
}
