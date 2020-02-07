using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace HackerRank
{
    [TestFixture]
    public class Search
    {
        [Test]
        public void BuyIcecream()
        {
            var sample1 = whatFlavorsReturnValue(new[] { 2, 1, 3, 5, 6 }, 5); // 1 and 3
            sample1.Value1.Should().Be(1);
            sample1.Value2.Should().Be(3);

            var sample2 = whatFlavorsReturnValue(new[] { 1, 4, 5, 3, 2 }, 4); // 1 and 4
            sample2.Value1.Should().Be(1);
            sample2.Value2.Should().Be(4);

            var sample3 = whatFlavorsReturnValue(new[] { 2, 2, 4, 3 }, 4); // 1 and 2
            sample3.Value1.Should().Be(1);
            sample3.Value2.Should().Be(2);

            var tests = GetTests();
            var testAnswers = GetTestAnswers();

            for (int idx = 0; idx < tests.Count; idx++)
            {
                IcecreamTest test = tests[idx];
                // First test - 7839 10519
                //var money = test.Money;
                //var firstBought = test.Cost[7838];
                //var secondBought = test.Cost[10518];
                //var next = test.Cost[10519];

                Console.WriteLine($"Test: {idx}");
                var answer = whatFlavorsReturnValue(test.Cost, test.Money);
                answer.Should().NotBeNull();
                answer.Should().BeEquivalentTo(testAnswers[idx], $"Test: {idx}");
            }

            //Assert.Fail("Try online");
        }

        [Test]
        public void BuyIcecreamWriteOut()
        {
            //whatFlavors(new[] { 2, 1, 3, 5, 6 }, 5); // 1 and 3

            //whatFlavors(new[] { 1, 4, 5, 3, 2 }, 4); // 1 and 4

            //whatFlavors(new[] { 2, 2, 4, 3 }, 4); // 1 and 2

            var tests = GetTests();

            for (int idx = 0; idx < tests.Count; idx++)
            {
                IcecreamTest test = tests[idx];
                //Console.WriteLine($"Test: {idx}");
                whatFlavors(test.Cost, test.Money);
            }

            Assert.Fail("Try online");
        }


        private IList<IcecreamTest> GetTests()
        {
            // The first line contains an integer, t, the number of trips to the ice cream parlor.            // Each of the next t sets of 3 lines is as follows:            //  The first line contains money.            //  The second line contains an integer, n, the size of the array cost.            //  The third line contains space - separated integers denoting the cost[i].
            var fileContents = FileHelper.GetFileLines(new[] { "Files", "HashSet.txt" });
            var numberOfTests = Convert.ToInt32(fileContents[0]);

            var tests = new List<IcecreamTest>();

            for(var testIdx = 0; testIdx < numberOfTests; ++testIdx)
            {
                var moneyLine = 1 + (testIdx * 3);
                var costLine = 3 + (testIdx * 3);

                var money = Convert.ToInt32(fileContents[moneyLine]);
                var costLineText = fileContents[costLine];
                var costTexts = costLineText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var cost = costTexts.Select(t => Convert.ToInt32(t)).ToArray();

                tests.Add(new IcecreamTest { Money = money, Cost = cost });
            }

            return tests;
        }

        private IList<IcecreamTestAnswers> GetTestAnswers()
        {
            var fileContents = FileHelper.GetFileLines(new[] { "Files", "HashSetAnswers.txt" });

            var testAnswers = new List<IcecreamTestAnswers>();

            for(var idx = 0; idx < fileContents.Count; ++idx)
            {
                var lineText = fileContents[idx];
                var numbers = lineText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt32(t)).ToList();
                testAnswers.Add(new IcecreamTestAnswers { Value1 = numbers[0], Value2 = numbers[1] });
            }

            return testAnswers;
        }

        static IcecreamTestAnswers oldwhatFlavorsReturnValue(int[] cost, int money)
        {
            var flavours = new HashSet<int>(cost);

            for (var idx1 = 0; idx1 < cost.Length - 1; ++idx1)
            {
                var availableMoney = money - cost[idx1];
                if (flavours.Contains(availableMoney))
                {
                    for (var idx2 = idx1 + 1; idx2 < cost.Length; idx2++)
                    {
                        if (cost[idx2] == availableMoney)
                        {
                            //Console.WriteLine($"{idx1 + 1} {idx2 + 1}");
                            return new IcecreamTestAnswers { Value1 = idx1 + 1, Value2 = idx2 + 1 };
                        }
                    }
                }
            }

            return null;
        }

        static IcecreamTestAnswers whatFlavorsReturnValue(int[] cost, int money)
        {
            var flavours = new Dictionary<int, int>();
            for (int idx = 0; idx < cost.Length; idx++)
            {
                int otherFlavourCost = money - cost[idx];
                if (flavours.ContainsKey(otherFlavourCost))
                {
                    return new IcecreamTestAnswers { Value1= flavours[otherFlavourCost]+1, Value2= idx + 1};
                }
                if (!flavours.ContainsKey(cost[idx])) flavours.Add(cost[idx], idx);
            }

            return null;
        }

        static void whatFlavors(int[] cost, int money)
        {
            var flavours = new Dictionary<int, int>();
            for (int idx = 0; idx < cost.Length; idx++)
            {
                int otherFlavourCost = money - cost[idx];
                if (flavours.ContainsKey(otherFlavourCost))
                {
                    Console.WriteLine($"{flavours[otherFlavourCost] + 1} {idx + 1}");
                    return;
                }
                if (!flavours.ContainsKey(cost[idx])) flavours.Add(cost[idx], idx);
            }
        }

        static void oldwhatFlavors(int[] cost, int money)
        {
            var flavours = new HashSet<int>(cost);

            for(var idx1 = 0; idx1 < cost.Length-1; ++idx1)
            {
                var availableMoney = money - cost[idx1];
                if (flavours.Contains(availableMoney))
                {
                    for(var idx2 = idx1 + 1; idx2 < cost.Length; idx2++)
                    {
                        if (cost[idx2] == availableMoney)
                        {
                            var firstFlavour = idx1 + 1;
                            var secondFlavour = idx2 + 1;
                            Console.WriteLine($"{firstFlavour} {secondFlavour}");
                            return;
                        }
                    }
                }
            }
            

        }
    }

    public class IcecreamTest
    {
        public int Money { get; set; }
        public int[] Cost { get; set; }
    }

    public class IcecreamTestAnswers
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}
