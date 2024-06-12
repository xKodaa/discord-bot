using Discord;
using Discord.WebSocket;
using discord_bot.Bot.Utility;

namespace discord_bot.Bot
{
    internal class Program
    {

        private static readonly DiscordSocketClient _client = new();
        private static AppConfigHandler AppConfigHandler => AppConfigHandler.Instance;

        public static async Task Main()
        {
            Logger.Client = _client;
            await InitBot();
        }

        private static async Task InitBot()
        {
            AppConfigHandler.Initialize();
            
            _client.Log += Logger.LogAsync;
            _client.Ready += Logger.Introduce;

            await _client.LoginAsync(TokenType.Bot, AppConfigHandler.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
