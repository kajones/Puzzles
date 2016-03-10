using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a digit string, return all possible letter combinations that the number could represent.
    ///
    /// A mapping of digit to letters (just like on the telephone buttons) is given below.
    ///
    /// 1: 
    /// 2: abc
    /// 3: def
    /// 4: ghi
    /// 5: jkl
    /// 6: mno
    /// 7: pqrs
    /// 8: tuv
    /// 9: wxyz
    ///
    /// Input:Digit string "23"
    /// Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
    /// Note:
    /// Although the above answer is in lexicographical order, your answer could be in any order you want.
    /// </summary>
    [TestFixture]
    public class Problem_0017_LetterCombinationsOfAPhoneNumber
    {
        private Dictionary<char, List<char>> map = new Dictionary<char, List<char>>();

        [Test]
        [TestCase("23", new[] {"ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"})]
        public void RunExamples(string digits, string[] expectedLetterCombinations)
        {
            var actualCombinations = LetterCombinations(digits);
            foreach(var expectedCombination in expectedLetterCombinations)
            {
                Assert.That(actualCombinations.Contains(expectedCombination), expectedCombination);
            }
            Assert.That(actualCombinations.Count, Is.EqualTo(expectedLetterCombinations.Length), "Number of combinations");
        }

        [Test]
        [TestCase("", new string[] { })]
        [TestCase("2", new[] { "a", "b", "c"})]
        public void RunTests(string digits, string[] expectedLetterCombinations)
        {
            var actualCombinations = LetterCombinations(digits);
            foreach (var expectedCombination in expectedLetterCombinations)
            {
                Assert.That(actualCombinations.Contains(expectedCombination), expectedCombination);
            }
            Assert.That(actualCombinations.Count, Is.EqualTo(expectedLetterCombinations.Length), "Number of combinations");
        }

        public IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits)) return new List<string>();

            var sourceLetters = GetSourceLetters(digits);
            
            var combinations = new List<string>();
            GenerateCombination(combinations, sourceLetters, "", 0);
            return combinations;
        }

        public void GenerateCombination(List<string> combinations, List<IList<char>> sourceLetters, string prefix, int position)
        {
            if (prefix.Length == sourceLetters.Count)
            {
                combinations.Add(prefix);
                return;
            }

            var currentLetters = sourceLetters[position];
            foreach(var letter in currentLetters)
            {
                GenerateCombination(combinations, sourceLetters, prefix + letter, position + 1);
            }
        }

        public List<IList<char>> GetSourceLetters(string digits)
        {
            PopulateMap();
            var sourceLetters = new List<IList<char>>();

            for (var idx = 0; idx < digits.Length; ++idx)
            {
                sourceLetters.Add(LettersFor(digits[idx]));
            }

            return sourceLetters;
        }

        public void PopulateMap()
        {
            map.Add('1', new List<char>());
            map.Add('2', new List<char> { 'a', 'b', 'c' });
            map.Add('3', new List<char> { 'd', 'e', 'f' });
            map.Add('4', new List<char> { 'g', 'h', 'i' });
            map.Add('5', new List<char> { 'j', 'k', 'l' });
            map.Add('6', new List<char> { 'm', 'n', 'o' });
            map.Add('7', new List<char> { 'p', 'q', 'r', 's' });
            map.Add('8', new List<char> { 't', 'u', 'v' });
            map.Add('9', new List<char> { 'w', 'x', 'y', 'z' });
            map.Add('0', new List<char> { ' ' });
        }

        public IList<char> LettersFor(char digit)
        {
            if (!map.ContainsKey(digit)) return new List<char>();

            return map[digit];
        }
    }
}
