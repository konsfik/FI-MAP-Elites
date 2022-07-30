using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Rect2i
    {
        [TestMethod]
        public void Test__Constructors() {
            Rect2i rect = Rect2i.From__Num_Points(
                num_points_x: 2, 
                num_points_y: 2
                );

            Assert.IsTrue(rect.Min_X() == 0);
            Assert.IsTrue(rect.Min_Y() == 0);
            Assert.IsTrue(rect.Max_X() == 1);
            Assert.IsTrue(rect.Max_Y() == 1);

            Assert.IsTrue(rect.Q__Inner_Width() == 1);
            Assert.IsTrue(rect.Q__Outer_Width() == 2);
            Assert.IsTrue(rect.Q__Inner_Height() == 1);
            Assert.IsTrue(rect.Q__Outer_Height() == 2);

            Assert.IsTrue(rect.Q__Num_Points_X() == 2);
            Assert.IsTrue(rect.Q__Num_Points_Y() == 2);
            Assert.IsTrue(rect.Q__Num_Points() == 4);

            rect = Rect2i.From__Outer_Dimensions(
                outer_width: 2,
                outer_height: 2
                );

            Assert.IsTrue(rect.Min_X() == 0);
            Assert.IsTrue(rect.Min_Y() == 0);
            Assert.IsTrue(rect.Max_X() == 1);
            Assert.IsTrue(rect.Max_Y() == 1);

            Assert.IsTrue(rect.Q__Inner_Width() == 1);
            Assert.IsTrue(rect.Q__Outer_Width() == 2);
            Assert.IsTrue(rect.Q__Inner_Height() == 1);
            Assert.IsTrue(rect.Q__Outer_Height() == 2);

            Assert.IsTrue(rect.Q__Num_Points_X() == 2);
            Assert.IsTrue(rect.Q__Num_Points_Y() == 2);
            Assert.IsTrue(rect.Q__Num_Points() == 4);

            rect = Rect2i.From__Inner_Dimensions(
                inner_width: 1,
                inner_height: 1
                );

            rect = Rect2i.From__Inner_Size(
                inner_size: 2
                );

            Assert.IsTrue(rect.Min_X() == 0);
            Assert.IsTrue(rect.Min_Y() == 0);
            Assert.IsTrue(rect.Max_X() == 2);
            Assert.IsTrue(rect.Max_Y() == 2);

            Assert.IsTrue(rect.Q__Inner_Width() == 2);
            Assert.IsTrue(rect.Q__Outer_Width() == 3);
            Assert.IsTrue(rect.Q__Inner_Height() == 2);
            Assert.IsTrue(rect.Q__Outer_Height() == 3);

            Assert.IsTrue(rect.Q__Num_Points_X() == 3);
            Assert.IsTrue(rect.Q__Num_Points_Y() == 3);
            Assert.IsTrue(rect.Q__Num_Points() == 9);
        }
    }
}
