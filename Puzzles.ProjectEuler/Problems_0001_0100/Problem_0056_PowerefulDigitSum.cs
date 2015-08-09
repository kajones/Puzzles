using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// A googol (10^100) is a massive number: one followed by one-hundred zeros; 100^100 is almost unimaginably large: 
    /// one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.
    ///
    /// Considering natural numbers of the form, a^b, where a, b less than 100, what is the maximum digital sum?
    [TestFixture]
    public class Problem_0056_PowerefulDigitSum
    {
        /// <summary>
        /// 972 from 99 to the power 95
        /// </summary>
        [Test, Explicit]
        public void FindMaximum()
        {
            BigInteger maxSum = 0;
            int maxA = 0;
            int maxB = 0;

            for (var a = 2; a < 100; ++a)
            {
                for (var b = 1; b < 100; ++b)
                {
                    BigInteger product = BigInteger.Pow(a, b);
                    var sum = DigitHelper.GetDigitSum(product.ToString());
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxA = a;
                        maxB = b;
                    }
                }
            }

            Console.WriteLine("{0} from {1} to the power {2}", maxSum, maxA, maxB);

            maxSum.Should().Be(972);
        }

    }
}
