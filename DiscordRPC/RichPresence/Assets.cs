using DiscordRPC.Exceptions;
using Newtonsoft.Json;
using System;
using System.Text;

namespace DiscordRPC
{
    /// <summary>
    /// Information about the pictures used in the Rich Presence.
    /// </summary>
    [Serializable]
    public class Assets
    {
        private string _largeimagekey;
        private string _largeimagetext;
        private string _smallimagekey;
        private string _smallimagetext;

        /// <summary>
        /// Name of the uploaded image for the large profile artwork.
        /// <para>Max 32 Bytes.</para>
        /// </summary>
        [JsonProperty("large_image", NullValueHandling = NullValueHandling.Ignore)]
        public string LargeImageKey
        {
            get => _largeimagekey;
            set
            {
                if (!RichPresence.ValidateString(value, out _largeimagekey, 32, Encoding.UTF8))
                    throw new StringOutOfRangeException(32);

                //Reset the large image ID
                LargeImageID = null;
            }
        }

        /// <summary>
        /// The tooltip for the large square image. For example, "Summoners Rift" or "Horizon Lunar Colony".
        /// <para>Max 128 Bytes.</para>
        /// </summary>
        [JsonProperty("large_text", NullValueHandling = NullValueHandling.Ignore)]
        public string LargeImageText
        {
            get => _largeimagetext;
            set
            {
                if (!RichPresence.ValidateString(value, out _largeimagetext, 128, Encoding.UTF8))
                    throw new StringOutOfRangeException(128);
            }
        }
        
        /// <summary>
        /// Name of the uploaded image for the small profile artwork.
        /// <para>Max 32 Bytes.</para>
        /// </summary>
        [JsonProperty("small_image", NullValueHandling = NullValueHandling.Ignore)]
        public string SmallImageKey
        {
            get => _smallimagekey;
            set
            {
                if (!RichPresence.ValidateString(value, out _smallimagekey, 32, Encoding.UTF8))
                    throw new StringOutOfRangeException(32);

                //Reset the small image id
                SmallImageID = null;
            }
        }

        /// <summary>
        /// The tooltip for the small circle image. For example, "LvL 6" or "Ultimate 85%".
        /// <para>Max 128 Bytes.</para>
        /// </summary>
        [JsonProperty("small_text", NullValueHandling = NullValueHandling.Ignore)]
        public string SmallImageText
        {
            get => _smallimagetext;
            set
            {
                if (!RichPresence.ValidateString(value, out _smallimagetext, 128, Encoding.UTF8))
                    throw new StringOutOfRangeException(128);
            }
        }

        /// <summary>
        /// The ID of the large image. This is only set after Update Presence and will 
        /// automatically become <see langword="null"/> when <see cref="LargeImageKey"/> is changed.
        /// </summary>
        [JsonIgnore]
        public ulong? LargeImageID { get; private set; }

        /// <summary>
        /// The ID of the small image. This is only set after Update Presence and will 
        /// automatically become <see langword="null"/> when <see cref="SmallImageKey"/> is changed.
        /// </summary>
        [JsonIgnore]
        public ulong? SmallImageID { get; private set; }

        /// <summary>
        /// Merges this asset with the other, taking into account for ID's instead of keys.
        /// </summary>
        /// <param name="other"></param>
        internal void Merge(Assets other)
        {
            //Copy over the names
            _smallimagetext = other._smallimagetext;
            _largeimagetext = other._largeimagetext;

            //Convert large ID
            if (ulong.TryParse(other._largeimagekey, out ulong largeID))
            {
                LargeImageID = largeID;
            }
            else
            {
                _largeimagekey = other._largeimagekey;
                LargeImageID = null;
            }

            //Convert the small ID
            if (ulong.TryParse(other._smallimagekey, out ulong smallID))
            {
                SmallImageID = smallID;
            }
            else
            {
                _smallimagekey = other._smallimagekey;
                SmallImageID = null;
            }
        }
    }
}
