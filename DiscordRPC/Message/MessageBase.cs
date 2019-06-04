using System;

namespace DiscordRPC.Message
{
	/// <summary>
	/// Messages received from Discord.
	/// </summary>
	public abstract class MessageBase
	{
		/// <summary>
		/// The type of message received from Discord
		/// </summary>
		public abstract MessageType Type { get; }

        /// <summary>
        /// The time the message was created
        /// </summary>
        public DateTime TimeCreated { get; }

        /// <summary>
        /// Creates a new instance of the message
        /// </summary>
        public MessageBase()
		{
			TimeCreated = DateTime.Now;
		}
	}
}
