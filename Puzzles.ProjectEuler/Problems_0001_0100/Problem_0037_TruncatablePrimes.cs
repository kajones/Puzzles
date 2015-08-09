using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    [TestFixture]
    public class Problem_0037_TruncatablePrimes
    {
        private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(1000000);

        [Test]
        [TestCase(3797, true)]
        [TestCase(37, true)]
        [TestCase(2, false)]
        [TestCase(5, false)]
        [TestCase(7, false)]
        [TestCase(23, true)]
        [TestCase(63, false)]
        public void ConfirmTruncatablePrime(int candidate, bool expectedResult)
        {
            var result = IsTruncatablePrime(candidate);
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Sum: 748317
        //23
        //37
        //53
        //73
        //313
        //317
        //373
        //797
        //3137
        //3797
        //739397
        /// </summary>
        [Test, Explicit]
        public void FindAllTruncatablePrimes()
        {
            var list = new List<int>();

            foreach (var prime in primes)
            {
                if (prime < 8) continue;
                var result = IsTruncatablePrime(prime);
                if (result)
                    list.Add(prime);

                if (list.Count == 11) break;
            }

            var sumOfList = list.Sum();
            Console.WriteLine("Sum: {0}", sumOfList);

            foreach (var tp in list)
            {
                Console.WriteLine("  {0}", tp);
            }

            sumOfList.Should().Be(748317);
        }

        private bool IsTruncatablePrime(int candidate)
        {
            if (!primes.Contains(candidate)) return false;
            if (candidate < 8) return false;

            // Right to left truncation
            var number = candidate / 10;
            while (number > 0)
            {
                if (!primes.Contains(number)) return false;
                number /= 10;
            }

            // Left to right truncation
            var candidateText = candidate.ToString(CultureInfo.InvariantCulture);
            for (var i = 1; i < candidateText.Length; ++i)
            {
                var text = candidateText.Substring(i);
                var partialNumber = Convert.ToInt32(text);
                if (!primes.Contains(partialNumber)) return false;
            }

            return true;
        }

    }
}
