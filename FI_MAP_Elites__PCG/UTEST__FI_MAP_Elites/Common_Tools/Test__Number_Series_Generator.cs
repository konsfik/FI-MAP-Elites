using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Number_Series_Generator
    {
        [TestMethod]
        public void Test__Multiplied_Series__Predefined_Steps()
        {
            var numbers =
                Number_Series_Generator
                .Multiplied_Series__Predefined_Steps(
                    initial_value: 1,
                    multiplier: 2,
                    num_values: 8
                    );

            Assert.IsTrue(numbers.Count == 8);

            Assert.IsTrue(numbers.Contains(1));
            Assert.IsTrue(numbers.Contains(2));
            Assert.IsTrue(numbers.Contains(4));
            Assert.IsTrue(numbers.Contains(8));
            Assert.IsTrue(numbers.Contains(16));
            Assert.IsTrue(numbers.Contains(32));
            Assert.IsTrue(numbers.Contains(64));
            Assert.IsTrue(numbers.Contains(128));

            numbers =
                Number_Series_Generator
                .Multiplied_Series__Predefined_Steps(
                    initial_value: 1,
                    multiplier: 3,
                    num_values: 8
                    );

            Assert.IsTrue(numbers.Count == 8);

            Assert.IsTrue(numbers.Contains(1));
            Assert.IsTrue(numbers.Contains(3));
            Assert.IsTrue(numbers.Contains(9));
            Assert.IsTrue(numbers.Contains(27));
            Assert.IsTrue(numbers.Contains(81));
            Assert.IsTrue(numbers.Contains(243));
            Assert.IsTrue(numbers.Contains(729));
            Assert.IsTrue(numbers.Contains(2187));

            numbers =
                Number_Series_Generator
                .Multiplied_Series__Predefined_Steps(
                    initial_value: 3,
                    multiplier: 2,
                    num_values: 9
                    );

            Assert.IsTrue(numbers.Count == 9);

            Assert.IsTrue(numbers.Contains(3));
            Assert.IsTrue(numbers.Contains(6));
            Assert.IsTrue(numbers.Contains(12));
            Assert.IsTrue(numbers.Contains(24));
            Assert.IsTrue(numbers.Contains(48));
            Assert.IsTrue(numbers.Contains(96));
            Assert.IsTrue(numbers.Contains(192));
            Assert.IsTrue(numbers.Contains(384));
            Assert.IsTrue(numbers.Contains(768));

            numbers =
                Number_Series_Generator
                .Multiplied_Series__Predefined_Steps(
                    initial_value: 5,
                    multiplier: 2,
                    num_values: 8
                    );

            Assert.IsTrue(numbers.Count == 8);

            Assert.IsTrue(numbers.Contains(5));
            Assert.IsTrue(numbers.Contains(10));
            Assert.IsTrue(numbers.Contains(20));
            Assert.IsTrue(numbers.Contains(40));
            Assert.IsTrue(numbers.Contains(80));
            Assert.IsTrue(numbers.Contains(160));
            Assert.IsTrue(numbers.Contains(320));
            Assert.IsTrue(numbers.Contains(640));

            numbers =
                Number_Series_Generator
                .Multiplied_Series__Predefined_Steps(
                    initial_value: 7,
                    multiplier: 2,
                    num_values: 8
                    );

            Assert.IsTrue(numbers.Count == 8);

            Assert.IsTrue(numbers.Contains(7));
            Assert.IsTrue(numbers.Contains(14));
            Assert.IsTrue(numbers.Contains(28));
            Assert.IsTrue(numbers.Contains(56));
            Assert.IsTrue(numbers.Contains(112));
            Assert.IsTrue(numbers.Contains(224));
            Assert.IsTrue(numbers.Contains(448));
            Assert.IsTrue(numbers.Contains(896));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Predefined_Steps(0, 2, 2)
                );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Predefined_Steps(2, 0, 2)
                );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Predefined_Steps(2, 2, 0)
                );
        }

        [TestMethod]
        public void Test__Multiplied_Series__Max_Value()
        {
            var numbers =
                Number_Series_Generator
                .Multiplied_Series__Max_Value(
                    initial_value: 1,
                    multiplier: 2,
                    max_value: 1000
                    );
            Assert.IsTrue(numbers.Count == 10);

            Assert.IsTrue(numbers.Contains(1));
            Assert.IsTrue(numbers.Contains(2));
            Assert.IsTrue(numbers.Contains(4));
            Assert.IsTrue(numbers.Contains(8));
            Assert.IsTrue(numbers.Contains(16));
            Assert.IsTrue(numbers.Contains(32));
            Assert.IsTrue(numbers.Contains(64));
            Assert.IsTrue(numbers.Contains(128));
            Assert.IsTrue(numbers.Contains(256));
            Assert.IsTrue(numbers.Contains(512));

            numbers =
                Number_Series_Generator
                .Multiplied_Series__Max_Value(
                    initial_value: 1,
                    multiplier: 2,
                    max_value: 1024
                    );
            Assert.IsTrue(numbers.Count == 11);

            Assert.IsTrue(numbers.Contains(1));
            Assert.IsTrue(numbers.Contains(2));
            Assert.IsTrue(numbers.Contains(4));
            Assert.IsTrue(numbers.Contains(8));
            Assert.IsTrue(numbers.Contains(16));
            Assert.IsTrue(numbers.Contains(32));
            Assert.IsTrue(numbers.Contains(64));
            Assert.IsTrue(numbers.Contains(128));
            Assert.IsTrue(numbers.Contains(256));
            Assert.IsTrue(numbers.Contains(512));
            Assert.IsTrue(numbers.Contains(1024));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Max_Value(0, 2, 2)
                );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Max_Value(2, 0, 2)
                );

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Number_Series_Generator.Multiplied_Series__Max_Value(2, 2, 0)
                );
        }
    }
}
