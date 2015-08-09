using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class BouncyNumberCheck
    {
        [Test]
        [TestCase(134468, false)]
        [TestCase(66420, false)]
        [TestCase(155349, true)]
        public void ConfirmBouncyNumbers(long candidate, bool expectedIsBouncy)
        {
            var isBouncy = BouncyNumberChecker.IsBouncy(candidate);
            isBouncy.Should().Be(expectedIsBouncy);
        }
    }
}
