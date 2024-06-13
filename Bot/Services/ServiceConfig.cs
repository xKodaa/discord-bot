using Discord;
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
                    services.AddSingleton(provider =>
                    {
                        var config = new DiscordSocketConfig
                        {
                            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages // Only needed intents for my discord bot
                        };
                        return new DiscordSocketClient(config);
                    });

                    services.AddSingleton<ConfigLoader>();
                    services.AddSingleton<Logger>();
                    services.AddHostedService<BotService>();
                })
                .Build();
        }
    }
}
