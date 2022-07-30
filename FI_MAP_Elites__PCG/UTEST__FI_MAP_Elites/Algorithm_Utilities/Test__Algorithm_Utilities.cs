using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace UTEST__FI_MAP_Elites.Algorithms.Shared_Elements
{
    [TestClass]
    public class Test__Algorithm_Utilities
    {
        [TestMethod]
        public void Test__Replacement_Utilities__Should_Be_Replaced()
        {
            EA__Fitness_Goal goal =
                EA__Fitness_Goal.MAXIMIZATION;
            EA__Individual_Replacement_Rule rule =
                EA__Individual_Replacement_Rule.REPLACE_IF_BETTER;

            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 1.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, -1.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NaN, goal, rule));


            goal = EA__Fitness_Goal.MAXIMIZATION;
            rule = EA__Individual_Replacement_Rule.REPLACE_IF_BETTER_OR_EQUAL;

            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 1.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, -1.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NaN, goal, rule));


            goal = EA__Fitness_Goal.MINIMIZATION;
            rule = EA__Individual_Replacement_Rule.REPLACE_IF_BETTER;

            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 1.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, -1.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.PositiveInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NaN, goal, rule));

            goal = EA__Fitness_Goal.MINIMIZATION;
            rule = EA__Individual_Replacement_Rule.REPLACE_IF_BETTER_OR_EQUAL;

            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 1.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, -1.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, 0.0, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NegativeInfinity, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, 0.0, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NegativeInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.PositiveInfinity, goal, rule));
            Assert.IsTrue(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NaN, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    0.0, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.NegativeInfinity, double.NaN, goal, rule));
            Assert.IsFalse(
                Replacement_Utilities.Should_Be_Replaced(
                    double.PositiveInfinity, double.NaN, goal, rule));

        }

    }
}
