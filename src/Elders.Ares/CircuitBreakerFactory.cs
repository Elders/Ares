using System.Collections.Concurrent;

namespace Elders.Ares
{
    internal static class CircuitBreakerFactory
    {
        private static ConcurrentDictionary<string, CircuitBreaker> breakers = new ConcurrentDictionary<string, CircuitBreaker>();

        public static CircuitBreaker Get(string name, IIOperationProperties properties)
        {
            return breakers.GetOrAdd(name, key => new CircuitBreaker(properties, AresMetricsFactory.Get(key, properties)));
        }
    }
}