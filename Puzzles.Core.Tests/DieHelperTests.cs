using System;
using System.Collections.Generic;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class DieHelperTests
    {
        [Test, Explicit]
        public void ConfirmSixSidedDieReturns1To6()
        {
            var requiredRolls = new HashSet<int> { 1, 2, 3, 4, 5, 6 };

            var die = new DieHelper(1, 6);
            for (var idx = 0; idx < 1000; ++idx)
            {
                var roll = die.Roll();
                if (roll < 1 || roll > 6)
                {
                    Assert.Fail("Rolled: {0}", roll);
                }
                requiredRolls.Remove(roll);
            }

            foreach (var requiredRoll in requiredRolls)
            {
                Console.WriteLine("Roll remaining: {0}", requiredRoll);
            }

            Assert.AreEqual(0, requiredRolls.Count);
        }
    }
}
