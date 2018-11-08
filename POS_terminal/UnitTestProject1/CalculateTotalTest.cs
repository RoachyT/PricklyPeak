using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_terminal;

namespace UnitTestProject1
{
    [TestClass]
    public class CalculateTotalTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            CalculateTotal calculateTest = new CalculateTotal();

            decimal result = calculateTest.GetFinalTotal(50.00m);

                Assert.AreEqual(53.00m, result);
        }
    }
}
