using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: 
    /// (you may want to display this pattern in a fixed font for better legibility)
    ///
    /// P   A   H   N
    ///  A P L S I I G
    ///   Y   I   R
    /// And then read line by line: "PAHNAPLSIIGYIR"
    /// Write the code that will take a string and make this conversion given a number of rows:
    ///
    /// string convert(string text, int nRows);
    /// convert("PAYPALISHIRING", 3) should return "PAHNAPLSIIGYIR".
    /// i.e. this is the ZigZag/Railroad cipher
    /// </summary>
    [TestFixture]
    public class Problem_0006_ZigZagConversion
    {
        [Test]
        public void RunExample()
        {
            var result = Convert("PAYPALISHIRING", 3);
            Assert.That(result, Is.EqualTo("PAHNAPLSIIGYIR"));
        }

        [Test]
        [TestCase("TRUSTTHISDOCUMENTALWAYS", 3, "TTSUTARSTIDCMNAWYUHOELS")]
        [TestCase("NOTALLPDFREADERSARECREATEDEQUAL", 3, "NLFDAREUOALDRAESRCETDQATPEREAEL")]
        [TestCase("AB", 1, "AB")]
        public void RunAlternateExamples(string plain, int numRows, string encrypted)
        {
            var result = Convert(plain, numRows);
            Assert.That(result, Is.EqualTo(encrypted));
        }

        [Test]
        [TestCase(1, new[] { 1,1,1,1,1})]
        [TestCase(2, new[] { 1,2,1,2,1,2})]
        [TestCase(3, new[] { 1,2,3,2,1,2,3,2,1})]
        [TestCase(4, new[] { 1,2,3,4,3,2,1,2,3,4,3,2,1})]
        [TestCase(5, new[] { 1,2,3,4,5,4,3,2,1,2,3,4,5,4,3,2,1})]
        public void ConfirmRailSelection(int numRails, int[] expectedSequence)
        {
            var railSelector = new RailSelector(numRails, expectedSequence.Length);

            var idx=0;
            foreach(var nextRail in railSelector.NextRail())
            {
                Assert.That(nextRail, Is.EqualTo(expectedSequence[idx]), string.Format("Rails:{0} Pos:{1}", numRails, idx));
                idx++;
            }
        }

        public string Convert(string s, int numRows)
        {
            var rows = new List<string>();
            for(var rail=0; rail < numRows; ++rail)
            {
                rows.Add(string.Empty);
            }

            var railSelector = new RailSelector(numRows, s.Length);

            var idx = 0;
            foreach(var nextRail in railSelector.NextRail())
            {
                rows[nextRail-1] += s[idx];
                idx++;
            }

            var builder = new StringBuilder();
            foreach(var railText in rows)
            {
                builder.Append(railText);
            }
            return builder.ToString();
        }

        public class RailSelector
        {
            private readonly int numberOfRails;
            private readonly int length;

            private int rail = 0;
            private bool ascending = true;

            public RailSelector(int numberOfRails, int length)
            {
                this.numberOfRails = numberOfRails;
                this.length = length;
            }

            public IEnumerable<int> NextRail()
            {
                if (numberOfRails == 1)
                {
                    for (var idx = 0; idx < length; ++idx)
                    {
                        yield return 1;
                    }
                    yield break;
                }

                for (var idx = 0; idx < length; ++idx)
                {
                    if (rail == 1) ascending = true;
                    if (rail == numberOfRails) ascending = false;

                    var increment = ascending ? 1 : -1;
                    rail += increment;

                    yield return rail;
                }
            }
        }
    }
}
