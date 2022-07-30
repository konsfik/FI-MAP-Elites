using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Geometric_Utilities
    {
        [TestMethod]
        public void Test__Points()
        {
            double epsilon = 0.000001;


            Vec2d[] pts = new Vec2d[3] {
                new Vec2d(1.0,1.0),
                new Vec2d(2.0,2.0),
                new Vec2d(3.0,3.0)
            };

            Vec2d[] left_pts = pts.Q__Mirrored_LR(0.0);
            Assert.IsTrue(left_pts[0].x.Q__Approximately_Equal(-1.0, epsilon));
            Assert.IsTrue(left_pts[1].x.Q__Approximately_Equal(-2.0, epsilon));
            Assert.IsTrue(left_pts[2].x.Q__Approximately_Equal(-3.0, epsilon));

            Vec2d[] right_pts = pts.Q__Mirrored_LR(4.0);
            Assert.IsTrue(right_pts[0].x.Q__Approximately_Equal(7.0, epsilon));
            Assert.IsTrue(right_pts[1].x.Q__Approximately_Equal(6.0, epsilon));
            Assert.IsTrue(right_pts[2].x.Q__Approximately_Equal(5.0, epsilon));

            Vec2d[] down_pts = pts.Q__Mirrored_UD(0.0);
            Assert.IsTrue(down_pts[0].y.Q__Approximately_Equal(-1.0, epsilon));
            Assert.IsTrue(down_pts[1].y.Q__Approximately_Equal(-2.0, epsilon));
            Assert.IsTrue(down_pts[2].y.Q__Approximately_Equal(-3.0, epsilon));

            Vec2d[] up_pts = pts.Q__Mirrored_UD(4.0);
            Assert.IsTrue(up_pts[0].y.Q__Approximately_Equal(7.0, epsilon));
            Assert.IsTrue(up_pts[1].y.Q__Approximately_Equal(6.0, epsilon));
            Assert.IsTrue(up_pts[2].y.Q__Approximately_Equal(5.0, epsilon));
        }

        

        [TestMethod]
        public void Test__Area()
        {
            double epsilon = 0.000001;

            List<Vec2d> points = new List<Vec2d>() {
                new Vec2d(0.0,0.0),
                new Vec2d(1.0,0.0),
                new Vec2d(1.0,1.0),
                new Vec2d(0.0,1.0),
            };

            Assert.IsTrue(points.Q__Area().Q__Approximately_Equal(1.0, epsilon));

            points = new List<Vec2d>() {
                new Vec2d(0.0,0.0),
                new Vec2d(1.0,0.0),
                new Vec2d(1.0,1.0),
                new Vec2d(0.0,1.0),
                new Vec2d(0.0,0.0),
            };

            Assert.IsTrue(points.Q__Area().Q__Approximately_Equal(1.0, epsilon));

            points = new List<Vec2d>() {
                new Vec2d(0.0,0.0),
                new Vec2d(1.0,0.0),
                new Vec2d(1.0,2.0),
                new Vec2d(0.0,2.0),
            };

            Assert.IsTrue(points.Q__Area().Q__Approximately_Equal(2.0, epsilon));

            points = new List<Vec2d>() {
                new Vec2d(0.0,0.0),
                new Vec2d(2.0,0.0),
                new Vec2d(2.0,2.0),
                new Vec2d(0.0,2.0),
            };

            Assert.IsTrue(points.Q__Area().Q__Approximately_Equal(4.0, epsilon));

            // self - intersection
            points = new List<Vec2d>() {
                new Vec2d(0.0,0.0),
                new Vec2d(1.0,0.0),
                new Vec2d(0.0,1.0),
                new Vec2d(1.0,1.0),
            };

            Assert.IsTrue(points.Q__Area().Q__Approximately_Equal(0.0, epsilon));
        }
    }
}
