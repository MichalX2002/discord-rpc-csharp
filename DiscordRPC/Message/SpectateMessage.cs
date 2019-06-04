
namespace DiscordRPC.Message
{
	/// <summary>
	/// Called when the Discord Client wishes for this process to spectate a game. D -> C. 
	/// </summary>
	public class SpectateMessage : JoinMessage
	{
        /// <summary>
        /// Gets the type of message received from Discord.
        /// </summary>
        public override MessageType Type => MessageType.Spectate;
    }
}
