using Discord;
using Discord.WebSocket;
using discord_bot.Bot.Utility;
using Microsoft.Extensions.Hosting;

namespace discord_bot.Bot.Services
{
    internal class BotService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly ConfigLoader _configLoader;
        private readonly Logger _logger;

        public BotService(DiscordSocketClient client, Logger logger, ConfigLoader configLoader)
        {
            _client = client;
            _configLoader = configLoader;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Log += _logger.LogAsync;
            _client.Ready += Introduce;

            await _client.LoginAsync(TokenType.Bot, _configLoader.Token);
            await _client.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _client.StopAsync();
        }

        public async Task Introduce()
        {
            if (_client.GetChannel(_configLoader.MainChannelID) is SocketTextChannel mainChannel)
            {
                await mainChannel.SendMessageAsync("Gay");
            }
            await Task.CompletedTask;
        }
    }
}
