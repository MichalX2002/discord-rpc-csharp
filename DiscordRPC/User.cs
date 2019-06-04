using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordRPC
{
	/// <summary>
	/// Object representing a Discord user. This is used for join requests.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Possible formats for avatars
		/// </summary>
		public enum AvatarFormat
		{
            /// <summary>
            /// Portable Network Graphics format (.png)
            /// <para>
            /// Lossless format that supports transparency.
            /// Mostly recommended for stationary formats with wide support from many libraries.
            /// </para>
            /// </summary>
            PNG,

            /// <summary>
            /// Joint Photographic Experts Group format (.jpeg)
            /// <para>The format most cameras use. Lossy and does not support transparency.</para>
            /// </summary>
            JPEG,

			/// <summary>
			/// WebP format (.webp)
			/// <para>Picture only version of WebM. Lossy/lossless and supports transparency.</para>
			/// </summary>
			WebP,

			/// <summary>
			/// Graphics Interchange Format (.gif)
			/// <para>Animated avatar accessible only to Nitro users.</para>
			/// </summary>
			GIF
		}

		/// <summary>
		/// Possible square sizes of avatars.
		/// </summary>
		public enum AvatarSize
		{
			/// <summary> 16 x 16 pixels.</summary>
			x16 = 16,

			/// <summary> 32 x 32 pixels.</summary>
			x32 = 32,

			/// <summary> 64 x 64 pixels.</summary>
			x64 = 64,

			/// <summary> 128 x 128 pixels.</summary>
			x128 = 128,

			/// <summary> 256 x 256 pixels.</summary>
			x256 = 256,

			/// <summary> 512 x 512 pixels.</summary>
			x512 = 512,

			/// <summary> 1024 x 1024 pixels.</summary>
			x1024 = 1024,

			/// <summary> 2048 x 2048 pixels.</summary>
			x2048 = 2048
		}

		/// <summary>
		/// The snowflake ID of the user. 
		/// </summary>
		[JsonProperty("id")]
		public ulong ID { get; set; }

		/// <summary>
		/// The username of the player.
		/// </summary>
		[JsonProperty("username")]
		public string Username { get; set; }

		/// <summary>
		/// The discriminator of the user.
		/// </summary>
		[JsonProperty("discriminator")]
		public int Discriminator { get; set; }

        /// <summary>
        /// The avatar hash of the user. 
        /// To get a URI for the avatar, use <see cref="GetAvatarURL(AvatarFormat, AvatarSize)"/>.
        /// <para>
        /// This can be <see langword="null"/> if the user has no avatar. 
        /// <see cref="GetAvatarURL(AvatarFormat, AvatarSize)"/> will account for this and return the Discord default.
        /// </para>
        /// </summary>
        [JsonProperty("avatar")]
		public string Avatar { get; set; }

        /// <summary>
        /// The endpoint for the CDN. 
        /// <para>"cdn.discordapp.com" by default.</para>
        /// </summary>
        public string CdnEndpoint { get; private set; } = "cdn.discordapp.com";

        /// <summary>
        /// Updates the URL paths to the appropriate configuration
        /// </summary>
        /// <param name="configuration">The configuration received by the OnReady event.</param>
        internal void SetConfiguration(Configuration configuration)
		{
			CdnEndpoint = configuration.CdnHost;
		}

        /// <summary>
        /// Gets if the user has the default avatar.
        /// </summary>
        public bool HasDefaultAvatar => string.IsNullOrEmpty(Avatar);

        /// <summary>
        /// Gets a URL that can be used to download the user's avatar.
        /// If the user has not yet set their avatar, it will return the default one that Discord is using. 
        /// <para>The default avatar is only available in the <see cref="AvatarFormat.PNG"/> format.</para>
        /// </summary>
        /// <param name="format">The format of the target avatar</param>
        /// <param name="size">The optional size of the avatar you wish for. Defaults to x128.</param>
        /// <returns></returns>
        public string GetAvatarURL(AvatarFormat format, AvatarSize size = AvatarSize.x128)
		{
			//Prepare the endpoint
			string endpoint = "/avatars/" + ID + "/" + Avatar;

			//The user has no avatar, so we better replace it with the default
			if (HasDefaultAvatar)
			{
				//Make sure we are only using PNG
				if (format != AvatarFormat.PNG)
					throw new BadImageFormatException(
                        "The user has the default avatar and the requested format " + format.ToString() + 
                        " is not supported. Default avatars are only available in the PNG format.");

				//Get the endpoint
				int descrim = Discriminator % 5;
				endpoint = "/embed/avatars/" + descrim;
			}

			//Finish of the endpoint
			return string.Format("https://{0}{1}{2}?size={3}", CdnEndpoint, endpoint, GetAvatarExtension(format), (int)size);
		}

		/// <summary>
		/// Returns the file extension of the specified format.
		/// </summary>
		/// <param name="format">The format to get the extention off</param>
		/// <returns>Returns a period prefixed file extension.</returns>
		public string GetAvatarExtension(AvatarFormat format)
		{
			return "." + format.ToString().ToLowerInvariant();
		}

		/// <summary>
		/// Formats the user information into username#discriminator
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Username + "#" + Discriminator.ToString("D4");
		}
	}
}
