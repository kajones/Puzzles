using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class FactorHelper
    {
        /// <summary>
        /// HCF == GCD
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static long GetHighestCommonFactor(long first, long second)
        {
            var firstFactors = GetFactorsOf(first);
            var secondFactors = GetFactorsOf(second);

            return firstFactors.Where(secondFactors.Contains).Max();
        }

        /// <summary>
        /// Perfect number is where the sum of proper factors equals the number itself e.g. 28 (1,2,4,7,14)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPerfect(long number)
        {
            var properFactors = GetProperFactorsOf(number);
            var factorsSum = properFactors.Sum();

            return (factorsSum == number);
        }

        /// <summary>
        /// An abundant number is where the sum of the proper factors is greater than the number itself
        /// - lowest example is 12 (1,2,3,4,6 => 16)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsAbundant(long number)
        {
            var properFactors = GetProperFactorsOf(number);
            var factorsSum = properFactors.Sum();

            return (factorsSum > number);
        }

        public static List<long> GetProperFactorsOf(long number)
        {
            return GetFactorsOf(number, false);
        }

        public static List<long> GetFactorsOf(long number, bool includeNumber = true)
        {
            var factors = new List<long>();

            var limit = (long) Math.Sqrt(number);

            for (long candidateFactor = 1; candidateFactor <= limit; ++ candidateFactor)
            {
                if (number%candidateFactor != 0) continue;

                var result = number/candidateFactor;

                factors.Add(candidateFactor);
                if (result != candidateFactor)
                {
                    if (result == number)
                    {
                        if (includeNumber)
                        {
                            factors.Add(result);
                        }
                    }
                    else
                    {
                        factors.Add(result);
                    }
                }
            }

            return factors;
        }

        /// <summary>
        /// GCD == HCF
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GetGreatestCommonDivisor(long a, long b)
        {
            if (b == 0) return a;

            return GetGreatestCommonDivisor(b, a%b);
        }

        /// <summary>
        /// Two integers are coprime (aka relatively prime, mutually prime) if the GCD is 1
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool AreCoprime(long first, long second)
        {
            long a = first > second ? first : second;
            long b = first > second ? second : first;

            var gcd = GetGreatestCommonDivisor(a, b);
            return (gcd == 1);
        }
    }
}
