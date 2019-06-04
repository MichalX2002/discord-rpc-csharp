using DiscordRPC.RPC.Payload;
using Newtonsoft.Json;

namespace DiscordRPC.RPC.Commands
{
	internal class CloseCommand : ICommand
	{
		/// <summary>
		/// The process ID
		/// </summary>
		[JsonProperty("pid")]
		public int PID { get; }

		/// <summary>
		/// The rich presence to be set. Can be null.
		/// </summary>
		[JsonProperty("close_reason")]
        public string CloseReason { get; }

        public CloseCommand(int pid, string closeReason)
        {
            PID = pid;
            CloseReason = closeReason ?? "Unity 5.5 doesn't handle thread aborts. Can you please close me Discord?";
        }

        public CloseCommand() : this(0, null)
        {
        }

		public PayloadBase PreparePayload(long nonce)
		{
			return new ArgumentPayload()
			{
				Command = Command.Dispatch,
				Nonce = null,
				Arguments = null
			};
		}
	}
}
