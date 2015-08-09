using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:
    ///
    /// 1634 = 1power4 + 6power4 + 3power4 + 4power4
    /// 8208 = 8power4 + 2power4 + 0power4 + 8power4
    /// 9474 = 9power4 + 4power4 + 7power4 + 4power4
    /// As 1 = 1power4 is not a sum it is not included.
    ///
    /// The sum of these numbers is 1634 + 8208 + 9474 = 19316.
    ///
    /// Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
    [TestFixture]
    public class Problem_0030_DigitFifthPowers
    {
        [Test]
        public void FindNumbersThatCanBeWrittenAsTheSumOfTheirFourthPowerOfTheirDigits()
        {
            var numbers = GetNumbers(4);

            Assert.IsTrue(numbers.Contains(1634), "1634");
            Assert.IsTrue(numbers.Contains(8208), "8208");
            Assert.IsTrue(numbers.Contains(9474), "9474");
        }

        [Test, Explicit]
        public void FindNumbersThatCanBeWrittenAsTheSumOfTheirFifthPowerOfTheirDigits()
        {
            var numbers = GetNumbers(5);

            var sum = numbers.Sum();
            Console.WriteLine("Sum: {0}", sum);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }

        private List<int> GetNumbers(int powerToCheck)
        {
            var powerDictionary = GetPowersOfDigits(powerToCheck);

            var list = new List<int>();

            for (var i = 2; i < 1000000; ++i)
            {
                var digits = DigitHelper.GetDigits(i);
                int sum = 0;
                foreach (var digit in digits)
                {
                    sum += powerDictionary[digit];
                    if (sum > i) break;
                }
                if (sum == i)
                    list.Add(i);
            }

            return list;
        }

        private static Dictionary<int, int> GetPowersOfDigits(int power)
        {
            var powerDictionary = new Dictionary<int, int>();

            for (var i = 0; i < 10; ++i)
            {
                powerDictionary.Add(i, (int)Math.Pow(i, power));
            }

            return powerDictionary;
        }
    }
}
