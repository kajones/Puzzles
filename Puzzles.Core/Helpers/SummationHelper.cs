using System.Collections.Generic;

namespace Puzzles.Core.Helpers
{
    public class SummationHelper
    {
        private readonly long[] Numbers;

        private Dictionary<long, Dictionary<long, long>> sums = new Dictionary<long, Dictionary<long, long>>();

        public SummationHelper(long targetTotal)
        {
            Numbers = new long[targetTotal - 1];
            for (var n = 1; n < targetTotal; ++n)
            {
                Numbers[n - 1] = n;
            }
        }

        public long WaysToSum(long targetTotal)
        {
            if (targetTotal == 1) return 1;

            return WaysToSum(targetTotal, Numbers.Length);
        }

        private long WaysToSum(long amount, long index)
        {
            if (sums.ContainsKey(amount) && sums[amount].ContainsKey(index))
                return sums[amount][index];

            if (index == 1) return Numbers[0];
            if (amount == 0) return Numbers[0];
            if (amount < 0) return 0;

            var reduceIndex = WaysToSum(amount, index - 1);
            EnsureSum(amount, index - 1, reduceIndex);

            long key = amount - Numbers[index - 1];
            var reduceAmount = WaysToSum(key, index);
            EnsureSum(key, index, reduceAmount);

            return reduceIndex + reduceAmount;
        }

        private void EnsureSum(long key1, long key2, long value)
        {
            if (!sums.ContainsKey(key1)) sums.Add(key1, new Dictionary<long, long>());
            if (!sums[key1].ContainsKey(key2)) sums[key1].Add(key2, value);
        }
    }
}