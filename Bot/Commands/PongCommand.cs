using Discord.Commands;

namespace discord_bot.Bot.Commands
{
    public class PongCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("says pong")]
        public async Task PongAsync()
        {
            await ReplyAsync("pong! demente");
        }
    }
}
