﻿using System;
using System.Diagnostics;
using System.IO;
using DiscordRPC.Logging;

namespace DiscordRPC.Registry
{
    internal class UnixUriSchemeCreator : IUriSchemeCreator
    {
        private ILogger logger;

        public UnixUriSchemeCreator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool RegisterUriScheme(UriSchemeRegister register)
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            if (string.IsNullOrEmpty(home))
            {
                logger.Error("Failed to register because the HOME variable was not set.");
                return false;
            }

            string exe = register.ExecutablePath;
            if (string.IsNullOrEmpty(exe))
            {
                logger.Error("Failed to register because the application was not located.");
                return false;
            }

            //Prepare the command
            string command = register.UsingSteamApp ? 
                ("xdg-open steam://rungameid/" + register.SteamAppID) : exe;

            //Prepare the file
            string desktopFileFormat = 
@"[Desktop Entry]
Name=Game {0}
Exec={1} %u
Type=Application
NoDisplay=true
Categories=Discord;Games;
MimeType=x-scheme-handler/Discord-{2}";
            
            string file = string.Format(desktopFileFormat, register.ApplicationID, command, register.ApplicationID);

            //Prepare the path
            string filename = "/Discord-" + register.ApplicationID + ".desktop";
            string filepath = home + "/.local/share/applications";
            var directory = Directory.CreateDirectory(filepath);
            if (!directory.Exists)
            {
                logger.Error("Failed to register because {0} does not exist", filepath);
                return false;
            }

            //Write the file
            File.WriteAllText(filepath + filename, file);

            //Register the Mime type
            if (!RegisterMime(register.ApplicationID))
            {
                logger.Error("Failed to register because the Mime failed.");
                return false;
            }
            return true;
        }

        private bool RegisterMime(string appid)
        {
            //Format the arguments
            string format = "default Discord-{0}.desktop x-scheme-handler/Discord-{0}";
            string arguments = string.Format(format, appid);

            //Run the process and wait for response
            Process process = Process.Start("xdg-mime", arguments);
            process.WaitForExit();
            
            //Return if succesful
            return process.ExitCode >= 0;
        }
    }
}
