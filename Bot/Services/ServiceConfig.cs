using Discord;
using Discord.Commands;
using Discord.WebSocket;
using discord_bot.Bot.Commands.CommandHandler;
using discord_bot.Bot.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace discord_bot.Bot.Services
{
    public class ServiceConfig
    {
        // BuildHost method that initializes services and singleton classes
        public static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");  // inits configuration folder wher appsettings.json is located
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(provider =>
                    {
                        var config = new DiscordSocketConfig
                        {
                            // Only needed intents for my discord bot
                            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
                        };
                        return new DiscordSocketClient(config); // discord client with specific configuration
                    });

                    // singletons
                    services.AddSingleton<ConfigLoader>();
                    services.AddSingleton<Logger>();
                    services.AddSingleton<CommandService>();
                    services.AddSingleton<CommandHandler>();

                    // services
                    services.AddHostedService<BotService>();
                })
                .Build();
        }
    }
}
