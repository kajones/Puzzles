using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The square root of 2 can be written as an infinite continued fraction.
    ///
    ///  √2 = 1 +	 1
    ///           ------------
    ///            2 +  1
    ///                -------
    ///                2 +	1
    ///                    -------
    ///                    2 +	1
    ///                       ------
    ///                          2 + ...
    /// The infinite continued fraction can be written, √2 = [1;(2)], (2) indicates that 2 repeats ad infinitum. In a similar way, √23 = [4;(1,3,1,8)].
    ///
    /// It turns out that the sequence of partial values of continued fractions for square roots provide the best rational approximations. Let us consider the convergents for √2.
    ///
    /// 1 +	1 = 3/2
    ///    ---
    ///     2
    ///
    /// 1 +	1     = 7/5
    ///    -------
    ///    2 +  1
    ///        ---
    ///         2
    ///
    /// 1 +	1         = 17/12
    ///     ---------
    ///     2 + 1
    ///         ------
    ///         2 +	1
    ///            ----
    ///             2
    ///
    /// 1 +	1           = 41/29
    ///     ------------
    ///      2 + 1
    ///          -------
    ///          2 + 1
    ///              ----
    ///              2 + 1
    ///                  ----
    ///                  2
    ///
    /// Hence the sequence of the first ten convergents for √2 are:
    ///
    /// 1, 3/2, 7/5, 17/12, 41/29, 99/70, 239/169, 577/408, 1393/985, 3363/2378, ...
    /// What is most surprising is that the important mathematical constant,
    /// e = [2; 1,2,1, 1,4,1, 1,6,1 , ... , 1,2k,1, ...].
    ///
    /// The first ten terms in the sequence of convergents for e are:
    ///
    /// 2, 3, 8/3, 11/4, 19/7, 87/32, 106/39, 193/71, 1264/465, 1457/536, ...
    /// The sum of digits in the numerator of the 10th convergent is 1+4+5+7=17.
    ///
    /// Find the sum of digits in the numerator of the 100th convergent of the continued fraction for e.
    /// </summary>
    [TestFixture]
    public class Problem_0065_ConvergentsOfE
    {
        [Test]
        [TestCase(3, 8)]
        [TestCase(4, 11)]
        [TestCase(5, 19)]
        [TestCase(6, 87)]
        [TestCase(7, 106)]
        [TestCase(8, 193)]
        [TestCase(9, 1264)]
        [TestCase(10, 1457)]
        public void ConfirmNumerator(long k, long expectedNumerator)
        {
            var numerator = GetNumerator(k);
            Assert.AreEqual((BigInteger)expectedNumerator, numerator);
        }

        /// <summary>
        /// 6963524437876961749120273824619538346438023188214475670667
        ///Sum: 272
        /// </summary>
        [Test, Explicit]
        public void FindOneHundrethNumerator()
        {
            var numerator = GetNumerator(100);
            Console.WriteLine(numerator);

            var sum = DigitHelper.GetDigitSum(numerator.ToString());
            Console.WriteLine("Sum: {0}", sum);

            sum.Should().Be(272);
        }

        private static BigInteger GetNumerator(long k)
        {
            if (k < 3) throw new ApplicationException("Unknown numerator");

            BigInteger prevNkLess1 = 1;
            BigInteger prevNk = 2;
            BigInteger numerator = 0;

            for (var count = 2; count <= k; ++count)
            {
                var multiplier = (count % 3 == 0) ? (2 * (count / 3)) : 1;

                numerator = (multiplier * prevNk) + prevNkLess1;
                prevNkLess1 = prevNk;
                prevNk = numerator;
            }

            return numerator;

        }
    }
}
