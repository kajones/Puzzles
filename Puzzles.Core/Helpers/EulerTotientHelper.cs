using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class EulerTotientHelper
    {
        public static long CalculatePhi(long number)
        {
            var factors = PrimeHelper.FindPrimeFactors(number);
            var distinctFactors = factors.Distinct();

            double phi = number;
            foreach (var factor in distinctFactors)
            {
                phi *= (1.0 - (1.0 / factor));
            }

            return (long)phi;
        }
    }
}