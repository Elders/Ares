using System.Threading;

namespace Elders.Ares
{
    public class MetricNumber
    {
        protected int value;

        private MetricNumber()
        {
            Reset();
        }

        public int Value { get { return value; } }

        public void Increment()
        {
            Interlocked.Increment(ref value);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref value, 0);
        }

        public static MetricNumber Create()
        {
            return new MetricNumber();
        }
    }
}