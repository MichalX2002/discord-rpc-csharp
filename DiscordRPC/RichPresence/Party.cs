using DiscordRPC.Helper;
using Newtonsoft.Json;
using System;

namespace DiscordRPC
{
    /// <summary>
    /// Structure representing the part the player is in.
    /// </summary>
    [Serializable]
    public class Party
    {
        private string _partyID;

        /// <summary>
        /// A unique ID for the player's current party/lobby/group. 
        /// If this is not supplied, they player will not be in a party and the rest of the information will not be sent. 
        /// <para>Max 128 bytes.</para>
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get => _partyID; set => _partyID = value.GetNullOrString(); }

        /// <summary>
        /// The current size of the players party/lobby/group.
        /// </summary>
        [JsonIgnore]
        public int Size { get; set; }

        /// <summary>
        /// The maxium size of the party/lobby/group.
        /// This is required to be larger than <see cref="Size"/>.
        /// <para>If it is smaller than the current party size,
        /// it will automatically be set to <see cref="Size"/> when the presence is sent.
        /// </para>
        /// </summary>
        [JsonIgnore]
        public int Max { get; set; }

#pragma warning disable IDE0051 // Remove unused private members
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        private int[] SizeArray // this is used for serialization
#pragma warning restore IDE0051
        {
            // https://github.com/discordapp/Discord-rpc/issues/111
            // see issue regarding the 2 element requirement

            get
            {
                int size = Math.Max(1, Size);
                return new int[] { size, Math.Max(size, Max) };
            }
            set
            {
                if (value.Length != 2)
                {
                    Size = 0;
                    Max = 0;
                }
                else
                {
                    Size = value[0];
                    Max = value[1];
                }
            }
        }
    }
}
