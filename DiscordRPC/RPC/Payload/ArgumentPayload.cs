using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordRPC.RPC.Payload
{
	/// <summary>
	/// The payload that is sent by the client to Discord for events such as setting the rich presence.
	/// <para> SetPrecense </para>
	/// </summary>
	internal class ArgumentPayload : PayloadBase
	{
		/// <summary>
		/// Gets or sets the data the server sent too us.
		/// </summary>
		[JsonProperty("args", NullValueHandling = NullValueHandling.Ignore)]
		public JObject Arguments { get; set; }
		
		public ArgumentPayload() : base()
        {
        }

		public ArgumentPayload(long nonce) : base(nonce)
        {
        }

		public ArgumentPayload(object args, long nonce) : base(nonce)
		{
			SetArgumentData(args);
		}

		/// <summary>
		/// Creates and sets the argument <see cref="JObject"/> from an <see cref="object"/>.
		/// </summary>
		/// <param name="obj"></param>
		public void SetArgumentData(object obj)
		{
			Arguments = JObject.FromObject(obj);
		}

		/// <summary>
		/// Gets the <see cref="object"/> stored within the argument <see cref="JObject"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetObject<T>()
		{
			return Arguments.ToObject<T>();
		}

		public override string ToString()
		{
			return "Argument " + base.ToString();
		}
	}
}

