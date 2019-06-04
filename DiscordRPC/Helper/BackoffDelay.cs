using System;

namespace DiscordRPC.Helper
{
	internal class BackoffDelay
	{
		/// <summary>
		/// The maximum time the backoff can reach
		/// </summary>
		public int Maximum { get; private set; }

		/// <summary>
		/// The minimum time the backoff can start at
		/// </summary>
		public int Minimum { get; private set; }

        /// <summary>
        /// The current time of the backoff
        /// </summary>
        public int Current { get; private set; }

        /// <summary>
        /// The current number of failures
        /// </summary>
        public int Fails { get; private set; }

        /// <summary>
        /// The random number generator.
        /// </summary>
        public Random Random { get; set; }

		private BackoffDelay()
        {
        }

		public BackoffDelay(int min, int max) : this(min, max, new Random())
        {
        }

		public BackoffDelay(int min, int max, Random random)
		{
			Minimum = min;
			Maximum = max;

			Current = min;
			Fails = 0;
			Random = random;
		}

		/// <summary>
		/// Resets the backoff
		/// </summary>
		public void Reset()
		{
			Fails = 0;
			Current = Minimum;
		}

		public int NextDelay()
		{
			//Increment the failures
			Fails++;

			double diff = (Maximum - Minimum) / 100f;
			Current = (int)Math.Floor(diff * Fails) + Minimum;

			return Math.Min(Math.Max(Current, Minimum), Maximum);
		}
	}
}
