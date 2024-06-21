using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using discord_bot.Bot.Model;

namespace discord_bot.Bot.Commands
{
    public class DicksizeCommand : ModuleBase<SocketCommandContext>
    {
        [Command("pero")]
        [Summary("řekne ti délku tvého pipíka")]
        public async Task DickSizeAsync()
        {
            var messageReference = new MessageReference(Context.Message.Id);
            await ReplyAsync(PickRandomSize(), messageReference: messageReference);
        }


        private static string PickRandomSize()
        {
            SizeService sizeService = new();
            return sizeService.GetRandomAnswer();
        }
    }
}
