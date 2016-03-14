using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a linked list, reverse the nodes of a linked list k at a time and return its modified list.
    ///
    /// If the number of nodes is not a multiple of k then left-out nodes in the end should remain as it is.
    ///
    /// You may not alter the values in the nodes, only nodes itself may be changed.
    ///
    /// Only constant memory is allowed.
    ///
    /// For example,
    /// Given this linked list: 1->2->3->4->5
    ///
    /// For k = 2, you should return: 2->1->4->3->5
    ///
    /// For k = 3, you should return: 3->2->1->4->5
    /// </summary>
    [TestFixture]
    public class Problem_0025_ReverseNodesInKGroup
    {
        [Test]
        public void NullList()
        {
            var result = ReverseKGroup(null, 2);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void SingleNodeGroupOf2()
        {
            var list = new ListNode(1);

            var result = ReverseKGroup(list, 2);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next, Is.Null);
        }

        [Test]
        public void TwoNodesGroupOf2()
        {
            var list = GenerateList(new[] { 1, 2 });

            var result = ReverseKGroup(list, 2);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(1));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void FourNodesGroupsOf2()
        {
            var list = GenerateList(new[] { 1, 2,3,4 });

            var result = ReverseKGroup(list, 2);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(1));
            Assert.That(result.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        [Test]
        public void RunExampleForK2()
        {
            var list = GenerateList(new[] { 1, 2, 3, 4, 5 });

            var result = ReverseKGroup(list, 2);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(1));
            Assert.That(result.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.next.val, Is.EqualTo(5));
            Assert.That(result.next.next.next.next.next, Is.Null);
        }

        [Test]
        public void RunExampleForK3()
        {
            var list = GenerateList(new[] { 1, 2, 3, 4, 5 });

            var result = ReverseKGroup(list, 3);

            Assert.That(result.val, Is.EqualTo(3));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(1));
            Assert.That(result.next.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.next.val, Is.EqualTo(5));
            Assert.That(result.next.next.next.next.next, Is.Null);
        }

        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (k < 2) return head;

            ListNode newHead = null;
            ListNode lastGroupEnd = null;
            ListNode current = head;
            ListNode startGroup = null;

            var stack = new Stack<ListNode>();

            do
            {
                if (stack.Count == k)
                {
                    var groupNode = stack.Pop();

                    if (lastGroupEnd == null)
                    {
                        newHead = groupNode;
                    }
                    else
                    {
                        lastGroupEnd.next = groupNode;
                    }

                    while (stack.Count > 0)
                    {
                        var temp = stack.Pop();
                        groupNode.next = temp;
                        groupNode = temp;
                    }
                    lastGroupEnd = groupNode;
                    startGroup = current;
                }

                stack.Push(current);

                if (current != null)
                {
                    current = current.next;
                }
            } while (current != null);
            
            
            if (stack.Count == k)
            {
                var groupNode = stack.Pop();
                if (newHead == null) newHead = groupNode;
                var startFinalGroup = groupNode;

                while (stack.Count > 0)
                {
                    var temp = stack.Pop();
                    groupNode.next = temp;
                    groupNode = temp;
                }
                groupNode.next = null;
                if (lastGroupEnd != null)
                {
                    lastGroupEnd.next = startFinalGroup;
                }
                return newHead;
            }
            else
            {
                if (lastGroupEnd != null)
                {
                    lastGroupEnd.next = startGroup;
                }
            }
            
            return newHead ?? head;
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
