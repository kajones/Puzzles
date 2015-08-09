using System;
using System.Collections.Generic;
using System.Xml.Schema;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// For a number written in Roman numerals to be considered valid there are basic rules which must be followed. 
    /// Even though the rules allow some numbers to be expressed in more than one way there is always a "best" way of writing a particular number.
    ///
    /// For example, it would appear that there are at least six ways of writing the number sixteen:
    ///
    /// IIIIIIIIIIIIIIII
    /// VIIIIIIIIIII
    /// VVIIIIII
    /// XIIIIII
    /// VVVI
    /// XVI
    ///
    /// However, according to the rules only XIIIIII and XVI are valid, and the last example is considered to be the most efficient, 
    /// as it uses the least number of numerals.
    ///
    /// The 11K text file, roman.txt (right click and 'Save Link/Target As...'), contains one thousand numbers written in valid, 
    /// but not necessarily minimal, Roman numerals; see About... Roman Numerals for the definitive rules for this problem.
    ///
    /// Find the number of characters saved by writing each of these in their minimal form.
    ///
    /// Note: You can assume that all the Roman numerals in the file contain no more than four consecutive identical units.
    /// </summary>
    [TestFixture]
    public class Problem_0089_RomanNumerals
    {
        private static List<RomanNumeralSubstitution> substitutions;

        /// <summary>
        /// 743 characters saved
        /// </summary>
        [Test, Explicit]
        public void CompareRomanRepresentationsFromTheFile()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0089_RomanNumerals.txt");
            var romanNumerals = fileContent.Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            romanNumerals.Length.Should().Be(1000);

            var charactersSaved = 0;

            foreach (var romanNumeral in romanNumerals)
            {
                var digitalEquivalent = RomanNumeralGenerator.GetDigitalRepresentation(romanNumeral);
                var minimalRoman = RomanNumeralGenerator.GetRomanRepresentation(digitalEquivalent);

                var originalLength = romanNumeral.Length;
                var minimalLength = minimalRoman.Length;
                if (minimalLength > originalLength)
                {
                    Assert.Fail("Need to check {0} {1}", digitalEquivalent, romanNumeral);
                }
                else
                {
                    var diff = originalLength - minimalLength;
                    charactersSaved += diff;
                }
            }

            Console.WriteLine("Characters saved: {0}", charactersSaved);
            charactersSaved.Should().Be(743);
        }

        [Test, Explicit]
        public void CountSavingsBySubstitution()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0089_RomanNumerals.txt");
            var romanNumerals = fileContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            romanNumerals.Length.Should().Be(1000);

            var savingsCount = 0;
            PopulateSavingsSet();

            foreach (var romanNumeral in romanNumerals)
            {
                savingsCount += GetSubstitutedSavings(romanNumeral);
            }

            Console.WriteLine("Savings: {0}", savingsCount);
            savingsCount.Should().Be(743);
        }

        private static int GetSubstitutedSavings(string romanNumeral)
        {
            var savingsCount = 0;

            foreach (var romanNumeralSubstitution in substitutions)
            {
                if (romanNumeral.IndexOf(romanNumeralSubstitution.From, StringComparison.Ordinal) <= -1) continue;

                romanNumeral = romanNumeral.Replace(romanNumeralSubstitution.From, romanNumeralSubstitution.To);
                savingsCount += romanNumeralSubstitution.Saving;
            }
            

            return savingsCount;
        }

        private static void PopulateSavingsSet()
        {
            substitutions = new List<RomanNumeralSubstitution>
            {
                new RomanNumeralSubstitution("DCCCC", "CM"),
                new RomanNumeralSubstitution("CCCC", "CD"),
                new RomanNumeralSubstitution("LXXXX", "XC"),
                new RomanNumeralSubstitution("XXXX", "XL"),
                new RomanNumeralSubstitution("VIIII", "IX"),
                new RomanNumeralSubstitution("IIII", "IV")
            };
        }

        internal class RomanNumeralSubstitution
        {
            internal string From { get; set; }
            internal string To { get; set; }
            internal int Saving { get; set; }

            public RomanNumeralSubstitution(string fromText, string to)
            {
                From = fromText;
                To = to;
                Saving = fromText.Length - to.Length;
            }
        }
    }
}
