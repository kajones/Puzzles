using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The smallest number expressible as the sum of a prime square, prime cube, and prime fourth power is 28. 
    /// In fact, there are exactly four numbers below fifty that can be expressed in such a way:
    ///
    /// 28 = 2^2 + 2^3 + 2^4
    /// 33 = 3^2 + 2^3 + 2^4
    /// 49 = 5^2 + 2^3 + 2^4
    /// 47 = 2^2 + 3^3 + 2^4
    ///
    /// How many numbers below fifty million can be expressed as the sum of a prime square, prime cube, and prime fourth power?
    /// </summary>
    [TestFixture]
    public class Problem_0087_PrimePowerTriples
    {
        private readonly List<long> primes = PrimeHelper.GetPrimesUpTo((long)7100);
        private readonly List<long> squares = new List<long>();
        private readonly List<long> cubes = new List<long>();
        private readonly List<long> fourthPowers = new List<long>();

        [Test]
        public void ConfirmExample()
        {
            foreach (var prime in primes)
            {
                var square = prime * prime;
                var cube = square * prime;
                var fourth = cube * prime;

                squares.Add(square);
                cubes.Add(cube);
                fourthPowers.Add(fourth);
            }

            long total = 0;

            foreach (var square in squares)
            {
                foreach (var cube in cubes)
                {
                    foreach (var fourthPower in fourthPowers)
                    {
                        var number = square + cube + fourthPower;
                        if (number < 50)
                            total++;
                        else
                            break;
                    }
                }
            }

            Assert.AreEqual(4, total);
        }

        /// <summary>
        /// Problem is not specific to ignore duplicates?
        /// i.e fourth power of x == square of (x*x)
        /// 1097343
        /// </summary>
        [Test, Explicit]
        public void CalculatePowersBelowFiftyMillion()
        {
            const long limit = 50000000;

            foreach (var prime in primes)
            {
                var square = prime * prime;

                if (square < limit)
                    squares.Add(square);
                else
                {
                    break;
                }

                var cube = square * prime;
                if (cube < limit)
                    cubes.Add(cube);
                else
                {
                    continue;
                }

                var fourth = cube * prime;
                if (fourth < limit)
                    fourthPowers.Add(fourth);
                else
                {
                    continue;
                }
            }

            var answers = new HashSet<long>();

            foreach (var square in squares)
            {
                foreach (var cube in cubes)
                {
                    var squarePlusCube = square + cube;
                    if (squarePlusCube > limit) break;
                    if (squarePlusCube < 0)
                    {
                        Assert.Fail("Wrap!");
                    }
                    foreach (var fourthPower in fourthPowers)
                    {
                        var number = squarePlusCube + fourthPower;
                        if (number < limit)
                        {
                            if (!answers.Contains(number))
                                answers.Add(number);
                        }
                        else
                            break;
                    }
                }
            }

            Console.WriteLine("Total: {0}", answers.Count);

            answers.Count.Should().Be(1097343);
        }
    }
}
