using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Propositional.Parsing.AsText.Connectives;
using Resolution.Propositional.Parsing.AsText.Connectives.ConnectiveSets;

namespace Resolution.Tests.ParsingTests.ConnectiveSetTests
{
    [TestClass]
    public class ConnectiveSetTests
    {
        [TestMethod]
        public void IsAndAConnectiveTest()
        {
            AbstractConnective connective = new And();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.IsTrue(set.IsConnective(connective.Symbol));
        }

        [TestMethod]
        public void IsNotAConnectiveTest()
        {
            AbstractConnective connective = new Not();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.IsTrue(set.IsConnective(connective.Symbol));
        }

        [TestMethod]
        public void IsOrAConnectiveTest()
        {
            AbstractConnective connective = new Or();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.IsTrue(set.IsConnective(connective.Symbol));
        }

        [TestMethod]
        public void IsImplicationAConnectiveTest()
        {
            AbstractConnective connective = new Implication();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.IsTrue(set.IsConnective(connective.Symbol));
        }

        [TestMethod]
        public void IsBiconditionalAConnectiveTest()
        {
            AbstractConnective connective = new Biconditional();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.IsTrue(set.IsConnective(connective.Symbol));
        }

        [TestMethod]
        public void AndPrecedende()
        {
            AbstractConnective connective = new And();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.AreEqual(set.Get(connective.Symbol).Precedence, connective.Precedence);
        }

        [TestMethod]
        public void OrPrecedende()
        {
            AbstractConnective connective = new Or();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.AreEqual(set.Get(connective.Symbol).Precedence, connective.Precedence);
        }
        [TestMethod]
        public void NotPrecedende()
        {
            AbstractConnective connective = new Not();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.AreEqual(set.Get(connective.Symbol).Precedence, connective.Precedence);
        }
        [TestMethod]
        public void ImplicationPrecedende()
        {
            AbstractConnective connective = new Implication();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.AreEqual(set.Get(connective.Symbol).Precedence, connective.Precedence);
        }
        [TestMethod]
        public void BiconditionalPrecedende()
        {
            AbstractConnective connective = new Biconditional();
            IConnectiveSet set = new StandardConnectiveSet();
            Assert.AreEqual(set.Get(connective.Symbol).Precedence, connective.Precedence);
        }
    }
}
