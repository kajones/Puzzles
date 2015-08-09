using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0101_0200
{
    /// <summary>
    /// Working from left-to-right if no digit is exceeded by the digit to its left 
    /// it is called an increasing number; for example, 134468.
    ///
    /// Similarly if no digit is exceeded by the digit to its right it is called a decreasing number; for example, 66420.
    ///
    /// We shall call a positive integer that is neither increasing nor decreasing a "bouncy" number; for example, 155349.
    ///
    /// Clearly there cannot be any bouncy numbers below one-hundred, 
    /// but just over half of the numbers below one-thousand (525) are bouncy. 
    /// In fact, the least number for which the proportion of bouncy numbers first reaches 50% is 538.
    ///
    /// Surprisingly, bouncy numbers become more and more common 
    /// and by the time we reach 21780 the proportion of bouncy numbers is equal to 90%.
    ///
    /// Find the least number for which the proportion of bouncy numbers is exactly 99%.
    /// </summary>
    [TestFixture]
    public class Problem_0112_BouncyNumbers
    {
        [Test]
        [TestCase(538, 0.50)]
        [TestCase(1000, 0.525)]
        [TestCase(21780, 0.90)]
        public void ConfirmProportionOfBouncyNumbers(long limit, double expectedProportion)
        {
            var proportion = CalculateProportionOfBouncyNumbers(limit);
            proportion.Should().Be(expectedProportion);
        }

        [Test, Explicit]
        public void FindBallpark()
        {
            //   50000 - 0.94
            //   60000 - 0.947
            //  100000 - 0.95
            //  200000 - 0.968
            //  500000 - 0.984
            //  700000 - 0.987
            //  900000 - 0.9878
            // 1000000 - 0.987
            // 1500000 - 0.989
            // 1600000 - 0.990
            // 1700000 - 0.9906
            long candidate = 1600000;
            var proportion = CalculateProportionOfBouncyNumbers(candidate);
            Console.WriteLine("{0} - {1}", candidate, proportion);
        }

        [Test]
        public void FindLowestNinetyNinePercentWithBouncyNumbers()
        {
            int candidate = 99;
            int countBouncy = 0;
 
            while(100*countBouncy < 99*candidate) {
                candidate++;
                if(BouncyNumberChecker.IsBouncy(candidate))
                    countBouncy++;
            }

            Console.WriteLine("Number: {0}", candidate);
            candidate.Should().Be(1587000);
        }

        private static double CalculateProportionOfBouncyNumbers(long limit)
        {
            double countBouncy = 0;

            for (var i = 1; i <= limit; ++ i)
            {
                var isBouncy = BouncyNumberChecker.IsBouncy(i);
                if (isBouncy) countBouncy++;
            }

            return ((double)countBouncy) / ((double)limit);
        }
    }
}
