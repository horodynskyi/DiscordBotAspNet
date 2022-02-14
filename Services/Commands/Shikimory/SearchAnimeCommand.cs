using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands.Shikimory
{
    public class SearchAnimeCommand : DiscordSlashCommand
    {
        public override string Name => "SearchAnime";

        public override void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            throw new NotImplementedException();
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
