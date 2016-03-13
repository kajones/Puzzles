using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Merge k sorted linked lists and return it as one sorted list. Analyze and describe its complexity.
    /// </summary>
    [TestFixture]
    public class Problem_0023_MergeKSortedLists
    {
        [Test]
        public void NoLists()
        {
            var result = MergeKLists(new ListNode[0]);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void SingleList()
        {
            var list1 = GenerateList(new[] {1, 2});

            var result = MergeKLists(new ListNode[] {list1});

            Assert.That(result.val, Is.EqualTo(list1.val));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next, Is.Null);
        }

        [Test]
        public void TwoLists()
        {
            var list1 = GenerateList(new[] { 1, 3 });
            var list2 = GenerateList(new[] { 2, 4 });

            var result = MergeKLists(new ListNode[] { list1, list2 });

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.next, Is.Null);
        }

        [Test]
        public void TwoEmptyLists()
        {
            var result = MergeKLists(new ListNode[] { null, null });

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ThreeLists()
        {
            var list1 = GenerateList(new[] { 1, 6 });
            var list2 = GenerateList(new[] { 2, 5 });
            var list3 = GenerateList(new[] { 3, 4 });

            var result = MergeKLists(new ListNode[] { list1, list2, list3 });

            Assert.That(result.val, Is.EqualTo(1));
            Assert.That(result.next.val, Is.EqualTo(2));
            Assert.That(result.next.next.val, Is.EqualTo(3));
            Assert.That(result.next.next.next.val, Is.EqualTo(4));
            Assert.That(result.next.next.next.next.val, Is.EqualTo(5));
            Assert.That(result.next.next.next.next.next.val, Is.EqualTo(6));
            Assert.That(result.next.next.next.next.next.next, Is.Null);
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0) return null;
            if (lists.Length == 1) return lists[0];

            ListNode head = null;
            ListNode current = null;

            var toManipulate = new List<ListNode>(lists.Where(l => l != null));
            while (toManipulate.Count > 0)
            {
                int lowestValue = int.MaxValue;
                int lowestIdx = 0;
                for (var idx = 0; idx < toManipulate.Count; ++idx)
                { 
                    if (toManipulate[idx].val < lowestValue)
                    {
                        lowestValue = toManipulate[idx].val;
                        lowestIdx = idx;
                    }
                }

                if (head == null)
                {
                    head = toManipulate[lowestIdx];
                    current = head;
                }
                else
                {
                    current.next = toManipulate[lowestIdx];
                    current = current.next;
                }

                var next = toManipulate[lowestIdx].next;
                if (next == null)
                {
                    toManipulate.RemoveAt(lowestIdx);
                }
                else
                {
                    toManipulate[lowestIdx] = next;
                }
            }

            return head;
        }

        private static ListNode GenerateList(int[] values)
        {
            ListNode first = new ListNode(values[0]);
            ListNode previous = first;
            for(var idx = 1; idx < values.Length; ++idx)
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
