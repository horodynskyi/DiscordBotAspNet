using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands
{
    public class GetHistoryCommand : DiscordSlashCommand
    {
        public override string Name => "command history";

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override async void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            using (StreamReader r = new StreamReader("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\CommandHistory.txt"))
            {
                var data = r.ReadToEnd();
                await msg.Channel.SendMessageAsync(data);
            }           
        }
    }
}
