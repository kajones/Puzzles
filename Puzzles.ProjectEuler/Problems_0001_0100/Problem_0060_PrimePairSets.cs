using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The primes 3, 7, 109, and 673, are quite remarkable. 
    /// By taking any two primes and concatenating them in any order the result will always be prime. 
    /// For example, taking 7 and 109, both 7109 and 1097 are prime. 
    /// 
    /// The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
    ///
    /// Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
    /// </summary>
    [TestFixture]
    public class Problem_0060_PrimePairSets
    {
        [Test]
        public void ConfirmAllPrimePairCombinations()
        {
            var candidates = new List<string> { "3", "7", "109", "673" };
            var pairCombinations = CombinationHelper.GetCombinations(candidates, 2);

            foreach (var pairCombination in pairCombinations)
            {
                var firstThenSecond = Convert.ToInt64(pairCombination[0] + pairCombination[1]);
                var secondThenFirst = Convert.ToInt64(pairCombination[1] + pairCombination[0]);

                Assert.IsTrue(PrimeHelper.IsPrime(firstThenSecond), "Expect prime {0}", firstThenSecond);
                Assert.IsTrue(PrimeHelper.IsPrime(secondThenFirst), "Expect prime {0}", secondThenFirst);

            }
        }

        /// <summary>
        /// Sum: 26033 from 13 5197 5701 6733 8389
        /// </summary>
        [Test, Explicit]
        public void FindSetOfFive()
        {
            var minSum = Math.Pow(2, 10000);

            var primes = PrimeHelper.GetPrimesUpTo(10000);

            for (var aIdx = 0; aIdx < primes.Count; ++aIdx)
            {
                var a = primes[aIdx];
                for (var bIdx = (aIdx + 1); bIdx < primes.Count; ++bIdx)
                {
                    var b = primes[bIdx];
                    if (!BothConcatenationsPrime(a, b)) continue;
                    for (var cIdx = (bIdx + 1); cIdx < primes.Count; ++cIdx)
                    {
                        var c = primes[cIdx];
                        if (!BothConcatenationsPrime(c, a)) continue;
                        if (!BothConcatenationsPrime(c, b)) continue;
                        for (var dIdx = (cIdx + 1); dIdx < primes.Count; ++dIdx)
                        {
                            var d = primes[dIdx];
                            if (!BothConcatenationsPrime(d, a)) continue;
                            if (!BothConcatenationsPrime(d, b)) continue;
                            if (!BothConcatenationsPrime(d, c)) continue;
                            for (var eIdx = (dIdx + 1); eIdx < primes.Count; ++eIdx)
                            {
                                var e = primes[eIdx];
                                if (!BothConcatenationsPrime(e, a)) continue;
                                if (!BothConcatenationsPrime(e, b)) continue;
                                if (!BothConcatenationsPrime(e, c)) continue;
                                if (!BothConcatenationsPrime(e, d)) continue;

                                var sum = a + b + c + d + e;
                                Console.WriteLine("Sum: {0} from {1} {2} {3} {4} {5}", sum, a, b, c, d, e);
                                if (sum < minSum)
                                    minSum = sum;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Min sum {0}", minSum);
            minSum.Should().Be(26033);
        }

        private static bool BothConcatenationsPrime(int x, int y)
        {
            var xThenY = Convert.ToInt64(string.Format("{0}{1}", x, y));
            if (!PrimeHelper.IsPrime(xThenY)) return false;

            var yThenX = Convert.ToInt64(string.Format("{0}{1}", y, x));
            return PrimeHelper.IsPrime(yThenX);
        }
    }
}
