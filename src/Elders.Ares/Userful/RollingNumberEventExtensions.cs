using System;
using System.Collections.Generic;
using System.Linq;

namespace Elders.Ares.Userful
{
    /// <summary>
    /// Provides helper methods for the <see cref="RollingNumberEvent"/> enumeration.
    /// </summary>
    public static class RollingNumberEventExtensions
    {
        /// <summary>
        /// All possible values of the <see cref="RollingNumberEvent"/> enumeration.
        /// </summary>
        public static readonly IReadOnlyCollection<RollingNumberEvent> Values = Enum.GetValues(typeof(RollingNumberEvent)).Cast<RollingNumberEvent>().ToList();

        /// <summary>
        /// Gets whether the specified event is a Counter type or not.
        /// </summary>
        /// <param name="rollingNumberEvent">The specified event.</param>
        /// <returns>True if it's a Counter type, otherwise false.</returns>
        public static bool IsCounter(this RollingNumberEvent rollingNumberEvent)
        {
            return !rollingNumberEvent.IsMaxUpdater();
        }

        /// <summary>
        /// Gets whether the specified event is a MaxUpdater type or not.
        /// </summary>
        /// <param name="rollingNumberEvent">The specified event.</param>
        /// <returns>True if it's a MaxUpdater type, otherwise false.</returns>
        public static bool IsMaxUpdater(this RollingNumberEvent rollingNumberEvent)
        {
            return rollingNumberEvent == RollingNumberEvent.ThreadMaxActive;
        }
    }
}
