using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// You are given two linked lists representing two non-negative numbers. 
    /// The digits are stored in reverse order and each of their nodes contain a single digit. 
    /// Add the two numbers and return it as a linked list.
    ///
    /// Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
    /// Output: 7 -> 0 -> 8
    [TestFixture]
    public class Problem_0002_AddTwoNumbers
    {
        [Test]
        public void ConfirmValueExtractor()
        {
            var list1_1 = new ListNode(2);
            var list1_2 = new ListNode(4);
            var list1_3 = new ListNode(3);
            list1_1.next = list1_2;
            list1_2.next = list1_3;

            var value = GetValueOfList(list1_1);
            value.Should().Be(342);

            var list2_1 = new ListNode(5);
            var list2_2 = new ListNode(6);
            var list2_3 = new ListNode(4);
            list2_1.next = list2_2;
            list2_2.next = list2_3;

            value = GetValueOfList(list2_1);
            value.Should().Be(465);
        }

        [Test]
        public void ConfirmGetValueOfZeroList()
        {
            var list = new ListNode(0);

            var value = GetValueOfList(list);
            value.Should().Be(0);
        }

        [Test]
        public void ConfirmGetValueOfLargeNumber()
        {
            var list2_1 = new ListNode(1);
            var list2_2 = new ListNode(9);
            var list2_3 = new ListNode(9);
            var list2_4 = new ListNode(9);
            var list2_5 = new ListNode(9);
            var list2_6 = new ListNode(9);
            var list2_7 = new ListNode(9);
            var list2_8 = new ListNode(9);
            var list2_9 = new ListNode(9);
            var list2_10 = new ListNode(9);
            list2_1.next = list2_2;
            list2_2.next = list2_3;
            list2_3.next = list2_4;
            list2_4.next = list2_5;
            list2_5.next = list2_6;
            list2_6.next = list2_7;
            list2_7.next = list2_8;
            list2_8.next = list2_9;
            list2_9.next = list2_10;

            var value = GetValueOfList(list2_1);
            Assert.That(value, Is.EqualTo(9999999991));
        }

        [Test]
        [TestCase(1, new[] { 1})]
        [TestCase(0, new[] { 0 })]
        [TestCase(9, new[] { 9 })]
        [TestCase(10, new[] {0,1})]
        [TestCase(13, new[] { 3, 1})]
        [TestCase(100, new[] { 0, 0, 1 })]
        [TestCase(123, new[] { 3, 2, 1 })]
        public void ConfirmListGenerator(int value, int[] expectedListValues)
        {
            var list = TurnValueIntoList(value);
            foreach (var expectedListValue in expectedListValues)
            {
                Assert.AreEqual(list.val, expectedListValue, "Expected value " + expectedListValue);
                list = list.next;
            }
        }

        [Test]
        public void RunTest()
        {
            var list1_1 = new ListNode(2);
            var list1_2 = new ListNode(4);
            var list1_3 = new ListNode(3);
            list1_1.next = list1_2;
            list1_2.next = list1_3;

            var list2_1 = new ListNode(5);
            var list2_2 = new ListNode(6);
            var list2_3 = new ListNode(4);
            list2_1.next = list2_2;
            list2_2.next = list2_3;

            var result = AddTwoNumbers(list1_1, list2_1);

            result.val.Should().Be(7);
            result.next.val.Should().Be(0);
            result.next.next.val.Should().Be(8);
        }

        [Test]
        public void RunZeroValueTest()
        {
            var list1_1 = new ListNode(0);

            var list2_1 = new ListNode(0);

            var result = AddTwoNumbers(list1_1, list2_1);

            result.val.Should().Be(0);
        }

        [Test]
        public void RunTenBillionTest()
        {
            var list1_1 = new ListNode(9);

            var list2_1 = new ListNode(1);
            var list2_2 = new ListNode(9);
            var list2_3 = new ListNode(9);
            var list2_4 = new ListNode(9);
            var list2_5 = new ListNode(9);
            var list2_6 = new ListNode(9);
            var list2_7 = new ListNode(9);
            var list2_8 = new ListNode(9);
            var list2_9 = new ListNode(9);
            var list2_10 = new ListNode(9);
            list2_1.next = list2_2;
            list2_2.next = list2_3;
            list2_3.next = list2_4;
            list2_4.next = list2_5;
            list2_5.next = list2_6;
            list2_6.next = list2_7;
            list2_7.next = list2_8;
            list2_8.next = list2_9;
            list2_9.next = list2_10;

            var result = AddTwoNumbers(list1_1, list2_1);

            var value = GetValueOfList(result);

            Assert.That(value, Is.EqualTo(10000000000));
            result.val.Should().Be(0);
            result.next.val.Should().Be(0);
            result.next.next.val.Should().Be(0);
            result.next.next.next.val.Should().Be(0);
            result.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.next.next.next.next.val.Should().Be(0);
            result.next.next.next.next.next.next.next.next.next.next.val.Should().Be(1);
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var firstValue = GetValueOfList(l1);
            var secondValue = GetValueOfList(l2);

            var sum = firstValue + secondValue;

            return TurnValueIntoList(sum);         
        }

        public long GetValueOfList(ListNode listNode)
        {
            long value = 0;
            long multiplier = 1;
            do
            {
                value += (listNode.val*multiplier);
                listNode = listNode.next;
                multiplier *= 10;
            } while (listNode != null && listNode.next != null);

            if (listNode != null)
                value += (listNode.val*multiplier);

            return value;
        }

        public ListNode TurnValueIntoList(long value)
        {
            int lastDigit = GetLastDigit(value);
            var listNode = new ListNode(lastDigit);
            var currentNode = listNode;

            while (value >= 0)
            {
                value /= 10;
                lastDigit = GetLastDigit(value);

                if (value == 0) break;

                var nextNode = new ListNode(lastDigit);
                currentNode.next = nextNode;
                currentNode = nextNode;
            }

            return listNode;
        }

        private static int GetLastDigit(long value)
        {
            var textValue = value.ToString();
            return System.Convert.ToInt32(textValue.Substring(textValue.Length - 1));
        }
    }

    /// <summary>
    /// Definition for singly-linked list.
    ///  </summary>
    public class ListNode 
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
}
