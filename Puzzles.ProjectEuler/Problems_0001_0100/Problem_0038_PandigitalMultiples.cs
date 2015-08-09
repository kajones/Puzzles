using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Take the number 192 and multiply it by each of 1, 2, and 3:
    ///
    /// 192 × 1 = 192
    /// 192 × 2 = 384
    /// 192 × 3 = 576
    /// By concatenating each product we get the 1 to 9 pandigital, 192384576. 
    /// We will call 192384576 the concatenated product of 192 and (1,2,3)
    ///
    /// The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, 
    /// which is the concatenated product of 9 and (1,2,3,4,5).
    ///
    /// What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product 
    /// of an integer with (1,2, ... , n) where n > 1?
    /// </summary>
    [TestFixture]
    public class Problem_0038_PandigitalMultiples
    {
        [Test]
        [TestCase(192, new[] { 1, 2, 3 }, true, 192384576)]
        [TestCase(9, new[] { 1, 2, 3, 4, 5 }, true, 918273645)]
        [TestCase(111, new[] { 1, 2 }, false, -1)]
        [TestCase(123456789, new[] { 1 }, false, -1)]
        [TestCase(65399, new[] { 1, 2 }, false, -1)]
        public void ConfirmIsPandigitalMultiple(int number, int[] multipliers, bool expectedResult, int pandigitalMultiple)
        {
            var result = IsPandigitalMultiple(number, multipliers);
            Assert.AreEqual(expectedResult, result);

            var product = CheckIfCanGeneratePandigital(number);
            Assert.AreEqual(pandigitalMultiple, product, "Confirm value");
        }

        /// <summary>
        /// 932718654 (9327 => 1,2) 
        /// </summary>
        [Test, Explicit]
        public void FindLargestPandigitalNumberThatIsAConcatenatedProduct()
        {
            var products = new List<PandigitalMultiple>();

            for (var number = 1; number < 100000; ++number)
            {
                var result = CheckIfCanGeneratePandigital(number);
                if (result > 0)
                {
                    products.Add(new PandigitalMultiple(result, number, 0));
                }
            }

            foreach (var result in products.OrderByDescending(p => p.Product))
            {
                Console.WriteLine(result);
            }
        }

        private static int CheckIfCanGeneratePandigital(int number)
        {
            const int upperLimit = 1000000000;

            var multiplier = 1;
            int product = (number * multiplier);
            while (product < upperLimit)
            {
                multiplier++;
                var nextProduct = number * multiplier;
                var lengthNextProduct = DigitHelper.GetNumberLength(nextProduct);
                long combineProduct = product * (long)Math.Pow(10, lengthNextProduct);
                if (combineProduct < 0 || combineProduct > upperLimit) break;
                product = (int)combineProduct + nextProduct;
                if (product < 0 || product > upperLimit) break;

                if (!PandigitalHelper.IsPandigital(product)) continue;
                return product;
            }

            return -1;
        }

        private static bool IsPandigitalMultiple(int number, ICollection<int> multipliers)
        {
            if (multipliers.Count < 2) return false;

            var results = multipliers.Select(multiplier => number * multiplier);

            var concatenatedText = string.Concat(results);
            var digits = DigitHelper.GetDigits(concatenatedText);
            return PandigitalHelper.IsPandigital(digits);
        }
    }

    public struct PandigitalMultiple
    {
        public int Product { get; private set; }
        public int Number { get; private set; }
        public int LastMultiplier { get; private set; }

        public PandigitalMultiple(int product, int number, int lastMultiplier)
            : this()
        {
            Product = product;
            Number = number;
            LastMultiplier = lastMultiplier;
        }

        public override string ToString()
        {
            return string.Format("Product: {0} from {1} up to {2}", Product, Number, LastMultiplier);
        }
    }
}
