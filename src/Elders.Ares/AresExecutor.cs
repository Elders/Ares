using System;

namespace Elders.Ares
{
    public class OperationPropertiesSetter
    {
        public bool? CircuitBreakerEnabled { get; private set; }
        public int? CircuitBreakerErrorThresholdPercentage { get; private set; }
        public bool? CircuitBreakerForceClosed { get; private set; }
        public bool? CircuitBreakerForceOpen { get; private set; }
        public int? CircuitBreakerRequestVolumeThreshold { get; private set; }
        public TimeSpan? CircuitBreakerSleepWindow { get; private set; }
        public int? ExecutionIsolationSemaphoreMaxConcurrentRequests { get; private set; }
        public bool? ExecutionIsolationThreadInterruptOnTimeout { get; private set; }
        public TimeSpan? ExecutionIsolationThreadTimeout { get; private set; }
        public int? FallbackIsolationSemaphoreMaxConcurrentRequests { get; private set; }
        public bool? FallbackEnabled { get; private set; }
        public TimeSpan? MetricsHealthSnapshotInterval { get; private set; }
        public int? MetricsRollingPercentileBucketSize { get; private set; }
        public bool? MetricsRollingPercentileEnabled { get; private set; }
        public int? MetricsRollingPercentileWindowInMilliseconds { get; private set; }
        public int? MetricsRollingPercentileWindowBuckets { get; private set; }
        public int? MetricsRollingStatisticalWindowInMilliseconds { get; private set; }
        public int? MetricsRollingStatisticalWindowBuckets { get; private set; }
        public bool? RequestCacheEnabled { get; private set; }
        public bool? RequestLogEnabled { get; private set; }

