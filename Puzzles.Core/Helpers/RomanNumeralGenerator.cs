using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class RomanNumeralGenerator
    {
        private static readonly Dictionary<char, int> romanToDigitalMap = new Dictionary<char, int>();
        private static readonly Dictionary<string, int> romanPairToDigitalMap = new Dictionary<string, int>(); 
        private static readonly SortedDictionary<int, string> digitalToRomanMap = new SortedDictionary<int, string>();

        static RomanNumeralGenerator()
        {
            romanToDigitalMap.Add('I', 1);
            romanToDigitalMap.Add('V', 5);
            romanToDigitalMap.Add('X', 10);
            romanToDigitalMap.Add('L', 50);
            romanToDigitalMap.Add('C', 100);
            romanToDigitalMap.Add('D', 500);
            romanToDigitalMap.Add('M', 1000);

            romanPairToDigitalMap.Add("IV", 4);
            romanPairToDigitalMap.Add("IX", 9);
            romanPairToDigitalMap.Add("XL", 40);
            romanPairToDigitalMap.Add("XC", 90);
            romanPairToDigitalMap.Add("CD", 400);
            romanPairToDigitalMap.Add("CM", 900);

            digitalToRomanMap.Add(1000, "M");
            digitalToRomanMap.Add(900, "CM"); 
            digitalToRomanMap.Add(500, "D");
            digitalToRomanMap.Add(400, "CD"); 
            digitalToRomanMap.Add(100, "C");
            digitalToRomanMap.Add(90, "XC"); 
            digitalToRomanMap.Add(50, "L");
            digitalToRomanMap.Add(40, "XL"); 
            digitalToRomanMap.Add(10, "X");
            digitalToRomanMap.Add(9, "IX"); 
            digitalToRomanMap.Add(5, "V");
            digitalToRomanMap.Add(4, "IV"); 
            digitalToRomanMap.Add(1, "I");
        }

        /// <summary>
        /// Generate the Roman Numeral representation of a digital number
        /// 
        /// Numerals must be arranged in descending order of size.
        /// M, C, and X cannot be equalled or exceeded by smaller denominations.
        /// D, L, and V can each only appear once.
        /// 
        /// Subtractive rules:
        ///   Only one I, X, and C can be used as the leading numeral in part of a subtractive pair.
        ///   I can only be placed before V and X.
        ///   X can only be placed before L and C.
        ///   C can only be placed before D and M.
        /// </summary>
        /// <param name="digitalRepresentation"></param>
        /// <returns></returns>
        public static string GetRomanRepresentation(int digitalRepresentation)
        {
            var roman = string.Empty;
            var digitalRemainder = digitalRepresentation;

            // Check the highest digital value first
            foreach (var kvpDigitalRoman in digitalToRomanMap.Reverse())
            {
                while (digitalRemainder >= kvpDigitalRoman.Key)
                {
                    roman += kvpDigitalRoman.Value;
                    digitalRemainder -= kvpDigitalRoman.Key;
                }
            }

            return roman;
        }

        /// <summary>
        /// Given the Roman Numeral representation of a number, return the digital equivalent
        /// </summary>
        /// <param name="romanRepresentation"></param>
        /// <returns></returns>
        public static int GetDigitalRepresentation(string romanRepresentation)
        {
            int digitalValue = 0;
            var length = romanRepresentation.Length;
            var idx = 0;
            while (idx < length)
            {
                var currentChar = romanRepresentation[idx];
                var nextIdx = idx + 1;
                if (nextIdx < length)
                {
                    var nextChar = romanRepresentation[nextIdx];
                    var combined = string.Format("{0}{1}", currentChar, nextChar);
                    if (romanPairToDigitalMap.ContainsKey(combined))
                    {
                        digitalValue += romanPairToDigitalMap[combined];
                        idx += 2;
                        continue;
                    }
                }

                digitalValue += romanToDigitalMap[currentChar];

                idx++;
            }

            return digitalValue;
        }
    }
}
