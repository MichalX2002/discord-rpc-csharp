
namespace DiscordRPC.Message
{
	/// <summary>
	/// Representation of the message received by Discord when the presence has been updated.
	/// </summary>
	public class PresenceMessage : MessageBase
	{
        /// <summary>
        /// Gets the type of message received from Discord.
        /// </summary>
        public override MessageType Type => MessageType.PresenceUpdate;

        internal PresenceMessage() : this(null)
        {
        }

		internal PresenceMessage(RichPresenceResponse rpr)
		{
			if (rpr == null)
			{
				Presence = null;
				Name = "No Rich Presence";
				ApplicationID = "";
			}
			else
			{
				Presence = rpr;
				Name = rpr.Name;
				ApplicationID = rpr.ClientID;
			}
		}

		/// <summary>
		/// Gets the rich presence Discord has set.
		/// </summary>
		public RichPresence Presence { get; internal set; }

		/// <summary>
		/// Gets the name of the application Discord has set it for.
		/// </summary>
		public string Name { get; internal set; }

		/// <summary>
		/// Gets the ID of the application Discord has set it for.
		/// </summary>
		public string ApplicationID { get; internal set; }
	}
}
