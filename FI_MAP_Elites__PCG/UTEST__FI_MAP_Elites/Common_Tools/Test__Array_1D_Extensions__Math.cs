using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Common_Tools;

namespace UTEST.Common_Tools
{
    [TestClass]
    public class Test__Array_1D_Extensions__Math
    {
        [TestMethod]
        public void Test____Q__Sum() {
            double[] array = new double[] {0.0,1.0,2.0};

            Assert.IsTrue(array.Q__Sum() == 3.0);
        }

        [TestMethod]
        public void Test____Q__Product()
        {
            int[] array = new int[] {1, 2, 3, 4, 5};

            Assert.IsTrue(array.Q__Product(0) == 1);
            Assert.IsTrue(array.Q__Product(1) == 2);
            Assert.IsTrue(array.Q__Product(2) == 6);
            Assert.IsTrue(array.Q__Product(3) == 24);
            Assert.IsTrue(array.Q__Product(4) == 120);

            Assert.IsTrue(array.Q__Product() == 120);
        }
    }
}
