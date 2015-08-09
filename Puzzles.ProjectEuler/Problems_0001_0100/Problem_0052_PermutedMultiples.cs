using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, 
    /// but in a different order.
    ///
    /// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.
    /// </summary>
    [TestFixture]
    public class Problem_0052_PermutedMultiples
    {
        [Test]
        public void ConfirmExample()
        {
            const long x = 125874;
            const long doubleX = (2 * x);
            var haveSameDigits = SameDigits(x, doubleX);
            Assert.IsTrue(haveSameDigits);
        }

        [Test]
        public void FindSmallestNumberWithPermutedMultiples()
        {
            var tested = false;
            var found = false;
            for (long x = 100000; x <= 166666; ++x)
            {
                tested = true;
                var doubleX = (2 * x);
                if (!SameDigits(x, doubleX)) continue;

                var tripleX = (3 * x);
                if (!SameDigits(x, tripleX)) continue;

                var quadrupleX = (4 * x);
                if (!SameDigits(x, quadrupleX)) continue;

                var quintupleX = (5 * x);
                if (!SameDigits(x, quintupleX)) continue;

                var sextupleX = (6 * x);
                if (!SameDigits(x, sextupleX)) continue;

                found = true;
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", x, doubleX, tripleX, quadrupleX, quintupleX, sextupleX);

                x.Should().Be(142857);
                break;
            }

            Assert.IsTrue(tested, "Tested");
            Assert.IsTrue(found, "Found answer");
        }

        private static bool SameDigits(long x, long y)
        {
            var digitsX = DigitHelper.GetDigits(x).ToList();
            var digitsY = DigitHelper.GetDigits(y).ToList();

            if (digitsX.Count() != digitsY.Count()) return false;

            foreach (var digitX in digitsX)
            {
                if (!digitsY.Contains(digitX)) return false;
            }

            return true;
        }

    }
}
