using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Consider the fraction, n/d, where n and d are positive integers. 
    /// If n less than d and HCF(n,d)=1, it is called a reduced proper fraction.
    ///
    /// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:
    ///
    /// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
    ///
    /// It can be seen that 2/5 is the fraction immediately to the left of 3/7.
    ///
    /// By listing the set of reduced proper fractions for d ≤ 1,000,000 in ascending order of size, 
    /// find the numerator of the fraction immediately to the left of 3/7.
    /// </summary>
    [TestFixture]
    public class Problem_0071_OrderedFractions
    {
        [Test]
        public void ConfirmExample()
        {
            var list = new List<OrderedFraction>();

            for (long denominator = 1; denominator <= 8; ++denominator)
            {
                for (long numerator = 1; numerator < denominator; ++numerator)
                {
                    var hcf = FactorHelper.GetHighestCommonFactor(denominator, numerator);
                    if (hcf != 1) continue;

                    list.Add(new OrderedFraction(numerator, denominator));
                }
            }

            list.Sort();

            var indexOf3_7 = list.FindIndex(item => item.Numerator == 3 && item.Denominator == 7);
            if (indexOf3_7 > 0)
            {
                var before = list[indexOf3_7 - 1];
                Assert.AreEqual(2, before.Numerator, "Num");
                Assert.AreEqual(5, before.Denominator, "Denom");
            }
            else
            {
                Assert.Fail("Didn't find 3/7");
            }
        }

        /// <summary>
        /// 428570/999997
        /// </summary>
        [Test, Explicit]
        public void FindFractionLeftOf3Over7()
        {
            const long AMillion = 1000000;

            var threeSevenths = new OrderedFraction(3, 7);
            var valueThreshold = threeSevenths.Value;

            var largestOrderedFraction = new OrderedFraction(0, 1);

            for (long denominator = 2; denominator <= AMillion; ++denominator)
            {
                if (denominator == 7) continue;

                //long numLow = Convert.ToInt64(denominator*0.4);
                var numHigh = Convert.ToInt64(denominator * valueThreshold);

                for (long numerator = numHigh; numerator >= 1; --numerator)
                {
                    var orderedFraction = new OrderedFraction(numerator, denominator);
                    if (orderedFraction.Value > valueThreshold) continue;

                    var hcf = FactorHelper.GetHighestCommonFactor(denominator, numerator);
                    if (hcf != 1) continue;

                    if (orderedFraction.Value > largestOrderedFraction.Value)
                    {
                        largestOrderedFraction = orderedFraction;
                    }

                    break;
                }
            }

            Console.WriteLine("{0}/{1}", largestOrderedFraction.Numerator, largestOrderedFraction.Denominator);
            largestOrderedFraction.Numerator.Should().Be(428570);
        }
    }

    public class OrderedFraction : IComparable<OrderedFraction>
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }

        public double Value { get; private set; }

        public OrderedFraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            Value = (1.0 * numerator) / (1.0 * denominator);
        }

        public int CompareTo(OrderedFraction other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
