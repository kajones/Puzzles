using System;
using NUnit.Framework;

namespace HackerRank
{
    [TestFixture]
    public class Sorting
    {
        [Test]
        public void FindNumberOfSwapsInBubbleSort()
        {
            countSwaps(new[] { 1, 2, 3 }); // 0, 1, 3
            countSwaps(new[] { 3, 2, 1 }); // 3, 1, 3
        }

        // Complete the countSwaps function below.
        static void countSwaps(int[] a)
        {
            var n = a.Length;
            var numSwaps = 0;

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n - 1; j++)
                {
                    // Swap adjacent elements if they are in decreasing order
                    if (a[j] > a[j + 1])
                    {
                        //swap(a[j], a[j + 1]);
                        var temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                        numSwaps++;
                    }
                }

            }

            var firstElement = a[0];
            var lastElement = a[n - 1];

            Console.WriteLine($"Array is sorted in {numSwaps} swaps.");
            Console.WriteLine($"First Element: {firstElement}");
            Console.WriteLine($"Last Element: {lastElement}");
        }


    }
}
