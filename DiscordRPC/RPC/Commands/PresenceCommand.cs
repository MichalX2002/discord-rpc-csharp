using DiscordRPC.RPC.Payload;
using Newtonsoft.Json;

namespace DiscordRPC.RPC.Commands
{
	internal class PresenceCommand : ICommand
	{
		/// <summary>
		/// The process ID
		/// </summary>
		[JsonProperty("pid")]
		public int PID { get; }

		/// <summary>
		/// The rich presence to be set. Can be null.
		/// </summary>
		[JsonProperty("activity")]
		public RichPresence Presence { get; }

        public PresenceCommand(int pid, RichPresence presence)
        {
            PID = pid;
            Presence = presence;
        }

		public PayloadBase PreparePayload(long nonce)
		{
			return new ArgumentPayload(this, nonce)
			{
				Command = Command.SetActivity
			};
		}
	}
}
