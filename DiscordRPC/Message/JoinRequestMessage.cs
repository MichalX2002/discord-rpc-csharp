using Newtonsoft.Json;

namespace DiscordRPC.Message
{
	/// <summary>
	/// Called when some other person has requested access to this game. C -> D -> C.
	/// </summary>
	public class JoinRequestMessage : MessageBase
	{
        /// <summary>
        /// Gets the type of message received from Discord.
        /// </summary>
        public override MessageType Type => MessageType.JoinRequest;

        /// <summary>
        /// Gets the Discord user that is requesting access.
        /// </summary>
        [JsonProperty("user")]
		public User User { get; internal set; }
	}
}
