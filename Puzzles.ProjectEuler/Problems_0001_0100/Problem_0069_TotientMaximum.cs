using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Euler's Totient function, φ(n) [sometimes called the phi function], is used to determine the number of numbers less than n which are relatively prime to n. 
    /// For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
    ///
    /// n	Relatively Prime	φ(n)	n/φ(n)
    /// 2	1	                1	    2
    /// 3	1,2	                2	    1.5
    /// 4	1,3	                2	    2   
    /// 5	1,2,3,4	            4	    1.25
    /// 6	1,5	                2	    3
    /// 7	1,2,3,4,5,6	        6	    1.1666...
    /// 8	1,3,5,7	            4	    2
    /// 9	1,2,4,5,7,8	        6	    1.5
    /// 10	1,3,7,9	            4	    2.5
    /// It can be seen that n=6 produces a maximum n/φ(n) for n ≤ 10.
    ///
    /// Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum.
    /// </summary>
    [TestFixture]
    public class Problem_0069_TotientMaximum
    {
        [Test]
        [TestCase(2, new long[] { 1 })]
        [TestCase(3, new long[] { 1, 2 })]
        [TestCase(4, new long[] { 1, 3 })]
        [TestCase(5, new long[] { 1, 2, 3, 4 })]
        [TestCase(6, new long[] { 1, 5 })]
        [TestCase(7, new long[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(8, new long[] { 1, 3, 5, 7 })]
        [TestCase(9, new long[] { 1, 2, 4, 5, 7, 8 })]
        [TestCase(10, new long[] { 1, 3, 7, 9 })]
        public void ConfirmRelativelyPrime(long candidate, long[] expectedRelativePrimes)
        {
            var relativePrimes = GetRelativePrimes(candidate);

            Assert.AreEqual(expectedRelativePrimes.Length, relativePrimes.Count, "Number of relative primes");
            foreach (var expectedRelativePrime in expectedRelativePrimes)
            {
                Assert.IsTrue(relativePrimes.Contains(expectedRelativePrime), "Found expected relative prime {0}", expectedRelativePrime);
            }
        }

        /// <summary>
        /// 510510 
        /// </summary>
        [Test, Explicit]
        public void FindLargestPhiFraction()
        {
            long number = 0;
            double maxPhiFraction = 0;

            for (var n = 1; n <= 1000000; ++n)
            {
                var phi = EulerTotientHelper.CalculatePhi(n);
                var phiFraction = n / (1.0 * phi);
                if (!(phiFraction > maxPhiFraction)) continue;

                number = n;
                maxPhiFraction = phiFraction;
            }

            Console.WriteLine("Number:{0} fraction:{1}", number, maxPhiFraction);
            number.Should().Be(510510);
        }

        [Test]
        public void ConfirmMaximumTotientUnderTen()
        {
            var factors = GetFactorsUpTo(10);

            //foreach (var factor in factors)
            //{
            //    Console.WriteLine("{0}: {1}", factor.Key, string.Join(",", factor.Value));
            //}

            int number = 0;
            double maxPhiFraction = 0;

            for (var n = 1; n <= 10; ++n)
            {
                var nFactors = factors[n];
                nFactors.Remove(1);

                var numbersBelowN = Enumerable.Range(1, (n - 1));
                var numbersThatAreCoPrime = new List<long>();
                foreach (var l in numbersBelowN)
                {
                    var haveCommonFactor = false;
                    foreach (var nFactor in nFactors)
                    {
                        if (factors[l].Contains(nFactor))
                        {
                            haveCommonFactor = true;
                            break;
                        }
                    }

                    if (!haveCommonFactor)
                    {
                        numbersThatAreCoPrime.Add(l);
                    }
                }
                var phi = numbersThatAreCoPrime.Count;

                Console.WriteLine("{0} : {1}", n, phi);
                if (phi == 0) phi = 1;
                var phiFraction = n / phi;
                if (phiFraction > maxPhiFraction)
                {
                    number = n;
                    maxPhiFraction = phiFraction;
                }
            }

            Console.WriteLine("Number: {0}  PhiF: {1}", number, maxPhiFraction);
        }

        [Test, Ignore("Too slow")]
        public void ConfirmMaximumTotientUnderAMillion()
        {
            const long AMillion = 1000000;

            var factors = GetFactorsUpTo(AMillion);

            //foreach (var factor in factors)
            //{
            //    Console.WriteLine("{0}: {1}", factor.Key, string.Join(",", factor.Value));
            //}

            int number = 0;
            double maxPhiFraction = 0;

            for (var n = 2; n <= AMillion; n += 2)
            {
                var nFactors = factors[n];
                nFactors.Remove(1);

                var numbersBelowN = Enumerable.Range(1, (n - 1));
                var numbersThatAreCoPrime = new List<long>();
                foreach (var l in numbersBelowN)
                {
                    var haveCommonFactor = false;
                    foreach (var nFactor in nFactors)
                    {
                        if (factors[l].Contains(nFactor))
                        {
                            haveCommonFactor = true;
                            break;
                        }
                    }

                    if (!haveCommonFactor)
                    {
                        numbersThatAreCoPrime.Add(l);
                    }
                }
                var phi = numbersThatAreCoPrime.Count;

                Console.WriteLine("{0} : {1}", n, phi);
                if (phi == 0) phi = 1;
                var phiFraction = n / phi;
                if (phiFraction > maxPhiFraction)
                {
                    number = n;
                    maxPhiFraction = phiFraction;
                }
            }

            Console.WriteLine("Number: {0}  PhiF: {1}", number, maxPhiFraction);
        }


        private static Dictionary<long, List<long>> GetFactorsUpTo(long candidate)
        {
            var factorSet = new Dictionary<long, List<long>>();

            for (var i = 1; i <= candidate; ++i)
            {
                var factorsOfI = FactorHelper.GetProperFactorsOf(i);
                if ((i > 1) && (PrimeHelper.IsPrime(i)))
                    factorsOfI.Add(i);

                factorSet.Add(i, factorsOfI);
            }

            return factorSet;
        }

        [Test, Ignore("Too slow")]
        public void FindLargestPhiNumber()
        {
            long number = 0;
            double maxPhiFraction = 0;

            for (var n = 2; n <= 1000000; ++n)
            {
                var relativePrimes = GetRelativePrimes(n);

                var phiFraction = n / relativePrimes.Count;
                if (phiFraction > maxPhiFraction)
                {
                    maxPhiFraction = phiFraction;
                    number = n;
                }
            }

            Console.WriteLine("Number: {0} with fraction {1}", number, maxPhiFraction);
        }

        private static List<long> GetRelativePrimes(long candidate)
        {
            var relativePrimes = new List<long>();

            for (var i = 1; i < candidate; ++i)
            {
                var areCoPrime = FactorHelper.AreCoprime(i, candidate);
                if (areCoPrime)
                    relativePrimes.Add(i);
            }
            return relativePrimes;
        }
    }
}
