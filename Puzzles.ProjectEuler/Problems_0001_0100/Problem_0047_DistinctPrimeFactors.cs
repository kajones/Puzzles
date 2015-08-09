using System;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The first two consecutive numbers to have two distinct prime factors are:
    ///
    /// 14 = 2 × 7
    /// 15 = 3 × 5
    ///
    /// The first three consecutive numbers to have three distinct prime factors are:
    ///
    /// 644 = 2² × 7 × 23
    /// 645 = 3 × 5 × 43
    /// 646 = 2 × 17 × 19.
    ///
    /// Find the first four consecutive integers to have four distinct prime factors. What is the first of these numbers?
    /// </summary>
    [TestFixture]
    public class Problem_0047_DistinctPrimeFactors
    {
        /// <summary>
        /// 134043 134044 134045 134046
        /// </summary>
        [Test, Explicit]
        public void FindNumbers()
        {
            long firstNumber = 1;
            while (true)
            {
                var firstFactorsCount = GetCountDistinctFactors(firstNumber);
                if (firstFactorsCount != 4)
                {
                    firstNumber++;
                    continue;
                }

                var secondNumber = firstNumber + 1;
                var secondFactorsCount = GetCountDistinctFactors(secondNumber);
                if (secondFactorsCount != 4)
                {
                    firstNumber += 2;
                    continue;
                }

                var thirdNumber = firstNumber + 2;
                var thirdFactorsCount = GetCountDistinctFactors(thirdNumber);
                if (thirdFactorsCount != 4)
                {
                    firstNumber += 3;
                    continue;
                }

                var fourthNumber = firstNumber + 3;
                var fourthFactorsCount = GetCountDistinctFactors(fourthNumber);
                if (fourthFactorsCount != 4)
                {
                    firstNumber += 4;
                    continue;
                }

                Console.WriteLine("{0} {1} {2} {3}", firstNumber, secondNumber, thirdNumber, fourthNumber);
                break;
            }
        }

        private static int GetCountDistinctFactors(long number)
        {
            var factors = PrimeHelper.FindPrimeFactors(number);
            factors.Remove(number);

            var numberDistinct = factors.Distinct();
            return numberDistinct.Count();
        }
    }
}
