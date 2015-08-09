using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. 
    /// For example, 2143 is a 4-digit pandigital and is also prime.
    ///
    /// What is the largest n-digit pandigital prime that exists?
    [TestFixture]
    public class Problem_0041_PandigitalPrime
    {
        private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(10000);

        [Test]
        [TestCase(2143, true)]
        [TestCase(12, false)]
        [TestCase(123, false)]
        public void ConfirmPandigitalPrime(int candidatePrime, bool expectedResult)
        {
            var result = IsPandigitalPrime(candidatePrime);
            Assert.AreEqual(expectedResult, result);
        }

        [Test, Explicit]
        public void IsThereANineDigitPandigitalPrime()
        {
            var permutations = PermutationHelper.GetPermutations(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            var max = 0;
            foreach (var permutation in permutations.OrderByDescending(p => p))
            {
                var isPrime = PrimeHelper.IsPrime(permutation);
                if (!isPrime) continue;

                max = permutation;
                break;
            }

            Console.WriteLine(max);
        }

        /// <summary>
        /// 7652413
        /// </summary>
        [Test, Explicit]
        public void IsThereAnNDigitPrime()
        {
            var permutations = PermutationHelper.GetPermutations(new[] { 1, 2, 3, 4, 5, 6, 7 });

            var max = 0;
            foreach (var permutation in permutations.OrderByDescending(p => p))
            {
                var isPrime = PrimeHelper.IsPrime(permutation);
                if (!isPrime) continue;

                max = permutation;
                break;
            }

            Console.WriteLine(max);

            max.Should().Be(7652413);
        }

        private bool IsPandigitalPrime(int candidatePrime)
        {
            if (!primes.Contains(candidatePrime)) return false;

            return PandigitalHelper.IsPandigitalToNDigits(candidatePrime);
        }

    }
}
