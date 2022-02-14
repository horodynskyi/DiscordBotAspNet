using Discord.WebSocket;
using Services.Models;

namespace Services.Commands
{
    public class SendImageCommand : DiscordAPICommand
    {
        public override string Name => "send image";

        public override string[] Parameters { get; set; }

        public async override void Execute(DiscordSocketClient client, object data)
        {
            
            var channel = await client.GetChannelAsync(874217842650796032) as SocketTextChannel;

            await channel.SendMessageAsync(data.ToString());
        }

        public override void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            throw new NotImplementedException();
        }
    }
}
