using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using jane.Models;
using jane.Services;
using Microsoft.Extensions.Configuration;

namespace jane.Services
{
    public class StartupService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IConfigurationRoot config;

        public StartupService(
            IServiceProvider _provider,
            DiscordSocketClient _client,
            CommandService _commands,
            IConfigurationRoot _config
        )
        {
            provider = _provider;
            client = _client;
            commands = _commands;
            config = _config;
        }

        public async Task StartConnectionAsync()
        {
            client.Ready += Announce;

            string discordToken = config.GetRequiredSection("Settings").Get<Settings>().Token;
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new Exception("Bad token exception.");
            }

            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
        }

        private async Task Announce()
        {
            ISocketMessageChannel channel = await client.GetChannelAsync(config.GetRequiredSection("Settings").Get<Settings>().AnnounceChannel) as ISocketMessageChannel;

            await channel.SendMessageAsync("Hiya everyone! I'm so glad to be here!");
        }
    }
}