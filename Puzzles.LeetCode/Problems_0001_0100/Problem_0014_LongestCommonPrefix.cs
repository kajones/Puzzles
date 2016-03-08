using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Write a function to find the longest common prefix string amongst an array of strings.
    /// </summary>
    [TestFixture]
    public class Problem_0014_LongestCommonPrefix
    {
        [Test]
        [TestCase(new string[0], "")]
        [TestCase(new[] { "hello"}, "hello")]
        [TestCase(new[] { "hello", "there"}, "")]
        [TestCase(new[] { "hello", "hello"}, "hello")]
        [TestCase(new[] { "hello", "help"}, "hel")]
        public void RunTests(string[] strs, string expectedPrefix)
        {
            var prefix = LongestCommonPrefix(strs);
            Assert.That(prefix, Is.EqualTo(expectedPrefix));
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0) return string.Empty;

            var prefix = "";
            var maxPrefixLength = strs.Min(s => s.Length);
            var matching = true;
            while(prefix.Length < maxPrefixLength && matching)
            {
                var candidateChar = strs[0][prefix.Length];
                for(var idx = 1; idx < strs.Length; ++idx)
                {
                    if (strs[idx][prefix.Length] != candidateChar)
                    {
                        matching = false;
                        break;
                    }
                }

                if (matching)
                {
                    prefix += candidateChar;
                }
            }
            return prefix;
        }
    }
}
