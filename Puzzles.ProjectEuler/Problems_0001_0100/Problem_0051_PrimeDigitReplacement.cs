using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// By replacing the 1st digit of the 2-digit number *3, it turns out that six of the nine possible values: 
    /// 13, 23, 43, 53, 73, and 83, are all prime.
    ///
    /// By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example 
    /// having seven primes among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. 
    /// Consequently 56003, being the first member of this family, is the smallest prime with this property.
    ///
    /// Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, 
    /// is part of an eight prime value family.
    [TestFixture]
    public class Problem_0051_PrimeDigitReplacement
    {
        //private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(1000000);
 
        private Dictionary<int, List<List<int>>> combinations;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            // Don't bother swapping the last digit - it wouldn't produce enough primes
            combinations = new Dictionary<int, List<List<int>>>();
            combinations.Add(2, CombinationHelper.GetCombinations(0));
            combinations.Add(3, CombinationHelper.GetCombinations(10));
            combinations.Add(4, CombinationHelper.GetCombinations(210));
            combinations.Add(5, CombinationHelper.GetCombinations(3210));
            combinations.Add(6, CombinationHelper.GetCombinations(43210));
            combinations.Add(7, CombinationHelper.GetCombinations(543210));
            combinations.Add(8, CombinationHelper.GetCombinations(6543210));
            combinations.Add(9, CombinationHelper.GetCombinations(76543210));
            //combinations.Add(10, CombinationHelper.GetCombinations(876543210));
            //combinations.Add(11, CombinationHelper.GetCombinations(9876543210));
        }

        [Test, Explicit]
        [TestCase(6, 13)]
        [TestCase(7, 56003)]
        public void FindSmallestPrimeWithNSubstitutions(int numberInFamily, int expectedPrime)
        {
            var smallestPrime = FindPrimeForFamily(numberInFamily);
            Assert.AreEqual(expectedPrime, smallestPrime);
        }

        /// <summary>
        /// 121313 (0,2,4)
        /// </summary>
        [Test, Explicit]
        public void FindSmallestPrimeWithEightSubstitutions()
        {
            var smallestPrime = FindPrimeForFamily(8);
            Console.WriteLine(smallestPrime);

            smallestPrime.Should().Be(121313);
        }

        [Test, Explicit]
        public void CheckProposedAnswer()
        {
            var familyPrimes = GetFamilyPrimes(121313, 8);
            foreach (var prime in familyPrimes)
            {
                Console.WriteLine("{0}{1}", prime, Environment.NewLine);
            }

            familyPrimes.Count.Should().Be(8);
            familyPrimes.Should().Contain(121313);
        }

        private int FindPrimeForFamily(int numberInFamily)
        {
            var candidatePrime = 11;
            while (true)
            {
                if (PrimeHelper.IsPrime(candidatePrime))
                {
                    var familyPrimes = GetFamilyPrimes(candidatePrime, numberInFamily);
                    if (familyPrimes != null && familyPrimes.Count == numberInFamily)
                    {
                        foreach (var familyPrime in familyPrimes)
                        {
                            Console.WriteLine("{0} ", familyPrime);
                        }
                        return familyPrimes.Min(p => p);
                    }
                }

                candidatePrime += 2;
            }
        }

        private List<int> GetFamilyPrimes(int candidatePrime, int numberInFamily)
        {
            var digits = DigitHelper.GetDigits(candidatePrime).ToArray();
            if (candidatePrime == 121313 || candidatePrime == 120383)
            {
                Console.WriteLine("test");
            }
            var candidateLength = digits.Count();
            foreach (var combinationSet in combinations[candidateLength])
            {
                var list = new List<int>();
                var toSubstitute = (int[])digits.Clone();

                for (var substituteDigit = 0; substituteDigit < 10; ++substituteDigit)
                {
                    // Skip zero substitution in first position - changes number length
                    if (substituteDigit == 0 && combinationSet.Contains(0))
                        continue;

                    foreach (var position in combinationSet)
                    {
                        toSubstitute[position] = substituteDigit;
                    }

                    var revisedNumber = 0;
                    for (var multiplier = 0; multiplier < candidateLength; ++multiplier)
                    {
                        revisedNumber *= 10;
                        revisedNumber += toSubstitute[multiplier];
                    }
                    if (PrimeHelper.IsPrime(revisedNumber))
                    {
                        list.Add(revisedNumber);
                    }
                }

                if (list.Count == numberInFamily)
                {
                    return list;
                }
            }

            return null;
        }

    }
}
