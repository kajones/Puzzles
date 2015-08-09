using System;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// An irrational decimal fraction is created by concatenating the positive integers:
    ///
    /// 0.123456789101112131415161718192021...
    ///
    /// It can be seen that the 12th digit of the fractional part is 1.
    ///
    /// If dn represents the nth digit of the fractional part, find the value of the following expression.
    ///
    /// d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000
    [TestFixture]
    public class Problem_0040_ChampernownesConstant
    {
        [Test]
        public void ConfirmTwelfthDigit()
        {
            var digit = GetDigit(12);
            Assert.AreEqual("1", digit);
        }

        /// <summary>
        /// 210 (1 1 5 3 7 2 1)
        /// </summary>
        [Test, Explicit]
        public void ConfirmMultiplePositions()
        {
            //var digit1 = "1";
            //var digit10 = "1";
            //var digit100 = GetDigit(100);
            //var digit1000 = GetDigit(1000);
            //var digit10000 = GetDigit(10000);
            //var digit100000 = GetDigit(100000);
            //var digit1000000 = GetDigit(1000000);
            //// 1 1 5 7 7 2 8

            //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", digit1, digit10, digit100, digit1000, digit10000, digit100000, digit1000000);

            var text = GetText();
            var digit1 = text.Substring(1, 1);
            var digit10 = text.Substring(10, 1);
            var digit100 = text.Substring(100, 1);
            var digit1000 = text.Substring(1000, 1);
            var digit10000 = text.Substring(10000, 1);
            var digit100000 = text.Substring(100000, 1);
            var digit1000000 = text.Substring(1000000, 1);

            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", digit1, digit10, digit100, digit1000, digit10000, digit100000, digit1000000);
        }

        private static string GetText()
        {
            // 1000000
            var text = string.Empty;

            for (var i = 0; i < 1000000; ++i)
            {
                text += i;

                if (text.Length > 1000000) break;
            }

            return text;
        }

        private static string GetDigit(int position)
        {
            var text = string.Empty;

            for (var i = 1; i < position; ++i)
            {
                text += i;
                if (text.Length > position) break;
            }

            return text.Substring(position, 1);
        }
    }
}
