using Newtonsoft.Json;

namespace DiscordRPC
{
    /// <summary>
    /// A rich presence that has been parsed from the pipe as a response.
    /// </summary>
    internal class RichPresenceResponse : RichPresence
    {
        /// <summary>
        /// ID of the client
        /// </summary>
        [JsonProperty("application_id")]
        public string ClientID { get; private set; }

        /// <summary>
        /// Name of the bot
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
