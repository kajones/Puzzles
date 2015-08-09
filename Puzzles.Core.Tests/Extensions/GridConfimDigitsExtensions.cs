using System;
using FluentAssertions;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.Tests.Extensions
{
    public static class GridConfimDigitsExtensions
    {
        public static void ConfirmRowDigits(this Grid grid, int rowIdx, int[] expectedDigits)
        {
            expectedDigits.Length.Should().Be(9, "Need nine expected digits to check");

            for (var colIdx = 0; colIdx < 9; ++ colIdx)
            {
                grid.Squares[rowIdx, colIdx].Digit.Should().Be(expectedDigits[colIdx], "Value in column " + colIdx);
            }
        }

        public static void ConfirmPossibilities(this Grid grid, int rowIdx, int colIdx, int[] expectedPossibilities, string message = "")
        {
            foreach (var expectedPossibility in expectedPossibilities)
            {
                grid.Squares[rowIdx, colIdx].PossibleDigits.Should().Contain(expectedPossibility, " Possibilities for Row {0} Col {1} {2}", rowIdx, colIdx, message);
            }

            grid.Squares[rowIdx, colIdx].PossibleDigits.Count.Should().Be(expectedPossibilities.Length, " Number of possibilities for Row {0} Col {1} {2}{3}{4}", rowIdx, colIdx, message, Environment.NewLine, string.Join(",", grid.Squares[rowIdx, colIdx].PossibleDigits));
        }
    }
}