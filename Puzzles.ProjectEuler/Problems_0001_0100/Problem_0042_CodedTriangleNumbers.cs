using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The nth term of the sequence of triangle numbers is given by, tn = ½n(n+1); so the first ten triangle numbers are:
    ///
    /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
    ///
    /// By converting each letter in a word to a number corresponding to its alphabetical position 
    /// and adding these values we form a word value. For example, the word value for SKY is 19 + 11 + 25 = 55 = t10. 
    /// If the word value is a triangle number then we shall call the word a triangle word.
    ///
    /// Using words.txt (right click and 'Save Link/Target As...'), a 16K text file containing nearly two-thousand common English words, 
    /// how many are triangle words?
    [TestFixture]
    public class Problem_0042_CodedTriangleNumbers
    {
        private readonly List<int> numbers = new List<int>();

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            for (var n = 1; n < 1000; ++n)
            {
                double triangle = (n * (n + 1)) / 2;
                numbers.Add((int)triangle);
            }
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(3, true)]
        [TestCase(6, true)]
        [TestCase(10, true)]
        [TestCase(15, true)]
        [TestCase(21, true)]
        [TestCase(28, true)]
        [TestCase(36, true)]
        [TestCase(45, true)]
        [TestCase(55, true)]
        [TestCase(2, false)]
        [TestCase(4, false)]
        public void ConfirmIsTriangleNumber(int candidate, bool expectedResult)
        {
            var result = IsTriangleNumber(candidate);
            Assert.AreEqual(expectedResult, result);

            //Console.WriteLine("High: {0}", numbers.Max());
        }

        /// <summary>
        /// 162
        /// </summary>
        [Test, Explicit]
        public void CountTriangleWords()
        {
            var content = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0042_words.txt");
            content.Should().NotBeEmpty();

            var count = 0;

            foreach (var word in content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var cleanWord = word.Replace(@"""", "");
                var wordValue = LetterHelper.GetValue(cleanWord);
                if (IsTriangleNumber((int)wordValue))
                {
                    count++;
                }
            }

            Console.WriteLine("{0} words are triangle words", count);

            count.Should().Be(162);
        }

        private bool IsTriangleNumber(int candidate)
        {
            return numbers.Contains(candidate);
        }
    }
}
