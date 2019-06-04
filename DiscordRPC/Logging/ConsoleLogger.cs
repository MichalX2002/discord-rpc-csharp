using System;

namespace DiscordRPC.Logging
{
	/// <summary>
	/// Logs the outputs to the console using <see cref="Console.WriteLine()"/>
	/// </summary>
	public class ConsoleLogger : ILogger
	{
		/// <summary>
		/// The level of logging to apply to this logger.
		/// </summary>
		public LogLevel Level { get; set; }

		/// <summary>
		/// Gets or sets if the output should be colored.
		/// </summary>
		public bool IsColored { get; set; }

        /// <summary>
        /// Creates a new instance of a Console Logger.
        /// </summary>
        public ConsoleLogger()
        {
            Level = LogLevel.Info;
            IsColored = false;
        }

        /// <summary>
        /// Creates a new instance of a Console Logger with a set log level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="coloured"></param>
        public ConsoleLogger(LogLevel level, bool coloured = false)
        {
            Level = level;
            IsColored = coloured;
        }

        /// <summary>
        /// Informative log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Trace(string message, params object[] args)
        {
            if (Level > LogLevel.Trace)
                return;

            if (IsColored)
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("TRACE: " + message, args);
        }

        /// <summary>
        /// Informative log messages.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args)
		{
			if (Level > LogLevel.Info)
                return;

			if (IsColored)
                Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("INFO: " + message, args);
		}

		/// <summary>
		/// Warning log messages.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public void Warning(string message, params object[] args)
		{
			if (Level > LogLevel.Warning)
                return;

			if (IsColored)
                Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("WARN: " + message, args);
		}

		/// <summary>
		/// Error log messsages.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public void Error(string message, params object[] args)
		{
			if (Level > LogLevel.Error)
                return;

			if (IsColored)
                Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("ERR : " + message, args);
		}

	}
}
