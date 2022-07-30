using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Random_Library
    {
        /// <summary>
        /// This test makes sure that the Basic_Random class behaves exactly the same as the default Random class
        /// </summary>
        [TestMethod]
        public void Test_General_Behavior()
        {
            List<int> random_seeds = new List<int>();
            for (int i = 0; i < 1000; i++)
                random_seeds.Add(i);

            int num_repetitions = 1000;

            foreach (int seed in random_seeds)
            {
                Random default_prng = new Random(seed);
                PRNG_Basic basic_prng = new PRNG_Basic(seed);
                for (int rep = 0; rep < num_repetitions; rep++)
                {
                    Assert.IsTrue(
                        default_prng.Next() == basic_prng.Next()
                        );
                }
            }
        }

        /// <summary>
        /// This test makes sure that cloning the Basic_Random class works properly
        /// </summary>
        [TestMethod]
        public void Test_Cloning()
        {
            List<int> random_seeds = new List<int>();
            for (int i = 0; i < 1000; i++)
                random_seeds.Add(i);

            int num_repetitions = 1000;

            foreach (int seed in random_seeds)
            {
                PRNG_Basic basic_prng_1 = new PRNG_Basic(seed);
                PRNG_Basic basic_prng_2 = (PRNG_Basic)basic_prng_1.Clone();
                for (int rep = 0; rep < num_repetitions; rep++)
                {
                    PRNG_Basic basic_prng_3 = (PRNG_Basic)basic_prng_1.Clone();

                    int v1 = basic_prng_1.Next();
                    int v2 = basic_prng_2.Next();
                    int v3 = basic_prng_3.Next();

                    Assert.IsTrue(v1 == v2);
                    Assert.IsTrue(v2 == v3);
                }
            }
        }
    }
}
