using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class ConfirmIsPandigital
    {
        [Test]
        [TestCase(1, false)]
        [TestCase(123456789, true)]
        [TestCase(987654321, true)]
        [TestCase(112345678, false)]
        public void UsesAllNineDigits(int candidate, bool expectedResult)
        {
            var isPandigital = PandigitalHelper.IsPandigital(candidate);
            Assert.AreEqual(expectedResult, isPandigital);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(12, true)]
        [TestCase(321, true)]
        [TestCase(2413, true)]
        [TestCase(12345, true)]
        [TestCase(123456, true)]
        [TestCase(1234567, true)]
        [TestCase(12345678, true)]
        [TestCase(123456789, true)]
        [TestCase(2, false)]
        [TestCase(0, false)]
        [TestCase(10, false)]
        [TestCase(124, false)]
        public void ConfirmIsPandigitalToNDigits(int candidate, bool expectedResult)
        {
            var result = PandigitalHelper.IsPandigitalToNDigits(candidate);
            Assert.AreEqual(expectedResult, result);
        }

    }
}
