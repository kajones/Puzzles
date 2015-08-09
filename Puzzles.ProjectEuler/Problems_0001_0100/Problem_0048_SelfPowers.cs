using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The series, 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317.
    ///
    /// Find the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.
    [TestFixture]
    public class Problem_0048_SelfPowers
    {
        /// <summary>
        /// 9110846700 (520819110846700)
        /// </summary>
        [Test, Explicit]
        public void FindDigits()
        {
            const long LeastSignificantBoundary = 1000000000000;

            long result = 0;

            for (long number = 1; number <= 1000; ++number)
            {
                var current = number;
                for (long powerApplied = 1; powerApplied < number; ++powerApplied)
                {
                    current *= number;
                    while (current > LeastSignificantBoundary)
                    {
                        current -= LeastSignificantBoundary;
                    }
                }

                result += current;
            }

            Console.WriteLine(result);
            result.Should().Be(520819110846700);
        }
    }
}
