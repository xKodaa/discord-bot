using Discord;
using Discord.Commands;
using Discord.WebSocket;
using discord_bot.Bot.Utility;
using System.Reflection;
using System.Text;

namespace discord_bot.Bot.Commands.CommandHandler
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;  // command handler, includes all commands registered
        private readonly IServiceProvider _services;
        private readonly ConfigLoader _configLoader;
        private readonly Logger _logger;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services, ConfigLoader configLoader, Logger logger)
        {
            _commandService = commands;
            _client = client;
            _services = services;
            _configLoader = configLoader;
            _logger = logger;
        }

        // 
        public async Task InitializeAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            _client.Ready += Introduce;

            // 1) assembly loads all modules that extends ModuleBase and are annotated with [Command] attribute, 2) services to inject dependencies
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
     

            // if the message is not a user message or the author is a bot, ignore it
            if (message is not SocketUserMessage userMessage || message.Author.IsBot)   
                return;

            int argPos = 0;
            // if the message cointains the prefix, execute the command if found in the command list
            if (userMessage.HasStringPrefix(_configLoader.Prefix, ref argPos))
            {
                await _logger.LogAsync(new LogMessage(LogSeverity.Info, "CommandHandler", $"Command {userMessage} received from {userMessage.Author}"));
                var context = new SocketCommandContext(_client, userMessage);
                await _commandService.ExecuteAsync(context, argPos, _services);
            }
        }

        private async Task Introduce()
        {
            if (_client.GetChannel(_configLoader.MainChannelID) is SocketTextChannel mainChannel && _client.GetChannel(_configLoader.SecondaryChannelID) is SocketTextChannel secondaryChannel)
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine($"{Util.GetAppSplitter()}");
                messageBuilder.AppendLine($"**{_client.CurrentUser.Username} is online!**");
                messageBuilder.AppendLine("Listing available commands:");

                // Iterate over all commands and send them to the channel
                foreach (var module in _commandService.Modules)
                {
                    foreach (var command in module.Commands)
                    {
                        var result = await command.CheckPreconditionsAsync(null, _services);
                        if (result.IsSuccess)
                        {
                            messageBuilder.AppendLine($"**{_configLoader.Prefix}{command.Name}** - *{command.Summary}*");
                        }
                    }
                }
                var finalMessage = messageBuilder.ToString();
                await mainChannel.SendMessageAsync(finalMessage);
                //await secondaryChannel.SendMessageAsync(finalMessage);
            }
        }
    }
}
