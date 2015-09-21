using System;

namespace Elders.Ares
{
    public interface IIOperationProperties
    {
        IProperty<bool> CircuitBreakerEnabled { get; }
        IProperty<int> CircuitBreakerErrorThresholdPercentage { get; }
        IProperty<bool> CircuitBreakerForceClosed { get; }
        IProperty<bool> CircuitBreakerForceOpen { get; }
        IProperty<int> CircuitBreakerRequestVolumeThreshold { get; }
        IProperty<TimeSpan> CircuitBreakerSleepWindow { get; }

        /// <summary>
        /// Gets whether the command should interrupt the thread isolated hystrix execution on timeout or not.
        /// </summary>
        IProperty<bool> ExecutionIsolationThreadInterruptOnTimeout { get; }

        IProperty<int> ExecutionIsolationSemaphoreMaxConcurrentRequests { get; }

        IProperty<string> ExecutionIsolationThreadPoolKeyOverride { get; }

        IProperty<TimeSpan> ExecutionIsolationThreadTimeout { get; }

        IProperty<bool> FallbackEnabled { get; }

        /// <summary>
        /// Gets the maximum number of concurrent requests permitted to <see cref="IOperation.GetFallback"/> in this command.
        /// Requests beyond the concurrent limit will fail-fast and not attempt retrieving a fallback.
        /// </summary>
        IProperty<int> FallbackIsolationSemaphoreMaxConcurrentRequests { get; }

        IProperty<TimeSpan> MetricsHealthSnapshotInterval { get; }
        IProperty<int> MetricsRollingPercentileBucketSize { get; }
        IProperty<bool> MetricsRollingPercentileEnabled { get; }
        IProperty<int> MetricsRollingPercentileWindowInMilliseconds { get; }
        IProperty<int> MetricsRollingPercentileWindowBuckets { get; }
        IProperty<int> MetricsRollingStatisticalWindowInMilliseconds { get; }
        IProperty<int> MetricsRollingStatisticalWindowBuckets { get; }
        IProperty<bool> RequestCacheEnabled { get; }
        IProperty<bool> RequestLogEnabled { get; }
    }
}