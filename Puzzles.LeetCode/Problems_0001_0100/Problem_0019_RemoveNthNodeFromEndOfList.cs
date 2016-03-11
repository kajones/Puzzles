using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a linked list, remove the nth node from the end of list and return its head.
    ///
    /// For example,
    ///
    ///   Given linked list: 1->2->3->4->5, and n = 2.
    ///
    ///   After removing the second node from the end, the linked list becomes 1->2->3->5.
    /// Note:
    /// Given n will always be valid.
    /// Try to do this in one pass.
    /// </summary>
    [TestFixture]
    public class Problem_0019_RemoveNthNodeFromEndOfList
    {
        [Test]
        public void RunExample()
        {
            var node1 = new ListNode(1);
            var node2 = new ListNode(2);
            var node3 = new ListNode(3);
            var node4 = new ListNode(4);
            var node5 = new ListNode(5);
            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;

            var result = RemoveNthFromEnd(node1, 2);

            Assert.That(result, Is.Not.Null, "Have head");
            Assert.That(result.val, Is.EqualTo(1), "Head 1");
            Assert.That(result.next, Is.Not.Null, "Head + 1");
            Assert.That(result.next.val, Is.EqualTo(2), "2");
            Assert.That(result.next.next.val, Is.EqualTo(3), "3");
            Assert.That(result.next.next.next.val, Is.EqualTo(5), "5");
            Assert.That(result.next.next.next.next, Is.Null, "End of list");
        }

        [Test]
        public void SingleNodeList()
        {
            var node = new ListNode(1);

            var result = RemoveNthFromEnd(node, 1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void RemoveLastNodeFromTwo()
        {
            var node1 = new ListNode(1);
            var node2 = new ListNode(2);
            node1.next = node2;

            var result = RemoveNthFromEnd(node1, 1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next, Is.Null);
        }

        [Test]
        public void RemoveLastNodeFromThree()
        {
            var node1 = new ListNode(1);
            var node2 = new ListNode(2);
            var node3 = new ListNode(3);
            node1.next = node2;
            node2.next = node3;

            var result = RemoveNthFromEnd(node1, 1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void RemoveFirstNodeFromTwo()
        {
            var node1 = new ListNode(1);
            var node2 = new ListNode(2);
            node1.next = node2;

            var result = RemoveNthFromEnd(node1, 2);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next, Is.Null);
        }

        [Test]
        public void RemoveFirstNodeFromThree()
        {
            var node1 = new ListNode(1);
            var node2 = new ListNode(2);
            var node3 = new ListNode(3);
            node1.next = node2;
            node2.next = node3;

            var result = RemoveNthFromEnd(node1, 3);

            Assert.That(result.val, Is.EqualTo(2));
            Assert.That(result.next.val, Is.EqualTo(3));
            Assert.That(result.next.next, Is.Null);
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null) return null;
            if (head.next == null) return null;
           
            var queue = new Queue<ListNode>();

            var current = head;
            do
            {
                queue.Enqueue(current);
                current = current.next;
                while (queue.Count > n)
                {
                    queue.Dequeue();
                }
            } while (current != null && current.next != null);

            var removingFirst = n > queue.Count;
            ListNode nodeToLinkForward;
            if (removingFirst)
            {
                queue.Dequeue();
                if (queue.Count > 0)
                {
                    nodeToLinkForward = queue.Dequeue();
                }
                else
                {
                    nodeToLinkForward = current;
                }
            }
            else
            {
                nodeToLinkForward = queue.Dequeue();
                if (queue.Count > 0)
                {
                    queue.Dequeue();
                    if (queue.Count > 0)
                    {
                        nodeToLinkForward.next = queue.Dequeue();
                    }
                    else
                    {
                        nodeToLinkForward.next = current;
                    }
                }
                else
                {
                    nodeToLinkForward.next = null;
                }
            }

            return removingFirst ? nodeToLinkForward : head;
        }

        public class ListNode 
        {
            public int val;
            public ListNode next;
            public ListNode(int x) 
            { 
                val = x; 
            }
        }
    }
}
