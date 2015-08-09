using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class CombinationGeneration
    {
        [Test]
        public void ExtractCombinationsOfThreeElementsFromFive()
        {
            var generator = new CombinationGenerator();
            var combinations = generator.GetCombinations(new List<string> { "A", "B", "C", "D", "E" }, 3).ToList();

            Assert.AreEqual(10, combinations.Count(), "Ten ways to do this");

            // ABC
            // ABD
            // ABE
            // ACD
            // ACE
            // ADE
            // BCD
            // BCE
            // BDE
            // CDE

            var list = combinations.Select(combination => string.Concat(combination)).ToList();

            Assert.IsTrue(list.Contains("ABC"));
            Assert.IsTrue(list.Contains("ABD"));
            Assert.IsTrue(list.Contains("ABE"));
            Assert.IsTrue(list.Contains("ACD"));
            Assert.IsTrue(list.Contains("ACE"));
            Assert.IsTrue(list.Contains("ADE"));
            Assert.IsTrue(list.Contains("BCD"));
            Assert.IsTrue(list.Contains("BCE"));
            Assert.IsTrue(list.Contains("BDE"));
            Assert.IsTrue(list.Contains("CDE"));
        }
    }
}
