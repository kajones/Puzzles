using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests.Shapes
{
    [TestFixture]
    public class OctagonGeneration
    {
        /// <summary>
        /// 1, 8, 21, 40, 65,
        /// </summary>
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 8)]
        [TestCase(3, 21)]
        [TestCase(4, 40)]
        [TestCase(5, 65)]
        public void ConfirmOctagonGeneration(long n, long expectedOctagon)
        {
            var octagon = ShapeHelper.GetOctagon(n);
            Assert.AreEqual(expectedOctagon, octagon);
        }
    }
}
