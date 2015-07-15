using System;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// You are given the following information, but you may prefer to do some research for yourself.
    ///
    /// 1 Jan 1900 was a Monday.
    /// Thirty days has September,
    /// April, June and November.
    /// All the rest have thirty-one,
    /// Saving February alone,
    /// Which has twenty-eight, rain or shine.
    /// And on leap years, twenty-nine.
    /// A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
    /// How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
    /// </summary>
    [TestFixture]
    public class Problem_0019_CountingSundays
    {
        /// <summary>
        /// 171
        /// </summary>
        [Test, Explicit]
        public void BruteForceSundayCount()
        {
            long sundays = 0;

            var startDate = Convert.ToDateTime("1901-01-01");
            var endDate = Convert.ToDateTime("2000-12-31");

            var date = startDate;

            while (date <= endDate)
            {
                var day = date.DayOfWeek;
                if (day == DayOfWeek.Sunday)
                {
                    sundays++;
                }

                date = date.AddMonths(1);
            }

            Console.WriteLine(sundays);
        }

    }
}
