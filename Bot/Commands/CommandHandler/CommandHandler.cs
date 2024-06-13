using Discord.Commands;
using Discord.WebSocket;
using discord_bot.Bot.Utility;
using System.Reflection;

namespace discord_bot.Bot.Commands.CommandHandler
{
    internal class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly ConfigLoader _configLoader;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services, ConfigLoader configLoader)
        {
            _commands = commands;
            _client = client;
            _services = services;
            _configLoader = configLoader;
        }

        public async Task InitializeAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
            if (message is not SocketUserMessage userMessage || message.Author.IsBot)
                return;

            int argPos = 0;
            Console.WriteLine(userMessage.Content);
            if (userMessage.HasStringPrefix(_configLoader.Prefix, ref argPos) || !userMessage.Author.IsBot)
            {
                Console.WriteLine("Command received");
                var context = new SocketCommandContext(_client, userMessage);
                await _commands.ExecuteAsync(context, argPos, null);
            }
        }
    }
}
