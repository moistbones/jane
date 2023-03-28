using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace jane.Modules
{
    public class ResponseModule : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider serviceProvider;

        public ResponseModule(IServiceProvider _provider)
        {
            serviceProvider = _provider;
        }

        [Command("Jane")]
        [Summary("Jane responds to a message.")]
        public async Task JaneResponse([Remainder] string? text)
        {
            List<string> responses = serviceProvider.GetRequiredService<IConfigurationRoot>().GetRequiredSection("Responses").Get<List<string>>();

            Random rand = serviceProvider.GetRequiredService<Random>();

            string response = responses[rand.Next()];

            await ReplyAsync(response);
        }
    }
}