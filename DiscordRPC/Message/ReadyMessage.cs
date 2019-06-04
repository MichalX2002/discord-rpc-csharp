

using Newtonsoft.Json;

namespace DiscordRPC.Message
{
	/// <summary>
	/// Called when the ipc is ready to send arguments.
	/// </summary>
	public class ReadyMessage : MessageBase
	{
        /// <summary>
        /// Gets the type of message received from Discord.
        /// </summary>
        public override MessageType Type => MessageType.Ready;

        /// <summary>
        /// Gets or sets the configuration of the connection.
        /// </summary>
        [JsonProperty("config")]
		public Configuration Configuration { get; set; }

		/// <summary>
		/// Gets or sets the user the connection belongs to.
		/// </summary>
		[JsonProperty("user")]
		public User User { get; set; }

		/// <summary>
		/// Gets or sets the version of the RPC.
		/// </summary>
		[JsonProperty("v")]
		public int Version { get; set; }
	}
}
