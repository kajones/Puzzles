using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class FibonacciGeneration
    {
        [Test]
        [TestCase(1,1,2)]
        [TestCase(1,2,3)]
        [TestCase(2,3,5)]
        [TestCase(3,5,8)]
        [TestCase(5,8,13)]
        [TestCase(8,13,21)]
        [TestCase(13,21,34)]
        [TestCase(21,34,55)]
        [TestCase(34,55,89)]
        [TestCase(55,89,144)]
        public void GetNextFibonacciNumber(int firstNumber, int secondNumber, int expectedFibonacciNumber)
        {
            var actualFibonacci = FibonacciHelper.GetNextFibonacci(firstNumber, secondNumber);
            actualFibonacci.Should().Be(expectedFibonacciNumber);
        }

        [Test]
        public void GetFirstFibonacciWithNumberOfDigits()
        {
            var testSets = new HashSet<FibonacciDigit>
            {
                new FibonacciDigit(1, 1),
                new FibonacciDigit(2, 13),
                new FibonacciDigit(3, 144)
            };

            foreach (var testSet in testSets)
            {
                var actualFibonacci = FibonacciHelper.GetFirstFibonacciWithNDigits(testSet.Digit);
                actualFibonacci.Should().Be(testSet.Fibonacci);
            }
        }

        internal struct FibonacciDigit
        {
            internal int Digit { get; set; }
            internal BigInteger Fibonacci { get; set; }

            internal FibonacciDigit(int digit, BigInteger fibonacci)
                : this()
            {
                Digit = digit;
                Fibonacci = fibonacci;
            }
        }
    }
}
