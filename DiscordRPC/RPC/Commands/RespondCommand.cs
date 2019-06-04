using DiscordRPC.RPC.Payload;
using Newtonsoft.Json;

namespace DiscordRPC.RPC.Commands
{
    internal class RespondCommand : ICommand
	{
		/// <summary>
		/// The user ID that we are accepting / rejecting
		/// </summary>
		[JsonProperty("user_id")]
		public string UserID { get; }

		/// <summary>
		/// If true, the user will be allowed to connect.
		/// </summary>
		[JsonIgnore]
		public bool Accept { get; }

        public RespondCommand(string userID, bool accept)
        {
            UserID = userID;
            Accept = accept;
        }

        public PayloadBase PreparePayload(long nonce)
		{
			return new ArgumentPayload(this, nonce)
			{
				Command = Accept ? Command.SendActivityJoinInvite : Command.CloseActivityJoinRequest
			};
		}
	}
}
