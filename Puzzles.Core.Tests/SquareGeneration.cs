using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class SquareGeneration
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 4)]
        [TestCase(3, 9)]
        [TestCase(4, 16)]
        [TestCase(5, 25)]
        public void ConfirmSquares(long n, long expectedSquare)
        {
            var square = SquareHelper.GetSquare(n);
            Assert.AreEqual(expectedSquare, square);
        }
    }
}
