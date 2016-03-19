using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Implement strStr().
    /// Returns the index of the first occurrence of needle in haystack, 
    /// or -1 if needle is not part of haystack.
    /// </summary>
    [TestFixture]
    public class Problem_0028_ImplementStrStr
    {
        [Test]
        [TestCase("", "",0)]
        [TestCase("hello", "", 0)]
        [TestCase("hello", "h", 0)]
        [TestCase("hello", "e", 1)]
        [TestCase("hello", "l", 2)]
        [TestCase("hello", "o", 4)]
        [TestCase("hello", "x", -1)]
        [TestCase("hello", "ell", 1)]
        [TestCase("hello", "hello", 0)]
        [TestCase("hello goodbye", "goodbye", 6)]
        [TestCase("abcabcabc", "abc", 0)]
        [TestCase("cabcabcabc", "abc", 1)]
        [TestCase("", "abc", -1)]
        public void RunTests(string haystack, string needle, int expectedIndex)
        {
            var index = StrStr(haystack, needle);
            Assert.That(index, Is.EqualTo(expectedIndex));
        }

        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
                return 0;

            if (string.IsNullOrEmpty(haystack))
                return -1;

            for (var idx = 0; idx < (haystack.Length - needle.Length + 1); ++idx)
            {
                if (haystack[idx] != needle[0]) continue;

                var found = true;
                for(var offset = 1; offset < needle.Length; ++offset)
                {
                    if (haystack[idx + offset] == needle[offset]) continue;
                    found = false;
                    break;
                }

                if (found) return idx;
            }

            return -1;
        }
    }
}
