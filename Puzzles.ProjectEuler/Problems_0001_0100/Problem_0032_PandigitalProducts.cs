using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; 
    /// for example, the 5-digit number, 15234, is 1 through 5 pandigital.
    ///
    /// The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, 
    /// and product is 1 through 9 pandigital.
    ///
    /// Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
    ///
    /// HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.
    [TestFixture]
    public class Problem_0032_PandigitalProducts
    {
        [Test]
        [TestCase(39, 186, 7254, true)]
        [TestCase(2, 3, 6, false)]
        [TestCase(123, 345, 6789, false)]
        public void ConfirmIsPandigital(int a, int b, int product, bool expectedPandigital)
        {
            var isPandigital = IsPandigital(a, b, product);
            Assert.AreEqual(expectedPandigital, isPandigital);
        }

        /// 45228
        /// From: 6952
        ///7852
        ///5796
        ///5346
        ///4396
        ///7254
        ///7632
        [Test, Explicit]
        public void FindPandigitalProducts()
        {
            var products = new List<int>();

            // Highest pandigital standalone number = 987654321
            // Square root of this is 31426
            //var limit = (int) Math.Pow(10, 9);

            // Most lopsided calc would be single digit * 4 digit => 4/5 digit product

            for (int a = 1; a < 10000; ++a)
            {
                for (int b = (a + 1); b < 10000; ++b)
                {
                    var product = a * b;
                    if (product < 1000) continue; // Not going to have enough digits between a, b and product
                    if (product > 99999) break; // Too many digits

                    if (products.Contains(product)) continue;

                    var isPandigital = IsPandigital(a, b, product);
                    if (isPandigital)
                        products.Add(product);
                }
            }

            var sum = products.Sum();
            Console.WriteLine("Sum: {0}", sum);
            foreach (var product in products)
            {
                Console.WriteLine("  {0}", product);
            }

            sum.Should().Be(45228);
        }

        private static bool IsPandigital(int a, int b, int c)
        {
            var aDigits = DigitHelper.GetDigits(a);
            var bDigits = DigitHelper.GetDigits(b);
            var cDigits = DigitHelper.GetDigits(c);

            var combinedDigits = new List<int>();
            combinedDigits.AddRange(aDigits);
            combinedDigits.AddRange(bDigits);
            combinedDigits.AddRange(cDigits);

            return PandigitalHelper.IsPandigital(combinedDigits);
        }

    }
}
