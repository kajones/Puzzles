using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting 
    /// to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.
    ///
    /// We shall consider fractions like, 30/50 = 3/5, to be trivial examples.
    ///
    /// There are exactly four non-trivial examples of this type of fraction, less than one in value, 
    /// and containing two digits in the numerator and denominator.
    ///
    /// If the product of these four fractions is given in its lowest common terms, find the value of the denominator.
    [TestFixture]
    public class Problem_0033_DigitCancellingFractions
    {
        /// <summary>
        /// Orig:16/64  Revised: 1/4
        /// Orig:19/95  Revised: 1/5
        /// Orig:26/65  Revised: 2/5
        /// Orig:49/98  Revised: 4/8
        /// 
        /// Product = 8 / 800 or 1/100 so answer is 100
        /// </summary>
        [Test, Explicit]
        public void FindNonTrivialFractionsWhereRemovingACommonDigitStillGivesTheSameValue()
        {
            for (var numerator = 10; numerator < 100; ++numerator)
            {
                var digitsInNumerator = DigitHelper.GetDigits(numerator).ToList();
                for (var denominator = (numerator + 1); denominator < 100; ++denominator)
                {
                    var digitsInDenominator = DigitHelper.GetDigits(denominator).ToList();
                    var commonDigits = digitsInNumerator.Where(digitsInDenominator.Contains).Distinct().ToList();
                    if (commonDigits.Count == 0) continue;

                    var originalFractionValue = (decimal)numerator / (decimal)denominator;

                    foreach (var commonDigit in commonDigits)
                    {
                        if (commonDigit == 0) continue;
                        var revisedNumerator = GetRemainingDigit(digitsInNumerator, commonDigit);
                        var revisedDenominator = GetRemainingDigit(digitsInDenominator, commonDigit);
                        if (revisedNumerator == 0) continue;
                        if (revisedDenominator == 0) continue;

                        var revisedFractionValue = (decimal)revisedNumerator / (decimal)revisedDenominator;
                        if (originalFractionValue == revisedFractionValue)
                        {
                            Console.WriteLine("Orig:{0}/{1}  Revised: {2}/{3}", numerator, denominator, revisedNumerator, revisedDenominator);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove the first instance of the common digit from the number
        /// But e.g. if the number is 22 and the digit 2, only remove one instance of the 2 i.e. return 2
        /// </summary>
        /// <param name="originalNumberDigits"></param>
        /// <param name="commonDigit"></param>
        /// <returns></returns>
        private static int GetRemainingDigit(IList<int> originalNumberDigits, int commonDigit)
        {
            if (originalNumberDigits[0] == commonDigit)
                return originalNumberDigits[1];

            return originalNumberDigits[0];
        }
    }
}
