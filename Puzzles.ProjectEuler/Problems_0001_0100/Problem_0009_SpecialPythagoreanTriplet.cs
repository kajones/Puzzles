using System;
using NUnit.Framework;
using Puzzles.Core.Models;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A Pythagorean triplet is a set of three natural numbers, a less than b less than c for which
    ///
    /// asquared + bsquared = csquared
    /// For example, 32
    ///  3squared + 4squared = 9 + 16 = 25 = 5squared.
    ///
    /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    /// Find the product abc.
    /// </summary>
    [TestFixture]
    public class Problem_0009_SpecialPythagoreanTriplet
    {
        /// <summary>
        /// A:375, B:200, C:425
        /// 31875000
        /// </summary>
        [Test, Explicit]
        public void CanGenerateTriples()
        {
            //var triple = GetTripleToSum(12);
            //var triple = GetTripleToSum(30);
            //var triple = GetTripleToSum(56);
            var triple = GetTripleToSum(1000);

            Assert.IsNotNull(triple, "Found triple: {0}", triple);
            Console.WriteLine(triple);

            Console.WriteLine(triple.Value.a * triple.Value.b * triple.Value.c);
        }

        private static Triple? GetTripleToSum(int sum)
        {
            for (var m = 1; m < sum; m++)
            {
                var mSquared = (long)Math.Pow(m, 2);

                for (var n = m + 1; n < sum; n++)
                {
                    var nSquared = (long)Math.Pow(n, 2);

                    var a = nSquared - mSquared;
                    var b = 2 * n * m;
                    var c = nSquared + mSquared;

                    if ((a + b + c) == sum)
                    {
                        return new Triple(a, b, c);
                    }
                }
            }

            return null;
        }

    }
}
