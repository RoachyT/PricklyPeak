using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_terminal;

namespace CalculatePriceTest
{
    [TestClass]
    public class CalculatePriceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            CalculatePrice calculateTest = new CalculatePrice();

            decimal result = calculateTest.GetTax(50.00m);

            Assert.AreEqual(3.00m, result);
        }
    }
}
