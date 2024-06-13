using Discord;
using Discord.Commands;
using discord_bot.Bot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Commands
{
    public class ListCommandsCommand : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly ConfigLoader _configLoader;

        public ListCommandsCommand(CommandService commandService, ConfigLoader configLoader) 
        {
            _commandService = commandService; 
            _configLoader = configLoader;
        }

        [Command("help")]
        [Summary("prints all commands and their description")]
        public async Task ListCommandsAsync()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Available commands:");

            foreach (var module in _commandService.Modules) 
            {
                foreach (var command in module.Commands) 
                {
                    var res = await command.CheckPreconditionsAsync(Context);
                    if (res.IsSuccess) 
                    {
                        sb.AppendLine($"{_configLoader.Prefix}{command.Name} - {command.Summary}");
                    }
                }
            }

            var messageReference = new MessageReference(Context.Message.Id);
            await ReplyAsync(sb.ToString(), messageReference: messageReference);
        }
    }
}
