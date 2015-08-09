using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// All square roots are periodic when written as continued fractions and can be written in the form:
    ///
    /// √N = a0 +	 1
    ///          -------------
    ///           a1 + 1
    ///               ----------
    ///               a2 +	1
    ///                   ----------      
    ///                     a3 + ...
    /// For example, let us consider √23:
    ///
    /// √23 = 4 + √23 — 4 = 4 + 	 1         = 4 +  1
    ///                        -----------       -------------
    ///                          1                 1 + √23 - 3
    ///                        -----------             -------
    ///                          √23—4                     7
    ///
    /// If we continue we would get the following expansion:
    ///
    /// √23 = 4 +	1
    ///          -------------------
    ///           1 +	1
    ///               ---------------
    ///                3 +	1
    ///                    ----------
    ///                     1 +	1
    ///                         ------
    ///                         8 + ...
    /// 
    /// The process can be summarised as follows:
    ///
    /// a0 = 4,	 1      = √23+4 = 1 + √23—3
    ///         ------    -----       -----
    ///         √23—4       7           7
    ///
    /// a1 = 1,  7      = 7(√23+3) = 3 + √23—3
    ///         -------   --------       -----
    ///         √23—3        14            2
    ///
    /// a2 = 3,	2       = 2(√23+3) = 1 + √23—4
    ///         -------   --------       -----     
    ///         √23—3        14             7
    /// 
    /// a3 = 1,	7        = 7(√23+4) = 8 + √23—4
    ///         -------    --------
    ///         √23—4         7
    ///
    /// a4 = 8,	1        = √23+4     = 1 + √23—3
    ///         --------   ------          -----
    ///          √23—4        7               7
    ///
    /// a5 = 1,	7        = 7(√23+3)   = 3 + √23—3
    ///         --------   --------         -----
    ///         √23—3        14                2
    ///
    /// a6 = 3,	2        = 2(√23+3)    = 1 + √23—4
    ///         --------   --------          -----
    ///         √23—3         14                7
    /// 
    /// a7 = 1,	7        = 7(√23+4)    = 8 + √23—4
    /// 
    /// It can be seen that the sequence is repeating. For conciseness, we use the notation √23 = [4;(1,3,1,8)], to indicate that the block (1,3,1,8) repeats indefinitely.
    ///
    /// The first ten continued fraction representations of (irrational) square roots are:
    ///
    /// √2=[1;(2)], period=1
    /// √3=[1;(1,2)], period=2
    /// √5=[2;(4)], period=1
    /// √6=[2;(2,4)], period=2
    /// √7=[2;(1,1,1,4)], period=4
    /// √8=[2;(1,4)], period=2
    /// √10=[3;(6)], period=1
    /// √11=[3;(3,6)], period=2
    /// √12= [3;(2,6)], period=2
    /// √13=[3;(1,1,1,1,6)], period=5
    ///
    /// Exactly four continued fractions, for N ≤ 13, have an odd period.
    ///
    /// How many continued fractions for N ≤ 10000 have an odd period?
    /// </summary>
    [TestFixture]
    public class Problem_0064_OddPeriodSquareRoots
    {
        [Test]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 0)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(7, 4)]
        [TestCase(8, 2)]
        [TestCase(9, 0)]
        [TestCase(10, 1)]
        [TestCase(13, 5)]
        public void CalcPeriodLength(int n, int expectedPeriod)
        {
            var length = GetPeriodLength(n);
            Assert.AreEqual(expectedPeriod, length);
        }

        /// <summary>
        /// 1322 (Problem 64)
        /// </summary>
        [Test, Explicit]
        public void FindNumberOfOddPeriodLengthsUpToTenThousand()
        {
            var count = 0;

            for (var n = 1; n <= 10000; ++n)
            {
                var periodLength = GetPeriodLength(n);
                var isOdd = (periodLength % 2 == 1);
                if (isOdd)
                    count++;
            }

            Console.WriteLine("{0} examples of odd period", count);

            count.Should().Be(1322);
        }

        [Test, Explicit]
        public void DisplayNumerator()
        {
            GetPeriodLength(14);
        }

        private static int GetPeriodLength(int n)
        {
            int a_0, a, b, c, b_0, c_0, result = 0;
            a_0 = (int)Math.Sqrt(n * 1.0);
            b = b_0 = a_0;
            c = c_0 = n - (a_0 * a_0);

            if (c == 0) return 0;

            do
            {
                a = (a_0 + b) / c;
                b = (a * c) - b;
                c = (n - (b * b)) / c;

                result++;

                // Console.WriteLine("{0} {1} {2}", a, b, c);

            } while ((b != b_0) || (c != c_0));

            return result;
        }
    }
}
