using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A common security method used for online banking is to ask the user for three random characters from a passcode. 
    /// For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: 317.
    ///
    /// The text file, keylog.txt, contains fifty successful login attempts.
    ///
    /// Given that the three characters are always asked for in order, analyse the file 
    /// so as to determine the shortest possible secret passcode of unknown length.
    /// </summary>
    [TestFixture]
    public class Problem_0079_PasscodeDerivation
    {
        [Test]
        [TestCase("", "cxyz", "abc", "abcxyz", null, null)]
        [TestCase("123", "cxyz", "abc", "123abcxyz", null, null)]
        [TestCase("", "cxyz", "abcd", "abc", "xyz", "d")]
        [TestCase("12", "cxyz", "abcd", "12abc", "xyz", "d")]
        public void TakeAllRightUpToCommonLeftChar(string stem, string left, string right, string expectedStem, string expectedLeft, string expectedRight)
        {
            var original = new CodeNode(stem, left, right);
            var result = original.TakeRightUpToFirstLeft();

            Assert.AreEqual(expectedStem, result.Stem, "Stem");
            Assert.AreEqual(expectedLeft, result.RemainingLeft, "Left");
            Assert.AreEqual(expectedRight, result.RemainingRight, "Right");
        }

        [Test]
        [TestCase("", "abc", "cx", "abcx", null, null)]
        [TestCase("12", "abc", "cx", "12abcx", null, null)]
        [TestCase("12", "abcd", "cx", "12abc", "d", "x")]
        public void TakeAllLeftUpToCommonRightChar(string stem, string left, string right, string expectedStem, string expectedLeft, string expectedRight)
        {
            var original = new CodeNode(stem, left, right);
            var result = original.TakeLeftUpToFirstRight();

            Assert.AreEqual(expectedStem, result.Stem, "Stem");
            Assert.AreEqual(expectedLeft, result.RemainingLeft, "Left");
            Assert.AreEqual(expectedRight, result.RemainingRight, "Right");
        }

        [Test]
        [TestCase("abc", "abc", new[] { "abc" })]
        [TestCase("abc", "ayc", new[] { "abyc", "aybc" })]
        [TestCase("abc", "xyc", new[] { "abxyc", "axbyc", "axybc", "xabyc", "xaybc", "xyabc" })]
        [TestCase("abc", "bde", new[] { "abcde", "abdce", "abdec" })]
        public void ConfirmAvailablePasscodes(string attempt, string nextAttempt, string[] expectedPasscodes)
        {
            var result = GetPossiblePasscodes(attempt, nextAttempt);

            foreach (var answer in result)
            {
                Console.WriteLine(answer);
            }

            Assert.AreEqual(expectedPasscodes.Length, result.Count, "Same number of answers");
            foreach (var expectedPasscode in expectedPasscodes)
            {
                Assert.IsTrue(result.Contains(expectedPasscode), expectedPasscode);
            }
        }

        /// <summary>
        /// 73162890
        /// </summary>
        [Test, Explicit]
        public void FindShortestPasscodeForFiftyAttempts()
        {
            const string resourcePath = "Puzzles.ProjectEuler.DataFiles.Problem_0079_keylog.txt";
            var fileContent = FileHelper.GetEmbeddedResourceContent(resourcePath);
            var attempts = fileContent.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            attempts.Count.Should().Be(50);

            var existing = new List<string> { attempts[0] };
            for (var idx = 1; idx < attempts.Count; ++idx)
            {
                var results = new List<string>();
                foreach (var previousAttempt in existing)
                {
                    var combinationResults = GetPossiblePasscodes(previousAttempt, attempts[idx]);
                    results.AddRange(combinationResults);
                }

                existing = results.Distinct().ToList();
            }

            var shortest = existing.OrderBy(item => item.Length).First();
            Console.WriteLine("Shortest passcode: {0}", shortest);
            shortest.Should().Be("73162890");

            foreach (var candidate in existing)
            {
                Console.WriteLine(candidate);
            }
        }

        private static List<string> GetPossiblePasscodes(string attempt, string nextAttempt)
        {
            if (attempt == nextAttempt) return new List<string> { attempt };

            var root = new CodeNode(string.Empty, attempt, nextAttempt);
            var toProcess = new Queue<CodeNode>();
            toProcess.Enqueue(root);
            var result = new List<string>();

            while (toProcess.Count > 0)
            {
                var codeNode = toProcess.Dequeue();

                if (codeNode.IsComplete)
                {
                    result.Add(codeNode.Stem);
                    continue;
                }

                if (codeNode.NextCharSame)
                {
                    toProcess.Enqueue(codeNode.TakeBoth());
                    continue;
                }

                if (codeNode.NextLeftInRight)
                {
                    toProcess.Enqueue(codeNode.TakeRightUpToFirstLeft());
                    continue;
                }

                if (codeNode.NextRightInLeft)
                {
                    toProcess.Enqueue(codeNode.TakeLeftUpToFirstRight());
                    continue;
                }

                toProcess.Enqueue(codeNode.TakeLeft());
                toProcess.Enqueue(codeNode.TakeRight());
            }

            return result;
        }
    }

    public class CodeNode
    {
        public string RemainingLeft { get; protected set; }
        public string RemainingRight { get; protected set; }
        public bool IsComplete { get; private set; }
        public string Stem { get; private set; }

        public bool NextCharSame
        {
            get { return RemainingLeft[0] == RemainingRight[0]; }
        }

        public bool NextLeftInRight
        {
            get
            {
                return RemainingRight.Contains(RemainingLeft.Substring(0, 1));
            }
        }

        public bool NextRightInLeft
        {
            get { return RemainingLeft.Contains(RemainingRight.Substring(0, 1)); }
        }

        public CodeNode(string stem, string left, string right)
        {
            if (string.IsNullOrEmpty(left))
            {
                Stem = string.Concat(stem, right);
                IsComplete = true;
            }
            else if (string.IsNullOrEmpty(right))
            {
                Stem = string.Concat(stem, left);
                IsComplete = true;
            }
            else
            {
                Stem = stem;
                RemainingLeft = left;
                RemainingRight = right;
                IsComplete = false;
            }
        }

        public CodeNode TakeLeft()
        {
            var nextStem = string.Concat(Stem, RemainingLeft.Substring(0, 1));
            var left = GetRemainingLeftAfterFirst();

            return new CodeNode(nextStem, left, RemainingRight);
        }

        public CodeNode TakeRight()
        {
            var nextStem = string.Concat(Stem, RemainingRight.Substring(0, 1));
            var right = GetRemainingRightAfterFirst();

            return new CodeNode(nextStem, RemainingLeft, right);
        }

        public CodeNode TakeBoth()
        {
            var nextStem = string.Concat(Stem, RemainingLeft.Substring(0, 1));
            var left = GetRemainingLeftAfterFirst();
            var right = GetRemainingRightAfterFirst();

            return new CodeNode(nextStem, left, right);
        }

        public CodeNode TakeRightUpToFirstLeft()
        {
            var firstLeft = RemainingLeft.Substring(0, 1);
            var left = GetRemainingLeftAfterFirst();

            var positionOfCommon = RemainingRight.IndexOf(firstLeft, StringComparison.Ordinal);
            var toTake = positionOfCommon + 1;
            var nextStem = string.Concat(Stem, RemainingRight.Substring(0, toTake));
            var right = RemainingRight.Length > toTake
                ? RemainingRight.Substring(toTake)
                : string.Empty;

            return new CodeNode(nextStem, left, right);
        }

        public CodeNode TakeLeftUpToFirstRight()
        {
            var firstRight = RemainingRight.Substring(0, 1);
            var right = GetRemainingRightAfterFirst();

            var positionOfCommon = RemainingLeft.IndexOf(firstRight, StringComparison.Ordinal);
            var toTake = positionOfCommon + 1;
            var nextStem = string.Concat(Stem, RemainingLeft.Substring(0, toTake));
            var left = RemainingLeft.Length > toTake ? RemainingLeft.Substring(toTake) : string.Empty;

            return new CodeNode(nextStem, left, right);
        }

        private string GetRemainingLeftAfterFirst()
        {
            return RemainingLeft.Length > 1 ? RemainingLeft.Substring(1) : string.Empty;
        }

        private string GetRemainingRightAfterFirst()
        {
            return RemainingRight.Length > 1 ? RemainingRight.Substring(1) : string.Empty;
        }
    }
}
