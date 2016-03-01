using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a string, find the length of the longest substring without repeating characters. 
    /// For example, the longest substring without repeating letters for "abcabcbb" is "abc", 
    /// which the length is 3. For "bbbbb" the longest substring is "b", with the length of 1.
    /// </summary>
    [TestFixture]
    public class Problem_0003_LongestStringWithoutRepeatingCharacters
    {
        [Test]
        [TestCase("abcabcbb", 3)]
        [TestCase("bbbbb", 1)]
        public void ConfirmExamples(string sourceString, int expectedLength)
        {
            var actualLength = LengthOfLongestSubstring(sourceString);
            Assert.That(actualLength, Is.EqualTo(expectedLength));
        }

        [Test]
        [TestCase("abcdefghijklmnopqrstuvwxyz", 26)]
        [TestCase("aabcdefghijklmnopqrstuvwxyz", 26)]
        [TestCase("abcdefghijklmnopqrstuvwxyza", 26)]
        [TestCase("abababab", 2)]
        [TestCase("abcdefedcba", 6)]
        public void CheckOtherStrings(string sourceString, int expectedLength)
        {
            var actualLength = LengthOfLongestSubstring(sourceString);
            Assert.That(actualLength, Is.EqualTo(expectedLength));
        }

        public int LengthOfLongestSubstring(string s)
        {
            HashSet<char> charsFound;
            int maxLength = 0;

            for(var idx=0; idx < s.Length; ++idx)
            {
                charsFound = new HashSet<char>();

                for (var charIdx = idx; charIdx < s.Length; ++charIdx)
                {
                    var nextChar = s[charIdx];
                    if (!charsFound.Contains(nextChar))
                    {
                        charsFound.Add(nextChar);
                        continue;
                    }

                    break;
                }

                maxLength = Math.Max(charsFound.Count, maxLength);
            }

            return maxLength;
        }
    }
}
