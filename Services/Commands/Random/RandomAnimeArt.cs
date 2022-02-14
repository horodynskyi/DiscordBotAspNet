using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands.RandomCommands
{
    public class RandomAnimeArt : DiscordSlashCommand
    {
        public override string Name => "randomanimeart";

        public override async void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var danbooruService = new DanbooruService();
            var url = await danbooruService.GetRandomArt((Int64)msg.Data.Options.First().Value == 1);
            await msg.RespondAsync(url ?? "Not found");
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
