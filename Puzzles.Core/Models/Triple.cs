namespace Puzzles.Core.Models
{
    public struct Triple
    {
        public long a { get; private set; }
        public long b { get; private set; }
        public long c { get; private set; }

        public Triple(long a, long b, long c)
            : this()
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override string ToString()
        {
            return string.Format("A:{0}, B:{1}, C:{2}", a, b, c);
        }
    }
}
