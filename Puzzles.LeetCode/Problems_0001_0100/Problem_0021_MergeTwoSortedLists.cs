using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Merge two sorted linked lists and return it as a new list. 
    /// The new list should be made by splicing together the nodes of the first two lists.
    /// </summary>
    [TestFixture]
    public class Problem_0021_MergeTwoSortedLists
    {
        [Test]
        public void MergeEmptyLists()
        {
            var result = MergeTwoLists(null, null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void MergeSingleItemLists()
        {
            var list1Node1 = new ListNode(1);
            var list2Node1 = new ListNode(2);

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void MergeSingleItemListsWithL2HavingSmallerValue()
        {
            var list1Node1 = new ListNode(2);
            var list2Node1 = new ListNode(1);

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void MergeLong1ToShort2()
        {
            var list1Node1 = new ListNode(1);
            var list1Node2 = new ListNode(2);
            var list1Node3 = new ListNode(3);
            list1Node1.next = list1Node2;
            list1Node2.next = list1Node3;
            var list2Node1 = new ListNode(2);

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        [Test]
        public void MergeShort1ToLong2()
        {
            var list1Node1 = new ListNode(1);
            var list2Node1 = new ListNode(2);
            var list2Node2 = new ListNode(3);
            var list2Node3 = new ListNode(4);
            list2Node1.next = list2Node2;
            list2Node2.next = list2Node3;

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        [Test]
        public void RunTest()
        {
            var list1Node1 = new ListNode(1);
            var list1Node2 = new ListNode(2);
            list1Node1.next = list1Node2;

            var list2Node1 = new ListNode(2);
            var list2Node2 = new ListNode(3);
            list2Node1.next = list2Node2;

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        [Test]
        public void RunExample()
        {
            // [-2,5]
            // [-9,-6,-3,-1,1,6]
            var list1Node1 = new ListNode(-2);
            var list1Node2 = new ListNode(5);
            list1Node1.next = list1Node2;

            var list2Node1 = new ListNode(-9);
            var list2Node2 = new ListNode(-6);
            var list2Node3 = new ListNode(-3);
            var list2Node4 = new ListNode(-1);
            var list2Node5 = new ListNode(1);
            var list2Node6 = new ListNode(6);
            list2Node1.next = list2Node2;
            list2Node2.next = list2Node3;
            list2Node3.next = list2Node4;
            list2Node4.next = list2Node5;
            list2Node5.next = list2Node6;

            var result = MergeTwoLists(list1Node1, list2Node1);

            Assert.That(result.val, Is.EqualTo(-9));
            Assert.That(result.next.val, Is.EqualTo(-6));
            Assert.That(result.next.next.val, Is.EqualTo(-3));
            Assert.That(result.next.next.next.val, Is.EqualTo(-2));
            Assert.That(result.next.next.next.next.val, Is.EqualTo(-1));
            Assert.That(result.next.next.next.next.next.val, Is.EqualTo(1));
            Assert.That(result.next.next.next.next.next.next.val, Is.EqualTo(5));
            Assert.That(result.next.next.next.next.next.next.next.val, Is.EqualTo(6));
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
            {
                return l2;
            }
            if (l2 == null)
            {
                return l1;
            }

            var head = l1;
            var left = l1;
            var right = l2;
            if (l1.val > l2.val)
            {
                head = l2;
                left = l2;
                right = l1;
            }

            while (left != null && right != null)
            {
                if (left.next != null)
                {
                    while (left.next != null && left.next.val <= right.val)
                    {
                        left = left.next;
                    }
                }
                else
                {
                    left.next = right;
                    break;
                }

                var leftNext = left.next;
                left.next = right;
                if (leftNext == null) break;

                left = right;
                right = leftNext;
            }

            return head;
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
