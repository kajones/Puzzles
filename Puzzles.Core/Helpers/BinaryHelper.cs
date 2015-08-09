using System;

namespace Puzzles.Core.Helpers
{
    public static class BinaryHelper
    {
        public static string GetBinaryAsText(int decimalNumber)
        {
            return Convert.ToString(decimalNumber, 2);
        }
    }
}