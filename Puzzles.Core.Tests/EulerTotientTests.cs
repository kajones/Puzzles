using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class EulerTotientTests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 4)]
        [TestCase(6, 2)]
        [TestCase(7, 6)]
        [TestCase(8, 4)]
        [TestCase(9, 6)]
        [TestCase(10, 4)]
        [TestCase(36, 12)]
        public void ConfirmPhiForKnownValues(long number, int expectedPhi)
        {
            var phi = EulerTotientHelper.CalculatePhi(number);
            Assert.AreEqual(expectedPhi, phi);
        }

    }
}
