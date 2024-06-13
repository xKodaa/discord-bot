using Discord;
using Discord.WebSocket;


namespace discord_bot.Bot.Utility
{
    internal class Logger
    {
        private static DiscordSocketClient? _client;
        private static ConfigLoader? _configLoader;

        public Logger(DiscordSocketClient client, ConfigLoader config)
        {
            _client = client;
            _configLoader = config;
        }

        public static async Task Introduce() 
        {
            if (_client == null || _configLoader == null) 
            {
                LogMessage(new LogMessage(LogSeverity.Error, nameof(Logger), "Client or ConfigLoader is null, cannot introduce so I'm shutting down"));
                Environment.Exit(1);
            }

            if (_client.GetChannel(_configLoader.MainChannelID) is SocketTextChannel mainChannel)
            {
                await mainChannel.SendMessageAsync("Jebu ti babu zmrde");
            }
            await Task.CompletedTask;
        }

        public static void LogMessage(LogMessage message)
        {
            SetConsoleColor(message.Severity);
            Console.WriteLine($"{DateTime.Now} - [{message.Source}]\t{message.Message}");
            ResetConsoleColor();
        }

        public static Task LogAsync(LogMessage message)
        {
            SetConsoleColor(message.Severity);
            Console.WriteLine($"{DateTime.Now} - [{message.Source}]\t{message.Message}");
            ResetConsoleColor();
            return Task.CompletedTask;
        }

        private static void SetConsoleColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }

        private static void ResetConsoleColor()
        {
            Console.ResetColor();
        }
    }
}
