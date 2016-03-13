using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a linked list, swap every two adjacent nodes and return its head.
    ///
    /// For example,
    /// Given 1->2->3->4, you should return the list as 2->1->4->3.
    ///
    /// Your algorithm should use only constant space. 
    /// You may not modify the values in the list, only nodes itself can be changed.
    /// </summary>
    [TestFixture]
    public class Problem_0024_SwapNodesInPairs
    {
        [Test]
        public void EmptyListReturnsNothing()
        {
            var result = SwapPairs(null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void SingleItemListIsUnchanged()
        {
            var result = SwapPairs(new ListNode(3));

            Assert.That(result.val, Is.EqualTo(3));
            Assert.That(result.next, Is.Null);
        }

        [Test]
        public void PairListIsSwapped()
        {
            var list = GenerateList(new[] { 1, 2 });

            var result = SwapPairs(list);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(1));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void RunExample()
        {
            var list = GenerateList(new[] { 1, 2, 3, 4 });

            var result = SwapPairs(list);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(1));
            Assert.That(result.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null) return head;

            ListNode newHead = head.next;
            var left = head;
            var right = head.next;
            ListNode current = null;

            while (left != null && right != null)
            {
                var origLeft = left;
                var nextPair = right.next;
                left = right;
                left.next = origLeft;
                origLeft.next = nextPair;

                if (current != null)
                {
                    current.next = right;
                }
                current = origLeft;

                if (nextPair == null)
                {
                    break;
                }

                left = nextPair;
                right = nextPair.next;
            }

            return newHead;
        }

        private static ListNode GenerateList(int[] values)
        {
            ListNode first = new ListNode(values[0]);
            ListNode previous = first;
            for (var idx = 1; idx < values.Length; ++idx)
            {
                var next = new ListNode(values[idx]);
                previous.next = next;
                previous = next;
            }
            return first;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x)
            {
                val = x;
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
    }
}
