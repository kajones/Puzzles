using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class CombinationFlagGeneration
    {
        [Test]
        [TestCase(0, new int[0])]
        [TestCase(1, new[] { 0 })]
        [TestCase(2, new[] { 1 })]
        [TestCase(3, new[] { 0, 1 })]
        [TestCase(4, new[] { 2 })]
        [TestCase(5, new[] { 0, 2 })]
        [TestCase(6, new[] { 1, 2 })]
        [TestCase(7, new[] { 0, 1, 2 })]
        [TestCase(8, new[] { 3 })]
        [TestCase(9, new[] { 0, 3 })]
        [TestCase(10, new[] { 1, 3 })]
        [TestCase(11, new[] { 0, 1, 3 })]
        [TestCase(12, new[] { 2, 3 })]
        [TestCase(13, new[] { 0, 2, 3 })]
        [TestCase(14, new[] { 1, 2, 3 })]
        [TestCase(15, new[] { 0, 1, 2, 3 })]
        public void ConfirmFlagGeneration(long value, int[] positionsSet)
        {
            var flags = CombinationHelper.ConstructSetFromBits(4, value).ToList();

            Assert.AreEqual(positionsSet.Length, flags.Count(), "Number of position flags set");
            foreach (var position in positionsSet)
            {
                Assert.IsTrue(flags.Contains(position), "Have position {0}", position);
            }
        }
    }
}
