using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It is possible to write ten as the sum of primes in exactly five different ways:
    ///
    /// 7 + 3
    /// 5 + 5
    /// 5 + 3 + 2
    /// 3 + 3 + 2 + 2
    /// 2 + 2 + 2 + 2 + 2
    ///
    /// What is the first value which can be written as the sum of primes in over five thousand different ways?
    /// </summary>
    [TestFixture]
    public class Problem_0077_PrimeSummations
    {
        private long[] Numbers;

        [Test]
        [TestCase(3, 1, "3")]
        [TestCase(4, 1, "2 2")]
        [TestCase(5, 1, "3 2")]
        [TestCase(6, 2, "3 3, 2 2 2")]
        [TestCase(7, 2, " 5 2, 3 2 2")]
        [TestCase(8, 3, "5 3, 3 3 2, 2 2 2 2")]
        [TestCase(9, 4, "7 2, 5 2 2, 3 2 2 2, 3 3 3")]
        [TestCase(10, 5, "7 3, 5 5, 5 3 2, 3 3 2 2, 2 2 2 2 2")]
        public void ConfirmExample(long targetTotal, long expectedWays, string ways)
        {
            var waysToPrimeSum = CalculatePrimeSummations(targetTotal);
            Assert.AreEqual(expectedWays, waysToPrimeSum, ways);
        }

        /// <summary>
        /// 71
        /// </summary>
        [Test, Explicit]
        public void FindNumberYouCanCalculateAtLeastFiveThousandWays()
        {
            long number = 10;

            while (true)
            {
                number++;
                var waysToPrimeSum = CalculatePrimeSummations(number);
                if (waysToPrimeSum > 5000)
                {
                    Console.WriteLine("Number: {0}; ways: {1}", number, waysToPrimeSum);
                    number.Should().Be(71);
                    break;
                }
            }
        }

        private long CalculatePrimeSummations(long targetTotal)
        {
            var primes = PrimeHelper.GetPrimesUpTo(targetTotal);
            Numbers = primes.ToArray();

            var queue = new Queue<ValueToCalculate>();
            queue.Enqueue(new ValueToCalculate(targetTotal, Numbers.Length - 1));

            var count = 0;
            while (queue.Count > 0)
            {
                var valueToCalculate = queue.Dequeue();

                var remainder = valueToCalculate.RemainingValue - Numbers[valueToCalculate.Index];
                if (remainder > 0)
                {
                    var remainderValue = new ValueToCalculate(remainder, valueToCalculate.Index);
                    //var remainderValue = new ValueToCalculate(string.Format("{0} {1}", valueToCalculate.Used, Numbers[valueToCalculate.Index]), remainder, valueToCalculate.Index);
                    queue.Enqueue(remainderValue);
                }
                else if (remainder == 0)
                {
                    count++;
                }

                if (valueToCalculate.Index > 0)
                {
                    var usingNextIndex = new ValueToCalculate(valueToCalculate.RemainingValue,
                        valueToCalculate.Index - 1);
                    queue.Enqueue(usingNextIndex);
                }
            }

            return count;
        }
    }

    public class ValueToCalculate
    {
        public string Used { get; private set; }
        public long RemainingValue { get; private set; }
        public long Index { get; private set; }

        public ValueToCalculate(string used, long remainingValue, long index)
        {
            Used = used;
            RemainingValue = remainingValue;
            Index = index;
        }

        public ValueToCalculate(long remainingValue, long index)
        {
            RemainingValue = remainingValue;
            Index = index;
        }

        public override string ToString()
        {
            return string.Format("{0}; {1} [{2}]", Used, RemainingValue, Index);
        }
    }
}
