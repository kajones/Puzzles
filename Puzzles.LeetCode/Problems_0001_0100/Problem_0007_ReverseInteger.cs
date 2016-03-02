using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Reverse digits of an integer.
    ///
    /// Example1: x = 123, return 321
    /// Example2: x = -123, return -321
    /// 
    /// If overflow return zero
    /// </summary>
    [TestFixture]
    public class Problem_0007_ReverseInteger
    {
        [Test]
        [TestCase(123, 321)]
        [TestCase(-123, -321)]
        [TestCase(1534236469, 0)]
        public void RunExamples(int original, int expectedResult)
        {
            var result = Reverse(original);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(1,1)]
        [TestCase(-1,-1)]
        [TestCase(10, 1)]
        [TestCase(12, 21)]
        [TestCase(100, 1)]
        [TestCase(1234,4321)]
        [TestCase(12345,54321)]
        public void RunAlternateTests(int original, int expectedResult)
        {
            var result = Reverse(original);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public int Reverse(int x)
        {
            var isNegative = (x < 0);
            if (isNegative)
            {
                x *= -1;
            }

            var xText = x.ToString();
            var builder = new StringBuilder();
            for(var idx = xText.Length-1; idx >=0; --idx)
            {
                builder.Append(xText[idx]);
            }
            var textResult = builder.ToString();
            int result;
            try
            {
                result = Convert.ToInt32(textResult);
                if (isNegative)
                {
                    result *= -1;
                }
            }
            catch (OverflowException)
            {
                result = 0;
            }

            return result;
        }
    }
}
