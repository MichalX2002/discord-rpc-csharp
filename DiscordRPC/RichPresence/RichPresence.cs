using Newtonsoft.Json;
using System;
using System.Text;
using DiscordRPC.Exceptions;
using DiscordRPC.Helper;

namespace DiscordRPC
{
	/// <summary>
	/// The Rich Presence structure that will be sent and received by Discord. Use this class to build your presence and update it appropriately.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[Serializable]
	public class RichPresence
    {
        private string _state;
        private string _details;

        /// <summary>
        /// The user's current <see cref="Party"/> status. For example, "Playing Solo" or "With Friends".
        /// <para>Max 128 bytes</para>
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
		public string State
        {
            get => _state;
            set
            {
                if (!ValidateString(value, out _state, 128, Encoding.UTF8))
                    throw new StringOutOfRangeException("State", 0, 128);
            }
        }

		/// <summary>
		/// What the user is currently doing. For example, "Competitive - Total Mayhem".
		/// <para>Max 128 bytes</para>
		/// </summary>
		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public string Details
        {
            get => _details;
            set
            {
                if (!ValidateString(value, out _details, 128, Encoding.UTF8))
                    throw new StringOutOfRangeException(128);
            }
        }
		
		/// <summary>
		/// Gets or sets the elapsed/remaining time data.
		/// </summary>
		[JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
		public Timestamps Timestamps { get; set; }

		/// <summary>
		/// Gets or sets the names of the images to use and the tooltips to give those images.
		/// </summary>
		[JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
		public Assets Assets { get; set; }
		
		/// <summary>
		/// Gets or sets the party the player is currently in.
        /// The <see cref="Party.ID"/> must be set for this to be included in the Rich Presence update.
		/// </summary>
		[JsonProperty("party", NullValueHandling = NullValueHandling.Ignore)]
		public Party Party { get; set; }
		
		/// <summary>
		/// The secrets used for 'Join/Spectate'. Secrets are obfuscated data of your choosing. 
        /// They could be match IDs, player IDs, lobby IDs, etc. 
        /// Make this object null if you do not wish too or are unable to implement the 'Join/Request' feature.
		/// <para>
        /// To keep security on the up and up, Discord requires that you properly hash/encode/encrypt/
        /// put-a-padlock-on-and-swallow-the-key-but-wait-then-how-would-you-open-it your secrets.
        /// </para>
		/// <para>
        /// Visit the <see href="https://discordapp.com/developers/docs/rich-presence/how-to#secrets">
        /// Rich Presence How-To</see> for more information.
        /// </para>
		/// </summary>
		[JsonProperty("secrets", NullValueHandling = NullValueHandling.Ignore)]
		public Secrets Secrets { get; set; }

        ///// <summary>
        ///// Marks the <see cref="Secrets.MatchSecret"/> as a game session with a specific beginning and end. It was going to be used as a form of notification, but was replaced with the join feature. It may potentially have use in the future, but it currently has no use.
        ///// <para>
        ///// "TLDR it marks the matchSecret field as an instance,
        ///// that is to say a context in game that’s not like a lobby state/not in game state.
        ///// It was gonna be used for 'notify me', but we scrapped that for 'ask to join'.
        ///// We may put it to another use in the future. For now, don’t worry about it" 
        ///// - Mason (Discord API Server 14 / 03 / 2018)
        /////	</para>
        ///// </summary>
        //[JsonProperty("instance", NullValueHandling = NullValueHandling.Ignore)]
        //[Obsolete("This was going to be used, but was replaced by JoinSecret instead")]
        //private bool Instance { get; set; }

        /// <summary>
        /// Clones the presence into a new instance.
        /// Used for thread safe writing and reading.
        /// <para>This function will ignore properties if they are in a invalid state.</para>
        /// </summary>
        /// <returns></returns>
        public RichPresence Clone()
		{
            return new RichPresence
            {
                State = _state != null ? _state.Clone() as string : null,
                Details = _details != null ? _details.Clone() as string : null,
                Timestamps = Timestamps,

                Secrets = !HasSecrets() ? null : new Secrets
                {
                    //MatchSecret = this.Secrets.MatchSecret?.Clone() as string,
                    JoinSecret = Secrets.JoinSecret != null ? Secrets.JoinSecret.Clone() as string : null,
                    SpectateSecret = Secrets.SpectateSecret != null ? Secrets.SpectateSecret.Clone() as string : null
                },

				Assets = !HasAssets() ? null : new Assets
				{
					LargeImageKey = Assets.LargeImageKey != null ? Assets.LargeImageKey.Clone() as string  : null,
					LargeImageText = Assets.LargeImageText != null ? Assets.LargeImageText.Clone() as string : null,
					SmallImageKey = Assets.SmallImageKey != null ? Assets.SmallImageKey.Clone() as string : null,
					SmallImageText = Assets.SmallImageText != null ? Assets.SmallImageText.Clone() as string : null
				},

				Party = !HasParty() ? null : new Party
				{
					ID = Party.ID,
					Size = Party.Size,
					Max = Party.Max
				}
			};
		}

		/// <summary>
		/// Merges the passed presence with this one, taking into account the image key to image id annoyance.
		/// </summary>
		/// <param name="presence"></param>
		internal void Merge(RichPresence presence)
		{
			_state = presence._state;
			_details = presence._details;
			Party = presence.Party;
			Timestamps = presence.Timestamps;
			Secrets = presence.Secrets;

			//If they have assets, we should merge them
			if (presence.HasAssets())
			{
				//Make sure we actually have assets
				if (!HasAssets())
				{
					//We dont, so we will just use theirs
					Assets = presence.Assets;
				}
				else
				{
					//We do, so we better merge them!
					Assets.Merge(presence.Assets);
				}
			}
			else
			{
				//They dont have assets, so we will just set ours to null
				Assets = null;
			}	
		}

        #region Has Checks
        /// <summary>
        /// Does the Rich Presence have valid timestamps?
        /// </summary>
        /// <returns></returns>
        public bool HasTimestamps()
		{
			return Timestamps.Start != null && Timestamps.End != null;
		}

		/// <summary>
		/// Does the Rich Presence have valid assets?
		/// </summary>
		/// <returns></returns>
		public bool HasAssets()
		{
			return Assets != null;
		}

		/// <summary>
		/// Does the Rich Presence have a valid party?
		/// </summary>
		/// <returns></returns>
		public bool HasParty()
		{
			return Party != null && Party.ID != null;
		}

		/// <summary>
		/// Does the Rich Presence have valid secrets?
		/// </summary>
		/// <returns></returns>
		public bool HasSecrets()
		{
			return Secrets != null && (Secrets.JoinSecret != null || Secrets.SpectateSecret != null);
		}
        #endregion

        #region Builder
        /// <summary>
        /// Sets the state of the Rich Presence. See also <seealso cref="State"/>.
        /// </summary>
        /// <param name="state">The user's current <see cref="Party"/> status.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithState(string state)
        {
            State = state;
            return this;
        }

        /// <summary>
        /// Sets the details of the Rich Presence. See also <seealso cref="Details"/>.
        /// </summary>
        /// <param name="details">What the user is currently doing.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithDetails(string details)
        {
            Details = details;
            return this;
        }

        /// <summary>
        /// Sets the timestamp of the Rich Presence. See also <seealso cref="Timestamps"/>.
        /// </summary>
        /// <param name="timestamps">The time elapsed / remaining time data.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithTimestamps(Timestamps timestamps)
        {
            Timestamps = timestamps;
            return this;
        }

        /// <summary>
        /// Sets the assets of the Rich Presence. See also <seealso cref="Assets"/>.
        /// </summary>
        /// <param name="assets">The names of the images to use and the tooltips to give those images.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithAssets(Assets assets)
        {
            Assets = assets;
            return this;
        }

        /// <summary>
        /// Sets the Rich Presence's party. See also <seealso cref="Party"/>.
        /// </summary>
        /// <param name="party">The party the player is currently in.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithParty(Party party)
        {
            Party = party;
            return this;
        }

        /// <summary>
        /// Sets the Rich Presence's secrets. See also <seealso cref="Secrets"/>.
        /// </summary>
        /// <param name="secrets">The secrets used for Join / Spectate.</param>
        /// <returns>The modified Rich Presence.</returns>
        public RichPresence WithSecrets(Secrets secrets)
        {
            Secrets = secrets;
            return this;
        }
        #endregion

        /// <summary>
        /// Attempts to call <see cref="StringTools.GetNullOrString(string)"/> on the string and return the result, if its within a valid length.
        /// </summary>
        /// <param name="str">The string to check</param>
        /// <param name="result">The formatted string result</param>
        /// <param name="bytes">The maximum number of bytes the string can take up</param>
        /// <param name="encoding">The encoding to count the bytes with</param>
        /// <returns>True if the string fits within the number of bytes</returns>
        internal static bool ValidateString(string str, out string result, int bytes, Encoding encoding)
		{
			result = str;
			if (str == null)
				return true;

			//Trim the string, for the best chance of fitting
			var s = str.Trim();

			//Make sure it fits
			if (!s.WithinLength(bytes, encoding))
				return false;

			//Make sure its not empty
			result = s.GetNullOrString();
			return true;
		}

		/// <summary>
		/// Operator that converts a presence into a boolean for null checks.
		/// </summary>
		/// <param name="presesnce"></param>
		public static implicit operator bool(RichPresence presesnce)
		{
			return presesnce != null;
		}
	}
}
