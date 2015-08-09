using System;

namespace Puzzles.Core.Helpers
{
    public class DieHelper
    {
        private readonly Random rdm;
        private readonly int lowerLimit;
        private readonly int upperLimit;

        public DieHelper(int lowerLimit, int upperLimit)
        {
            rdm = new Random(DateTime.Now.Millisecond);

            this.lowerLimit = lowerLimit; //Rdm inclusive lower limit
            this.upperLimit = upperLimit + 1; // Rdm exclusive upper limit
        }

        public int Roll()
        {
            return rdm.Next(lowerLimit, upperLimit);
        }
    }
}