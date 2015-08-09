using NUnit.Framework;
using FluentAssertions;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// If a box contains twenty-one coloured discs, 
    /// composed of fifteen blue discs and six red discs, 
    /// and two discs were taken at random, 
    /// it can be seen that the probability of taking two blue discs, 
    /// P(BB) = (15/21)×(14/20) = 1/2.
    ///
    /// The next such arrangement, for which there is exactly 50% chance 
    /// of taking two blue discs at random, 
    /// is a box containing eighty-five blue discs and thirty-five red discs.
    ///
    /// By finding the first arrangement to contain over 10^12 = 1,000,000,000,000 discs 
    /// in total, determine the number of blue discs that the box would contain.
    /// </summary>
    [TestFixture]
    public class Problem_0100_ArrangedProbability
    {
        [Test]
        [TestCase(15, 6)]
        [TestCase(85, 35)]
        public void ConfirmBlueDiscProbabilityExamples(long blueCount, long redCount)
        {
            var probability = CalculateProbabilityOfTwoBlue(blueCount, redCount);
            probability.Should().BeApproximately(0.5, 0.001);
        }

        [Test]
        public void Solve()
        {
            Assert.Fail("To implement");
        }

        private static double CalculateProbabilityOfTwoBlue(long blueCount, long redCount)
        {
            double total = blueCount + redCount;
            return (blueCount / total) * ((blueCount - 1) / (total - 1));
        }
    }
}
