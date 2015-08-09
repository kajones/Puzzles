namespace Puzzles.Core.Helpers
{
    public static class ShapeHelper
    {
        /// <summary>
        /// P3,n => n(n+1)/2
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetTriangle(long n)
        {
            return (n*(n + 1))/2;
        }

        /// <summary>
        /// P5,n => n(3n−1)/2	
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetPentagon(long n)
        {
            return (n*((3*n) - 1))/2;
        }

        /// <summary>
        /// P6,n => n(2n−1)	
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetHexagon(long n)
        {
            return (n*((2*n) - 1));
        }

        /// <summary>
        /// P7,n => n(5n−3)/2	
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetHeptagon(long n)
        {
            return ((n*((5*n) - 3))/2);
        }

        /// <summary>
        /// P8,n => n(3n−2)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetOctagon(long n)
        {
            return (n*((3*n) - 2));
        }
    }
}