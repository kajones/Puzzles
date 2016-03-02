using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a string S, find the longest palindromic substring in S. 
    /// You may assume that the maximum length of S is 1000, 
    /// and there exists one unique longest palindromic substring.
    /// </summary>
    [TestFixture]
    public class Problem_0005_LongestPalindromicSubstring
    {
        [Test]
        [TestCase("a", "a", "Single char")]
        [TestCase("bb", "bb", "Two matching chars")]
        [TestCase("ab", "a", "Two different chars so first char")]
        [TestCase("ccd", "cc", "First two chars match")]
        [TestCase("ABA", "ABA", "Whole odd string")]
        [TestCase("ABBA", "ABBA", "Whole even string")]
        [TestCase("asdfghgwoeih", "ghg", "Odd Substring")]
        [TestCase("asjkllkjwi", "jkllkj", "Even substring")]
        [TestCase("asasdfdsasb", "sasdfdsas", "Repeated char in palindrome")]
        [TestCase("aaafghgxxxbnmnb", "bnmnb", "Longer of two")]
        [TestCase("", "", "Empty string")]
        public void RunTests(string s, string expectedPalindrome, string message)
        {
            var longestPalindrome = LongestPalindrome(s);
            Assert.That(longestPalindrome, Is.EqualTo(expectedPalindrome), message);
        }

        public string LongestPalindrome(string s)
        {
            if (s.Length == 1) return s;

            var oddPalindrome = GetLongestOddLengthPalindrome(s);
            var evenPalindrome = GetLongestEvenLengthPalindrome(s);

            return oddPalindrome.Length > evenPalindrome.Length
                ? oddPalindrome
                : evenPalindrome;
        }

        private static string GetLongestEvenLengthPalindrome(string s)
        {
            if (s.Length == 2 && s[0] == s[1]) return s;

            string longestPalindrome = string.Empty;

            int leftIdx = 0;
            int rightIdx = 0;

            for (var rightMiddleIdx = 1; rightMiddleIdx < s.Length; ++rightMiddleIdx)
            {
                var leftMiddleIdx = rightMiddleIdx - 1;
                if (s[leftMiddleIdx] != s[rightMiddleIdx]) continue;

                for (var offset=1; offset < s.Length; ++offset)
                {
                    leftIdx = leftMiddleIdx - offset;
                    rightIdx = rightMiddleIdx + offset;
                    if (leftIdx < 0) break;
                    if (rightIdx >= s.Length) break;

                    if (s[leftIdx] != s[rightIdx]) break;
                }

                // Previous pair of leftIdx-rightIdx was a palindrome
                var length = rightIdx - leftIdx - 1;
                var currentPalindrome = s.Substring(leftIdx + 1, length);
                if (currentPalindrome.Length > longestPalindrome.Length)
                {
                    longestPalindrome = currentPalindrome;
                }
            }

            return longestPalindrome;
        }

        private static string GetLongestOddLengthPalindrome(string s)
        {
            string longestPalindrome = string.Empty;

            int leftIdx = 0;
            int rightIdx = 0;

            for (var middlePos = 0; middlePos < s.Length; ++middlePos)
            {
                for (var offset = 1; offset < s.Length; ++offset)
                {
                    leftIdx = middlePos - offset;
                    rightIdx = middlePos + offset;
                    if (leftIdx < 0) break;
                    if (rightIdx >= s.Length) break;

                    if (s[leftIdx] != s[rightIdx]) break;
                }

                // Previous pair of leftIdx-rightIdx was a palindrome
                var length = (rightIdx - leftIdx - 1);
                var currentPalindrome = s.Substring(leftIdx + 1, length);
                if (currentPalindrome.Length > longestPalindrome.Length)
                {
                    longestPalindrome = currentPalindrome;
                }
            }

            return longestPalindrome;
        }
    }
}
