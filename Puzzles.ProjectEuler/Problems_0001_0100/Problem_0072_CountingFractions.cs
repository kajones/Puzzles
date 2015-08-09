using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Consider the fraction, n/d, where n and d are positive integers. If n less than d and HCF(n,d)=1, it is called a reduced proper fraction.
    ///
    /// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:
    ///
    /// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
    ///
    /// It can be seen that there are 21 elements in this set.
    ///
    /// How many elements would be contained in the set of reduced proper fractions for d ≤ 1,000,000?
    /// </summary>
    [TestFixture]
    public class Problem_0072_CountingFractions
    {
        [Test]
        public void ConfirmExample()
        {
            long count = 0;
            for (var denominator = 2; denominator <= 8; ++denominator)
            {
                // Phi is the number of coprimes less than a number so this is the number of reduced proper fractions for a given number
                var phi = EulerTotientHelper.CalculatePhi(denominator);
                count += phi;

                Console.WriteLine("{0}:{1}", denominator, phi);
            }

            Assert.AreEqual(21, count);
        }

        /// <summary>
        /// 303963552391
        /// </summary>
        [Test, Explicit]
        public void FindReducedProperFractionsBelowAMillion()
        {
            long count = 0;
            for (var denominator = 2; denominator <= 1000000; ++denominator)
            {
                var phi = EulerTotientHelper.CalculatePhi(denominator);
                count += phi;
            }

            Console.WriteLine("Number: {0}", count);
            count.Should().Be(303963552391);
        }
    }
}
