using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The first known prime found to exceed one million digits was discovered in 1999, 
    /// and is a Mersenne prime of the form 2 Pow 6972593−1; it contains exactly 2,098,960 digits. 
    /// Subsequently other Mersenne primes, of the form 2p−1, have been found which contain more digits.
    ///
    /// However, in 2004 there was found a massive non-Mersenne prime which contains 2,357,207 digits: 28433×2 Pow 7830457+1.
    ///
    /// Find the last ten digits of this prime number.
    /// </summary>
    [TestFixture]
    public class Problem_0097_LargestNonMersennePrime
    {
        /// <summary>
        /// 8739992577
        /// </summary>
        [Test, Explicit]
        public void CalculatePrime()
        {
            var first = new BigInteger(28433);
            var second = BigInteger.Pow(2, 7830457);
            var add = new BigInteger(1);
            var result = BigInteger.Multiply(first, second);
            var prime = BigInteger.Add(result, add);
            var text = prime.ToString();
            var length = text.Length;
            length.Should().Be(2357207);
            var ending = text.Substring(length - 15);
            Console.WriteLine("Prime ending {0}", ending);
            ending.Should().Be("790198739992577");
        }

        [Test, Explicit]
        public void AnotherBigIntegerApproach()
        {
            const long mod = 10000000000;
            var prime = 28433 * BigInteger.ModPow(2, 7830457, mod) + 1;
            var lastTenDigits = prime % mod;
            Console.Write(lastTenDigits);
            lastTenDigits.Should().Be(8739992577);
        }

        /// <summary>
        /// a^b=a * a^(b-1)
        /// 
        /// a^20 = c^10 where c=a^2 
        /// c^10 = d^5 where d=c^2 
        /// d^5 = d * e^2 where e=d^2 
        /// e^2 = e*e 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double Power(double a, int b)
        {
            if (b < 0)
            {
                throw new ApplicationException("B must be a positive integer or zero");
            }
            if (b == 0) return 1;
            if (a == 0) return 0;
            if (b % 2 == 0)
            {
                return Power(a * a, b / 2);
            }
            else if (b % 2 == 1)
            {
                return a * Power(a * a, b / 2);
            }
            return 0;
        }
    }
}
