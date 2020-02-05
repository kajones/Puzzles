using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A number chain is created by continuously adding the square of the digits in a number to form a new number until it has been seen before.
    ///
    /// For example,
    ///
    /// 44 → 32 → 13 → 10 → 1 → 1
    /// 85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89
    ///
    /// Therefore any chain that arrives at 1 or 89 will become stuck in an endless loop. 
    /// What is most amazing is that EVERY starting number will eventually arrive at 1 or 89.
    ///
    /// How many starting numbers below ten million will arrive at 89?
    /// </summary>
    [TestFixture]
    public class Problem_0092_SquareDigitChains
    {
        private static readonly Dictionary<int, int> squares = new Dictionary<int, int>();

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            squares.Add(0, 0);
            squares.Add(1, 1);
            squares.Add(2, 4);
            squares.Add(3, 9);
            squares.Add(4, 16);
            squares.Add(5, 25);
            squares.Add(6, 36);
            squares.Add(7, 49);
            squares.Add(8, 64);
            squares.Add(9, 81);
        }

        [Test]
        [TestCase(44, false)]
        [TestCase(85, true)]
        [TestCase(89, true)]
        public void ConfirmExamples(long startingDigit, bool expectedArriveAtEightyNine)
        {
            var finish = FinalDigitOfChain(startingDigit);
            var finishesOn89 = finish == 89;
            finishesOn89.Should().Be(expectedArriveAtEightyNine);
        }

        /// <summary>
        /// 8581146
        /// </summary>
        [Test, Explicit]
        public void FindCountOfChainsEndingIn89()
        {
            var count = 0;
            for (long startingDigit = 1; startingDigit < 10000000; ++startingDigit)
            {
                var finish = FinalDigitOfChain(startingDigit);
                if (finish == 89)
                {
                    count++;
                }
            }

            Console.WriteLine(count);

            count.Should().Be(8581146);
        }

        private static decimal FinalDigitOfChain(long startingDigit)
        {
            var numbers = new HashSet<long>();
            var currentNumber = startingDigit;

            while (!numbers.Contains(currentNumber))
            {
                if (currentNumber == 1) return 1;
                if (currentNumber == 89) return 89;

                numbers.Add(currentNumber);    
                var digits = DigitHelper.GetDigits(currentNumber);
                currentNumber = GetSquareSum(digits);
            }

            return currentNumber;
        }

        private static long GetSquareSum(IEnumerable<int> digits)
        {
            long sum = 0;

            foreach (var digit in digits)
            {
                var square = squares[digit];
                sum += square;
            }

            return sum;
        }
    }
}
