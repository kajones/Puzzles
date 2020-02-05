using System.Linq;
using NUnit.Framework;

namespace HackerRank
{
    public class Preparatory
    {
        [Test]
        public void SockPairs()
        {
            var pairs = sockMerchant(9, new[] { 10, 20, 20, 10, 10, 30, 50, 10, 20 });
            Assert.AreEqual(3, pairs);
        }

        static int sockMerchant(int n, int[] ar)
        {
            var pairs = ar.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() / 2 });
            return pairs.Sum(p => p.Count);

        }

        [Test]
        public void CountValleys()
        {
            var count = countingValleys(8, "UDDDUDUU");
            Assert.AreEqual(1, count);
        }

        // Complete the countingValleys function below.
        static int countingValleys(int n, string s)
        {
            var height = 0;
            var valleys = 0;
            var inValley = false;

            foreach(var step in s)
            {
                if (step == 'U')
                {
                    height++;
                    if (inValley && height == 0) valleys++;
                }
                else if (step == 'D')
                {
                    height--;
                }

                if (height < 0)
                    inValley = true;
                else
                    inValley = false;
            }

            return valleys;
        }

        [Test]
        public void JumpingClouds()
        {
            var res = jumpingOnClouds( new[] { 0, 0, 1, 0, 0, 1, 0 });
            Assert.AreEqual(4, res);

            var res2 = jumpingOnClouds(new[] { 0, 0, 0, 0, 1, 0 });
            Assert.AreEqual(3, res2);
        }

        static int jumpingOnClouds(int[] c)
        {
            var steps = 0;

            var idx = 0;

            while (idx < c.Length)
            {
                var longer = idx + 2;
                if (longer < c.Length && c[longer] == 0)
                {
                    idx = longer;
                    steps++;
                    continue;
                }

                var shorter = idx + 1;
                if (shorter < c.Length && c[shorter] == 0)
                    {
                    idx = shorter;
                    steps++;
                    continue;
                }

                break;
            }

            return steps;

        }

        [Test]
        public void CountAInRepeatedString()
        {
            var count1 = repeatedString("aba", 10);
            Assert.AreEqual(7, count1);

            var count2 = repeatedString("a", 1000000000000);
            Assert.AreEqual(1000000000000, count2);
        }

        // Complete the repeatedString function below.
        static long repeatedString(string s, long n)
        {
            if (n < s.Length)
            {
                var shortString = s.Substring(0, (int)n);
                return shortString.Count(c => c == 'a');
            }

            var instancesOfString = n / s.Length;
            var remainderStringLength = n % s.Length;
            var remainderString = s.Substring(0, (int) remainderStringLength);

            var perInstanceCount = s.Count(c => c == 'a');
            var remainderCount = remainderString.Count(c => c == 'a');

            return (instancesOfString * perInstanceCount) + remainderCount;
        }
    }
}


