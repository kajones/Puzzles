using System;
using System.Collections.Generic;

namespace Puzzles.Core
{
    public static class PrimeHelper
    {
        public static List<long> GetPrimesUpTo(long limit)
        {
            var list = new List<long> { 2, 3 };

            for (var candidatePrime = 5; candidatePrime < limit; candidatePrime += 2)
            {
                if (IsPrime(candidatePrime))
                    list.Add(candidatePrime);
            }

            return list;
        }

        public static List<int> GetPrimesUpTo(int limit)
        {
            var list = new List<int> {2, 3};

            for (var candidatePrime = 5; candidatePrime < limit; candidatePrime += 2)
            {
                if (IsPrime(candidatePrime))    
                    list.Add(candidatePrime);
            }
            
            return list;
        }

        public static bool IsPrime(long candidatePrime)
        {
            if (candidatePrime == 2) return true;
            if (candidatePrime == 3) return true;

            if (candidatePrime % 2 == 0) return false;

            var sqrtLimit = Math.Sqrt(candidatePrime);

            for (var i = 3; i <= sqrtLimit; i += 2)
            {
                if (candidatePrime % i == 0)
                    return false;
            }

            return true;
        }

        public static IList<long> FindPrimeFactors(long number)
        {
            var factors = new List<long>();

            var divisor = 2;

            while (number > 1)
            {
                while (number % divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
                divisor++;

                if (divisor * divisor <= number) continue;

                if (number > 1)
                    factors.Add(number);

                break;
            }

            return factors;
        }

    }
}
