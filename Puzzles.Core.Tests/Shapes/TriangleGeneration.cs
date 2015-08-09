using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests.Shapes
{
    [TestFixture]
    public class TriangleGeneration
    {
        /// <summary>
        /// 1, 3, 6, 10, 15
        /// </summary>
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(3, 6)]
        [TestCase(4, 10)]
        [TestCase(5, 15)]
        public void ConfirmTriangleNumbers(long n, long expectedTriangle)
        {
            var triangle = ShapeHelper.GetTriangle(n);
            Assert.AreEqual(expectedTriangle, triangle);
        }
    }
}
