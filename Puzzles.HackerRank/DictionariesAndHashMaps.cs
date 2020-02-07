using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace HackerRank
{
    [TestFixture]
    public class DictionariesAndHashMaps
    {
        [Test]
        public void CommonSubstring()
        {
            var res1 = twoStrings("hello", "world");
            res1.Should().Be("YES");

            var res2 = twoStrings("hi", "world");
            res2.Should().Be("NO");
        }

        static string twoStrings(string s1, string s2)
        {
            var chars1 = s1.Select(c => c).ToHashSet();
            var chars2 = s2.Select(c => c).ToHashSet();

            if (chars1.Any(c => chars2.Contains(c)))
                return "YES";

            return "NO";
        }

        [Test]
        public void FindSubstringAnagrams()
        {
            // a/a, b/b, ab/ba, abb/bba
            var res1 = sherlockAndAnagrams("abba");
            res1.Should().Be(4);

            var res2 = sherlockAndAnagrams("abcd");
            res2.Should().Be(0);

            var res3 = sherlockAndAnagrams("ifailuhkqq");
            res3.Should().Be(3);

            var res4 = sherlockAndAnagrams("kkkk");
            res4.Should().Be(10);

            var res5 = sherlockAndAnagrams("cdcd");
            res5.Should().Be(5);

            var t1_1 = sherlockAndAnagrams("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var t1_2 = sherlockAndAnagrams("bbcaadacaacbdddcdbddaddabcccdaaadcadcbddadababdaaabcccdcdaacadcababbabbdbacabbdcbbbbbddacdbbcdddbaaa");
            var t1_3 = sherlockAndAnagrams("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var t1_4 = sherlockAndAnagrams("cacccbbcaaccbaacbbbcaaaababcacbbababbaacabccccaaaacbcababcbaaaaaacbacbccabcabbaaacabccbabccabbabcbba");
            var t1_5 = sherlockAndAnagrams("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var t1_6 = sherlockAndAnagrams("bbcbacaabacacaaacbbcaabccacbaaaabbcaaaaaaaccaccabcacabbbbabbbbacaaccbabbccccaacccccabcabaacaabbcbaca");
            var t1_7 = sherlockAndAnagrams("cbaacdbaadbabbdbbaabddbdabbbccbdaccdbbdacdcabdbacbcadbbbbacbdabddcaccbbacbcadcdcabaabdbaacdccbbabbbc");
            var t1_8 = sherlockAndAnagrams("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var t1_9 = sherlockAndAnagrams("babacaccaaabaaaaaaaccaaaccaaccabcbbbabccbbabababccaabcccacccaaabaccbccccbaacbcaacbcaaaaaaabacbcbbbcc");
            var t1_10 = sherlockAndAnagrams("bcbabbaccacbacaacbbaccbcbccbaaaabbbcaccaacaccbabcbabccacbaabbaaaabbbcbbbbbaababacacbcaabbcbcbcabbaba");

            t1_1.Should().Be(166650);
            t1_2.Should().Be(4832);
            t1_3.Should().Be(166650);
            t1_4.Should().Be(13022);
            t1_5.Should().Be(166650);
            t1_6.Should().Be(9644);
            t1_7.Should().Be(6346);
            t1_8.Should().Be(166650);
            t1_9.Should().Be(8640);
            t1_10.Should().Be(11577);
        }

        // Complete the sherlockAndAnagrams function below.
        static int sherlockAndAnagrams(string s)
        {
            var letters = new Dictionary<string, int>();
            var factorials = new Dictionary<int, BigInteger>();
            var combinations = new Dictionary<int, int>();

            for (var idx = 0; idx < s.Length; ++idx)
            {
                for (var length = 1; length < s.Length - idx + 1; ++length)
                {
                    var substring = s.Substring(idx, length);
                    var sorted = string.Concat(substring.OrderBy(c => c));
                    if (!letters.ContainsKey(sorted)) letters.Add(sorted, 0);
                    letters[sorted]++;
                }
            }

            foreach(var l in letters)
            {
                Console.WriteLine($"{l.Key}: {l.Value}");
            }

            var maxValue = letters.Values.Max();
            BigInteger value = 1;
            for(var number = 1; number <= maxValue; ++number)
            {
                value *= number;
                factorials.Add(number, value);
            }

            combinations.Add(2, 1);
            for (var number = 3; number <= maxValue; ++number)
            {
                var numerator = factorials[number];
                var denominator = 2 * (factorials[number-2]);
                combinations.Add(number, Convert.ToInt32( (numerator / denominator).ToString()));
            }

            return letters.Where(l => l.Value > 1).Sum(l => combinations[l.Value]);
        }

        [Test]
        public void CheckForWordsForNoteInMagazine()
        {
            checkMagazine(new[] {  "Attack", "at", "dawn"}, new[] {  "attack", "at", "dawn"}); // No - case mismatch

            checkMagazine(new[] { "give", "me", "one", "grand", "today", "night" }, new[] { "give", "one", "grand", "today" }); // Yes

            checkMagazine(new[] { "two", "times", "three", "is", "not", "four" }, new[] { "two", "times", "two", "is", "four" }); // No - only one "two" available

            checkMagazine(new[] { "ive", "got", "a", "lovely", "bunch", "of", "coconuts" }, new[] { "ive", "got", "some", "coconuts" }); // No

            checkMagazine(new[] { "give", "me", "one", "grand", "today", "night" }, new[] { "give", "one", "grand", "today" }); // Yes
        }

        // Complete the checkMagazine function below.
        static void checkMagazine(string[] magazine, string[] note)
        {
            var magazineWords = magazine.GroupBy(w => w).Select(g => new { g.Key, Count = g.Count() }).ToDictionary(w => w.Key, w => w.Count);
            var noteWords = note.GroupBy(w => w).Select(g => new { g.Key, Count = g.Count() }).ToDictionary(w => w.Key, w => w.Count);

            var canMake = true;
            foreach(var noteWord in noteWords.Keys)
            {
                if (!magazineWords.ContainsKey(noteWord) || magazineWords[noteWord] < noteWords[noteWord])
                {
                    canMake = false;
                    break;
                }
            }

            if (canMake)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }

        [Test]
        public void CountTripletsInArray()
        {
            // Count the number of triplets within an array that satisfy the geometric progression
            // i.e. multiply a number by the ratio then again to get the third number
            // The entries must also be in sequence within the array
            // i.e. lowest value then middle value then highest value

            // e.g. 1,4,16 is a triplet for ratio 4 and 4,16,64 is another triplet for ratio 4
            var res1 = countTriplets(new List<long> { 1, 4, 16, 64 }, 4);
            res1.Should().Be(2);

            var res2 = countTriplets(new List<long> { 1, 2, 2, 4 }, 2);
            res2.Should().Be(2);

            var res3 = countTriplets(new List<long> { 1, 3, 9, 9, 27, 81 }, 3);
            res3.Should().Be(6);

            var res4 = countTriplets(new List<long> { 1, 5, 5, 25, 125 }, 5);
            res4.Should().Be(4);

            var expt1 = countTriplets(new List<long> { 1, 1, 1 }, 1);
            expt1.Should().Be(1);

            var expt2 = countTriplets(new List<long> { 1, 1, 2, 2, 4 }, 2);
            expt2.Should().Be(4);

            var expt3 = countTriplets(new List<long> { 1234, 1234, 1234, 1234 }, 1);
            expt3.Should().Be(4);

            var expt4 = countTriplets(new List<long> { 1234, 1234, 1234, 1234, 1234 }, 1);
            expt4.Should().Be(10);

            var expt5 = countTriplets(new List<long> { 1234, 1234, 1234, 1234, 1234, 1234 }, 1);
            expt5.Should().Be(20);

            var expt6 = countTriplets(new List<long> { 1, 1, 1, 3, 3, 9, 9, 9, 27 }, 3);
            expt6.Should().Be(24);

            // Index 0, 1, 4; 0, 3, 4; 2, 3, 4 but NOT 2, 1, 4
            var expt7 = countTriplets(new List<long> { 1, 2, 1, 2, 4 }, 2);
            expt7.Should().Be(3);

            // Test Case 2
            // Factors of 1617 are 1,3,7,11,21,33,49,77,147,231,539,1617
            var aHundredOnes = Enumerable.Repeat<long>(1, 100).ToList();
            Assert.AreEqual(100, aHundredOnes.Count);
            var testCase2Result = countTriplets(aHundredOnes, 1);
            testCase2Result.Should().Be(161700);

            // Test Case 3 - 100,000 long 1237 factor 1
            var hundredThousandSame = Enumerable.Repeat<long>(1237, 100000).ToList();
            Assert.AreEqual(100000, hundredThousandSame.Count);
            var testCase3Result = countTriplets(hundredThousandSame, 1);
            testCase3Result.Should().Be(166661666700000);


            // Test Case 6
            var testCase6Line = FileHelper.GetFileContent(@"Files/CountTriplets_TestCase6.txt");
            var testCase6 = testCase6Line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt64(n)).ToList();
            var testCase6Result = countTriplets(testCase6, 3);
            testCase6Result.Should().Be(2325652489);
        }

        static long countTriplets(List<long> arr, long r)
        {
            var potentialTriplets = new Dictionary<long, long>();
            var actualTriplets = new Dictionary<long, long>();

            long numberOfTriplets = 0;

            foreach(var item in arr)
            {
                var prevItem = item / r;

                if (item % r == 0)
                {
                    if (actualTriplets.ContainsKey(prevItem))
                    {
                        numberOfTriplets += actualTriplets[prevItem];
                    }

                    if (potentialTriplets.ContainsKey(prevItem))
                    {
                        var countPrev = potentialTriplets[prevItem];
                        if (!actualTriplets.ContainsKey(item)) actualTriplets.Add(item, 0);
                        actualTriplets[item] += countPrev;
                    }
                }

                if (!potentialTriplets.ContainsKey(item)) potentialTriplets.Add(item, 0);
                potentialTriplets[item]++;
            }

            return numberOfTriplets;
        }
    }
}
