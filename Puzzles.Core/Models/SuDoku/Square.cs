using System;
using System.Collections.Generic;
using System.Globalization;

namespace Puzzles.Core.Models.SuDoku
{
    public class Square
    {
        public bool IsSolved { get { return Digit != 0; } }
        public HashSet<int> PossibleDigits { get; private set; }
        public int Digit { get; private set; }

        public Square()
        {
            PossibleDigits = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Digit = 0;
        }

        public void SetDigit(int digit)
        {
            if (digit == 0) return;

            Digit = digit;
            PossibleDigits.Clear();
        }

        public void ClearDigit()
        {
            Digit = 0;
        }

        public void RemovePossibleDigit(int digit)
        {
            if (!PossibleDigits.Contains(digit)) return;

            PossibleDigits.Remove(digit);
            if (PossibleDigits.Count == 0 && !IsSolved) throw new ApplicationException("Removed last possibility!!!");
        }

        public override string ToString()
        {
            if (IsSolved) return Digit.ToString(CultureInfo.InvariantCulture);

            return "0";
        }
    }
}
