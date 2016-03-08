using NUnit.Framework;
using Puzzles.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given an integer, convert it to a roman numeral.
    ///
    /// Input is guaranteed to be within the range from 1 to 3999.
    /// </summary>
    [TestFixture]
    public class Problem_0012_IntegerToRoman
    {
        [Test]
        [TestCase(1, "I")]
        [TestCase(2, "II")]
        [TestCase(3, "III")]
        [TestCase(4, "IV")]
        [TestCase(5, "V")]
        [TestCase(6, "VI")]
        [TestCase(7, "VII")]
        [TestCase(8, "VIII")]
        [TestCase(9, "IX")]
        [TestCase(10, "X")]
        [TestCase(11, "XI")]
        [TestCase(12, "XII")]
        [TestCase(13, "XIII")]
        [TestCase(14, "XIV")]
        [TestCase(15, "XV")]
        [TestCase(16, "XVI")]
        [TestCase(18, "XVIII")]
        [TestCase(19, "XIX")]
        [TestCase(20, "XX")]
        [TestCase(21, "XXI")]
        [TestCase(29, "XXIX")]
        [TestCase(30, "XXX")]
        [TestCase(40, "XL")]
        [TestCase(49, "XLIX")]
        [TestCase(50, "L")]
        [TestCase(51, "LI")]
        [TestCase(100, "C")]
        [TestCase(500, "D")]
        [TestCase(621, "DCXXI")]
        [TestCase(1000, "M")]
        [TestCase(3000, "MMM")]
        [TestCase(3999, "MMMCMXCIX")]
        public void RunTests(int num, string expectedRoman)
        {
            var roman = IntToRoman(num);
            Assert.That(roman, Is.EqualTo(expectedRoman));
        }

        public string IntToRoman(int num)
        {
            var roman = RomanNumeralGenerator.GetRomanRepresentation(num);
            return roman;
        }
    }
}
