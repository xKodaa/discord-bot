using Discord;
using Discord.WebSocket;


namespace discord_bot.Bot.Utility
{
    public class Logger
    {
        private readonly DiscordSocketClient _client;

        public Logger(DiscordSocketClient client)
        {
            _client = client;
        }

        // Custom log method that colors the console output based on the severity of the log message
        public Task LogAsync(LogMessage message)
        {
            SetConsoleColor(message.Severity);
            Console.WriteLine($"{DateTime.Now} - [{message.Source}]:\t{message.Message}");
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
