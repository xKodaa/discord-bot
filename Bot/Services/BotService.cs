using Discord;
using Discord.Commands;
using Discord.WebSocket;
using discord_bot.Bot.Commands.CommandHandler;
using discord_bot.Bot.Utility;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace discord_bot.Bot.Services
{
    public class BotService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly ConfigLoader _configLoader;
        private readonly Logger _logger;
        private readonly CommandHandler _commandHandler;
        private readonly CommandService _commandService;

        public BotService(DiscordSocketClient client, Logger logger, ConfigLoader configLoader, CommandHandler commandHandler, CommandService commandService)
        {
            _client = client;
            _configLoader = configLoader;
            _logger = logger;
            _commandHandler = commandHandler;
            _commandService = commandService;
        }

        // StartAsync is called when the application starts via await host.RunAsync() in Program class;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Log += _logger.LogAsync;

            await _commandHandler.InitializeAsync();

            await _client.LoginAsync(TokenType.Bot, _configLoader.Token);
            await _client.StartAsync();
        }

        // StopAsync is called when the application stops
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _client.StopAsync();
        }
    }
}
