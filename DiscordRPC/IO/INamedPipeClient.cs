using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordRPC.IO
{
	/// <summary>
	/// Pipe client used to communicate with Discord.
	/// </summary>
	public interface INamedPipeClient : IDisposable
	{
		/// <summary>
		/// Gets the logger used by the pipe client.
		/// </summary>
		ILogger Logger { get; set; }

		/// <summary>
		/// Gets if the pipe client currently connected.
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Gets the pipe that the client is currently connected to.
		/// </summary>
		int ConnectedPipe { get; }

        /// <summary>
        /// Attempts to connect to the pipe. 
        /// <para>
        /// If 0-9 is passed to pipe, it should only try to connect to the specified pipe.
        /// If -1 is passed, the pipe will find the first available pipe.
        /// </para>
        /// </summary>
        /// <param name="pipe">
        /// If -1 is passed, the pipe will find the first available pipe,
        /// otherwise it connects to the pipe that was supplied.
        /// </param>
        /// <returns></returns>
        bool Connect(int pipe);

        /// <summary>
        /// Reads a frame if there is one available.
        /// Returns <see langword="false"/> if there is none. 
        /// <para>This should be non blocking (aka use a Peek first).</para>
        /// </summary>
        /// <param name="frame">The frame that has been read.</param>
        /// <returns>Returns true if a frame has been read, otherwise false.</returns>
        bool ReadFrame(out PipeFrame frame);

		/// <summary>
		/// Writes the frame to the pipe. Returns false if any errors occur.
		/// </summary>
		/// <param name="frame">The frame to be written</param>
		bool WriteFrame(PipeFrame frame);
		
		/// <summary>
		/// Closes the pipe connection.
		/// </summary>
		void Close();
	}
}
