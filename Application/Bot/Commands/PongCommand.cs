using Discord;
using Discord.Commands;

namespace discord_bot.Bot.Commands
{
    public class PongCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("says pong")]
        public async Task PongAsync()
        {
            var messageReference = new MessageReference(Context.Message.Id);
            await ReplyAsync("pong! demente", messageReference: messageReference);
        }
    }
}
