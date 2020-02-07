using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace HackerRank
{
    [TestFixture]
    public class Arrays
    {
        [Test]
        public void FindMaxHourglassValue()
        {
            var array1 = new int[][] {
                new[] { 1, 1, 1, 0, 0, 0 },
                new[] { 0, 1, 0, 0, 0, 0 },
                new[] { 1, 1, 1, 0, 0, 0 },
                new[] { 0, 0, 2, 4, 4, 0 },
                new[] { 0, 0, 0, 2, 0, 0 },
                new[] { 0, 0, 1, 2, 4, 0 } 
            };
            var max1 = hourglassSum(array1);
            Assert.AreEqual(19, max1);
        }

        // Hourglass is the selected elements in an array
        // 1 2 3
        //   4
        // 5 6 7
        // Complete the hourglassSum function below.
        static int hourglassSum(int[][] arr)
        {
            var values = new List<int>();

            for (var rowIdx = 0; rowIdx < arr.Length-2; ++ rowIdx)
            {
                var rowA = arr[rowIdx];
                var rowB = arr[rowIdx + 1];
                var rowC = arr[rowIdx + 2];
                for (var colIdx = 0; colIdx < rowA.Length-2; ++colIdx)
                {
                    var sum = rowA[colIdx] + rowA[colIdx + 1] + rowA[colIdx + 2]
                        + rowB[colIdx + 1]
                        + rowC[colIdx] + rowC[colIdx + 1] + rowC[colIdx + 2];
                    values.Add(sum);
                }
            }

            return values.Max();
        }

        [Test]
        public void DoLeftRotation()
        {
            var result = rotLeft(new[] { 1, 2, 3, 4, 5 }, 4);
            result.Should().BeEquivalentTo(new[] { 5, 1, 2, 3, 4 });
        }

        // Complete the rotLeft function below.
        static int[] rotLeft(int[] a, int d)
        {
            var queue = new Queue<int>(a);

            for(var i = 0; i < d; ++i)
            {
                var popped = queue.Dequeue();
                queue.Enqueue(popped);
            }

            return queue.ToArray();
        }

        [Test]
        public void BribeToJumpQueue()
        {
            minimumBribes(new[] { 2, 1, 5, 3, 4 }); // 3
            //Assert.AreEqual(3, res1);
            minimumBribes(new[] { 2, 5, 1, 3, 4 }); // Too chaotic > 2 bribes for one person

            minimumBribes(new[] { 5, 1, 2, 3, 7, 8, 6, 4 });

            minimumBribes(new[] { 1, 2, 5, 3, 7, 8, 6, 4 });
        }

        // Complete the minimumBribes function below.
        static void minimumBribes(int[] q)
        {
            var bribes = new Dictionary<int, int>();
            var tooChaotic = false;

            var working = Enumerable.Range(1, q.Length).ToArray();

            for(var idx = 0; idx < working.Length; ++idx)
            {
                if (q[idx] == working[idx]) continue;

                if (idx < working.Length - 1)
                {
                    if (working[idx + 1] == q[idx])
                    {
                        var temp = working[idx];
                        working[idx] = working[idx + 1];
                        working[idx + 1] = temp;

                        if (!bribes.ContainsKey(q[idx])) bribes.Add(q[idx], 0);
                        bribes[q[idx]]++;

                        continue;
                    }
                }

                if (idx < working.Length - 2)
                {
                    if (working[idx + 2] == q[idx])
                    {
                        var temp = working[idx + 1];
                        working[idx + 1] = working[idx + 2];
                        working[idx + 2] = temp;

                        temp = working[idx];
                        working[idx] = working[idx + 1];
                        working[idx + 1] = temp;

                        if (!bribes.ContainsKey(q[idx])) bribes.Add(q[idx], 0);
                        bribes[q[idx]]+=2;

                        continue;
                    }
                }

                tooChaotic = true;
                break;
            }

            if (bribes.Any(kvp => kvp.Value > 2) || tooChaotic)
            {
                Console.WriteLine("Too chaotic");
            }
            else
            {
                Console.WriteLine(bribes.Sum(kvp => kvp.Value));
            }
        }

        [Test]
        public void AddValuesToArray()
        {
            var queries1 = new int[][] {
                new[] {1, 2, 100 },
                new[] {2, 5, 100 },
                new[] {3, 4, 100} }
            ;
            var max = arrayManipulation(5, queries1);
            max.Should().Be(200);

            var queries2 = new int[][] {
                new int[] {2, 6, 8 },
                new int[] {3, 5, 7 },
                new int[] {1, 8, 1 },
                new int[] {5, 9, 15} };

            var max2 = arrayManipulation(10, queries2);
            Assert.AreEqual(31, max2);

            var query3Text = FileHelper.GetFileLines(new[] { "Files", "Arrays1636.txt" });
            var queries3 = LinesToNumbers(query3Text);

            var max3 = arrayManipulation(4000, queries3);
            Assert.AreEqual(max3, 7542539201);

            // Int up to 2,147,483,647
        }

        private int[][] LinesToNumbers(List<string> query3Text)
        {
            var numbers = new List<int[]>();

            foreach(var line in query3Text)
            {
                var i1 = line.IndexOf(" ");
                var i2 = line.IndexOf(" ", i1 + 1);

                var n1 = Convert.ToInt32(line.Substring(0, i1));
                var n2 = Convert.ToInt32(line.Substring(i1, i2 - i1));
                var n3 = Convert.ToInt32(line.Substring(i2));

                numbers.Add(new[] { n1, n2, n3 });
            }

            return numbers.ToArray();
        }

        static long arrayManipulation(int n, int[][] queries)
        {
            var working = new long[n+2];

            foreach (var query in queries)
            {
                long toAdd = query[2];

                working[query[0]] += toAdd;
                working[query[1]+1] -= toAdd;
            }

            long max = 0;
            for(var idx = 1; idx < working.Length; ++idx)
            {
                working[idx] = working[idx - 1] + working[idx];
                if (working[idx] > max) max = working[idx];
            }
            return max;
        }


        [Test]
        public void FindMinimumSwapsToOrder()
        {
            var res1 = minimumSwaps(new[] { 7, 1, 3, 2, 4, 5, 6 });
            res1.Should().Be(5);

            var res2 = minimumSwaps(new[] { 4, 3, 1, 2 });
            res2.Should().Be(3);

            var res3 = minimumSwaps(new[] { 2, 3, 4, 1, 5 });
            res3.Should().Be(3);

            var res4 = minimumSwaps(new[] { 1, 3, 5, 2, 4, 6, 7 });
            res4.Should().Be(3);
        }

        static int minimumSwaps(int[] arr)
        {
            var positionsOfValues = new int[arr.Length + 2];
            for(var idx = 0; idx < arr.Length; ++idx)
            {
                positionsOfValues[arr[idx]] = idx;
            }

            var swapCount = 0;
            for(var idx = 0; idx < arr.Length; ++idx)
            {
                if (arr[idx] == idx + 1) continue;

                // Store the current (incorrect) value
                var temp = arr[idx];

                // Update to the correct value
                arr[idx] = idx + 1;

                // Move the value that was in the wrong place to where the correct value had been
                arr[positionsOfValues[idx + 1]] = temp;

                // Update the known position of that incorrect value to its new location
                positionsOfValues[temp] = positionsOfValues[idx + 1];

                swapCount++;
            }

            return swapCount;
        }
    }
}
