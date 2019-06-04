using Newtonsoft.Json;

namespace DiscordRPC.IO
{
	internal readonly struct Handshake
	{       
		/// <summary>
		/// Gets the version of the IPC API we are using.
		/// </summary>
		[JsonProperty("v")]
		public int Version { get; }

		/// <summary>
		/// Gets the ID of the app.
		/// </summary>
		[JsonProperty("client_id")]
		public string ClientID { get; }

        public Handshake(int version, string clientID)
        {
            Version = version;
            ClientID = clientID;
        }
    }
}
