using System.Collections.Generic;

namespace Puzzles.Core.Tests.DataSources
{
    internal static class DigitalRomanDataSource
    {
        internal static IEnumerable<DigitalRomanPair> GetSamplePairs()
        {
            return new[]
            {
                new DigitalRomanPair(1, "I"),
                new DigitalRomanPair(2, "II"),
                new DigitalRomanPair(3, "III"),
                new DigitalRomanPair(4, "IV"),
                new DigitalRomanPair(5, "V"),
                new DigitalRomanPair(6, "VI"),
                new DigitalRomanPair(7, "VII"),
                new DigitalRomanPair(8, "VIII"),
                new DigitalRomanPair(9, "IX"),
                new DigitalRomanPair(10, "X"),
                new DigitalRomanPair(11, "XI"),
                new DigitalRomanPair(12, "XII"),
                new DigitalRomanPair(13, "XIII"),
                new DigitalRomanPair(14, "XIV"),
                new DigitalRomanPair(15, "XV"),
                new DigitalRomanPair(16, "XVI"),
                new DigitalRomanPair(17, "XVII"),
                new DigitalRomanPair(18, "XVIII"),
                new DigitalRomanPair(19, "XIX"),
                new DigitalRomanPair(20, "XX"),
                new DigitalRomanPair(21, "XXI"),
                new DigitalRomanPair(40, "XL"),
                new DigitalRomanPair(44, "XLIV"),
                new DigitalRomanPair(49, "XLIX"),
                new DigitalRomanPair(50, "L"),
                new DigitalRomanPair(51, "LI"),
                new DigitalRomanPair(60, "LX"),
                new DigitalRomanPair(90, "XC"),
                new DigitalRomanPair(100, "C"),
                new DigitalRomanPair(400, "CD"),
                new DigitalRomanPair(490, "CDXC"),
                new DigitalRomanPair(500, "D"),
                new DigitalRomanPair(900, "CM"),
                new DigitalRomanPair(1000, "M"),
                new DigitalRomanPair(1900, "MCM"),
                new DigitalRomanPair(2000, "MM")
            };
        }
    }

    internal struct DigitalRomanPair
    {
        internal int Digital { get; set; }
        internal string Roman { get; set; }

        public DigitalRomanPair(int digital, string roman) : this()
        {
            Digital = digital;
            Roman = roman;
        }
    }
}
