using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It is well known that if the square root of a natural number is not an integer, then it is irrational. 
    /// The decimal expansion of such square roots is infinite without any repeating pattern at all.
    ///
    /// The square root of two is 1.41421356237309504880..., and the digital sum of the first one hundred decimal digits is 475.
    ///
    /// For the first one hundred natural numbers, find the total of the digital sums of the first one hundred decimal digits 
    /// for all the irrational square roots.
    /// </summary>
    [TestFixture]
    public class Problem_0080_SquareRootDigitalExpansion
    {
        [Test]
        public void ConfirmExample()
        {
            var sqrtText = FindSquareRootToOneHundredDigits(2);
            Console.WriteLine(sqrtText);
            var hundredTotal = FindSumFirstHundredDecimalDigits(sqrtText);
            Assert.AreEqual(475, hundredTotal);
        }

        /// <summary>
        /// 40886
        /// </summary>
        [Test, Explicit]
        public void FindSumForFirstHundredNumbers()
        {
            long total = 0;

            for (int i = 1; i <= 100; ++i)
            {
                if (SquareHelper.IsSquare(i)) continue;

                var sqrtText = FindSquareRootToOneHundredDigits(i);
                var hundredTotal = FindSumFirstHundredDecimalDigits(sqrtText);
                total += hundredTotal;
            }

            Console.WriteLine("Total: {0}", total);

            total.Should().Be(40886);
        }

        private long FindSumFirstHundredDecimalDigits(string numberAsText)
        {
            if (numberAsText.Length < 100) return 0;

            var left100 = numberAsText.Substring(0, 100);
            var total = 0;
            for (var idx = 0; idx < left100.Length; ++idx)
            {
                total += Convert.ToInt32(left100.Substring(idx, 1));
            }
            return total;
        }

        /// Write the original number in decimal form. The numbers are written similar to the long division algorithm, and, as in long division, 
        /// the root will be written on the line above. Now separate the digits into pairs, starting from the decimal point 
        /// and going both left and right. The decimal point of the root will be above the decimal point of the square. 
        /// One digit of the root will appear above each pair of digits of the square.
        ///
        /// Beginning with the left-most pair of digits, do the following procedure for each pair:
        ///
        /// Starting on the left, bring down the most significant (leftmost) pair of digits not yet used (if all the digits have been used, 
        /// write "00") and write them to the right of the remainder from the previous step (on the first step, there will be no remainder). 
        /// In other words, multiply the remainder by 100 and add the two digits. This will be the current value c.
        /// Find p, y and x, as follows:
        /// Let p be the part of the root found so far, ignoring any decimal point. (For the first step, p = 0).
        /// Determine the greatest digit x such that x(20p + x) \le c. We will use a new variable y = x(20p + x).
        /// Note: 20p + x is simply twice p, with the digit x appended to the right).
        /// Note: You can find x by guessing what c/(20·p) is and doing a trial calculation of y, then adjusting x upward or downward as necessary.
        /// Place the digit x as the next digit of the root, i.e., above the two digits of the square you just brought down. Thus the next p will be the old p times 10 plus x.
        /// Subtract y from c to form a new remainder.
        /// If the remainder is zero and there are no more digits to bring down, then the algorithm has terminated. Otherwise go back to step 1 for another iteration.
        public string FindSquareRootToOneHundredDigits(int number)
        {
            if (SquareHelper.IsSquare(number)) return Math.Sqrt(number).ToString(CultureInfo.InvariantCulture);

            var numberAsText = string.Format("{0}.{1}", number.ToString("0000"), "".PadRight(220, '0'));

            BigInteger p = 0;
            BigInteger remainder = 0;
            int idx = 0;
            var pastLeadingZeros = false;
            var resultBuilder = new StringBuilder();
            BigInteger x;
            BigInteger y;

            while (idx < numberAsText.Length - 2)
            {
                if (numberAsText.Substring(idx, 1) == ".")
                {
                    idx++;
                }

                var currentPair = Convert.ToInt64(numberAsText.Substring(idx, 2));
                var c = (remainder * 100) + currentPair;
                FindXandY(c, p, out x, out y);

                if (x != 0)
                    pastLeadingZeros = true;

                if (pastLeadingZeros)
                    resultBuilder.AppendFormat("{0}", x);
                if (resultBuilder.Length >= 100)
                    break;

                p = (10 * p) + x;
                remainder = c - y;
                idx += 2;
            }

            return resultBuilder.ToString();
        }

        private void FindXandY(BigInteger c, BigInteger p, out BigInteger x, out BigInteger y)
        {
            // Want to find the largest x such that x(20p + x) less than or equal to c
            var limit = Math.Exp(BigInteger.Log(c) / 2);

            var twentyP = (20 * p);
            var maxX = 0;

            for (var candidate = 1; candidate <= limit; ++candidate)
            {
                var result = candidate * (twentyP + candidate);
                if (result > c) break;

                maxX = candidate;
            }

            x = maxX;
            y = maxX * (twentyP + maxX);
        }
    }
}
