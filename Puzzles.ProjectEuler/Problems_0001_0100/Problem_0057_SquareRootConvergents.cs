using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It is possible to show that the square root of two can be expressed as an infinite continued fraction.
    ///
    /// √ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...
    ///
    /// By expanding this for the first four iterations, we get:
    ///
    /// 1 + 1/2 = 3/2 = 1.5
    /// 1 + 1/(2 + 1/2) = 7/5 = 1.4
    /// 1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
    /// 1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...
    ///
    /// The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 
    /// 1393/985, is the first example where the number of digits in the numerator exceeds the number of digits in the denominator.
    ///
    /// In the first one-thousand expansions, how many fractions contain a numerator with more digits than denominator?
    /// </summary>
    [TestFixture]
    public class Problem_0057_SquareRootConvergents
    {
        [Test]
        [TestCase(1, 3, 2)]
        [TestCase(2, 7, 5)]
        [TestCase(3, 17, 12)]
        [TestCase(4, 41, 29)]
        [TestCase(5, 99, 70)]
        [TestCase(6, 239, 169)]
        [TestCase(7, 577, 408)]
        [TestCase(8, 1393, 985)]
        public void ConfirmKnownFractions(int term, long expectedNumerator, long expectedDenominator)
        {
            var fraction = GetNthTerm(term);
            Assert.AreEqual(new BigInteger(expectedNumerator), fraction.Numerator, "Numerator");
            Assert.AreEqual(new BigInteger(expectedDenominator), fraction.Denominator, "Denominator");
        }

        /// <summary>
        /// 153
        /// </summary>
        [Test, Explicit]
        public void FindAllFractionsToThousandthTerm()
        {
            var count = 0;

            for (var idx = 1; idx <= 1000; ++idx)
            {
                var fraction = GetNthTerm(idx);
                if (fraction.Numerator < 0) Assert.Fail("Error in numerator {0} {1}", idx, fraction.Numerator);
                if (fraction.Denominator < 0) Assert.Fail("Error in denominator {0} {1}", idx, fraction.Denominator);

                var numDigits = DigitHelper.GetNumberLength(fraction.Numerator);
                var denomDigits = DigitHelper.GetNumberLength(fraction.Denominator);
                if (numDigits > denomDigits)
                    count++;
            }

            Console.WriteLine("Numerator had more digits {0} times", count);

            count.Should().Be(153);
        }

        private static Fraction GetNthTerm(int term)
        {
            BigInteger numerator = 3;
            BigInteger denominator = 2;
            BigInteger previousDenominator = 1;
            if (term == 1)
                return new Fraction(numerator, denominator);

            var count = 1;
            while (count < term)
            {
                numerator += (2 * denominator);
                var temp = denominator;
                denominator = previousDenominator + (2 * denominator);
                previousDenominator = temp;

                count++;
            }

            return new Fraction(numerator, denominator);
        }
    }

    public struct Fraction
    {
        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }

        public Fraction(BigInteger numerator, BigInteger denominator)
            : this()
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }
}
