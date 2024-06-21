using discord_bot.Bot.Services;
using Microsoft.Extensions.Hosting;

namespace discord_bot.Bot
{
    public class Program
    {
        public static async Task Main()
        {
            IHost host = ServiceConfig.BuildHost(); // Initialization of services and singleton classes
            await host.RunAsync();      // RunAsync() calls StartAsync of every registered class that implements IHostedService (BotService in this case)
        }
    }
}
