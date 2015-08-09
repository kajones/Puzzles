using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    ///     4
    ///      \
    ///       3
    ///      /  \
    ///     1 -- 2 -- 6
    ///    /
    ///   5
    /// 
    /// Clockwise arrangements of lines starting with lowest outside number
    /// 9	4,2,3; 5,3,1; 6,1,2
    /// 9	4,3,2; 6,2,1; 5,1,3
    /// 10	2,3,5; 4,5,1; 6,1,3
    /// 10	2,5,3; 6,3,1; 4,1,5
    /// 11	1,4,6; 3,6,2; 5,2,4
    /// 11	1,6,4; 5,4,2; 3,2,6
    /// 12	1,5,6; 2,6,4; 3,4,5
    /// 12	1,6,5; 3,5,4; 2,4,6
    /// the maximum string for a 3-gon ring is 432621513.
    /// 
    /// Using the numbers 1 to 10, and depending on arrangements, it is possible to form 16- and 17-digit strings. 
    /// What is the maximum 16-digit string for a "magic" 5-gon ring?
    /// 
    /// 
    ///                   A
    ///                    \         
    ///                     B       D
    ///                    /   \   /
    ///                   J      C
    ///                  / \    /
    ///                 K   G--F -- E
    ///                      \
    ///                       H
    /// 
    /// i.e. ABC DCF EFG JGH KJB
    ///  </summary>
    [TestFixture]
    public class Problem_0068_Magic5GonRing
    {
        private const string NineDigitFormat = "{0}{1}{2}{3}{4}{5}{6}{7}{8}";
        private const string ThreeDigitFormat = "{0}{1}{2}";
        private const string FiveLineFormat = "{0}{1}{2}{3}{4}";

        /// 9	4,2,3; 5,3,1; 6,1,2
        /// 9	4,3,2; 6,2,1; 5,1,3
        /// 10	2,3,5; 4,5,1; 6,1,3
        /// 10	2,5,3; 6,3,1; 4,1,5
        /// 11	1,4,6; 3,6,2; 5,2,4
        /// 11	1,6,4; 5,4,2; 3,2,6
        /// 12	1,5,6; 2,6,4; 3,4,5
        /// 12	1,6,5; 3,5,4; 2,4,6
        [Test]
        public void ConfirmThreeGonRing()
        {
            const int limit = 7;
            var results = new List<string>();

            // Three lines which must add up to the same number
            // ABC,FCD,EDB using the digits 1-9
            // Then if all digits placed, starting with the lowest of A, F, E create a number from the line and then go round clockwise for the other three lines
            for (var a = 1; a < limit; ++a)
            {
                for (var b = 1; b < limit; ++b)
                {
                    if (b == a) continue;
                    for (var c = 1; c < limit; ++c)
                    {
                        if ((c == b) || (c == a)) continue;
                        var firstLineTotal = a + b + c;
                        for (var d = 1; d < limit; ++d)
                        {
                            if ((d == c) || (d == b) || (d == a)) continue;
                            for (var e = 1; e < limit; ++e)
                            {
                                if ((e == d) || (e == c) || (e == b) || (e == a)) continue;
                                var secondLineTotal = e + d + b;
                                if (secondLineTotal != firstLineTotal) continue;
                                for (var f = 1; f < limit; ++f)
                                {
                                    if ((f == e) || (f == d) || (f == c) || (f == b) || (f == a)) continue;
                                    var thirdLineTotal = f + c + d;
                                    if (thirdLineTotal != secondLineTotal) continue;

                                    var concatenatedNumber = GetSixNumber(a, b, c, d, e, f);
                                    if (!results.Contains(concatenatedNumber))
                                        results.Add(concatenatedNumber);
                                }
                            }
                        }
                    }
                }
            }

            results.Sort();
            results.Reverse();
            foreach (var result in results)
            {
                Console.WriteLine("{0}", result);
            }

            Assert.AreEqual(8, results.Count, "Eight solutions");

            Assert.AreEqual("432621513", results[0], "Largest solution");
        }

        /// <summary>
        /// 6531031914842725
        /// </summary>
        [Test, Explicit]
        public void FindFiveGonRingMaximum()
        {
            const int limit = 11;
            var results = new List<string>();

            // Five lines which must add up to the same number
            // ABC,DCF,EFG,HGJ,KJB using the digits 1-10
            // Then if all digits placed, starting with the lowest of A, D,E,H,K create a number from the line and then go round clockwise for the other lines
            for (var a = 1; a < limit; ++a)
            {
                for (var b = 1; b < limit; ++b)
                {
                    if (b == a) continue;
                    for (var c = 1; c < limit; ++c)
                    {
                        if ((c == b) || (c == a)) continue;
                        var firstLineTotal = a + b + c;
                        for (var d = 1; d < limit; ++d)
                        {
                            if ((d == c) || (d == b) || (d == a)) continue;
                            for (var e = 1; e < limit; ++e)
                            {
                                if ((e == d) || (e == c) || (e == b) || (e == a)) continue;
                                for (var f = 1; f < limit; ++f)
                                {
                                    if ((f == e) || (f == d) || (f == c) || (f == b) || (f == a)) continue;

                                    var secondLineTotal = d + c + f;
                                    if (secondLineTotal != firstLineTotal) continue;

                                    for (var g = 1; g < limit; ++g)
                                    {
                                        if ((g == f) || (g == e) || (g == d) || (g == c) || (g == b) || (g == a))
                                            continue;

                                        var thirdLineTotal = e + f + g;
                                        if (thirdLineTotal != secondLineTotal) continue;

                                        for (var h = 1; h < limit; ++h)
                                        {
                                            if ((h == g) || (h == f) || (h == e) || (h == d) || (h == c) || (h == b) ||
                                                (h == a)) continue;
                                            for (var j = 1; j < limit; ++j)
                                            {
                                                if ((j == h) || (j == g) || (j == f) || (j == e) || (j == d) || (j == c) ||
                                                    (j == b) || (j == a)) continue;

                                                var fourthLineTotal = h + g + j;
                                                if (fourthLineTotal != thirdLineTotal) continue;

                                                for (var k = 1; k < limit; ++k)
                                                {
                                                    if ((k == j) || (k == h) || (k == g) || (k == f) || (k == e) ||
                                                        (k == d) || (k == c) || (k == b) || (k == a))
                                                        continue;

                                                    var fifthLineTotal = k + j + b;
                                                    if (fifthLineTotal != fourthLineTotal) continue;

                                                    var concatenatedNumber = GetTenNumber(a, b, c, d, e, f, g, h, j, k).PadLeft(20, '0');
                                                    if (!results.Contains(concatenatedNumber))
                                                        results.Add(concatenatedNumber);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            results.Sort();
            results.Reverse();
            foreach (var result in results)
            {
                Console.WriteLine("{0}", result);
            }

            results.Should().Contain("00006531031914842725");
        }

        private static string GetTenNumber(int a, int b, int c, int d, int e, int f, int g, int h, int j, int k)
        {
            var aLine = string.Format(ThreeDigitFormat, a, b, c);
            var dLine = string.Format(ThreeDigitFormat, d, c, f);
            var eLine = string.Format(ThreeDigitFormat, e, f, g);
            var hLine = string.Format(ThreeDigitFormat, h, g, j);
            var kLine = string.Format(ThreeDigitFormat, k, j, b);

            var lowest = (int)Math.Min(a, Math.Min(d, Math.Min(e, Math.Min(h, k))));

            if (lowest == a)
            {
                return string.Format(FiveLineFormat, aLine, dLine, eLine, hLine, kLine);
            }
            if (lowest == d)
            {
                return string.Format(FiveLineFormat, dLine, eLine, hLine, kLine, aLine);
            }
            if (lowest == e)
            {
                return string.Format(FiveLineFormat, eLine, hLine, kLine, aLine, dLine);
            }
            if (lowest == h)
            {
                return string.Format(FiveLineFormat, hLine, kLine, aLine, dLine, eLine);
            }
            return string.Format(FiveLineFormat, kLine, aLine, dLine, eLine, hLine);
        }

        private static string GetSixNumber(int a, int b, int c, int d, int e, int f)
        {
            // ABC
            // FCD
            // EDB
            if ((a < f) && (a < e))
                return string.Format(NineDigitFormat, a, b, c, f, c, d, e, d, b);

            if (f < e)
                return string.Format(NineDigitFormat, f, c, d, e, d, b, a, b, c);

            return string.Format(NineDigitFormat, e, d, b, a, b, c, f, c, d);
        }
    }
}
