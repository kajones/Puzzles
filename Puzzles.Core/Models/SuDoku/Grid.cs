using System.Text;

namespace Puzzles.Core.Models.SuDoku
{
    public class Grid
    {
        public int Number { get; set; }

        public int Key
        {
            get
            {
                if (!IsSolved) return -1;
                var firstDigit = Squares[0, 0].Digit;
                var secondDigit = Squares[0, 1].Digit;
                var thirdDigit = Squares[0, 2].Digit;
                return ((firstDigit * 100) + (secondDigit * 10) + thirdDigit);
            }
        }
        public Square[,] Squares { get; set; }

        public bool IsSolved
        {
            get
            {
                foreach (var square in Squares)
                {
                    if (!square.IsSolved) return false;
                }

                return true;
            }
        }

        public int UnsolvedSquareCount
        {
            get
            {
                var count = 0;
                foreach (var square in Squares)
                {
                    if (!square.IsSolved) count++;
                }
                return count;
            }
        }

        public string Summary()
        {
            var builder = new StringBuilder();

            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                if (rowIdx % 3 == 0) builder.AppendLine();
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    if (colIdx % 3 == 0) builder.Append("  ");
                    if (Squares[rowIdx, colIdx].IsSolved)
                    {
                        builder.Append(Squares[rowIdx, colIdx].Digit);
                    }
                    else
                    {
                        builder.Append("0");
                    }
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
