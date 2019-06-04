using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordRPC.Message
{
	/// <summary>
	/// Failed to establish any connection with Discord. Discord is potentially not running?
	/// </summary>
	public class ConnectionFailedMessage : MessageBase
	{
        /// <summary>
        /// The type of message received from Discord
        /// </summary>
        public override MessageType Type => MessageType.ConnectionFailed;

        /// <summary>
        /// The pipe we failed to connect to.
        /// </summary>
        public int FailedPipe { get; internal set; }
	}
}
