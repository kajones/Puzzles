using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.Core.Helpers
{
    public static class ArraySplitter
    {
        public static T[] GetLeftArray<T>(T[] source)
        {
            var itemsToCopy = Convert.ToInt32(source.Length/2);
            
            var leftArray = new T[itemsToCopy];
            Array.Copy(source, 0, leftArray, 0, itemsToCopy);
            return leftArray;
        }

        public static T[] GetRightArray<T>(T[] source)
        {
            var rightOfMiddle = Convert.ToInt32(Math.Ceiling((double)((double)source.Length / 2)));
            var lengthToCopy = source.Length - rightOfMiddle;

            var rightArray = new T[lengthToCopy];
            Array.Copy(source, rightOfMiddle, rightArray, 0, lengthToCopy);
            return rightArray;
        }
    }
}
