using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Math
    {
        [TestMethod]
        public void Test__Mean()
        {
            double epsilon = 0.0000_0001;

            List<double> double_list = new List<double>() { 0.0, 1.0, 2.0, 3.0 };
            Assert.IsTrue(double_list.Q__Mean().Q__Approximately_Equal(6.0 / 4.0, epsilon));

            double_list = new List<double>() { -1.0, 0.0, 1.0 };
            Assert.IsTrue(double_list.Q__Mean().Q__Approximately_Equal(0.0, epsilon));

            double_list = new List<double>() { 0.0, 0.0, 0.0, 0.0, 10.0 };
            Assert.IsTrue(double_list.Q__Mean().Q__Approximately_Equal(10.0 / 5.0, epsilon));

            double_list = new List<double>() { 0.0, 0.0, double.NaN };
            Assert.IsTrue(double.IsNaN(double_list.Q__Mean()));

            double_list = new List<double>() { 0.0, double.PositiveInfinity };
            Assert.IsTrue(double.IsInfinity(double_list.Q__Mean()));
            Assert.IsTrue(double.IsPositiveInfinity(double_list.Q__Mean()));

            double_list = new List<double>() { 0.0, double.NegativeInfinity };
            Assert.IsTrue(double.IsInfinity(double_list.Q__Mean()));
            Assert.IsTrue(double.IsNegativeInfinity(double_list.Q__Mean()));

            double_list = new List<double>() { double.PositiveInfinity, double.NegativeInfinity};
            Assert.IsTrue(double.IsNaN(double_list.Q__Mean()));

            double[] double_array = new double[] { 0.0, 1.0, 2.0, 3.0 };
            Assert.IsTrue(double_array.Q__Mean().Q__Approximately_Equal(6.0 / 4.0, epsilon));
        }

        [TestMethod]
        public void Test__Variance()
        {
            double epsilon = 0.0000_0000_01;

            // 0.25 * 0.25 = 0.0625 | 0.0625 * 3 = 0.1875
            // 0.75 * 0.75 = 0.5625
            // 0.5625 + 0.1875  = 0.75
            // 0.75 / 4 = 0.1875
            List<double> numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                1.0,
            };
            double variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(0.1875, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                1.0,
                1.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(0.25, epsilon));

            numbers = new List<double>() {
                0.0,
                1.0,
                1.0,
                1.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(0.1875, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                2.0,
                2.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(1.0, epsilon));

            numbers = new List<double>() {
                0.0,
                4.0,
                4.0,
                4.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(3.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                4.0,
                4.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(4.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                4.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(3.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                8.0,
                8.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(16.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.0,
            };
            variance = numbers.Q__Variance(false);
            Assert.IsTrue(variance.Q__Approximately_Equal(0.0, epsilon));



            numbers = new List<double>() {
                12.0,
                0.0,
                0.0,
                0.0,
            };
            variance = numbers.Q__Variance(false);

            numbers = new List<double>() {
                6.0,
                6.0,
                0.0,
                0.0,
            };
            variance = numbers.Q__Variance(false);

            numbers = new List<double>() {
                4.0,
                4.0,
                4.0,
                0.0,
            };
            variance = numbers.Q__Variance(false);
        }

        [TestMethod]
        public void Test__Fractional_Similarity()
        {
            double epsilon = 0.0000_0001;

            double num = 0.0;
            Assert.IsTrue(num.Q__Fractional_Similarity(0.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(1.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(2.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(4.0).Q__Approximately_Equal(0.0, epsilon));

            num = 1.0;
            Assert.IsTrue(num.Q__Fractional_Similarity(0.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(1.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(2.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(4.0).Q__Approximately_Equal(0.25, epsilon));

            num = 2.0;
            Assert.IsTrue(num.Q__Fractional_Similarity(0.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(1.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(2.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(4.0).Q__Approximately_Equal(0.5, epsilon));

            num = 4.0;
            Assert.IsTrue(num.Q__Fractional_Similarity(0.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(1.0).Q__Approximately_Equal(0.25, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(2.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Similarity(4.0).Q__Approximately_Equal(1.0, epsilon));

            num = 0.0;
            Assert.ThrowsException<Exception>(() => { num.Q__Fractional_Similarity(-1.0); });

            num = 1.0;
            Assert.ThrowsException<Exception>(() => { num.Q__Fractional_Similarity(-1.0); });

            num = -1.0;
            Assert.ThrowsException<Exception>(() => { num.Q__Fractional_Similarity(0.0); });
            Assert.ThrowsException<Exception>(() => { num.Q__Fractional_Similarity(1.0); });
            Assert.ThrowsException<Exception>(() => { num.Q__Fractional_Similarity(-1.0); });

        }

        [TestMethod]
        public void Test__Fractional_Error()
        {
            double epsilon = 0.0000_0001;

            double num = 0.0;
            Assert.IsTrue(num.Q__Fractional_Error(0.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(1.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(2.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(4.0).Q__Approximately_Equal(1.0, epsilon));

            num = 1.0;
            Assert.IsTrue(num.Q__Fractional_Error(0.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(1.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(2.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(4.0).Q__Approximately_Equal(0.75, epsilon));

            num = 2.0;
            Assert.IsTrue(num.Q__Fractional_Error(0.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(1.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(2.0).Q__Approximately_Equal(0.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(4.0).Q__Approximately_Equal(0.5, epsilon));

            num = 4.0;
            Assert.IsTrue(num.Q__Fractional_Error(0.0).Q__Approximately_Equal(1.0, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(1.0).Q__Approximately_Equal(0.75, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(2.0).Q__Approximately_Equal(0.5, epsilon));
            Assert.IsTrue(num.Q__Fractional_Error(4.0).Q__Approximately_Equal(0.0, epsilon));

        }

        [TestMethod]
        public void Test__Diversity()
        {
            PRNG_Basic rand = new PRNG_Basic();

            double epsilon = 0.0000_0000_01;

            // 0.25 * 0.25 = 0.0625 | 0.0625 * 3 = 0.1875
            // 0.75 * 0.75 = 0.5625
            // 0.5625 + 0.1875  = 0.75
            // 0.75 / 4 = 0.1875
            List<double> numbers = new List<double>() {
                0.0,
                0.1,
                0.2,
                0.3,
            };
            numbers.M__Shuffle(rand);
            double diversity = numbers.Q__Diversity();
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.1, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.2,
                0.3,
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 0.2 / 4.0 = 0.125
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.05, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.3,
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 0.3 / 4.0 = 0.075
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.075, epsilon));

            numbers = new List<double>() {
                0.0,
                0.1,
                0.2,
                0.3,
                0.4,
                0.5,
                0.6,
                0.7,
                0.8,
                0.9,
                1.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.1, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.2,
                0.3,
                0.4,
                0.5,
                0.6,
                0.7,
                0.8,
                0.9,
                1.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 0.9 / 11 = ... 0.0818181818181818181...
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.9 / 11.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.3,
                0.4,
                0.5,
                0.6,
                0.7,
                0.8,
                0.9,
                1.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 0.8 / 11 = ... 0.072727272727272...
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.8 / 11.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.9,
                1.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 0.2 / 11 = ... 0.0181818181818...
            Assert.IsTrue(diversity.Q__Approximately_Equal(0.2 / 11.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                1.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 1.0 / 11 = 0.0909090909
            Assert.IsTrue(diversity.Q__Approximately_Equal(1.0 / 11.0, epsilon));



            // special test
            numbers = new List<double>() {
                0.0,
                1.0,
                2.0,
                3.0,
                4.0,
                5.0,
                6.0,
                7.0,
                8.0,
                9.0,
                10.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            Assert.IsTrue(diversity.Q__Approximately_Equal(1.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                55.0
            };
            numbers.M__Shuffle(rand);
            diversity = numbers.Q__Diversity();
            // 1+2+3+4+5+6+7+8+9+10=55
            Assert.IsTrue(diversity.Q__Approximately_Equal(55.0 / 11.0, epsilon));
        }

        [TestMethod]
        public void Test__Minimum_Difference() { 
            
        }


        [TestMethod]
        public void Test__Simplistic_Variance()
        {
            double epsilon = 0.0000_0000_01;

            List<double> numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                1.0,
            };
            double variance = numbers.Q__Simplistic_Variance();
            // mean: 0.25
            // sv_sum = abs(0.0 - 0.25) + abs(0.0 - 0.25) + abs(0.0 - 0.25) + abs(1.0 - 0.25)
            // sv_sum = 0.25 + 0.25 + 0.25 + 0.75 = 1.5
            // sv = sv_sum / 4 = 1.5 / 4
            Assert.IsTrue(variance.Q__Approximately_Equal(1.5 / 4.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                1.0,
                1.0,
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(2.0 / 4.0, epsilon));

            numbers = new List<double>() {
                0.0,
                1.0,
                1.0,
                1.0,
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(1.5 / 4.0, epsilon));

            numbers = new List<double>() {
                // mean: 12 / 4 = 3
                12.0,   // distance from mean: 9
                0.0,    // 3
                0.0,    // 3
                0.0,    // 3
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(18.0 / 4.0, epsilon));

            numbers = new List<double>() {
                // mean: 3
                6.0,   // distance from mean: 3
                6.0,    // 3
                0.0,    // 3
                0.0,    // 3
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(12.0 / 4.0, epsilon));

            // testing for a fixed quantity, spread along values
            numbers = new List<double>() {
                0.25,
                0.25,
                0.25,
                0.25,
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(0.0 / 4.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.5,
                0.5,
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(1.0 / 4.0, epsilon));

            numbers = new List<double>() {
                0.0,
                0.0,
                0.0,
                1.0,
            };
            variance = numbers.Q__Simplistic_Variance();
            Assert.IsTrue(variance.Q__Approximately_Equal(1.5 / 4.0, epsilon));
        }

    }
}
