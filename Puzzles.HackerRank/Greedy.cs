using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace HackerRank
{
    [TestFixture]
    public class Greedy
    {
        [Test]
        public void FindMinimumAbsoluteDifference()
        {
            var res1 = minimumAbsoluteDifference(new[] { -2, 2, 4 });
            res1.Should().Be(2);

            var res2 = minimumAbsoluteDifference(new[] { 3, -7, 0 });
            res2.Should().Be(3);

            var res3 = minimumAbsoluteDifference(new[] { -59, -36, -13, 1, -53, -92, -2, -96, -54, 75 });
            res3.Should().Be(1);

            var res4 = minimumAbsoluteDifference(new[] { 1, - 3, 71, 68, 17 });
            res4.Should().Be(3);
        }

        // Complete the minimumAbsoluteDifference function below.
        static int minimumAbsoluteDifference(int[] arr)
        {
            var orderedValues = arr.OrderBy(x => x).ToList();

            var min = Int32.MaxValue;
            for(var idx = 0; idx < orderedValues.Count-1; ++idx)
            {
                var diff = Math.Abs(orderedValues[idx] - orderedValues[idx + 1]);
                if (diff < min) min = diff;
            }

            return min;
        }

        [Test]
        public void MaximizeLuck()
        {
            var contest1 = new int[][]
            {
                new[] { 5,1},
                new[] { 1,1},
                new[] { 4,0}
            };

            var contest2 = new int[][]
            {
                new[] {5,  1 },
                new[] {2,  1 },
                new[] {1,  1 },
                new[] {8,  1 },
                new[] {10, 0 },
                new[] {5,  0 }
            };

            var balance1 = luckBalance(2, contest1);
            balance1.Should().Be(10);

            var balance2 = luckBalance(1, contest1);
            balance2.Should().Be(8);

            var balance3 = luckBalance(3, contest2);
            balance3.Should().Be(29);
        }

        // Complete the luckBalance function below.
        static int luckBalance(int k, int[][] contests)
        {
            var importantLuckValues = new List<int>();
            int unimportantLuckBalance = 0;
            foreach(var contest in contests)
            {
                if (contest[1] == 0)
                    unimportantLuckBalance += contest[0];
                else
                    importantLuckValues.Add(contest[0]);
            }

            if (importantLuckValues.Count <= k)
            {
                var importantLuckBalance = importantLuckValues.Sum();
                return unimportantLuckBalance + importantLuckBalance;
            }

            var maximizeImmportantLuck = importantLuckValues.OrderByDescending(x => x).ToList();
            var importantLuck = 0;
            for(var idx = 0; idx < maximizeImmportantLuck.Count; ++idx)
            {
                if (idx < k)
                    importantLuck += maximizeImmportantLuck[idx];
                else
                    importantLuck -= maximizeImmportantLuck[idx];
            }
            return unimportantLuckBalance + importantLuck;
        }

        [Test]
        public void BuyFlowersAtMinimumCost()
        {
            // Cost per flower is (1 + previous purchases) * price for each individual
            // So three individuals buying 4 flowers buy most expensive three first then pay more for the cheaper one
            // i.e. 2 + 3 + 4 + (1+1)*1 = 11
            var res1 = getMinimumCost(3, new[] { 1, 2, 3, 4 });
            res1.Should().Be(11);

            var res2 = getMinimumCost(3, new[] { 2, 5, 6 });
            res2.Should().Be(13);

            var res3 = getMinimumCost(2, new[] { 2, 5, 6 });
            res3.Should().Be(15);

            var res4 = getMinimumCost(3, new[] { 1, 3, 5, 7, 9 });
            res4.Should().Be(29);
        }

        // Complete the getMinimumCost function below.
        static int getMinimumCost(int k, int[] c)
        {
            if (k >= c.Length) return c.Sum();

            var individuals = new int[k].Select(x => new List<int>()).ToList();
            var flowersByPrice = c.OrderByDescending(p => p).ToList();

            var individualIndex = 0;
            foreach(var flower in flowersByPrice)
            {
                individuals[individualIndex].Add(flower * (individuals[individualIndex].Count + 1));
                individualIndex++;
                if (individualIndex >= individuals.Count) individualIndex = 0;
            }

            return individuals.Sum(x => x.Sum());
        }

        [Test]
        public void FindMinimumUnfairness()
        {
            // Taking k elements from an array, unfairness is max number - min number in the subarray
            var res1 = maxMin(2, new[] {1,4,7,2 });
            res1.Should().Be(1);

            var res2 = maxMin(3, new[] { 10, 100, 300, 200, 1000, 20, 30 });
            res2.Should().Be(20);  // max(10,20,30) - min(10,20,30) = 30 - 10 = 20

            var res3 = maxMin(4, new[] { 1, 2, 3, 4, 10, 20, 30, 40, 100, 200 });
            res3.Should().Be(3); // max(1,2,3,4) - min(1,2,3,4) = 4 - 1 = 3

            var res4 = maxMin(2, new[] { 1, 2, 1, 2, 1 });
            res4.Should().Be(0); // Choose two numbers same for 0

            // Test case 16
            var res5 = maxMin(3, new[] { 100, 200, 300, 350, 400, 401, 402 });
            res5.Should().Be(2);
        }

        // Complete the maxMin function below.
        static int maxMin(int k, int[] arr)
        {
            // By sorting you minimise the distance between the smallest and largest numbers in a subarray
            var sortedNumbers = arr.OrderBy(x => x).ToList();
            var minUnfairness = Int32.MaxValue;

            for(var idx = 0; idx < arr.Length - k+1; ++idx)
            {
                var min = sortedNumbers[idx];
                var max = sortedNumbers[idx + k - 1];
                var diff = max - min;
                if (diff < minUnfairness) minUnfairness = diff;
            }

            return minUnfairness;
        }
    }
}
