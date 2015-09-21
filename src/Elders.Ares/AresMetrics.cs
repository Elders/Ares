using System;
using System.Threading;
using Elders.Ares.Userful;

namespace Elders.Ares
{
    public class AresMetrics
    {
        private readonly string key;
        private long lastHealthCountsSnapshot = DateTime.UtcNow.ToFileTimeUtc();
        private volatile Health healthSnapshot;
        private readonly RollingNumber counter;

        public AresMetrics(string key, IIOperationProperties properties)
        {
            this.key = key;

            healthSnapshot = new Health(0, 0);
            this.counter = new RollingNumber(properties.MetricsRollingStatisticalWindowInMilliseconds, properties.MetricsRollingStatisticalWindowBuckets);
        }

        public Health GetHealth()
        {
            long lastTime = this.lastHealthCountsSnapshot;
            long currentTime = DateTime.UtcNow.ToFileTimeUtc();
            if (currentTime - lastTime >= 100 || this.healthSnapshot == null)
            {
                if (Interlocked.CompareExchange(ref this.lastHealthCountsSnapshot, currentTime, lastTime) == lastTime)
                {
                    long success = counter.GetRollingSum(RollingNumberEvent.Success);
                    long failure = counter.GetRollingSum(RollingNumberEvent.Failure); // fallbacks occur on this
                    long timeout = counter.GetRollingSum(RollingNumberEvent.Timeout); // fallbacks occur on this
                    long threadPoolRejected = counter.GetRollingSum(RollingNumberEvent.ThreadPoolRejected); // fallbacks occur on this
                    long semaphoreRejected = counter.GetRollingSum(RollingNumberEvent.SemaphoreRejected); // fallbacks occur on this
                    long shortCircuited = counter.GetRollingSum(RollingNumberEvent.ShortCircuited); // fallbacks occur on this

                    long totalCount = failure + success + timeout + threadPoolRejected + shortCircuited + semaphoreRejected;
                    long errorCount = failure + timeout + threadPoolRejected + shortCircuited + semaphoreRejected;

                    healthSnapshot = new Health(totalCount, errorCount);
                }
            }
            return healthSnapshot;
        }

        public void MarkSuccess()
        {
            counter.Increment(RollingNumberEvent.Success);
        }

        public void MarkFailure()
        {
            counter.Increment(RollingNumberEvent.Failure);
        }

        public void MarkTimeout()
        {
            counter.Increment(RollingNumberEvent.Timeout);
        }

        public void MarkShortCircuited()
        {
            counter.Increment(RollingNumberEvent.ShortCircuited);
        }

        public void ResetAll()
        {
            counter.Reset();
        }
    }
}
