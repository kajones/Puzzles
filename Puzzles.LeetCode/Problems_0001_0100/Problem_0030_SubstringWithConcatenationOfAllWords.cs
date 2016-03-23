using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// You are given a string, s, and a list of words, 
    /// words, that are all of the same length. 
    /// Find all starting indices of substring(s) in s that is a concatenation of each word in words 
    /// exactly once and without any intervening characters.
    ///
    /// For example, given:
    /// s: "barfoothefoobarman"
    /// words: ["foo", "bar"]
    ///
    /// You should return the indices: [0,9].
    /// (order does not matter).
    /// </summary>
    [TestFixture]
    public class Problem_0030_SubstringWithConcatenationOfAllWords
    {
        [Test]
        public void RunExample()
        {
            var result = FindSubstring("barfoothefoobarman", new[] { "foo", "bar" });
            Assert.That(result.Contains(0), Is.True);
            Assert.That(result.Contains(9), Is.True);
            Assert.That(result.Count, Is.EqualTo(2));

            result.Should().BeEquivalentTo(new List<int> { 0, 9 });
        }

        [Test]
        [TestCase("barfoofoobarthefoobarman", new[] { "bar", "foo", "the" }, new[] { 6, 9, 12})]
        [TestCase("wordgoodgoodgoodbestword", new[] {"word","good","best","good"}, new[] {8})]
        [TestCase("aaaaaaaa", new[] {"aa","aa","aa"}, new[] {0,1,2})]
        public void RunTests(string s, string[] words, int[] expectedPositions)
        {
            var positions = FindSubstring(s, words);

            positions.Should().BeEquivalentTo(expectedPositions);
        }

        public IList<int> FindSubstring(string s, string[] words)
        {
            if (words.Length == 0) return new List<int>();
            if (string.IsNullOrEmpty(s)) return new List<int>();

            var requiredWordSet = GenerateDictionary(words);
            var wordSet = new Dictionary<string, int>(requiredWordSet);
            var wordLength = words[0].Length;
            var substringLength = wordLength * words.Length;

            var positions = new List<int>();
            for(var idx = 0; idx < (s.Length - substringLength + 1); ++idx)
            {
                var candidateSubstring = s.Substring(idx, wordLength);
                if (!requiredWordSet.ContainsKey(candidateSubstring)) continue;

                foreach(var key in requiredWordSet.Keys)
                {
                    wordSet[key] = 0;
                }
                wordSet[candidateSubstring] = 1;
                for (var offset = idx + wordLength; offset < (idx + substringLength); offset += wordLength)
                {
                    var nextWord = s.Substring(offset, wordLength);
                    if (!wordSet.ContainsKey(nextWord)) break;

                    wordSet[nextWord] = wordSet[nextWord]+1;
                }
                var isMatch = true;
                foreach(var key in requiredWordSet.Keys)
                {
                    if (wordSet[key] != requiredWordSet[key])
                    {
                        isMatch = false;
                        break;
                    }
                }
                if (isMatch)
                {
                    positions.Add(idx);
                }
            }
            return positions;
        }

        private static Dictionary<string, int> GenerateDictionary(IEnumerable<string> words)
        {
            var dictionary = new Dictionary<string, int>();
            foreach(var word in words)
            {
                if (dictionary.ContainsKey(word))
                {
                    dictionary[word]++;
                    continue;
                }
                dictionary.Add(word, 1);
            }
            return dictionary;
        }
    }
}
