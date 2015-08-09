using System;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:
    ///
    /// 21 22 23 24 25
    /// 20  7  8  9 10
    /// 19  6  1  2 11
    /// 18  5  4  3 12
    /// 17 16 15 14 13
    ///
    /// It can be verified that the sum of the numbers on the diagonals is 101.
    ///
    /// What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
    /// </summary>
    [TestFixture]
    public class Problem_0028_NumberSpiralDiagonals
    {
        //111	112	113	114	115	116	117	118	119	120	121
        //110	73	74	75	76	77	78	79	80	81	82
        //109	72	43	44	45	46	47	48	49	50	83
        //108	71	42	21	22	23	24	25	26	51	84
        //107	70	41	20	7	8	9	10	27	52	85
        //106	69	40	19	6	1	2	11	28	53	86
        //105	68	39	18	5	4	3	12	29	54	87
        //104	67	38	17	16	15	14	13	30	55	88
        //103	66	37	36	35	34	33	32	31	56	89
        //102	65	64	63	62	61	60	59	58	57	90
        //101	100	99	98	97	96	95	94	93	92	91

        [Test]
        [TestCase(1, 1)]
        [TestCase(3, 25)]
        [TestCase(5, 101)]
        [TestCase(7, 261)]
        [TestCase(9, 537)]
        [TestCase(11, 961)]
        public void ConfirmKnownDiagonals(int length, int diagonalTotal)
        {
            var total = CalculateDiagonalForSquareLength(length);
            Assert.AreEqual(diagonalTotal, total, length.ToString());
        }

        [Test]
        public void FindDiagonalTotalFor1001Square()
        {
            var total = CalculateDiagonalForSquareLength(1001);

            Console.WriteLine(total);
        }

        private static int CalculateDiagonalForSquareLength(int requiredLength)
        {
            if (requiredLength == 1) return 1;

            var length = 1;
            var numberSquares = length * length;
            var previousSquares = numberSquares;

            var topLeftBottomRightTotal = 1;
            var bottomLeftTopRightTotal = 0;

            while (length < requiredLength)
            {
                length += 2;
                numberSquares = length * length;

                var topLeft = numberSquares - (length - 1);
                var bottomRight = previousSquares + (length - 1);
                var bottomLeft = numberSquares - length - (length - 2);
                var topRight = numberSquares;

                previousSquares = numberSquares;
                topLeftBottomRightTotal += (topLeft + bottomRight);
                bottomLeftTopRightTotal += (bottomLeft + topRight);
            }

            return topLeftBottomRightTotal + bottomLeftTopRightTotal;
        }
    }
}
