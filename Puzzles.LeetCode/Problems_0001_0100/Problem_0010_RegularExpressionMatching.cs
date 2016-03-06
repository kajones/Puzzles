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
    /// Implement regular expression matching with support for '.' and '*'.
    /// 
    /// '.' Matches any single character.
    /// '*' Matches zero or more of the preceding element.
    ///
    /// The matching should cover the entire input string (not partial).
    ///
    /// The function prototype should be:
    /// bool isMatch(const char *s, const char *p)
    ///
    /// Some examples:
    /// isMatch("aa","a") → false
    /// isMatch("aa","aa") → true
    /// isMatch("aaa","aa") → false
    /// isMatch("aa", "a*") → true
    /// isMatch("aa", ".*") → true
    /// isMatch("ab", ".*") → true
    /// isMatch("aab", "c*a*b") → true
    /// </summary>
    [TestFixture]
    public class Problem_0010_RegularExpressionMatching
    {
        [Test]
        [TestCase("aa","a", false)]
        [TestCase("aa","aa", true)]
        [TestCase("aaa","aa", false)]
        [TestCase("aa", "a*", true)]
        [TestCase("aa", ".*", true)]
        [TestCase("ab", ".*", true)]
        [TestCase("aab", "c*a*b", true)]
        public void RunExamples(string toSearch, string searchPattern, bool expectedResult)
        {
            var actualResult = IsMatch(toSearch, searchPattern);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("aa", ".", false, "Wildcard is for a single character so mismatch")]
        [TestCase("a", ".*..a*", false, "Need at least two characters before a but missing")]
        [TestCase("a", ".*.a*", true, "Can ignore the two * and the . matches against a")]
        [TestCase("a", ".*a*", true, "Can ignore the .* since * means zero or more")]
        [TestCase("ab", "..", true, "Need any two characters")]
        [TestCase("a", "..", false, "Need two characters and only one provided")]
        [TestCase("abc", "..", false, "Need exactly two characters and three provided")]
        [TestCase("a", ".*.", true, "Can ignore the .* and it matches any single character")]
        [TestCase("ab", ".*.", true, "Zero to many wildcard then a single character")]
        [TestCase("abc", ".*.", true, "Zero to many wildcard then a single character")]
        [TestCase("ab", ".*..", true, "Zero to many wildcard then two characters matches any two")]
        [TestCase("abc", ".*..", true, "Zero to many wildcard then two characters matches any three")]
        [TestCase("abc", ".*..c", true, "Zero to many wildcard then two characters then c matches ..c")]
        [TestCase("abbbbbc", ".*..c", true, "Zero to many wildcard then two characters then c matches any with at least three characters ending c")]
        [TestCase("bb", "..*c", false, "Search does not include c")]
        [TestCase("aaa", "a*a", true, "Multiple matching characters followed by same character")]
        [TestCase("aac", "a*c", true, "Multiple matching characters followed by different character")]
        [TestCase("aaa", "ab*a*c*a", true, "Can ignore wildcards of specified characters")]
        [TestCase("aa", "ab*a*c*a", true, "Can ignore wildcards of specified characters")]
        [TestCase("a", "", false, "No pattern so no match")]
        [TestCase("", "", true, "No search no pattern")]
        public void RunAdditionalTests(string toSearch, string searchPattern, bool expectedResult, string message)
        {
            var actualResult = IsMatch(toSearch, searchPattern);
            Assert.That(actualResult, Is.EqualTo(expectedResult), message);
        }

        public bool IsMatch(string s, string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return string.IsNullOrEmpty(s);
            }
            var matchers = GenerateMatchers(p);
            var matchingPositions = ApplyPattern(s, 0, matchers, 0);
            return matchingPositions.Any(l => l == s.Length);
        }

        private IEnumerable<int> ApplyPattern(string search, int start, IList<IMatcher> matchers, int currentMatcher)
        {
            var nextPositions = matchers[currentMatcher].NextPosition(search, start);

            if (currentMatcher == matchers.Count - 1)
            {
                foreach (var np in nextPositions)
                {
                    yield return np;
                }
                yield break;
            }

            foreach(var np in nextPositions)
            {
                var nextMatches = ApplyPattern(search, np, matchers, currentMatcher + 1);
                foreach(var nextMatch in nextMatches)
                {
                    yield return nextMatch;
                }
            }
        }

        private IList<IMatcher> GenerateMatchers(string p)
        {
            var matchers = new List<IMatcher>();
            for (var idx = 0; idx < p.Length; ++idx)
            {
                var currChar = p[idx];

                if (idx < (p.Length - 1))
                {
                    if (p[idx+1] == '*')
                    {
                        if (currChar == '.')
                        {
                            matchers.Add(new WildcardInstanceMatcher());
                        }
                        else
                        {
                            matchers.Add(new CharacterInstanceMatcher(currChar));
                        }
                        idx++;
                        continue;
                    }
                }

                if (currChar == '.')
                {
                    matchers.Add(new SingleWildcardMatcher());
                }
                else
                {
                    matchers.Add(new SingleCharacterMatcher(currChar));
                }
            }
            return matchers;
        }

        [Test]
        [TestCase("abcdef", 3, 'd', new [] { 4 }, "Next is matching char")]
        [TestCase("abcdef", 3, 'f', new int[] { }, "Next is wrong char")]
        [TestCase("abcddef", 3, 'd', new[] { 4}, "Next is matching char - don't progress to further match")]
        [TestCase("abcd", 3, 'd', new int[] { 4 }, "At end of search so no match")]
        [TestCase("aa", 1, 'a', new[] { 2}, "Matched final character so next position")]
        [TestCase("aab", 2, 'b', new[] { 3}, "Matched final character so next position")]
        [TestCase("aab", 1, 'b', new int[] { }, "No match at position 1")]
        [TestCase("aab", 0, 'b', new int[] { }, "No match at position 0")]
        [TestCase("bb", 1, 'c', new int[] { }, "No match at final position")]
        public void MatchSingleCharacter(string search, int start, char character, int[] expectedNextPositions, string message)
        {
            var matcher = new SingleCharacterMatcher(character);
            var nextPositions = matcher.NextPosition(search, start);

            nextPositions.Should().BeEquivalentTo(expectedNextPositions, message);
        }

        [Test]
        [TestCase("abc", 2, new[] { 3 }, "At end so match - onto next position")]
        [TestCase("abc", 1, new[] { 2 }, "Penultimate so matches anything")]
        [TestCase("a", 0, new[] { 1}, "Single char match - move to end")]
        [TestCase("a", 1, new int[] { }, "Already beyond end - fail")]
        public void MatchSingleWildcardCharacter(string search, int start, int[] expectedNextPositions, string message)
        {
            var matcher = new SingleWildcardMatcher();
            var nextPositions = matcher.NextPosition(search, start);

            nextPositions.Should().BeEquivalentTo(expectedNextPositions, message);
        }

        [Test]
        [TestCase("abcd", 3, 'd', new int[] { 3, 4}, "Match zero i.e. return same position")]
        [TestCase("abcd", 2, 'd', new int[] { 2}, "Match zero within search i.e. return same position")]
        [TestCase("abcc", 2, 'c', new int[] { 2,3,4}, "Match zero and one instance to end of string")]
        [TestCase("bbb", 0, 'b', new int[] { 0,1,2,3}, "Match every position in string")]
        [TestCase("abccd", 2, 'c', new[] { 2,3, 4}, "Match two c within string but different character stops run")]
        [TestCase("aab", 0, 'a', new[] { 0, 1, 2 }, "Matches first two positions")]
        public void MatchZeroOrMoreSingleCharacter(string search, int start, char character, int[] expectedNextPositions, string message)
        {
            var matcher = new CharacterInstanceMatcher(character);
            var nextPositions = matcher.NextPosition(search, start);

            nextPositions.Should().BeEquivalentTo(expectedNextPositions, message);
        }

        [Test]
        [TestCase("abcd", 0, new[] { 0,1,2,3, 4}, "Whole of string")]
        [TestCase("abcd", 2, new[] { 2,3, 4}, "From defined start to end of string")]
        [TestCase("abcd", 3, new[] { 3, 4}, "End of string")]
        public void MatchZeroOrMoreWildcardCharacters(string search, int start, int[] expectedNextPositions, string message)
        {
            var matcher = new WildcardInstanceMatcher();
            var nextPositions = matcher.NextPosition(search, start);

            nextPositions.Should().BeEquivalentTo(expectedNextPositions, message);
        }
    }



    public interface IMatcher
    {
        IEnumerable<int> NextPosition(string search, int start);
    }

    public class SingleCharacterMatcher : IMatcher
    {
        private char character;

        public SingleCharacterMatcher(char character)
        {
            this.character = character;
        }

        public IEnumerable<int> NextPosition(string search, int start)
        {
            if (start >= search.Length) return new int[] { };

            if (search[start] == character) return new[] { start + 1 };

            return new int[] { };
        }
    }

    public class SingleWildcardMatcher : IMatcher
    {

        public IEnumerable<int> NextPosition(string search, int start)
        {
            if (start >= search.Length) return new int[] { };
            if (start == search.Length - 1) return new int[] { search.Length};

            return new[] { start + 1 };
        }
    }

    public class CharacterInstanceMatcher : IMatcher
    {
        private char character;

        public CharacterInstanceMatcher(char character)
        {
            this.character = character;
        }

        public IEnumerable<int> NextPosition(string search, int start)
        {
            var matches = new List<int>();
            var finalCharMatch = true;
            for(var idx = start; idx < search.Length; ++idx)
            {
                if (search[idx] == character)
                {
                    matches.Add(idx);
                    continue;
                }

                finalCharMatch = false;
                matches.Add(idx);
                break;
            }

            if (matches.Count == 0)
            {
                matches.Add(start);
            }
            if (finalCharMatch)
            {
                matches.Add(search.Length);
            }

            return matches;
        }
    }

    public class WildcardInstanceMatcher : IMatcher
    {

        public IEnumerable<int> NextPosition(string search, int start)
        {
            var matches = new List<int>();
            for (var idx = start; idx <= search.Length; ++idx)
            {
                matches.Add(idx);
            }
            return matches;
        }
    }
}
