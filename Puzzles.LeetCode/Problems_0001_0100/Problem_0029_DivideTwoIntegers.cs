using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Divide two integers without using multiplication, division and mod operator.
    ///
    /// If it is overflow, return MAX_INT.
    /// </summary>
    [TestFixture]
    public class Problem_0029_DivideTwoIntegers
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 2)]
        [TestCase(4, 2, 2)]
        [TestCase(6, 3, 2)]
        [TestCase(6, 2, 3)]
        [TestCase(12, 3, 4)]
        [TestCase(20, 3, 6)]
        [TestCase(50, 3, 16)]
        [TestCase(3, 0, Int32.MaxValue)]
        [TestCase(1, -1, -1)]
        [TestCase(6, -2, -3)]
        [TestCase(-6, 3, -2)]
        [TestCase(2147483647, 1, 2147483647)]
        [TestCase(2147483647, 3, 715827882)]
        [TestCase(-1010369383, -2147483648, 0)]
        [TestCase(-2147483648, -1, 2147483647)]
        [TestCase(-2147483648, 2, -1073741824)]
        [TestCase(-2147483648, -2147483648, 1)]
        public void RunTests(int dividend, int divisor, int expectedResult)
        {
            var result = Divide(dividend, divisor);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public int Divide(int dividend, int divisor)
        {
            if (divisor == 0) return Int32.MaxValue;
            if (divisor == 1) return dividend;
            if (divisor == -1)
            {
                return (dividend == Int32.MinValue) ? Int32.MaxValue : -dividend;
            }
            if (divisor == Int32.MinValue)
            {
                return (dividend == Int32.MinValue) ? 1 : 0;
            }

            long calcDividend = dividend;
            long calcDivisor = divisor;
            var negativeResult = false;
            if (dividend < 0)
            {
                negativeResult = true;
                calcDividend = 0 - calcDividend;
            }
            if (divisor < 0)
            {
                negativeResult = !negativeResult;
                calcDivisor = 0 - calcDivisor;
            }

            long result = 0;

            long remainingDividend = calcDividend;
            while (remainingDividend > 0)
            {
                long multiplier = 1;
                long runningResult = calcDivisor;

                long lastMultiplier = 0;
                long lastRunningResult = 0;

                // Keep doubling the divisor until beyond the original dividend
                // Keep a count of how many doublings have occurred
                while (remainingDividend > runningResult)
                {
                    lastMultiplier = multiplier;
                    lastRunningResult = runningResult;

                    runningResult = runningResult << 1;
                    multiplier = multiplier << 1;
                }

                result += lastMultiplier;
                remainingDividend -= lastRunningResult;
                if (remainingDividend <= calcDivisor)
                {
                    if (remainingDividend == calcDivisor)
                        result += 1;
                    break;
                }
            }

            var answer = negativeResult ? -result : result;
            if (answer > int.MaxValue) return int.MaxValue;

            return (int)answer;
        }
    }
}
