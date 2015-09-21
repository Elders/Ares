namespace Elders.Ares.Userful
{
    /// <summary>
    /// <para>
    /// Various states/events that can be captured in the <see cref="RollingNumber"/>.
    /// </para>
    /// <para>
    /// Events can be type of Counter or MaxUpdater, which can be determined using the
    /// <see cref="RollingNumberEventExtensions.IsCounter()"/> or
    /// <see cref="RollingNumberEventExtensions.IsMaxUpdater()"/> extension methods.
    /// </para>
    /// <para>
    /// The Counter type events can be used with <see cref="RollingNumber.Increment()"/>, <see cref="RollingNumber.Add()"/>,
    /// <see cref="RollingNumber.GetRollingSum()"/> methods.
    /// </para>
    /// <para>
    /// The MaxUpdater type events can be used with <see cref="RollingNumber.UpdateRollingMax()"/> and <see cref="RollingNumber.GetRollingMax()"/> methods.
    /// </para>
    /// </summary>
    public enum RollingNumberEvent
    {
        /// <summary>
        /// When a <see cref="IOperation" /> successfully completes.
        /// </summary>
        Success,

        /// <summary>
        /// When a <see cref="IOperation" /> fails to complete.
        /// </summary>
        Failure,

        /// <summary>
        /// When a <see cref="IOperation" /> times out (fails to complete).
        /// </summary>
        Timeout,

        /// <summary>
        /// When a <see cref="IOperation" /> performs a short-circuited fallback.
        /// </summary>
        ShortCircuited,

        /// <summary>
        /// When a <see cref="IOperation" /> is unable to queue up (thread pool rejection).
        /// </summary>
        ThreadPoolRejected,

        /// <summary>
        /// When a <see cref="IOperation" /> is unable to execute due to reaching the semaphore limit.
        /// </summary>
        SemaphoreRejected,

        /// <summary>
        /// When a <see cref="IOperation" /> returns a Fallback successfully.
        /// </summary>
        FallbackSuccess,

        /// <summary>
        /// When a <see cref="IOperation" /> attempts to retrieve a fallback but fails.
        /// </summary>
        FallbackFailure,

        /// <summary>
        /// When a <see cref="IOperation" /> attempts to retrieve a fallback but it is rejected due to too many concurrent executing fallback requests.
        /// </summary>
        FallbackRejection,

        /// <summary>
        /// When a <see cref="IOperation" /> throws an exception.
        /// </summary>
        ExceptionThrown,

        /// <summary>
        /// When a thread is executed.
        /// </summary>
        ThreadExecution,

        /// <summary>
        /// A MaxUpdater event which is used to determine the maximum number of concurrent threads.
        /// </summary>
        ThreadMaxActive,

        /// <summary>
        /// When a command is fronted by an <see cref="HystrixCollapser" /> then this marks how many requests are collapsed into the single command execution.
        /// </summary>
        Collapsed,

        /// <summary>
        /// When a response is coming from a cache. The cache-hit ratio can be determined by dividing this number by the total calls.
        /// </summary>
        ResponseFromCache,
    }
}
