using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// There are exactly ten ways of selecting three from five, 12345:
    ///
    /// 123, 124, 125, 134, 135, 145, 234, 235, 245, and 345
    ///
    /// In combinatorics, we use the notation, 5C3 = 10.
    ///
    /// In general,
    ///
    /// nCr = n!
    ///       --------
    ///       r!(n−r)!
    /// ,where r ≤ n, n! = n×(n−1)×...×3×2×1, and 0! = 1.
    /// It is not until n = 23, that a value exceeds one-million: 23C10 = 1144066.
    ///
    /// How many, not necessarily distinct, values of  nCr, for 1 ≤ n ≤ 100, are greater than one-million?
    /// </summary>
    [TestFixture]
    public class Problem_0053_CombinatoricSelections
    {
        [Test]
        public void ConfirmFirstExample()
        {
            var fact5 = MathHelper.LargeFactorial(5);
            var fact3 = MathHelper.LargeFactorial(3);
            var fact2 = MathHelper.LargeFactorial(2);

            var result = fact5 / (fact3 * fact2);

            Assert.AreEqual((BigInteger)10, result);
        }

        [Test]
        public void ConfirmSecondExample()
        {
            var fact23 = MathHelper.LargeFactorial(23);
            var fact10 = MathHelper.LargeFactorial(10);
            var fact13 = MathHelper.LargeFactorial(13);

            BigInteger result = fact23 / (fact10 * fact13);

            Assert.AreEqual((BigInteger)1144066, result);
        }

        /// <summary>
        /// 4075
        /// </summary>
        [Test, Explicit]
        public void FindNumberOfCombinationsGreaterThanAMillionForAHundredOptions()
        {
            var factorials = new Dictionary<int, BigInteger>();
            for (var i = 0; i <= 100; ++i)
            {
                var fact = MathHelper.LargeFactorial(i);
                factorials.Add(i, fact);
            }

            BigInteger AMillion = 1000000;

            var count = 0;

            for (int n = 1; n <= 100; ++n)
            {
                for (int r = 1; r < n; ++r)
                {
                    var result = (factorials[n] / (factorials[r] * factorials[n - r]));
                    if (result > AMillion)
                        count++;
                }
            }

            Console.WriteLine("Number over a million {0}", count);

            count.Should().Be(4075);
        }

        
    }
}
