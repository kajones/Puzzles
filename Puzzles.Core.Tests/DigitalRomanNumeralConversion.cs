using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;
using Puzzles.Core.Tests.DataSources;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class DigitalRomanNumeralConversion
    {
        [Test]
        public void CanConvertRomanToDigitalRepresentation()
        {
            var testSet = DigitalRomanDataSource.GetSamplePairs();
            foreach (var digitalRomanPair in testSet)
            {
                var actualDigital = RomanNumeralGenerator.GetDigitalRepresentation(digitalRomanPair.Roman);
                actualDigital.Should().Be(digitalRomanPair.Digital);
            }
        }

        [Test]
        public void CanConvertDigitalToRomanNumeralMinimalRepresentation()
        {
            var testSet = DigitalRomanDataSource.GetSamplePairs();
            foreach (var digitalRomanPair in testSet)
            {
                var actualRomanRepresentation = RomanNumeralGenerator.GetRomanRepresentation(digitalRomanPair.Digital);
                actualRomanRepresentation.Should().Be(digitalRomanPair.Roman, "Digital number: {0}", digitalRomanPair.Digital);                
            }
        }
    }
}
