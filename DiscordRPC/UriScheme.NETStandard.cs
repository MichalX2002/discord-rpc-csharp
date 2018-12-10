using System.Diagnostics;

namespace DiscordRPC
{
    internal static class UriScheme
    {
        /// <summary>
        /// Gets the current location of the app
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationLocation()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        /// <summary>
        /// Registers the URI scheme. If Steam ID is passed, the application will be launched through steam instead of directly.
        /// <para>Additional arguments can be supplied if required.</para>
        /// </summary>
        /// <param name="appid">The ID of the discord application</param>
        /// <param name="steamid">Optional field to indicate if this game should be launched through steam instead</param>
        /// <param name="arguments">Optional arguments to be appended to the end.</param>
        public static void RegisterUriScheme(string appid, string steamid = null, string arguments = null)
        {
        }

    }
}
