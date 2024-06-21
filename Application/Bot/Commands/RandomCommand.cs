using Discord;
using Discord.Commands;
using discord_bot.Bot.Model;

namespace discord_bot.Bot.Commands
{
    public class RandomCommand : ModuleBase<SocketCommandContext>
    {
        [Command("random")]
        [Summary("picks a random text and sends it to user")]
        public async Task RandomAsync()
        {
            var messageReference = new MessageReference(Context.Message.Id);
            await ReplyAsync(PickRandomSentence(), messageReference: messageReference);
        }

        private static string PickRandomSentence()
        {
            AnswerService answerService = new();
            return answerService.GetRandomAnswer();
        }
    }
}
