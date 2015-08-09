using System;
using System.Numerics;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The Fibonacci sequence is defined by the recurrence relation:
    ///
    /// Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
    /// Hence the first 12 terms will be:
    ///
    /// F1 = 1
    /// F2 = 1
    /// F3 = 2
    /// F4 = 3
    /// F5 = 5
    /// F6 = 8
    /// F7 = 13
    /// F8 = 21
    /// F9 = 34
    /// F10 = 55
    /// F11 = 89
    /// F12 = 144
    /// The 12th term, F12, is the first term to contain three digits.
    ///
    /// What is the first term in the Fibonacci sequence to contain 1000 digits?
    /// </summary>
    [TestFixture]
    public class Problem_0025_ThousandDigitFibonacci
    {
        [Test, Explicit]
        public void AlternateFindAThousandDigits()
        {
            var number = FibonacciHelper.GetFirstFibonacciWithNDigits(1000);
            Console.WriteLine(number);
        }

        /// <summary>
        /// Term 4782
        /// </summary>
        [Test, Explicit]
        public void FindFirstFibonacciWithAThousandDigits()
        {
            var i = 0;
            int count = 2;
            BigInteger limit = BigInteger.Pow(10, 999);
            var fib = new BigInteger[3];

            fib[0] = 1;
            fib[2] = 1;

            while (fib[i] <= limit)
            {
                i = (i + 1) % 3;
                count++;
                fib[i] = fib[(i + 1) % 3] + fib[(i + 2) % 3];
            }

            Console.WriteLine("The first term in the fibonnaci sequence to have more than 1000 digits is term number: {0}", count);
            Console.WriteLine("Fib: {0} {1} {2}", fib[0], fib[1], fib[2]);
        }

    }
}
