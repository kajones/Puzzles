using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// By replacing each of the letters in the word CARE with 1, 2, 9, and 6 respectively, we form a square number: 1296 = 36^2. 
    /// What is remarkable is that, by using the same digital substitutions, the anagram, RACE, also forms a square number: 9216 = 96^2. 
    /// We shall call CARE (and RACE) a square anagram word pair and specify further that leading zeroes are not permitted, 
    /// neither may a different letter have the same digital value as another letter.
    ///
    /// Using words.txt (right click and 'Save Link/Target As...'), a 16K text file containing nearly two-thousand common English words, 
    /// find all the square anagram word pairs (a palindromic word is NOT considered to be an anagram of itself).
    /// 
    /// What is the largest square number formed by any member of such a pair?
    ///
    /// NOTE: All anagrams formed must be contained in the given text file.
    /// </summary>
    [TestFixture]
    public class Problem_0098_AnagramicSquares
    {
        private const string fileName = "Puzzles.ProjectEuler.DataFiles.Problem_0098_words.txt";

        private HashSet<string> squaresSet;

            /// <summary>
        /// 14: ADMINISTRATION
        /// </summary>
        [Test, Ignore("Ignore this")]
        public void FindLongestWord()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent(fileName);
            var words = fileContent.Split(',');
            var noQuote = words.Select(word => word.Replace(@"""", "")).ToList();

            var longest = noQuote.Max(w => w.Length);
            var sampleLongword = noQuote.FirstOrDefault(w => w.Length == longest);
            Console.WriteLine("{0}: {1}", longest, sampleLongword);
        }

        /// <summary>
        /// What is the longest word that could be a square?
        /// CDEINORTU
        /// </summary>
        [Test, Ignore("Ignore this")]
        public void FindLongestAnagramKey()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent(fileName);
            var words = fileContent.Split(',');
            var noQuote = words.Select(word => word.Replace(@"""", "")).ToList();
            var candidateAnagrams = noQuote.Select(noQuoteWord => new Anagram(noQuoteWord)).ToList();
            var anagramGroups = candidateAnagrams.GroupBy(a => a.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .OrderByDescending(g => g.Length);

            Console.WriteLine(anagramGroups.First());
        }

        /// <summary>
        /// Largest group: Key:OPST Anagrams:POST,SPOT,STOP
        /// </summary>
        [Test, Ignore("Ignore this")]
        public void ConfirmIfAnyAnagramGroupHasMoreThanTwoEntries()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent(fileName);
            var words = fileContent.Split(',');
            var noQuote = words.Select(word => word.Replace(@"""", "")).ToList();
            var candidateAnagrams = noQuote.Select(noQuoteWord => new Anagram(noQuoteWord)).ToList();
            var anagramGroups =
                candidateAnagrams.GroupBy(a => a.Key)
                    .Select(g => new {g.Key, Anagrams = g.ToList()})
                    .OrderByDescending(g => g.Anagrams.Count);

            var largestGroup = anagramGroups.First();
            Console.WriteLine("Largest group: Key:{0} Anagrams:{1}", largestGroup.Key, string.Join(",", largestGroup.Anagrams.Select(a => a.Word)));
        }

        [Test, Ignore("Ignore this")]
        public void ConfirmSquareTextPopulation()
        {
            var squaresAsText = GetSquaresAsText();
            squaresAsText.Should().Contain("1");
            squaresAsText.Should().Contain("4");
            squaresAsText.Should().Contain("25");
            squaresAsText.Should().Contain("100");
            squaresAsText.Should().Contain("40000");
        }

        [Test]
        public void ConfirmAreAnagrams()
        {
            var careWord = new Anagram("CARE");
            var raceWord = new Anagram("RACE");

            careWord.Key.Should().Be("ACER");
            raceWord.Key.Should().Be("ACER");

            var areAnagrams = AreAnagrams(careWord, raceWord);
            areAnagrams.Should().BeTrue();
        }

        [Test]
        public void FindValidMapsForWordsToMakeSquares()
        {
            squaresSet = GetSquaresAsText();

            var careMaps = GetCharacterMaps("CARE");
            var foundExpectedMap = false;

            foreach (var careMap in careMaps)
            {
                if (careMap['C'] != 1) continue;
                if (careMap['A'] != 2) continue;
                if (careMap['R'] != 9) continue;
                if (careMap['E'] != 6) continue;

                foundExpectedMap = true;
                break;
            }

            foundExpectedMap.Should().BeTrue();

            var raceMaps = GetCharacterMaps("RACE");
            foundExpectedMap = false;
            foreach (var raceMap in raceMaps)
            {
                if (raceMap['C'] != 1) continue;
                if (raceMap['A'] != 2) continue;
                if (raceMap['R'] != 9) continue;
                if (raceMap['E'] != 6) continue;

                foundExpectedMap = true;
                break;
            }

            foundExpectedMap.Should().BeTrue();
        }

        /// <summary>
        /// Prebuild a list of valid squares (so not constantly checking if a number is a square)
        /// Group the words into anagram groups (share common letters into a key)
        /// For each word in an anagram group find the valid digit substitutions that create a square number
        /// Then cross-match between words in an anagram group to find common digit substitution maps
        /// Then find the highest square value in an anagramic square pair
        /// 
        /// 18769
        /// </summary>
        [Test, Explicit]
        public void FindLargestAnagramSquareValue()
        {
            squaresSet = GetSquaresAsText();

            var fileContent = FileHelper.GetEmbeddedResourceContent(fileName);
            var words = fileContent.Split(',');
            var noQuote = words.Select(word => word.Replace(@"""", "")).ToList();
            var candidateAnagrams = noQuote.Select(noQuoteWord => new Anagram(noQuoteWord)).ToList();
            var anagramGroups = candidateAnagrams.GroupBy(a => a.Key)
                .Where(g => g.Count() > 1)
                .Select(g => new { g.Key, Anagrams = g.ToList() });

            int largestValue = 0;

            foreach (var anagramGroup in anagramGroups)
            {
                var wordCharacterMaps = new Dictionary<string, List<Dictionary<char, int>>> ();
                foreach (var anagram in anagramGroup.Anagrams)
                {
                    var validMaps = GetCharacterMaps(anagram.Word);
                    wordCharacterMaps.Add(anagram.Word, validMaps);
                }

                var keyList = wordCharacterMaps.Keys.ToList();

                for (var firstIdx = 0; firstIdx < keyList.Count; ++ firstIdx)
                {
                    for (var secondIdx = (firstIdx + 1); secondIdx < keyList.Count; ++ secondIdx)
                    {
                        var firstMaps = wordCharacterMaps[keyList[firstIdx]];
                        var secondMaps = wordCharacterMaps[keyList[secondIdx]];

                        var matchingMaps = GetMatchingMaps(firstMaps, secondMaps);
                        foreach (var matchingMap in matchingMaps)
                        {
                            var valueForWord = GetValue(keyList[firstIdx], matchingMap);
                            if (valueForWord > largestValue)
                            {
                                largestValue = valueForWord;
                            }
                            valueForWord = GetValue(keyList[secondIdx], matchingMap);
                            if (valueForWord > largestValue)
                            {
                                largestValue = valueForWord;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Largest value is: {0}", largestValue);
            largestValue.Should().Be(18769);
        }

        private static IEnumerable<Dictionary<char, int>> GetMatchingMaps(IEnumerable<Dictionary<char, int>> firstMaps, List<Dictionary<char, int>> secondMaps)
        {
            var matchingMaps = new List<Dictionary<char, int>>();
            foreach (var firstMap in firstMaps)
            {
                foreach (var secondMap in secondMaps)
                {
                    var match = true;

                    foreach (var firstKey in firstMap.Keys)
                    {
                        if (firstMap[firstKey] == secondMap[firstKey]) continue;
                        match = false;
                        break;
                    }

                    if (match)
                    {
                        matchingMaps.Add(firstMap);
                    }
                }
            }

            return matchingMaps;
        }

        private static int GetValue(string word, IReadOnlyDictionary<char, int> matchingMap)
        {
            var value = "";

            foreach (var c in word)
            {
                value += matchingMap[c];
            }

            return Convert.ToInt32(value);
        }

        private static bool AreAnagrams(Anagram first, Anagram second)
        {
            return first.Key == second.Key && !string.IsNullOrEmpty(first.Key);
        }

        private static HashSet<string> GetSquaresAsText()
        {
            var results = new HashSet<string>();
            var currentNumber = 1;
            var squareAsText = GetSquareAsText(currentNumber);
            while (squareAsText.Length <= 8)
            {
                results.Add(squareAsText);
                currentNumber++;
                squareAsText = GetSquareAsText(currentNumber);
            }

            return results;
        }

        private static string GetSquareAsText(int currentNumber)
        {
            var square = currentNumber*currentNumber;
            return square.ToString(CultureInfo.InvariantCulture);
        }

        private List<Dictionary<char, int>> GetCharacterMaps(string word)
        {
            var wordLength = word.Length;
            var squaresOfSameLength = squaresSet.Where(s => s.Length == wordLength);
            var wordCharacters = word.ToCharArray();

            var maps = new List<Dictionary<char, int>>();
            foreach (var square in squaresOfSameLength)
            {
                var map = new Dictionary<char, int>();
                var allMapped = true;
                
                for (var idx = 0; idx < wordLength; ++idx)
                {
                    var currentChar = wordCharacters[idx];
                    var proposedDigit = (int) Char.GetNumericValue(square[idx]);
                    //int proposedDigit = square[idx];

                    // Cannot map a character to two different digits
                    if (map.ContainsKey(currentChar))
                    {
                        if (map[currentChar] == proposedDigit) continue;
                        allMapped = false;
                        break;
                    }

                    // Cannot re-use a digit
                    if (map.ContainsValue(proposedDigit))
                    {
                        allMapped = false;
                        break;
                    }

                    map.Add(currentChar, proposedDigit);
                }

                if (allMapped)
                {
                    maps.Add(map);
                }
            }
            return maps;
        }

        internal class Anagram
        {
            internal string Key { get; private set; }
            internal string Word { get; private set; }
            internal int Length { get; private set; }
            internal char[] DistinctLetters { get; private set; }

            public Anagram(string word)
            {
                Word = word;
                Length = word.Length;

                var lettersInWord = word.ToCharArray();
                DistinctLetters = lettersInWord.Distinct().ToArray();
                var sortedLetters = lettersInWord.OrderBy(c => c);
                Key = string.Concat(sortedLetters);
            }
        }

        internal class SquareAnagram
        {
            internal int SquareNumber { get; private set; }
            internal string Substitution { get; private set; }

            public SquareAnagram(int squareNumber, string substitution)
            {
                SquareNumber = squareNumber;
                Substitution = substitution;
            }
        }
    }
}