        public OperationPropertiesSetter WithCircuitBreakerEnabled(bool value)
        {
            CircuitBreakerEnabled = value;
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerErrorThresholdPercentage(int value)
        {
            CircuitBreakerErrorThresholdPercentage = value;
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerForceClosed(bool value)
        {
            CircuitBreakerForceClosed = value;
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerForceOpen(bool value)
        {
            CircuitBreakerForceOpen = value;
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerRequestVolumeThreshold(int value)
        {
            CircuitBreakerRequestVolumeThreshold = value;
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerSleepWindowInMilliseconds(int value)
        {
            CircuitBreakerSleepWindow = TimeSpan.FromMilliseconds(value);
            return this;
        }
        public OperationPropertiesSetter WithCircuitBreakerSleepWindow(TimeSpan value)
        {
            CircuitBreakerSleepWindow = value;
            return this;
        }
        public OperationPropertiesSetter WithExecutionIsolationSemaphoreMaxConcurrentRequests(int value)
        {
            ExecutionIsolationSemaphoreMaxConcurrentRequests = value;
            return this;
        }

        public OperationPropertiesSetter WithExecutionIsolationThreadInterruptOnTimeout(bool value)
        {
            ExecutionIsolationThreadInterruptOnTimeout = value;
            return this;
        }
        public OperationPropertiesSetter WithExecutionIsolationThreadTimeoutInMilliseconds(int milliseconds)
        {
            ExecutionIsolationThreadTimeout = TimeSpan.FromMilliseconds(milliseconds);
            return this;
        }
        public OperationPropertiesSetter WithExecutionIsolationThreadTimeout(TimeSpan value)
        {
            ExecutionIsolationThreadTimeout = value;
            return this;
        }
        public OperationPropertiesSetter WithFallbackIsolationSemaphoreMaxConcurrentRequests(int value)
        {
            FallbackIsolationSemaphoreMaxConcurrentRequests = value;
            return this;
        }
        public OperationPropertiesSetter WithFallbackEnabled(bool value)
        {
            FallbackEnabled = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsHealthSnapshotInterval(TimeSpan value)
        {
            MetricsHealthSnapshotInterval = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingPercentileBucketSize(int value)
        {
            MetricsRollingPercentileBucketSize = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingPercentileEnabled(bool value)
        {
            MetricsRollingPercentileEnabled = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingPercentileWindow(int value)
        {
            MetricsRollingPercentileWindowInMilliseconds = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingPercentileWindowBuckets(int value)
        {
            MetricsRollingPercentileWindowBuckets = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingStatisticalWindowInMilliseconds(int value)
        {
            MetricsRollingStatisticalWindowInMilliseconds = value;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingStatisticalWindow(TimeSpan value)
        {
            MetricsRollingStatisticalWindowInMilliseconds = (int)value.TotalMilliseconds;
            return this;
        }
        public OperationPropertiesSetter WithMetricsRollingStatisticalWindowBuckets(int value)
        {
            MetricsRollingStatisticalWindowBuckets = value;
            return this;
        }
        public OperationPropertiesSetter WithRequestCacheEnabled(bool value)
        {
            RequestCacheEnabled = value;
            return this;
        }
        public OperationPropertiesSetter WithRequestLogEnabled(bool value)
        {
            RequestLogEnabled = value;
            return this;
        }
    }

    public class PropertiesCommandDefault : IIOperationProperties
    {
        private const int DefaultMetricsRollingStatisticalWindowInMilliseconds = 10000;// default => statisticalWindow: 10000 = 10 seconds (and default of 10 buckets so each bucket is 1 second)
        private const int DefaultMetricsRollingStatisticalWindowBuckets = 10;// default => statisticalWindowBuckets: 10 = 10 buckets in a 10 second window so each bucket is 1 second
        private const int DefaultCircuitBreakerRequestVolumeThreshold = 20;// default => statisticalWindowVolumeThreshold: 20 requests in 10 seconds must occur before statistics matter
        private static readonly TimeSpan DefaultCircuitBreakerSleepWindow = TimeSpan.FromSeconds(5.0);// default => sleepWindow: 5000 = 5 seconds that we will sleep before trying again after tripping the circuit
        private const int DefaultCircuitBreakerErrorThresholdPercentage = 50;// default => errorThresholdPercentage = 50 = if 50%+ of requests in 10 seconds are failures or latent when we will trip the circuit
        private const bool DefaultCircuitBreakerForceOpen = false;// default => forceCircuitOpen = false (we want to allow traffic)
        private const bool DefaultCircuitBreakerForceClosed = false;// default => ignoreErrors = false 
        private static readonly TimeSpan DefaultExecutionIsolationThreadTimeout = TimeSpan.FromSeconds(1.0); // default => executionTimeoutInMilliseconds: 1000 = 1 second
        private const bool DefaultExecutionIsolationThreadInterruptOnTimeout = true;
        private const bool DefaultMetricsRollingPercentileEnabled = true;
        private const bool DefaultRequestCacheEnabled = true;
        private const int DefaultFallbackIsolationSemaphoreMaxConcurrentRequests = 10;
        private const bool DefaultFallbackEnabled = true;
        private const int DefaultExecutionIsolationSemaphoreMaxConcurrentRequests = 10;
        private const bool DefaultRequestLogEnabled = true;
        private const bool DefaultCircuitBreakerEnabled = true;
        private const int DefaultMetricsRollingPercentileWindowInMilliseconds = 60000; // default to 1 minute for RollingPercentile 
        private const int DefaultMetricsRollingPercentileWindowBuckets = 6; // default to 6 buckets (10 seconds each in 60 second window)
        private const int DefaultMetricsRollingPercentileBucketSize = 100; // default to 100 values max per bucket
        private static readonly TimeSpan DefaultMetricsHealthSnapshotInterval = TimeSpan.FromSeconds(0.5); // default to 500ms as max frequency between allowing snapshots of health (error percentage etc)

        public IProperty<bool> CircuitBreakerEnabled { get; private set; }
        public IProperty<int> CircuitBreakerErrorThresholdPercentage { get; private set; }
        public IProperty<bool> CircuitBreakerForceClosed { get; private set; }
        public IProperty<bool> CircuitBreakerForceOpen { get; private set; }
        public IProperty<int> CircuitBreakerRequestVolumeThreshold { get; private set; }
        public IProperty<TimeSpan> CircuitBreakerSleepWindow { get; private set; }
        public IProperty<int> ExecutionIsolationSemaphoreMaxConcurrentRequests { get; private set; }
        public IProperty<bool> ExecutionIsolationThreadInterruptOnTimeout { get; private set; }
        public IProperty<string> ExecutionIsolationThreadPoolKeyOverride { get; private set; }
        public IProperty<TimeSpan> ExecutionIsolationThreadTimeout { get; private set; }
        public IProperty<int> FallbackIsolationSemaphoreMaxConcurrentRequests { get; private set; }
        public IProperty<bool> FallbackEnabled { get; private set; }
        public IProperty<TimeSpan> MetricsHealthSnapshotInterval { get; private set; }
        public IProperty<int> MetricsRollingPercentileBucketSize { get; private set; }
        public IProperty<bool> MetricsRollingPercentileEnabled { get; private set; }
        public IProperty<int> MetricsRollingPercentileWindowInMilliseconds { get; private set; }
        public IProperty<int> MetricsRollingPercentileWindowBuckets { get; private set; }
        public IProperty<int> MetricsRollingStatisticalWindowInMilliseconds { get; private set; }
        public IProperty<int> MetricsRollingStatisticalWindowBuckets { get; private set; }
        public IProperty<bool> RequestCacheEnabled { get; private set; }
        public IProperty<bool> RequestLogEnabled { get; private set; }

        public PropertiesCommandDefault(OperationPropertiesSetter setter)
        {
            CircuitBreakerEnabled = PropertyFactory.AsProperty(setter.CircuitBreakerEnabled, DefaultCircuitBreakerEnabled);
            CircuitBreakerErrorThresholdPercentage = PropertyFactory.AsProperty(setter.CircuitBreakerErrorThresholdPercentage, DefaultCircuitBreakerErrorThresholdPercentage);
            CircuitBreakerForceClosed = PropertyFactory.AsProperty(setter.CircuitBreakerForceClosed, DefaultCircuitBreakerForceClosed);
            CircuitBreakerForceOpen = PropertyFactory.AsProperty(setter.CircuitBreakerForceOpen, DefaultCircuitBreakerForceOpen);
            CircuitBreakerRequestVolumeThreshold = PropertyFactory.AsProperty(setter.CircuitBreakerRequestVolumeThreshold, DefaultCircuitBreakerRequestVolumeThreshold);
            CircuitBreakerSleepWindow = PropertyFactory.AsProperty(setter.CircuitBreakerSleepWindow, DefaultCircuitBreakerSleepWindow);
            ExecutionIsolationSemaphoreMaxConcurrentRequests = PropertyFactory.AsProperty(setter.ExecutionIsolationSemaphoreMaxConcurrentRequests, DefaultExecutionIsolationSemaphoreMaxConcurrentRequests);
            ExecutionIsolationThreadInterruptOnTimeout = PropertyFactory.AsProperty(setter.ExecutionIsolationThreadInterruptOnTimeout, DefaultExecutionIsolationThreadInterruptOnTimeout);
            ExecutionIsolationThreadPoolKeyOverride = PropertyFactory.AsProperty<string>((string)null);
            ExecutionIsolationThreadTimeout = PropertyFactory.AsProperty(setter.ExecutionIsolationThreadTimeout, DefaultExecutionIsolationThreadTimeout);
            FallbackIsolationSemaphoreMaxConcurrentRequests = PropertyFactory.AsProperty(setter.FallbackIsolationSemaphoreMaxConcurrentRequests, DefaultFallbackIsolationSemaphoreMaxConcurrentRequests);
            FallbackEnabled = PropertyFactory.AsProperty(setter.FallbackEnabled, DefaultFallbackEnabled);
            MetricsHealthSnapshotInterval = PropertyFactory.AsProperty(setter.MetricsHealthSnapshotInterval, DefaultMetricsHealthSnapshotInterval);
            MetricsRollingPercentileBucketSize = PropertyFactory.AsProperty(setter.MetricsRollingPercentileBucketSize, DefaultMetricsRollingPercentileBucketSize);
            MetricsRollingPercentileEnabled = PropertyFactory.AsProperty(setter.MetricsRollingPercentileEnabled, DefaultMetricsRollingPercentileEnabled);
            MetricsRollingPercentileWindowInMilliseconds = PropertyFactory.AsProperty(setter.MetricsRollingPercentileWindowInMilliseconds, DefaultMetricsRollingPercentileWindowInMilliseconds);
            MetricsRollingPercentileWindowBuckets = PropertyFactory.AsProperty(setter.MetricsRollingPercentileWindowBuckets, DefaultMetricsRollingPercentileWindowBuckets);
            MetricsRollingStatisticalWindowInMilliseconds = PropertyFactory.AsProperty(setter.MetricsRollingStatisticalWindowInMilliseconds, DefaultMetricsRollingStatisticalWindowInMilliseconds);
            MetricsRollingStatisticalWindowBuckets = PropertyFactory.AsProperty(setter.MetricsRollingStatisticalWindowBuckets, DefaultMetricsRollingStatisticalWindowBuckets);
            RequestCacheEnabled = PropertyFactory.AsProperty(setter.RequestCacheEnabled, DefaultRequestCacheEnabled);
            RequestLogEnabled = PropertyFactory.AsProperty(setter.RequestLogEnabled, DefaultRequestLogEnabled);
        }
    }

    public sealed class AresExecutor
    {
        private readonly CircuitBreaker cb;
        private readonly AresMetrics metrics;
        private readonly IOperation operation;

        private AresExecutor(IOperation operation)
        {
            var props = new PropertiesCommandDefault(new OperationPropertiesSetter());
            this.cb = CircuitBreakerFactory.Get(operation.Name, props);
            this.metrics = AresMetricsFactory.Get(operation.Name, props);
            this.operation = operation;
        }

        private OperationResult Execute()
        {
            if (cb.AllowExecution())
            {
                try
                {
                    operation.Run();
                    metrics.MarkSuccess();
                    cb.OperationSuccess();
                    return OperationResult.Success;
                }
                catch (Exception ex)
                {
                    metrics.MarkFailure();
                    return OperationResult.Error(ex.Message);
                }
            }
            else
            {
                metrics.MarkShortCircuited();
                operation.FallBack();
                return OperationResult.Error("aaaaaaaaaaa");
            }
        }

        public static OperationResult Execute(string name, Action action)
        {
            var operation = new PreparedOperation(name, action);
            return Execute(operation);
        }

        public static OperationResult Execute(IOperation operation)
        {
            var op = new AresExecutor(operation);
            return op.Execute();
        }

        private class PreparedOperation : IOperation
        {
            private readonly Action action;

            public PreparedOperation(string name, Action action)
            {
                this.Name = name;
                this.action = action;
            }

            public string Name { get; private set; }


            public void FallBack()
            {
                throw new NotImplementedException();
            }

            public void Run()
            {
                action();
            }
        }
    }
}
