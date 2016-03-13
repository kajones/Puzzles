using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
    ///
    /// For example, given n = 3, a solution set is:
    ///
    /// "((()))", "(()())", "(())()", "()(())", "()()()"
    /// </summary>
    [TestFixture]
    public class Problem_0022_GenerateParentheses
    {
        [Test]
        public void NoParenthesis()
        {
            var result = GenerateParenthesis(0);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleParenthesis()
        {
            var result = GenerateParenthesis(1);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Contains("()"));
        }

        [Test]
        public void TwoParenthesis()
        {
            var result = GenerateParenthesis(2);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Contains("(())"));
            Assert.That(result.Contains("()()"));
        }

        [Test]
        public void RunExample()
        {
            var parenthesis = GenerateParenthesis(3);
            Assert.That(parenthesis.Count, Is.EqualTo(5));
            Assert.That(parenthesis.Contains("((()))"));
            Assert.That(parenthesis.Contains("(()())"));
            Assert.That(parenthesis.Contains("(())()"));
            Assert.That(parenthesis.Contains("()(())"));
            Assert.That(parenthesis.Contains("()()()"));
        }

        public IList<string> GenerateParenthesis(int n)
        {
            if (n == 0) return new List<string>();

            var list = new List<string>();
            AddParenthesis(list, n, "(", 1);
            return list;
        }

        private void AddParenthesis(List<string> list, int n, string output, int openCount)
        {
            var maxLen = n * 2;

            if (output.Length == maxLen)
            {
                list.Add(output);
                return;
            }

            // If there's room to open a bracket and then close it again
            if (openCount < maxLen - output.Length)
            {
                var addOpen = output + '(';
                AddParenthesis(list, n, addOpen, (openCount+1));
            }

            if (openCount > 0)
            {
                var addClose = output + ')';
                AddParenthesis(list, n, addClose, (openCount-1));
            }
        }
    }
}
