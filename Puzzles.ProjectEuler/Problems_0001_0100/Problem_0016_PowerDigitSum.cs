using System;
using System.Globalization;
using System.Numerics;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// 2power15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
    /// What is the sum of the digits of the number 2power1000?
    /// </summary>
    [TestFixture]
    public class Problem_0016_PowerDigitSum
    {
        [Test]
        public void Power15OfTwoSum()
        {
            var result = CalculateDigitSum(15);

            Assert.AreEqual(26, result);
        }

        /// <summary>
        /// 1366
        /// </summary>
        [Test]
        public void Power1000OfTwoSum()
        {
            BigInteger i = 1;
            i = i << 1000; // Multiply by 2 to the power 1000
            char[] myBigInt = i.ToString().ToCharArray();
            long sum = long.Parse(myBigInt[0].ToString(CultureInfo.InvariantCulture));
            for (int a = 0; a < myBigInt.Length - 1; a++)
            {
                sum += long.Parse(myBigInt[a + 1].ToString(CultureInfo.InvariantCulture));
            }
            Console.WriteLine(sum);
        }

        private static long CalculateDigitSum(long number)
        {
            double powerValue = Math.Pow(2, number);
            var text = powerValue.ToString(CultureInfo.InvariantCulture);

            long sum = 0;

            for (var idx = 0; idx < text.Length; ++idx)
            {
                var value = Convert.ToInt32(text.Substring(idx, 1));
                sum += value;
            }

            return sum;
        }

    }
}
