using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// 145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.
    ///
    /// Find the sum of all numbers which are equal to the sum of the factorial of their digits.
    ///
    /// Note: as 1! = 1 and 2! = 2 are not sums they are not included.
    /// </summary>
    [TestFixture]
    public class Problem_0034_DigitFactorials
    {
        private Dictionary<int, int> factorials;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            factorials = new Dictionary<int, int> { { 0, 1 } };
            for (var i = 1; i < 10; ++i)
            {
                var factorial = MathHelper.GetFactorial(i);
                factorials.Add(i, (int)factorial);
            }
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(145, true)]
        [TestCase(146, false)]
        [TestCase(40585, true)]
        public void ConfirmSumOfFactorialOfDigits(int number, bool expected)
        {
            var isSum = IsSumOfDigitFactorials(number);
            Assert.AreEqual(expected, isSum);
        }

        [Test]
        public void FindDigitFactorials()
        {
            for (var number = 10; number < 2540160; ++number)
            {
                var isSum = IsSumOfDigitFactorials(number);
                if (isSum)
                {
                    Console.WriteLine("Number: {0}{1}", number, Environment.NewLine);
                }
            }
        }

        [Test]
        public void RevisedFindDigitFactorials()
        {
            int[] facts = new int[10];
            int result = 0;
            for (int digit = 0; digit < 10; digit++)
            {
                facts[digit] = factorial(digit);
            }
            for (int i = 10; i < 2540161; i++)
            {
                int sumOfFacts = 0;
                int number = i;
                while (number > 0)
                {
                    int d = number % 10;
                    number /= 10;
                    sumOfFacts += facts[d];
                }

                if (sumOfFacts == i)
                {
                    Console.WriteLine("Found {0}", i);
                    result += i;
                }
            }

            Console.WriteLine("Sum: {0}", result);

            result.Should().Be(40730);
        }

        private static int factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            int y = x;
            for (int i = 1; i < x; i++)
            {
                y *= i;
            }
            return y;
        }

        private bool IsSumOfDigitFactorials(int candidate)
        {
            int sum = 0;
            var number = candidate;
            while (number > 0)
            {
                int d = number % 10;
                number /= 10;
                sum += factorials[d];
            }

            return (sum == candidate);
        }
    }
}
