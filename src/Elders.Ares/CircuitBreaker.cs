using Elders.Ares.Atomic;
using Elders.Ares.Userful;

namespace Elders.Ares
{

    public class Health
    {
        private readonly long totalCount;
        private readonly long errorCount;
        private readonly int errorPercentage;

        /// <summary>
        /// The total number of requests made by this command.
        /// </summary>
        public long TotalRequests { get { return this.totalCount; } }

        /// <summary>
        /// The total number of errors made by this command.
        /// This includes Failure, Timeout, ThreadPoolRejected, ShortCircuited and SemaphoreRejected <see cref="RollingNumberEvent"/> events.
        /// </summary>
        public long ErrorCount { get { return this.errorCount; } }

        /// <summary>
        /// The ratio of total requests and error counts in percents.
        /// </summary>
        public int ErrorPercentage { get { return this.errorPercentage; } }

        /// <summary>
        /// Initializes a new instance of HealthCounts.
        /// </summary>
        /// <param name="total">The total number of requests made by this command.</param>
        /// <param name="error">The total number of errors made by this command.</param>
        public Health(long total, long error)
        {
            this.totalCount = total;
            this.errorCount = error;

            if (total > 0)
            {
                this.errorPercentage = (int)((double)error / total * 100);
            }
            else
            {
                this.errorPercentage = 0;
            }
        }
    }

    public class CircuitBreaker : ICircuitBreaker
    {
        private readonly IIOperationProperties properties;
        private readonly AresMetrics metrics;

        /// <summary>
        /// Stores the state of this circuit breaker.
        /// </summary>
        private AtomicBoolean circuitOpen = new AtomicBoolean(false);

        /// <summary>
        /// Stores the last time the circuit breaker was opened or tested.
        /// </summary>
        private AtomicLong circuitOpenedOrLastTestedTime = new AtomicLong();

        public CircuitBreaker(IIOperationProperties properties, AresMetrics metrics)
        {
            this.properties = properties;
            this.metrics = metrics;
        }

        public bool AllowExecution()
        {
            if (this.properties.CircuitBreakerForceOpen.Get())
            {
                // properties have asked us to force the circuit open so we will allow NO requests
                return false;
            }

            if (this.properties.CircuitBreakerForceClosed.Get())
            {
                // we still want to allow IsOpen() to perform it's calculations so we simulate normal behavior
                this.IsOpen();

                // properties have asked us to ignore errors so we will ignore the results of isOpen and just allow all traffic through
                return true;
            }

            return !this.IsOpen() || this.AllowSingleTest();
        }

        /// <summary>
        /// Gets whether the circuit breaker should permit a single test request.
        /// </summary>
        /// <returns>True if single test is permitted, otherwise false.</returns>
        private bool AllowSingleTest()
        {
            long timeCircuitOpenedOrWasLastTested = this.circuitOpenedOrLastTestedTime.Value;

            // 1) if the circuit is open
            // 2) and it's been longer than 'sleepWindow' since we opened the circuit
            if (this.circuitOpen.Value && ActualTime.CurrentTimeInMillis > timeCircuitOpenedOrWasLastTested + this.properties.CircuitBreakerSleepWindow.Get().TotalMilliseconds)
            {
                // We push the 'circuitOpenedTime' ahead by 'sleepWindow' since we have allowed one request to try.
                // If it succeeds the circuit will be closed, otherwise another singleTest will be allowed at the end of the 'sleepWindow'.
                if (this.circuitOpenedOrLastTestedTime.CompareAndSet(timeCircuitOpenedOrWasLastTested, ActualTime.CurrentTimeInMillis))
                {
                    // if this returns true that means we set the time so we'll return true to allow the singleTest
                    // if it returned false it means another thread raced us and allowed the singleTest before we did
                    return true;
                }
            }

            return false;
        }

        public bool IsOpen()
        {
            if (this.circuitOpen.Value)
            {
                // if we're open we immediately return true and don't bother attempting to 'close' ourself as that is left to allowSingleTest and a subsequent successful test to close
                return true;
            }

            // we're closed, so let's see if errors have made us so we should trip the circuit open
            Health health = this.metrics.GetHealth();

            // check if we are past the statisticalWindowVolumeThreshold
            if (health.TotalRequests < this.properties.CircuitBreakerRequestVolumeThreshold.Get())
            {
                // we are not past the minimum volume threshold for the statisticalWindow so we'll return false immediately and not calculate anything
                return false;
            }

            if (health.ErrorPercentage < this.properties.CircuitBreakerErrorThresholdPercentage.Get())
            {
                return false;
            }
            else
            {
                // our failure rate is too high, trip the circuit
                if (this.circuitOpen.CompareAndSet(false, true))
                {
                    // if the previousValue was false then we want to set the currentTime
                    // How could previousValue be true? If another thread was going through this code at the same time a race-condition could have
                    // caused another thread to set it to true already even though we were in the process of doing the same
                    this.circuitOpenedOrLastTestedTime.Value = ActualTime.CurrentTimeInMillis;
                }

                return true;
            }
        }

        public void OperationSuccess()
        {
            if (this.circuitOpen)
            {
                // If we have been 'open' and have a success then we want to close the circuit. This handles the 'singleTest' logic
                this.circuitOpen.Value = false;

                // TODO how can we can do this without resetting the counts so we don't lose metrics of short-circuits etc?
                this.metrics.ResetAll();
            }
        }
    }
}