using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Determine whether an integer is a palindrome. Do this without extra space.
    /// </summary>
    [TestFixture]
    public class Problem_0009_PalindromeNumber
    {
        [Test]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(10, false)]
        [TestCase(11, true)]
        [TestCase(12, false)]
        [TestCase(100, false)]
        [TestCase(101, true)]
        [TestCase(2002, true)]
        [TestCase(2132, false)]
        [TestCase(12321, true)]
        [TestCase(12311, false)]
        [TestCase(123321, true)]
        [TestCase(123421, false)]
        [TestCase(-1, false)]
        [TestCase(-2147447412, false)]
        public void RunTests(int x, bool expectedIsPalindrome)
        {
            var isPalindrome = IsPalindrome(x);
            Assert.That(isPalindrome, Is.EqualTo(expectedIsPalindrome));
        }

        public bool IsPalindrome(int x)
        {
            var originalX = x;
            var reversedX = 0;

            while (x > 0)
            {
                reversedX = (10 * reversedX) + x % 10;
                x /= 10;
            }

            return (originalX == reversedX);
        }
    }
}
