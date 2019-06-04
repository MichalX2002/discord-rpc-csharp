using Newtonsoft.Json;

namespace DiscordRPC.RPC.Payload
{
	internal class ClosePayload : PayloadBase
	{
		/// <summary>
		/// Gets the close code the Discord gave us.
		/// </summary>
		[JsonProperty("code")]
		public int Code { get; }

		/// <summary>
		/// Gets the close reason Discord gave us.
		/// </summary>
		[JsonProperty("message")]
		public string Reason { get; }

        public ClosePayload(int code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
