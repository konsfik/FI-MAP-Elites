using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Utilities
    {
        

        [TestMethod]
        public void Test____Archive____Q__Cell_Coordinates__To__Cell_Index()
        {
            int[] cells_per_dim = new int[] { 2, 3, 4, 5 };

            Archive_N_Dim<int> archive = new Archive_N_Dim<int>(cells_per_dim);
            //Assert.ThrowsException()
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 0, 0 }) == 0);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 0, 0 }) == 1);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 0, 0 }) == 2);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 0, 0 }) == 3);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 0, 0 }) == 4);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 0, 0 }) == 5);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 1, 0 }) == 6);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 1, 0 }) == 7);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 1, 0 }) == 8);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 1, 0 }) == 9);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 1, 0 }) == 10);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 1, 0 }) == 11);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 2, 0 }) == 12);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 2, 0 }) == 13);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 2, 0 }) == 14);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 2, 0 }) == 15);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 2, 0 }) == 16);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 2, 0 }) == 17);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 3, 0 }) == 18);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 3, 0 }) == 19);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 3, 0 }) == 20);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 3, 0 }) == 21);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 3, 0 }) == 22);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 3, 0 }) == 23);

            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 0, 1 }) == 24);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 0, 1 }) == 25);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 0, 1 }) == 26);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 0, 1 }) == 27);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 0, 1 }) == 28);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 0, 1 }) == 29);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 1, 1 }) == 30);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 1, 1 }) == 31);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 1, 1 }) == 32);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 1, 1 }) == 33);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 1, 1 }) == 34);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 1, 1 }) == 35);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 2, 1 }) == 36);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 2, 1 }) == 37);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 2, 1 }) == 38);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 2, 1 }) == 39);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 2, 1 }) == 40);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 2, 1 }) == 41);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 3, 1 }) == 42);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 3, 1 }) == 43);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 3, 1 }) == 44);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 3, 1 }) == 45);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 3, 1 }) == 46);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 3, 1 }) == 47);

            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 0, 2 }) == 48);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 0, 2 }) == 49);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 0, 2 }) == 50);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 0, 2 }) == 51);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 0, 2 }) == 52);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 0, 2 }) == 53);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 1, 2 }) == 54);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 1, 2 }) == 55);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 1, 2 }) == 56);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 1, 2 }) == 57);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 1, 2 }) == 58);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 1, 2 }) == 59);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 2, 2 }) == 60);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 2, 2 }) == 61);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 2, 2 }) == 62);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 2, 2 }) == 63);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 2, 2 }) == 64);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 2, 2 }) == 65);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 3, 2 }) == 66);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 3, 2 }) == 67);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 3, 2 }) == 68);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 3, 2 }) == 69);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 3, 2 }) == 70);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 3, 2 }) == 71);

            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 0, 3 }) == 72);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 0, 3 }) == 73);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 0, 3 }) == 74);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 0, 3 }) == 75);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 0, 3 }) == 76);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 0, 3 }) == 77);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 1, 3 }) == 78);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 1, 3 }) == 79);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 1, 3 }) == 80);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 1, 3 }) == 81);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 1, 3 }) == 82);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 1, 3 }) == 83);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 2, 3 }) == 84);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 2, 3 }) == 85);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 2, 3 }) == 86);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 2, 3 }) == 87);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 2, 3 }) == 88);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 2, 3 }) == 89);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 3, 3 }) == 90);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 3, 3 }) == 91);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 3, 3 }) == 92);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 3, 3 }) == 93);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 3, 3 }) == 94);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 3, 3 }) == 95);

            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 0, 4 }) == 96);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 0, 4 }) == 97);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 0, 4 }) == 98);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 0, 4 }) == 99);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 0, 4 }) == 100);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 0, 4 }) == 101);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 1, 4 }) == 102);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 1, 4 }) == 103);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 1, 4 }) == 104);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 1, 4 }) == 105);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 1, 4 }) == 106);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 1, 4 }) == 107);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 2, 4 }) == 108);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 2, 4 }) == 109);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 2, 4 }) == 110);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 2, 4 }) == 111);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 2, 4 }) == 112);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 2, 4 }) == 113);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 0, 3, 4 }) == 114);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 0, 3, 4 }) == 115);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 1, 3, 4 }) == 116);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 1, 3, 4 }) == 117);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 0, 2, 3, 4 }) == 118);
            Assert.IsTrue(archive.Q__Cell_Coordinates__To__Cell_Index(new List<int>() { 1, 2, 3, 4 }) == 119);
        }

        [TestMethod]
        public void Test_Area()
        {
            List<Vec2d> pointz = new List<Vec2d>()
            {
                new Vec2d(0,0),
                new Vec2d(1,0),
                new Vec2d(1,1),
                new Vec2d(0,1),
                new Vec2d(0,0)
            };

            Assert.IsTrue(Almost_Equal(pointz.Q__Area(), 1.0, 0.01));
        }


        [TestMethod]
        public void Test__Polar_Coordinates()
        {
            double my_epsilon = 0.001;

            Vec2d vector = Vec2d.From__Polar_Coordinates(0, 1);

            Assert.IsTrue(Almost_Equal(vector.x, 1.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, 0.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(Math.PI / 4, 1);

            Assert.IsTrue(Almost_Equal(vector.x, Math.Sqrt(2.0) / 2.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, Math.Sqrt(2.0) / 2.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(Math.PI / 2, 1);

            Assert.IsTrue(Almost_Equal(vector.x, 0.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, 1.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(3.0 * Math.PI / 4.0, 1);

            Assert.IsTrue(Almost_Equal(vector.x, -Math.Sqrt(2.0) / 2.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, Math.Sqrt(2.0) / 2.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(Math.PI, 1);

            Assert.IsTrue(Almost_Equal(vector.x, -1.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, 0.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(3 * Math.PI / 2, 1);

            Assert.IsTrue(Almost_Equal(vector.x, 0.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, -1.0, my_epsilon));

            vector = Vec2d.From__Polar_Coordinates(2 * Math.PI, 1);

            Assert.IsTrue(Almost_Equal(vector.x, 1.0, my_epsilon));
            Assert.IsTrue(Almost_Equal(vector.y, 0.0, my_epsilon));
        }

        public bool Almost_Equal(double v1, double v2, double epsilon)
        {
            return Math.Abs(v1 - v2) <= epsilon;
        }


        

        [TestMethod]
        public void Test__Map_Value_Double__Usage()
        {
            double num, from_min, from_max, to_min, to_max;
            double remapped;

            from_min = 0.0;
            from_max = 1.0;
            to_min = 0.0;
            to_max = 10.0;
            List<double> nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            List<double> results = new List<double>()
            {
                -10.0, -5.0, 0.0, 5.0, 10.0, 15.0, 20.0, 25.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0;
            from_max = 1.0;
            to_min = 10.0;
            to_max = 0.0;
            nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            results = new List<double>()
            {
                20.0, 15.0, 10.0, 5.0, 0.0, -5.0, -10.0, -15.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            num = 0.0;
            remapped = num.Q__Mapped(0.0, 0.0, 0.0, 0.0);
            Assert.IsTrue(Double.IsNaN(remapped));
            Assert.IsTrue(remapped.Equals(Double.NaN));

            num = 0.0;
            remapped = num.Q__Mapped(0.0, 0.0, 0.0, 1.0);
            Assert.IsTrue(Double.IsNaN(remapped));

            num = 0.0;
            remapped = num.Q__Mapped(0.0, 1.0, 0.0, 0.0);
            Assert.IsTrue(remapped == 0.0);

        }


        [TestMethod]
        public void Test__Map_Value_Float__Usage()
        {
            float num, from_min, from_max, to_min, to_max;
            float remapped;

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 0.0f;
            to_max = 10.0f;
            List<float> nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            List<float> results = new List<float>()
            {
                -10.0f, -5.0f, 0.0f, 5.0f, 10.0f, 15.0f, 20.0f, 25.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 10.0f;
            to_max = 0.0f;
            nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            results = new List<float>()
            {
                20.0f, 15.0f, 10.0f, 5.0f, 0.0f, -5.0f, -10.0f, -15.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            num = 0.0f;
            remapped = num.Q__Mapped(0.0f, 0.0f, 0.0f, 0.0f);
            Assert.IsTrue(float.IsNaN(remapped));
            Assert.IsTrue(remapped.Equals(float.NaN));

            num = 0.0f;
            remapped = num.Q__Mapped(0.0f, 0.0f, 0.0f, 1.0f);
            Assert.IsTrue(float.IsNaN(remapped));

            num = 0.0f;
            remapped = num.Q__Mapped(0.0f, 1.0f, 0.0f, 0.0f);
            Assert.IsTrue(remapped == 0.0f);
        }


        [TestMethod]
        public void Test__Map_Value_Double_Constrained__Usage()
        {
            double from_min, from_max, to_min, to_max;
            double remapped;

            from_min = 0.0;
            from_max = 1.0;
            to_min = 0.0;
            to_max = 10.0;
            List<double> nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            List<double> results = new List<double>()
            {
                0.0, 0.0, 0.0, 5.0, 10.0, 10.0, 10.0, 10.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0;
            from_max = 1.0;
            to_min = 10.0;
            to_max = 0.0;
            nums = new List<double>()
            {
                -1.0, -0.5, 0.0, 0.5, 1.0, 1.5, 2.0, 2.5
            };
            results = new List<double>()
            {
                10.0, 10.0, 10.0, 5.0, 0.0, 0.0, 0.0, 0.0
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }
        }


        [TestMethod]
        public void Test__Map_Value_Float_Constrained__Usage()
        {
            float from_min, from_max, to_min, to_max;
            float remapped;

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 0.0f;
            to_max = 10.0f;
            List<float> nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            List<float> results = new List<float>()
            {
                0.0f, 0.0f, 0.0f, 5.0f, 10.0f, 10.0f, 10.0f, 10.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }

            from_min = 0.0f;
            from_max = 1.0f;
            to_min = 10.0f;
            to_max = 0.0f;
            nums = new List<float>()
            {
                -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f
            };
            results = new List<float>()
            {
                10.0f, 10.0f, 10.0f, 5.0f, 0.0f, 0.0f, 0.0f, 0.0f
            };
            for (int i = 0; i < nums.Count; i++)
            {
                remapped = nums[i].Q__Mapped_Constrained(from_min, from_max, to_min, to_max);
                Assert.IsTrue(remapped == results[i]);
            }
        }
    }
}
