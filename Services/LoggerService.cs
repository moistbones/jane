using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace jane.Services
{
	public class LoggerService
	{
        public LoggerService(DiscordSocketClient client, CommandService command)
        {
            client.Log += Log;
            command.Log += Log;
        }

        private Task Log(LogMessage msg)
        {
            //await LogEvent(msg.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task LogEvent(string message)
        {
            string path = Environment.CurrentDirectory + "/log.txt";
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                File.Create(path);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(message);
                }
            }
        }
	}
}