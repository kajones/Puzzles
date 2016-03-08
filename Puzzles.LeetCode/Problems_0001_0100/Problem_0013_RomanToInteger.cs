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
    /// Given a roman numeral, convert it to an integer.
    ///
    /// Input is guaranteed to be within the range from 1 to 3999.
    /// </summary>
    [TestFixture]
    public class Problem_0013_RomanToInteger
    {
        [Test]
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        [TestCase("V", 5)]
        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        [TestCase("IX", 9)]
        [TestCase("X", 10)]
        [TestCase("XI", 11)]
        [TestCase("XII", 12)]
        [TestCase("XIII", 13)]
        [TestCase("XIV", 14)]
        [TestCase("XV", 15)]
        [TestCase("XVI", 16)]
        [TestCase("XVIII", 18)]
        [TestCase("XIX", 19)]
        [TestCase("XX", 20)]
        [TestCase("XXI", 21)]
        [TestCase("XXIX", 29)]
        [TestCase("XXX", 30)]
        [TestCase("XL", 40)]
        [TestCase("XLIX", 49)]
        [TestCase("L", 50)]
        [TestCase("LI", 51)]
        [TestCase("C", 100)]
        [TestCase("D", 500)]
        [TestCase("DCXXI", 621)]
        [TestCase("M", 1000)]
        [TestCase("MMM", 3000)]
        [TestCase("MMMCMXCIX", 3999)]
        public void RunTests(string roman, int expectedNum)
        {
            var num = RomanToInt(roman);
            Assert.That(num, Is.EqualTo(expectedNum));
        }

        public int RomanToInt(string s)
        {
            var num = RomanNumeralGenerator.GetDigitalRepresentation(s);
            return num;
        }
    }
}
