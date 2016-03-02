using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Implement atoi to convert a string to an integer.
    ///
    /// Hint: Carefully consider all possible input cases. 
    /// If you want a challenge, please do not see below and ask yourself what are the possible input cases.
    ///
    /// Notes: It is intended for this problem to be specified vaguely (ie, no given input specs). 
    /// You are responsible to gather all the input requirements up front.
    /// </summary>
    [TestFixture]
    public class Problem_0008_StringToInteger
    {
        [Test]
        [TestCase("", 0, "")]
        [TestCase("0", 0, "")]
        [TestCase("1", 1, "")]
        [TestCase("2", 2, "")]
        [TestCase("10", 10, "")]
        [TestCase("19", 19, "")]
        [TestCase("200", 200, "")]
        [TestCase("123456789", 123456789, "")]
        [TestCase("001234", 1234, "Leading zeroes ignored")]
        [TestCase("1,000", 1000, "")]
        [TestCase("1,234,567", 1234567, "")]
        [TestCase("-1", -1, "")]
        [TestCase("-1234", -1234, "")]
        [TestCase("-1,234", -1234, "")]
        [TestCase("-", 0, "")]
        [TestCase("+", 0, "")]
        [TestCase("+3", 3, "")]
        [TestCase("+-2", 0, "Invalid number representation")]
        [TestCase("abc", 0, "Letters are not valid")]
        [TestCase("     10", 10,"Spaces before can be ignored")]
        [TestCase("10   ", 10, "Spaces after can be ignored")]
        [TestCase(" - 321", 0, "Invalid number if spaces between symbol and digits")]
        [TestCase("   +0 123", 0, "Invalid number if spaces between digits")]
        [TestCase("    +0a32", 0, "Ignore numbers after invalid character")]
        [TestCase("   -0012a42", -12, "Ignore spaces and leading zeroes but once a letter is found return number so far")]
        [TestCase("2147483648", 2147483647, "Expecting arithmetic overflow so return int max")]
        [TestCase("-2147483649", -2147483648, "Expecting arithmetic overflow so return int min")]
        [TestCase("    10522545459", 2147483647, "Expecting arithmetic overflow so return int max")]
        [TestCase("9223372036854775809", 2147483647, "Expecting arithmetic overflow so return int max")]
        public void RunTests(string str, int expectedInteger, string message)
        {
            var result = MyAtoi(str);
            Assert.That(result, Is.EqualTo(expectedInteger), message);
        }

        public int MyAtoi(string str)
        {
            var isNegative = str.Contains("-");

            // Remove formatting characters
            var noSymbolsText = str.Replace(",", "");
            noSymbolsText = noSymbolsText.Trim();

            // Ensure only one sign present
            var afterSignRemoval = noSymbolsText.Replace("-", "").Replace("+", "");
            if (afterSignRemoval.Length < (noSymbolsText.Length - 1)) return 0;

            var regex = new Regex(@"[\d]*");
            var digitsOnlyMatch = regex.Match(afterSignRemoval);
            var digitsText = digitsOnlyMatch.Value;

            long result = 0;
            long multiplier = 1;
            for(var idx=digitsText.Length-1; idx >=0; --idx)
            {
                var currChar = digitsText[idx];
                var currInteger = currChar - 48;
                result += (currInteger * multiplier);

                if (result > Int32.MaxValue) break;

                multiplier *= 10;
            }

            if (isNegative)
            {
                result *= -1;
            }

            if (result > Int32.MaxValue) return Int32.MaxValue;
            if (result < Int32.MinValue) return Int32.MinValue;

            return Convert.ToInt32(result);
        }

        public int MyAtoiUsingCharacters(string str)
        {
            var isNegative = str.Contains("-");

            // Ensure only one sign present
            var afterSignRemoval = str.Replace("-", "").Replace("+", "");
            if (afterSignRemoval.Length < (str.Length - 1)) return 0;

            // Remove formatting characters
            var noSymbolsText = afterSignRemoval.Replace(",", "");
            noSymbolsText = noSymbolsText.Trim();

            int result = 0;
            int multiplier = Convert.ToInt32(Math.Pow(10, noSymbolsText.Length-1));
            for (var idx = 0; idx < noSymbolsText.Length; ++idx)
            {
                var currChar = noSymbolsText[idx];
                var currInt = currChar - 48;
                if (currInt < 0 || currInt > 9) break;

                result += (currInt * multiplier);
                multiplier /= 10;
            }

            if (isNegative)
            {
                result *= -1;
            }

            return result;
        }
    }
}
