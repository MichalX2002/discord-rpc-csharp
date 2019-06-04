using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordRPC.Message
{
	/// <summary>
	/// The connection to the Discord client was succesfull. This is called before <see cref="MessageType.Ready"/>.
	/// </summary>
	public class ConnectionEstablishedMessage : MessageBase
	{
        /// <summary>
        /// The type of message received from Discord
        /// </summary>
        public override MessageType Type => MessageType.ConnectionEstablished;

        /// <summary>
        /// The pipe we ended up connecting to.
        /// </summary>
        public int ConnectedPipe { get; internal set; }
	}
}
