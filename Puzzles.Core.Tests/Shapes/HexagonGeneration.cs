using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests.Shapes
{
    [TestFixture]
    public class HexagonGeneration
    {
        /// <summary>
        /// 1, 6, 15, 28, 45, 
        /// </summary>
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 6)]
        [TestCase(3, 15)]
        [TestCase(4, 28)]
        [TestCase(5, 45)]
        public void ConfirmHexagonGeneration(long n, long expectedHexagon)
        {
            var hexagon = ShapeHelper.GetHexagon(n);
            Assert.AreEqual(expectedHexagon, hexagon);
        }
    }
}
