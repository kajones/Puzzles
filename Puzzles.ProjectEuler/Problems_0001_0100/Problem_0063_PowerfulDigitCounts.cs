using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The 5-digit number, 16807=7^5, is also a fifth power. 
    /// Similarly, the 9-digit number, 134217728=8^9, is a ninth power.
    ///
    /// How many n-digit positive integers exist which are also an nth power?
    [TestFixture]
    public class Problem_0063_PowerfulDigitCounts
    {
        /// <summary>
        /// Matches for n=3 would be 100 lte x^3 lt 1000 
        ///                i.e. 10^(3-1) lte x^3 lt 10^3
        /// 
        /// x lte 9
        /// 
        /// 10^(n-1) lte x^n
        /// 
        /// Take log10 of each side:
        ///      n-1 lte n log(x)
        /// i.e. n-1 lte log(x)
        ///      ---
        ///       n
        /// 
        /// 10 ^ ((n-1)/n) lte x
        /// </summary>
        [Test, Explicit]
        public void CalculatePowerDigits()
        {
            var result = 0;
            var lower = 0;
            var n = 1;

            while (lower < 10)
            {
                lower = (int) Math.Ceiling(Math.Pow(10, (n - 1.0)/n));
                result += 10 - lower;
                n++;
            }

            Console.WriteLine(result);
            result.Should().Be(49);
        }

        [Test, Explicit]
        public void BruteForce()
        {
            var count = 0;

            for (var power = 1; power <= 25; ++ power)
            {
                for (var x = 1; x <= 10; ++x)
                {
                    var result = (long) Math.Pow(x, power);
                    if (result.ToString(CultureInfo.InvariantCulture).Length != power) continue;

                    count++;
                    Console.WriteLine("{0} ({1}^{2})", result, x, power);
                }
            }

            Console.WriteLine(count);
            count.Should().Be(49);
        }


    }
}
