using System;
using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Consider quadratic Diophantine equations of the form:
    ///
    /// x^2 – Dy^2 = 1
    ///
    /// For example, when D=13, the minimal solution in x is 649^2 – 13×180^2 = 1.
    ///
    /// It can be assumed that there are no solutions in positive integers when D is square.
    ///
    /// By finding minimal solutions in x for D = {2, 3, 5, 6, 7}, we obtain the following:
    ///
    /// 3^2 – 2×2^2 = 1
    /// 2^2 – 3×1^2 = 1
    /// 9^2 – 5×4^2 = 1
    /// 5^2 – 6×2^2 = 1
    /// 8^2 – 7×3^2 = 1
    ///
    /// Hence, by considering minimal solutions in x for D ≤ 7, the largest x is obtained when D=5.
    ///
    /// Find the value of D ≤ 1000 in minimal solutions of x for which the largest value of x is obtained.
    /// </summary>
    [TestFixture]
    public class Problem_0066_DiophantineEquation
    {
        private readonly List<long> squares = new List<long>();

        [OneTimeSetUp]
        //[TestFixtureSetUp, Ignore("Not required")]
        public void FixtureSetUp()
        {
            for (var i = 0; i < 15000; ++i)
            {
                squares.Add(i * i);
            }
        }

        /// <summary>
        /// x^2 -Dy^2 = 1
        /// =>
        /// (x - sqrt(D)y)(x + sqrt(D)y) = 1
        /// 
        /// Therefore we find continuing fractions (problem 64) for the square root of D
        /// With the continuing fractions, then try to find the convergent
        /// n_i / d_i => this is the ith convergent of sqrt(D) and therefore the minimum solution to n_i-Dd_i=1
        /// 
        /// _i means ith element
        /// _{i-1} means (i-1)th element etc
        /// 
        /// n_i = a_in_{i-1} + n_{i-2}
        /// d_i = a_id_{i-1} + d_{i-2}
        /// 
        /// Seed values:
        /// n_{-1} = 1, n_{0} = a_0
        /// d_{-1} = 0, d_{0} = 1
        /// </summary>
        /// D with the maximum x: 661
        [Test, Explicit]
        public void FindMinimumXDValue()
        {
            var result = 0;
            BigInteger maxX = 0;

            for (var D = 2; D <= 1000; D++)
            {
                // Ignore perfect squares
                BigInteger limit = (int)Math.Sqrt(D);
                if (limit * limit == D) continue;

                var x = FindMinimumValueForX(limit, D);

                if (x <= maxX) continue;

                maxX = x;
                result = D;
            }

            Console.WriteLine("D with the maximum x: {0}", result);

            result.Should().Be(661);
        }

        private static BigInteger FindMinimumValueForX(BigInteger limit, int D)
        {
            BigInteger m = 0;
            BigInteger d = 1;
            BigInteger a = limit;

            BigInteger numm1 = 1;
            BigInteger num = a;

            BigInteger denm1 = 0;
            BigInteger den = 1;

            while (num * num - D * den * den != 1)
            {
                m = d * a - m;
                d = (D - m * m) / d;
                a = (limit + m) / d;

                BigInteger numm2 = numm1;
                numm1 = num;
                BigInteger denm2 = denm1;
                denm1 = den;

                num = a * numm1 + numm2;
                den = a * denm1 + denm2;
            }

            return num;
        }

        [Test]
        [TestCase(2, 3, 2)]
        [TestCase(5, 9, 4)]
        [TestCase(6, 5, 2)]
        [TestCase(7, 8, 3)]
        [TestCase(13, 649, 180)]
        [TestCase(191, 2999, 217)]
        public void ConfirmLargestValueOfX(int d, long expectedX, long expectedY)
        {
            long x;
            long y;
            FindMinimalSolution(d, out x, out y);
            Assert.AreEqual(expectedX, x, "X");
            Assert.AreEqual(expectedY, y, "Y");
        }

        /// <summary>
        /// D: 634; x: 982   (1000)
        /// D: 191; x: 2999  (3000)
        /// D: 539; x: 3970  (4000)
        /// D: 204; x: 4999  (5000)
        /// D: 787; x: 9959  (10000)
        /// D: 634; x: 14881 (15000)
        /// </summary>
        [Test, Ignore("Too slow")]
        public void FindTheLargestMinimumXForValuesOfDUpToAThousand()
        {
            long x;
            long y;

            long maxX = 0;
            long dGettingMaxX = 0;

            for (var d = 2; d <= 1000; ++d)
            {
                if (squares.Contains(d)) continue;

                FindMinimalSolution(d, out x, out y);
                if (x > maxX)
                {
                    maxX = x;
                    dGettingMaxX = d;
                }
            }

            Console.WriteLine("D: {0}; x: {1}", dGettingMaxX, maxX);
        }

        private void FindMinimalSolution(int d, out long x, out long y)
        {
            long solution = long.MaxValue;
            x = 0;
            y = 0;

            for (var xIdx = squares.Count - 1; xIdx > 2; --xIdx)
            {
                var xSquare = squares[xIdx];
                //for (var yIdx = xIdx - 1; yIdx >= 2; --yIdx)
                for (var yIdx = 2; yIdx < xIdx; ++yIdx)
                {
                    var ySquareD = (d * squares[yIdx]);
                    var thisSln = xSquare - ySquareD;
                    if (thisSln < 0) break;
                    if (thisSln <= solution)
                    {
                        solution = thisSln;
                        x = xIdx;
                        y = yIdx;
                    }
                }
            }
        }

        /// <summary>
        /// From the forum (marlin):
        /// 
        /// Not so hard if you know a couple obscure facts.
        ///
        /// Fact 1. Given two non-negative fractions A/B and C/D, the new fraction defined by (A+C)/(B+D) lies between the first two.
        ///
        /// Clearly if you had a real number bounded below by one fraction, A/B, and above by another fraction, C/D, 
        /// you could create this new fraction (A+B)/(B+D) which lands in the middle somewhere 
        /// and depending on whether the new fraction was above or below the real number, 
        /// you could tighten the bounds by replacing either the lower or the upper bound with this new fraction. 
        /// 
        /// Nothing terribly exciting in that. The exciting thing is this:
        ///
        /// Fact 2: if you start this process off bounded below by the fraction 0/1 (which is of course just zero) 
        /// and above by the fraction 1/0 (which you must just pretend is a fraction, it is of course actually infinity 
        /// - and we will eventually replace it with a real fraction when we tighten the upper bound) - 
        /// 
        /// This process will hit all the convergents from the continued fraction for the real number.
        ///
        /// The process first slides one bound, say the lower one up, and it does this for a while, 
        /// then it slides the upper one down for a while, and it goes back and forth, moving one for a while then the other. 
        /// 
        /// The convergents just happen to be the values of the bounds before you switch direction and move the other bound. 
        /// 
        /// Not every bound you create is a convergent but every convergent will show up as one of these bounds.
        /// </summary>
        [Test, Explicit]
        public void ConvergeByFractions()
        {
            BigInteger maxX = 0;
            long result = 0;

            for (var D = 2; D <= 1000; ++D)
            {
                var x = FindValueForN(D);
                if (x > maxX)
                {
                    maxX = x;
                    result = D;
                }
            }

            Console.WriteLine("D: {0} produces x: {1}", result, maxX);
        }

        private static BigInteger FindValueForN(long n)
        {
            BigInteger n1 = 0;
            BigInteger d1 = 1;
            BigInteger n2 = 1;
            BigInteger d2 = 0;
            // these are the two bounding fractions
            while (true)
            {
                var a = n1 + n2;
                var b = d1 + d2;
                // a/b is the new candidate somewhere in the middle
                var t = a * a - n * b * b; // see how close a^2/b^2 is to n 
                if (t == 1)
                {
                    // you have your pell solution (a,b)
                    //Console.WriteLine(a + " " + b);
                    return a;
                }

                if (t == 0)
                {
                    // problem, n was a square = (a/b)^2
                    // Console.WriteLine("error");
                    break;
                }

                // not there yet - adjust low or hi bound
                if (t > 0)
                {
                    n2 = a;
                    d2 = b;
                }
                else
                {
                    n1 = a;
                    d1 = b;
                }
            }

            return 0;
        }
    }
}
