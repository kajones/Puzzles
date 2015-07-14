using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100.Problems_0001_0010
{
    /// <summary>
    /// If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. 
    /// The sum of these multiples is 23.
    /// 
    /// Find the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    [TestFixture]
    public class Problem_0001_MultiplesOfThreeAndFive
    {
        [Test]
        public void FindNumbersBelowTenThatAreMultiplesOf3And5()
        {
            var multiples = FindMultiplesUpTo(10);

            Assert.AreEqual(4, multiples.Count(), "Four multiples");
            Assert.IsTrue(multiples.Contains(3), "3");
            Assert.IsTrue(multiples.Contains(5), "5");
            Assert.IsTrue(multiples.Contains(6), "6");
            Assert.IsTrue(multiples.Contains(9), "9");

            var sumOfMultiples = multiples.Sum();
            Assert.AreEqual(23, sumOfMultiples, "Sum");
        }

        /// <summary>
        ///  233168
        /// </summary>
        [Test, Explicit]
        public void FindNumbersBelowAThousandThatAreMultiplesOf3And5()
        {
            var multiples = FindMultiplesUpTo(1000);

            var result = multiples.Sum();
            Console.WriteLine(result);

            result.Should().Be(233168);
        }

        private static IList<int> FindMultiplesUpTo(int limit)
        {
            var list = new List<int>();

            for (var i = 1; i < limit; ++i)
            {
                if (i % 3 == 0)
                {
                    list.Add(i);
                    continue;
                }

                if (i % 5 == 0)
                    list.Add(i);
            }

            return list;
        }

    }
}
