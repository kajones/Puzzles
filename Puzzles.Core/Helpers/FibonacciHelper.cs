using System.Numerics;

namespace Puzzles.Core.Helpers
{
    public static class FibonacciHelper
    {
        public static BigInteger GetFirstFibonacciWithNDigits(int numberOfDigits)
        {
            if (numberOfDigits == 1) return 1;

            BigInteger previousNumber = 1;
            BigInteger lastNumber = 2;
            BigInteger nextNumber = previousNumber + lastNumber;
            BigInteger limit = BigInteger.Pow(10, 999); // 999 characters

            while (nextNumber <= limit)
            {
                previousNumber = lastNumber;
                lastNumber = nextNumber;
                nextNumber = previousNumber + lastNumber;
                var numberLength = DigitHelper.GetNumberLength(nextNumber);
                if (numberLength == numberOfDigits) return nextNumber;
            }

            return nextNumber;
        }

        public static int GetNextFibonacci(int number1, int number2)
        {
            return number1 + number2;
        }
    }

}
