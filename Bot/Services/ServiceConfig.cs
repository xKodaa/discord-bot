using Discord.WebSocket;
using discord_bot.Bot.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace discord_bot.Bot.Services
{
    internal class ServiceConfig
    {
        public static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ConfigLoader>();
                    services.AddSingleton<DiscordSocketClient>();
                    services.AddSingleton<Logger>();
                    services.AddHostedService<BotService>();
                })
                .Build();
        }
    }
}
