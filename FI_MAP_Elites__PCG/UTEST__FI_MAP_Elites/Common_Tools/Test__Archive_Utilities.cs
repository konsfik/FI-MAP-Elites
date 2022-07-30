using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Archive_Utilities
    {
        [TestMethod]
        public void Test____Index__To__Coordinates_2D()
        {
            int width = 2;
            int height = 3;

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(-3, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(-2, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(-1, width, height));

            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(0, width, height) == new Vec2i(0, 0));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(1, width, height) == new Vec2i(1, 0));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(2, width, height) == new Vec2i(0, 1));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(3, width, height) == new Vec2i(1, 1));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(4, width, height) == new Vec2i(0, 2));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_2D(5, width, height) == new Vec2i(1, 2));

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(6, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(7, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Index__To__Coordinates_2D(8, width, height));

        }

        [TestMethod]
        public void Test____Coordinates_2D__To__Index()
        {
            int width = 2;
            int height = 3;

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(-1, 0), width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(0, -1), width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(-1, -1), width, height));

            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(0, 0), width, height) == 0);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(1, 0), width, height) == 1);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(0, 1), width, height) == 2);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(1, 1), width, height) == 3);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(0, 2), width, height) == 4);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(1, 2), width, height) == 5);

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(2, 0), width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(0, 3), width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(new Vec2i(2, 3), width, height));



            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(-1, 0, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(0, -1, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(-1, -1, width, height));

            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(0, 0, width, height) == 0);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(1, 0, width, height) == 1);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(0, 1, width, height) == 2);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(1, 1, width, height) == 3);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(0, 2, width, height) == 4);
            Assert.IsTrue(Archive_Utilities.Coordinates_2D__To__Index(1, 2, width, height) == 5);

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(2, 0, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(0, 3, width, height));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_2D__To__Index(2, 3, width, height));

        }


        [TestMethod]
        public void Test____Index__To__Coordinates_ND()
        {
            int[] size__per__dimension = new int[4] { 2, 3, 4, 5 };

            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(0, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 0, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(1, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 0, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(2, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 0, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(3, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 0, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(4, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 0, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(5, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 0, 0 }));

            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 1, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(7, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 1, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(8, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 1, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(9, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 1, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(10, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 1, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(11, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 1, 0 }));

            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(12, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 2, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(13, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 2, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(14, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 2, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(15, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 2, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(16, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 2, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(17, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 2, 0 }));

            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(18, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 3, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(19, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 3, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(20, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 3, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(21, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 3, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(22, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 3, 0 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(23, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 3, 0 }));


            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(24, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 0, 1 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(25, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 0, 1 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(26, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 0, 1 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(27, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 0, 1 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(28, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 0, 1 }));
            Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(29, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 0, 1 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 1, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(7, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 1, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(8, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 1, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(9, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 1, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(10, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 1, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(11, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 1, 1 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(12, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 2, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(13, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 2, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(14, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 2, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(15, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 2, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(16, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 2, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(17, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 2, 1 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(18, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 3, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(19, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 3, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(20, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 3, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(21, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 3, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(22, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 3, 1 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(23, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 3, 1 }));


            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(24, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 0, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(25, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 0, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(26, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 0, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(27, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 0, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(28, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 0, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(29, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 0, 2 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(30, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 1, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(31, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 1, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(32, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 1, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(33, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 1, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(34, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 1, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(35, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 1, 2 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(36, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 2, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(37, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 2, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(38, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 2, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(39, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 2, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(40, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 2, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(41, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 2, 2 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(42, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 3, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(43, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 3, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(44, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 3, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(45, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 3, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(46, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 3, 2 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(47, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 3, 2 }));


            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(0, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 0, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(1, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 0, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(2, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 0, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(3, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 0, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(4, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 0, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(5, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 0, 3 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 1, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 1, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 1, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 1, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 1, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 1, 3 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 2, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 2, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 2, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 2, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 2, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 2, 3 }));

            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 3, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 3, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 3, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 3, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 3, 3 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 3, 3 }));


            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(0, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 0, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(1, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 0, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(2, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 0, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(3, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 0, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(4, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 0, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(5, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 0, 4 }));
                                                                                                                                        
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 1, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 1, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 1, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 1, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 1, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 1, 4 }));
                                                                                                                                        
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 2, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 2, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 2, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 2, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 2, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 2, 4 }));
                                                                                                                                        
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 0, 3, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 0, 3, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 1, 3, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 1, 3, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 0, 2, 3, 4 }));
            //Assert.IsTrue(Archive_Utilities.Index__To__Coordinates_ND(6, size__per__dimension).SequenceEqual(new List<int>() { 1, 2, 3, 4 }));


        }


        [TestMethod]
        public void Test____Coordinates_ND__To__Index()
        {
            int[] size_per_dimension = new int[4] { 2, 3, 4, 5 };

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { -1, 0, 0, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, -1, 0, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, -1, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, -1 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { -1, -1, -1, -1 }, size_per_dimension));

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 2, 0, 0, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 3, 0, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 4, 0 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 5 }, size_per_dimension));
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 2, 3, 4, 5 }, size_per_dimension));

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 0 }, size_per_dimension) == 0);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 0, 0 }, size_per_dimension) == 1);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 0, 0 }, size_per_dimension) == 2);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 0, 0 }, size_per_dimension) == 3);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 0, 0 }, size_per_dimension) == 4);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 0, 0 }, size_per_dimension) == 5);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 1, 0 }, size_per_dimension) == 6);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 1, 0 }, size_per_dimension) == 7);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 1, 0 }, size_per_dimension) == 8);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 1, 0 }, size_per_dimension) == 9);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 1, 0 }, size_per_dimension) == 10);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 1, 0 }, size_per_dimension) == 11);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 2, 0 }, size_per_dimension) == 12);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 2, 0 }, size_per_dimension) == 13);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 2, 0 }, size_per_dimension) == 14);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 2, 0 }, size_per_dimension) == 15);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 2, 0 }, size_per_dimension) == 16);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 2, 0 }, size_per_dimension) == 17);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 3, 0 }, size_per_dimension) == 18);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 3, 0 }, size_per_dimension) == 19);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 3, 0 }, size_per_dimension) == 20);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 3, 0 }, size_per_dimension) == 21);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 3, 0 }, size_per_dimension) == 22);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 3, 0 }, size_per_dimension) == 23);


            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 1 }, size_per_dimension) == 24);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 0, 1 }, size_per_dimension) == 25);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 0, 1 }, size_per_dimension) == 26);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 0, 1 }, size_per_dimension) == 27);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 0, 1 }, size_per_dimension) == 28);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 0, 1 }, size_per_dimension) == 29);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 1, 1 }, size_per_dimension) == 30);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 1, 1 }, size_per_dimension) == 31);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 1, 1 }, size_per_dimension) == 32);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 1, 1 }, size_per_dimension) == 33);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 1, 1 }, size_per_dimension) == 34);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 1, 1 }, size_per_dimension) == 35);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 2, 1 }, size_per_dimension) == 36);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 2, 1 }, size_per_dimension) == 37);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 2, 1 }, size_per_dimension) == 38);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 2, 1 }, size_per_dimension) == 39);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 2, 1 }, size_per_dimension) == 40);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 2, 1 }, size_per_dimension) == 41);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 3, 1 }, size_per_dimension) == 42);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 3, 1 }, size_per_dimension) == 43);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 3, 1 }, size_per_dimension) == 44);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 3, 1 }, size_per_dimension) == 45);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 3, 1 }, size_per_dimension) == 46);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 3, 1 }, size_per_dimension) == 47);


            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 2 }, size_per_dimension) == 48);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 0, 2 }, size_per_dimension) == 49);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 0, 2 }, size_per_dimension) == 50);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 0, 2 }, size_per_dimension) == 51);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 0, 2 }, size_per_dimension) == 52);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 0, 2 }, size_per_dimension) == 53);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 1, 2 }, size_per_dimension) == 54);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 1, 2 }, size_per_dimension) == 55);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 1, 2 }, size_per_dimension) == 56);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 1, 2 }, size_per_dimension) == 57);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 1, 2 }, size_per_dimension) == 58);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 1, 2 }, size_per_dimension) == 59);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 2, 2 }, size_per_dimension) == 60);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 2, 2 }, size_per_dimension) == 61);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 2, 2 }, size_per_dimension) == 62);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 2, 2 }, size_per_dimension) == 63);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 2, 2 }, size_per_dimension) == 64);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 2, 2 }, size_per_dimension) == 65);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 3, 2 }, size_per_dimension) == 66);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 3, 2 }, size_per_dimension) == 67);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 3, 2 }, size_per_dimension) == 68);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 3, 2 }, size_per_dimension) == 69);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 3, 2 }, size_per_dimension) == 70);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 3, 2 }, size_per_dimension) == 71);


            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 3 }, size_per_dimension) == 72);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 0, 3 }, size_per_dimension) == 73);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 0, 3 }, size_per_dimension) == 74);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 0, 3 }, size_per_dimension) == 75);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 0, 3 }, size_per_dimension) == 76);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 0, 3 }, size_per_dimension) == 77);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 1, 3 }, size_per_dimension) == 78);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 1, 3 }, size_per_dimension) == 79);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 1, 3 }, size_per_dimension) == 80);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 1, 3 }, size_per_dimension) == 81);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 1, 3 }, size_per_dimension) == 82);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 1, 3 }, size_per_dimension) == 83);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 2, 3 }, size_per_dimension) == 84);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 2, 3 }, size_per_dimension) == 85);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 2, 3 }, size_per_dimension) == 86);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 2, 3 }, size_per_dimension) == 87);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 2, 3 }, size_per_dimension) == 88);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 2, 3 }, size_per_dimension) == 89);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 3, 3 }, size_per_dimension) == 90);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 3, 3 }, size_per_dimension) == 91);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 3, 3 }, size_per_dimension) == 92);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 3, 3 }, size_per_dimension) == 93);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 3, 3 }, size_per_dimension) == 94);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 3, 3 }, size_per_dimension) == 95);


            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 0, 4 }, size_per_dimension) == 96);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 0, 4 }, size_per_dimension) == 97);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 0, 4 }, size_per_dimension) == 98);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 0, 4 }, size_per_dimension) == 99);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 0, 4 }, size_per_dimension) == 100);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 0, 4 }, size_per_dimension) == 101);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 1, 4 }, size_per_dimension) == 102);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 1, 4 }, size_per_dimension) == 103);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 1, 4 }, size_per_dimension) == 104);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 1, 4 }, size_per_dimension) == 105);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 1, 4 }, size_per_dimension) == 106);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 1, 4 }, size_per_dimension) == 107);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 2, 4 }, size_per_dimension) == 108);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 2, 4 }, size_per_dimension) == 109);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 2, 4 }, size_per_dimension) == 110);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 2, 4 }, size_per_dimension) == 111);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 2, 4 }, size_per_dimension) == 112);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 2, 4 }, size_per_dimension) == 113);

            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 0, 3, 4 }, size_per_dimension) == 114);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 0, 3, 4 }, size_per_dimension) == 115);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 1, 3, 4 }, size_per_dimension) == 116);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 1, 3, 4 }, size_per_dimension) == 117);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 0, 2, 3, 4 }, size_per_dimension) == 118);
            Assert.IsTrue(Archive_Utilities.Coordinates_ND__To__Index(new List<int>() { 1, 2, 3, 4 }, size_per_dimension) == 119);
        }
    }
}
