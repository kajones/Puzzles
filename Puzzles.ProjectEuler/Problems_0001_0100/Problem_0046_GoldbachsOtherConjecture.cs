using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.
    /// (composite = not a prime)
    ///
    /// 9 = 7 + 2×1^2
    /// 15 = 7 + 2×2^2
    /// 21 = 3 + 2×3^2
    /// 25 = 7 + 2×3^2
    /// 27 = 19 + 2×2^2
    /// 33 = 31 + 2×1^2
    ///
    /// It turns out that the conjecture was false.
    ///
    /// What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?
    [TestFixture]
    public class Problem_0046_GoldbachsOtherConjecture
    {
        private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(10000);
        private readonly List<int> squares = SquareHelper.GetSquaresUpTo(10000);

        /// <summary>
        /// 5777
        /// </summary>
        [Test, Explicit]
        public void FindNumber()
        {
            var canFindSum = true;
            var number = 33;

            while (canFindSum)
            {
                number += 2;
                if (primes.Contains(number)) continue;

                canFindSum = FindSum(number);
                if (!canFindSum)
                {
                    Console.WriteLine(number);
                    number.Should().Be(5777);
                    break;
                }
            }
        }

        private bool FindSum(int number)
        {
            foreach (var square in squares.Where(sq => sq < number))
            {
                var remainder = number - (2 * square);
                if (remainder < 0) continue;
                if (primes.Contains(remainder)) return true;
            }

            return false;
        }
    }
}
