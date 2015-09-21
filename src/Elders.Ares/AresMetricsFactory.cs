using System.Collections.Concurrent;

namespace Elders.Ares
{
    internal static class AresMetricsFactory
    {
        private static ConcurrentDictionary<string, AresMetrics> metrics = new ConcurrentDictionary<string, AresMetrics>();

        public static AresMetrics Get(string name, IIOperationProperties properties)
        {
            return metrics.GetOrAdd(name, key => new AresMetrics(key, properties));
        }
    }
}