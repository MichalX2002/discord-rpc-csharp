using System;
using System.Text;

namespace DiscordRPC.Helper
{
	/// <summary>
	/// Collectin of helpful string extensions
	/// </summary>
	public static class StringTools
    {
        private static readonly char[] CamelCaseSplitChars = new[] { '_', ' ' };

        /// <summary>
        /// Will return null if the string is whitespace, otherwise it will return the string. 
        /// </summary>
        /// <param name="str">The string to check</param>
        /// <returns>Null if the string is empty, otherwise the string</returns>
        public static string GetNullOrString(this string str)
		{
			return str.Length == 0 || string.IsNullOrEmpty(str.Trim()) ? null : str;
		}

		/// <summary>
		/// Does the string fit within the given amount of bytes? Uses UTF8 encoding.
		/// </summary>
		/// <param name="str">The string to check</param>
		/// <param name="bytes">The maximum number of bytes the string can take up</param>
		/// <returns>True if the string fits within the number of bytes</returns>
		public static bool WithinLength(this string str, int bytes)
		{
			return str.WithinLength(bytes, Encoding.UTF8);
		}

		/// <summary>
		/// Does the string fit within the given amount of bytes?
		/// </summary>
		/// <param name="str">The string to check</param>
		/// <param name="bytes">The maximum number of bytes the string can take up</param>
		/// <param name="encoding">The encoding to count the bytes with</param>
		/// <returns>True if the string fits within the number of bytes</returns>
		public static bool WithinLength(this string str, int bytes, Encoding encoding)
		{
			return encoding.GetByteCount(str) <= bytes;
		}

        /// <summary>
        /// Converts the string into CamelCase (Pascal Case).
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <returns></returns>
        public static string ToCamelCase(this string str)
        {
            if (str == null)
                return null;

            var parts = str.ToLower().Split(CamelCaseSplitChars, StringSplitOptions.RemoveEmptyEntries);
            var builder = new StringBuilder(str.Length);

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                builder.Append(char.ToUpper(part[0]));
                builder.Append(part, 1, part.Length - 1);
            }
            return builder.ToString();
        }

		/// <summary>
		/// Converts the string into UPPER_SNAKE_CASE.
		/// </summary>
		/// <param name="str">The string to convert</param>
		/// <returns></returns>
        public static string ToSnakeCase(this string str)
        {
            if (str == null)
                return null;

            var builder = new StringBuilder((int)(str.Length * 1.1));

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (char.IsWhiteSpace(c))
                {
                    builder.Append("_");
                    continue;
                }

                if (i > 0 && char.IsUpper(c) && !char.IsWhiteSpace(str[i - 1]))
                    builder.Append("_");
                builder.Append(char.ToUpper(c));
            }
            return builder.ToString();
        }
    }
}
