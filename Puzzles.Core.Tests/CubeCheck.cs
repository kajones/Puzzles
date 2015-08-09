using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class CubeCheck
    {
        [Test]
        [TestCase(1, true)]
        [TestCase(8, true)]
        [TestCase(7, false)]
        [TestCase(9, false)]
        [TestCase(27, true)]
        [TestCase(64, true)]
        [TestCase(125, true)]
        public void ConfirmCubes(long candidateCube, bool expectedResult)
        {
            var isCube = CubeHelper.IsCube(candidateCube);
            Assert.AreEqual(expectedResult, isCube);
        }
    }
}
