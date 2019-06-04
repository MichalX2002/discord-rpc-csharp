using DiscordRPC.RPC.Payload;

namespace DiscordRPC.Message
{
	/// <summary>
	/// Called as validation of a subscribe.
	/// </summary>
	public class SubscribeMessage : MessageBase
	{
        /// <summary>
        /// Gets the type of message received from Discord.
        /// </summary>
        public override MessageType Type => MessageType.Subscribe;

        /// <summary>
        /// Gets the event that was subscribed to.
        /// </summary>
        public EventType Event { get; internal set; }
		
		internal SubscribeMessage(ServerEvent evt)
		{
			switch (evt)
			{
				default:
				case ServerEvent.ActivityJoin:
					Event = EventType.Join;
					break;

				case ServerEvent.ActivityJoinRequest:
					Event = EventType.JoinRequest;
					break;

				case ServerEvent.ActivitySpectate:
					Event = EventType.Spectate;
					break;
			}
		}
	}
}
