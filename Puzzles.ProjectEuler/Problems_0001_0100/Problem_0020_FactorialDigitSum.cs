using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// n! means n × (n − 1) × ... × 3 × 2 × 1
    ///
    /// For example, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800,
    /// and the sum of the digits in the number 10! is 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27.
    ///
    /// Find the sum of the digits in the number 100!
    /// </summary>
    [TestFixture]
    public class Problem_0020_FactorialDigitSum
    {
        /// <summary>
        /// 648
        /// </summary>
        [Test, Explicit]
        public void OneHundredFactorial()
        {
            BigInteger factorial = MathHelper.LargeFactorial(100);

            //Console.WriteLine(factorial);

            var sum = DigitHelper.GetDigitSum(factorial.ToString());

            Console.WriteLine(sum);

            sum.Should().Be(648);
        }

    }
}
