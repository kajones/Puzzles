using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests.Shapes
{
    [TestFixture]
    public class PentagonGeneration
    {
        /// <summary>
        /// 1, 5, 12, 22, 35
        /// </summary>
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 5)]
        [TestCase(3, 12)]
        [TestCase(4, 22)]
        [TestCase(5, 35)]
        public void ConfirmPentagons(long n, long expectedPentagon)
        {
            var pentagon = ShapeHelper.GetPentagon(n);
            Assert.AreEqual(expectedPentagon, pentagon);
        }
    }
}
