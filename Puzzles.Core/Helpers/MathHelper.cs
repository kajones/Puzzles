using System.Numerics;

namespace Puzzles.Core.Helpers
{
    public static class MathHelper
    {
        public static long GetFactorial(long number)
        {
            long result = 1;

            for (long i = number; i > 1; i--)
            {
                result *= i;
            }

            return result;
        }

        public static long Factorial(long number)
        {
            if (number <= 1)
                return 1;
            return number * Factorial(number - 1);
        }

        public static BigInteger LargeFactorial(long factorialOf)
        {
            BigInteger factorial = 1;

            for (long number = factorialOf; number > 1; --number)
            {
                factorial *= number;
            }

            return factorial;
        }
    }

}
