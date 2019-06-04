using Newtonsoft.Json;
using System;

namespace DiscordRPC
{
    /// <summary>
    /// Structure representing the start and endtimes of a match.
    /// </summary>
    [Serializable]
    public struct Timestamps
    {
        /// <summary> Creates a new timestamp for now. </summary>
        public static Timestamps Now => new Timestamps(DateTime.UtcNow);

        /// <summary>
        /// Creates a new timestamp starting at UtcNow and ending in the supplied timespan
        /// </summary>
        /// <param name="seconds">How long the Timestamp will last for in seconds.</param>
        /// <returns>Returns a new timestamp with given duration.</returns>
        public static Timestamps FromTimeSpan(double seconds) => FromTimeSpan(TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// Creates a new timestamp starting at UtcNow and ending in the supplied timespan
        /// </summary>
        /// <param name="timespan">How long the Timestamp will last for.</param>
        /// <returns>Returns a new timestamp with given duration.</returns>
        public static Timestamps FromTimeSpan(TimeSpan timespan)
        {
            return new Timestamps()
            {
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow + timespan
            };
        }

        /// <summary>
        /// Gets or sets the time that the match started. 
        /// <para>When included (not-null), the time in the rich presence will be shown as "00:01 elapsed".</para>
        /// </summary>
        [JsonIgnore]
        public DateTime? Start { get; set; }

        /// <summary>
        /// Gets or sets the time the match will end. 
        /// <para>
        /// When included (not-null), the time in the rich presence will be shown as "00:01 remaining".
        /// This will override the start suffix "elapsed" to "remaining".
        /// </para>
        /// </summary>
        [JsonIgnore]
        public DateTime? End { get; set; }

        /// <summary>
        /// Creates a timestamp with the set start or end time.
        /// </summary>
        /// <param name="start">The start time</param>
        /// <param name="end">The end time</param>
        public Timestamps(DateTime? start, DateTime? end = null)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Converts between DateTime and Milliseconds to give the Unix Epoch Time for the <see cref="Timestamps.Start"/>.
        /// </summary>
        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? StartUnixMilliseconds
        {
            get => Start.HasValue ? ToUnixMilliseconds(Start.Value) : (ulong?)null;
            set => Start = value.HasValue ? FromUnixMilliseconds(value.Value) : (DateTime?)null;
        }

        /// <summary>
        /// Converts between DateTime and Milliseconds to give the Unix Epoch Time  for the <see cref="Timestamps.End"/>.
        /// <seealso cref="End"/>
        /// </summary>
		[JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? EndUnixMilliseconds
        {
            get => End.HasValue ? ToUnixMilliseconds(End.Value) : (ulong?)null;
            set => End = value.HasValue ? FromUnixMilliseconds(value.Value) : (DateTime?)null;
        }

        /// <summary>
        /// Converts a Unix Epoch time into a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="unixTime">The time in milliseconds since 1970 / 01 / 01</param>
        /// <returns></returns>
        public static DateTime FromUnixMilliseconds(ulong unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(Convert.ToDouble(unixTime));
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> into a Unix Epoch time (in milliseconds).
        /// </summary>
        /// <param name="date">The datetime to convert</param>
        /// <returns></returns>
        public static ulong ToUnixMilliseconds(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt64((date - epoch).TotalMilliseconds);
        }
    }
}
