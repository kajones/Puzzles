using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzles.Core.Helpers
{
    public static class NumberAsWordsHelper
    {
        private static readonly IDictionary<long, string> SingleDigits = new Dictionary<long, string>
            {
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"}
            };

        private static readonly IDictionary<long, string> TwoDigits = new Dictionary<long, string>
            {
                {10, "ten"},
                {11, "eleven"},
                {12, "twelve"},
                {13, "thirteen"},
                {14, "fourteen"},
                {15, "fifteen"},
                {16, "sixteen"},
                {17, "seventeen"},
                {18, "eighteen"},
                {19, "nineteen"},
                {20, "twenty"},
                {30, "thirty"},
                {40, "forty"},
                {50, "fifty"},
                {60, "sixty"},
                {70, "seventy"},
                {80, "eighty"},
                {90, "ninety"}
            };

        private const string AHundred = "hundred";

        //private const string AThousand = "thousand";

        public static string GetDescription(long number)
        {
            if (number > 1000) throw new ApplicationException("Do not support > 1000");

            if (number == 1000) return "one thousand";

            var desc = new StringBuilder();
            if (number > 99)
            {
                var numberOfHundreds = number / 100;
                desc.AppendFormat("{0} {1}", SingleDigits[numberOfHundreds], AHundred);

                number = number % (numberOfHundreds * 100);

                if (number == 0) return desc.ToString();

                desc.Append(" and");
            }

            var sortedTwoDigits = TwoDigits.Keys.OrderByDescending(k => k);

            while (number > 9)
            {
                foreach (var sortedTwoDigit in sortedTwoDigits)
                {
                    if (number < sortedTwoDigit) continue;
                    var prefix = desc.Length > 0 ? " " : "";
                    desc.AppendFormat("{0}{1}", prefix, TwoDigits[sortedTwoDigit]);
                    number -= sortedTwoDigit;
                    break;
                }
            }

            if (number > 0)
            {
                var prefix = desc.Length > 0 ? " " : "";
                desc.AppendFormat("{0}{1}", prefix, SingleDigits[number]);
            }

            return desc.ToString();
        }
    }
}
