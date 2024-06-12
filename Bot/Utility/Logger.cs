using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    internal class Logger
    {
        private static AppConfigHandler AppConfigHandler => AppConfigHandler.Instance;
        public static DiscordSocketClient Client { get; set; }

        static Logger()
        {
        }

        public static async Task Introduce() 
        {
            if (Client.GetChannel(AppConfigHandler.MainChannelID) is SocketTextChannel mainChannel)
            {
                await mainChannel.SendMessageAsync("Jebu ti babu zmrde");
            }
            await Task.CompletedTask;
        }

        public static Task LogAsync(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now}: {message.ToString()}");
            return Task.CompletedTask;
        }
    }
}
