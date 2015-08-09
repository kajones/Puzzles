using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A unit fraction contains 1 in the numerator. 
    /// The decimal representation of the unit fractions with denominators 2 to 10 are given:
    ///
    /// 1/2	  = 	0.5
    /// 1/3	  = 	0.(3)
    /// 1/4	  = 	0.25
    /// 1/5	  = 	0.2
    /// 1/6	  = 	0.1(6)
    /// 1/7	  = 	0.(142857)
    /// 1/8	  = 	0.125
    /// 1/9	  = 	0.(1)
    /// 1/10  = 	0.1
    /// Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.
    ///
    /// Find the value of d less than 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
    /// </summary>
    [TestFixture]
    public class Problem_0026_ReciprocalCycles
    {
        private static List<int> primes;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            primes = PrimeHelper.GetPrimesUpTo(1000);
        }

        [Test, Explicit]
        public void FindHighestPrimeUnderAThousand()
        {
            decimal highestPrime = primes[primes.Count - 1];
            Console.WriteLine(highestPrime);
            decimal fraction = 1 / highestPrime;
            Console.WriteLine(fraction);
        }

        [Test]
        [TestCase(2, 0)]
        [TestCase(3, 1)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(6, 1)]
        [TestCase(7, 6)]
        [TestCase(8, 0)]
        [TestCase(9, 1)]
        [TestCase(10, 0)]
        [TestCase(49, 42)]
        [TestCase(119, 48)]
        public void FindLengthOfReciprocalCycle(int d, int expectedCycle)
        {
            Assert.Fail("Need to look at why this is failing");
            var cycle = FindCycleLength(d);
            Assert.AreEqual(expectedCycle, cycle, d.ToString(CultureInfo.InvariantCulture));
        }

        private static int FindCycleLength(int number)
        {
            int[] foundRemainders = new int[number];
            int value = 1;
            int position = 0;

            while (foundRemainders[value] == 0 && value != 0)
            {
                foundRemainders[value] = position;
                value *= 10;
                value %= number;
                position++;
            }

            return position - 1;
        }

        /// <summary>
        /// 983
        /// http://www.mathblog.dk/project-euler-26-find-the-value-of-d-1000-for-which-1d-contains-the-longest-recurring-cycle/
        ///  Maximum sequence length for any number is one less digit than the number i.e. for 97 it would be 96
        /// </summary>
        [Test, Explicit]
        public void FindLongestCycleBelowAThousand()
        {
            int sequenceLength = 0;
            int num = 0;

            for (int i = 1000; i > 1; i--)
            {
                if (sequenceLength >= i)
                {
                    break;
                }

                int[] foundRemainders = new int[i];
                int value = 1;
                int position = 0;

                while (foundRemainders[value] == 0 && value != 0)
                {
                    foundRemainders[value] = position;
                    value *= 10;
                    value %= i;
                    position++;
                }

                if (position - foundRemainders[value] > sequenceLength)
                {
                    num = i;
                    sequenceLength = position - foundRemainders[value];
                }
            }

            Console.WriteLine("The number with the longest recurring cycle is {0}, and the cycle is length is {1}", num, sequenceLength);
        }

    }
}
