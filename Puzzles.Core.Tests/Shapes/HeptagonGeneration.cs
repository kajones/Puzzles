using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests.Shapes
{
    [TestFixture]
    public class HeptagonGeneration
    {
        /// <summary>
        /// 1, 7, 18, 34, 55,
        /// </summary>
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 7)]
        [TestCase(3, 18)]
        [TestCase(4, 34)]
        [TestCase(5, 55)]
        public void ConfirmHeptagonGeneration(long n, long expectedHeptagon)
        {
            var heptagon = ShapeHelper.GetHeptagon(n);
            Assert.AreEqual(expectedHeptagon, heptagon);
        }
    }
}
