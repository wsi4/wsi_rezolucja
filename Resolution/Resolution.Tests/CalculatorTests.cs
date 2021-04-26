using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution;

namespace ResolutionTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void AddTest()
        {
            int a = 10, b = 20;
            Assert.AreEqual(Calculator.Add(a, b), a + b);
        }
    }
}
