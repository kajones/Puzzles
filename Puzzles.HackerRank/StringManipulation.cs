using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HackerRank
{
    [TestFixture]
    public class StringManipulation
    {
        [Test]
        public void FindDeletionsForAnagram()
        {
            var res1 = makeAnagram("cde", "abc");
            Assert.AreEqual(4, res1);

            var res2 = makeAnagram("cde", "dcf");
            Assert.AreEqual(2, res2);

            var sorted1 = string.Concat("fcrxzwscanmligyxyvym".OrderBy(c => c));
            Console.WriteLine(sorted1);

            var sorted2 = string.Concat("jxwtrhvujlmrpdoqbisbwhmgpmeoke".OrderBy(c => c));
            Console.WriteLine(sorted2);

            var res3 = makeAnagram("fcrxzwscanmligyxyvym", "jxwtrhvujlmrpdoqbisbwhmgpmeoke");
            Assert.AreEqual(30, res3);

            //Assert.Fail();
        }

        // Complete the makeAnagram function below.
        static int makeAnagram(string a, string b)
        {
            var lettersA = GetLetters(a);
            var lettersB = GetLetters(b);

            var commonLetters = lettersA.Keys.Intersect(lettersB.Keys);
            var longestAnagram = 0;
            foreach(var letter in commonLetters)
            {
                var min = Math.Min(lettersA[letter], lettersB[letter]);
                longestAnagram += min;
            }
            return (a.Length - longestAnagram) + (b.Length - longestAnagram);
        }

        private static Dictionary<char, int> GetLetters(string s)
        {
            var letters = new Dictionary<char, int>();

            for(var idx = 0; idx < s.Length; ++idx)
            {
                if (!letters.ContainsKey(s[idx])) letters.Add(s[idx], 0);
                letters[s[idx]]++;
            }

            return letters;
        }
    }
}
