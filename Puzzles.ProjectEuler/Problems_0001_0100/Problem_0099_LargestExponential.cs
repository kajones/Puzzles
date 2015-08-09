using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Comparing two numbers written in index form like 2^11 and 3^7 is not difficult, 
    /// as any calculator would confirm that 2^11 = 2048 lt 3^7 = 2187.
    ///
    /// However, confirming that 632382^518061 > 519432^525806 would be much more difficult, as both numbers contain over three million digits.
    ///
    /// Using base_exp.txt (right click and 'Save Link/Target As...'), a 22K text file containing one thousand lines 
    /// with a base/exponent pair on each line, determine which line number has the greatest numerical value.
    ///
    /// NOTE: The first two lines in the file represent the numbers in the example given above.
    /// </summary>
    [TestFixture]
    public class Problem_0099_LargestExponential
    {
        [Test]
        public void ConfirmFirstExample()
        {
            var firstNumber = new LogValue("2,11");
            var secondNumber = new LogValue("3,7");

            var larger = FindLargerValue(firstNumber, secondNumber);
            larger.Should().Be(secondNumber);
        }

        [Test]
        public void ConfirmSecondExample()
        {
            var firstNumber = new LogValue("632382,518061");
            var secondNumber = new LogValue("519432,525806");

            var larger = FindLargerValue(firstNumber, secondNumber);

            larger.Should().Be(firstNumber);
        }

        [Test]
        public void ConfirmReadingFile()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0099_base_exp.txt");
            var lines = fileContent.Split('\n');
            lines.Length.Should().BeGreaterOrEqualTo(1000);

            var firstLine = lines[0];
            var secondLine = lines[1];

            var firstNumber = new LogValue(firstLine);
            var secondNumber = new LogValue(secondLine);

            firstNumber.BaseNumber.Should().Be(519432);
            firstNumber.Exponent.Should().Be(525806);

            secondNumber.BaseNumber.Should().Be(632382);
            secondNumber.Exponent.Should().Be(518061);
        }

        /// <summary>
        /// 709
        /// </summary>
        [Test, Explicit]
        public void FindLargestExponentInTheFile()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0099_base_exp.txt");
            var lines = fileContent.Split('\n');
            lines.Length.Should().BeGreaterOrEqualTo(1000);

            double largestNumber = 0;
            int lineNumberWithLargest = 0;

            for (var idx = 0; idx < lines.Length; ++idx)
            {
                var logValue = new LogValue(lines[idx]);
                if (logValue.Log > largestNumber)
                {
                    lineNumberWithLargest = idx + 1; // Avoid zero-base
                    largestNumber = logValue.Log;
                }
            }

            Console.WriteLine("Line with the larges number is: {0}", lineNumberWithLargest);
            lineNumberWithLargest.Should().Be(709);
        }

        /// <summary>
        /// Using the fact that log(x^y) = y log (x) to find values faster - multiplication rather than power calculation
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static LogValue FindLargerValue(LogValue first, LogValue second)
        {
            if (first.Log < second.Log) return second;
            return first;
        }

        internal class LogValue
        {
            internal int BaseNumber { get; private set; }
            internal int Exponent { get; private set; }

            internal double Log { get; private set; }

            internal LogValue(string expression)
            {
                var parts = expression.Split(',');
                BaseNumber = Convert.ToInt32(parts[0]);
                Exponent = Convert.ToInt32(parts[1]);

                Log = Exponent*Math.Log(BaseNumber);
            }
        }
    }
}
