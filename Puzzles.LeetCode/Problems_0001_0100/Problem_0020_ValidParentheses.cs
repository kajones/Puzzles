using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a string containing just the characters '(', ')', '{', '}', '[' and ']', 
    /// determine if the input string is valid.
    ///
    /// The brackets must close in the correct order, "()" and "()[]{}" are all valid 
    /// but "(]" and "([)]" are not.
    /// </summary>
    [TestFixture]
    public class Problem_0020_ValidParentheses
    {
        [Test]
        [TestCase("()", true)]
        [TestCase("()[]{}", true)]
        [TestCase("(]", false)]
        [TestCase("([)]", false)]
        public void RunExamples(string s, bool expectedIsValid)
        {
            var isValid = IsValid(s);
            Assert.That(isValid, Is.EqualTo(expectedIsValid));
        }

        [Test]
        [TestCase("}", false)]
        [TestCase("", true)]
        public void RunTests(string s, bool expectedIsValid)
        {
            var isValid = IsValid(s);
            Assert.That(isValid, Is.EqualTo(expectedIsValid));
        }

        public bool IsValid(string s)
        {
            var map = new Dictionary<char, char> 
            { 
                {'(', ')'},
                {'{', '}'},
                {'[', ']'}
            };

            var bracketStack = new Stack<char>();

            for (var idx = 0; idx < s.Length; ++idx)
            { 
                if (map.ContainsKey(s[idx]))
                {
                    bracketStack.Push(map[s[idx]]);
                    continue;
                }
                if (map.ContainsValue(s[idx]))
                {
                    if (bracketStack.Count == 0) return false;
                    var expectedClosingBracket = bracketStack.Pop();
                    if (s[idx] == expectedClosingBracket) continue;
                    return false;
                }
            }

            return (bracketStack.Count == 0);
        }
    }
}
