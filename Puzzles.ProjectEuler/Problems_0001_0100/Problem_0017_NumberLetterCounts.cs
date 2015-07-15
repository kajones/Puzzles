using System;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// If the numbers 1 to 5 are written out in words: one, two, three, four, five, 
    /// then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
    ///
    /// If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?
    ///
    /// NOTE: Do not count spaces or hyphens. 
    /// For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. 
    /// The use of "and" when writing out numbers is in compliance with British usage.
    /// </summary>
    [TestFixture]
    public class Problem_0017_NumberLetterCounts
    {
        [Test]
        [TestCase("two", 3)]
        [TestCase("three", 5)]
        [TestCase("four", 4)]
        [TestCase("one hundred and one", 16)]
        [TestCase("one hundred and fifteen", 20)]
        [TestCase("two hundred", 10)]
        [TestCase("three hundred and forty two", 23)]
        public void CanCountLettersInNumberDescriptionsIgnoringSeparators(string numberDescription, long expectedLetterCount)
        {
            var letterCount = CountLetters(numberDescription);

            Assert.AreEqual(expectedLetterCount, letterCount);
        }

        /// <summary>
        /// 21124
        /// </summary>
        [Test, Explicit]
        public void CountTheNumberOfLettersInWordDescriptionsUpToOneThousand()
        {
            long totalLetters = 0;

            for (var i = 1; i <= 1000; ++i)
            {
                var numberDescription = NumberAsWordsHelper.GetDescription(i);
                var letterCount = CountLetters(numberDescription);
                totalLetters += letterCount;
            }

            Console.WriteLine(totalLetters);
        }

        private long CountLetters(string numberDescription)
        {
            var text = numberDescription.Replace(" ", "").Replace("-", "");

            return text.Trim().Length;
        }

    }
}
