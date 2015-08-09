using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
    ///
    /// There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
    ///
    /// How many circular primes are there below one million?
    /// </summary>
    [TestFixture]
    public class Problem_0035_CircularPrimes
    {
        private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(1000000);

        [Test]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(5, true)]
        [TestCase(7, true)]
        [TestCase(11, true)]
        [TestCase(13, true)]
        [TestCase(17, true)]
        [TestCase(31, true)]
        [TestCase(37, true)]
        [TestCase(71, true)]
        [TestCase(73, true)]
        [TestCase(79, true)]
        [TestCase(97, true)]
        [TestCase(197, true)]
        [TestCase(971, true)]
        [TestCase(719, true)]
        [TestCase(12, false)]
        [TestCase(19, false)]
        public void ConfirmCircularPrimes(int candidatePrime, bool isCircular)
        {
            var result = IsCircularPrime(candidatePrime);
            Assert.AreEqual(isCircular, result, candidatePrime.ToString(CultureInfo.InvariantCulture));
        }

        [Test, Explicit]
        public void FindAllCircularPrimesBelowAMillion()
        {
            var list = new List<int>();
            foreach (var number in primes)
            {
                var result = IsCircularPrime(number);
                if (result)
                    list.Add(number);
            }

            Console.WriteLine("Number primes: {0}", list.Count);
            foreach (var prime in list)
            {
                Console.WriteLine("  {0}", prime);
            }

            list.Count.Should().Be(55);
        }

        private bool IsCircularPrime(int candidatePrime)
        {
            if (!primes.Contains(candidatePrime)) return false;

            var permutations = PermutationHelper.GetRotationPermutations(candidatePrime);

            return permutations.All(permutation => primes.Contains(permutation));
        }

    }
}
