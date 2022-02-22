using Discord;
using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands
{
    public class SendImageCommand : DiscordAPICommand
    {
        public override string Name => "send image";

        public async override Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            
            var channel = await client.GetChannelAsync(874217842650796032) as SocketTextChannel;

            await channel.SendMessageAsync(data.ToString());
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            throw new NotImplementedException();
        }
    }
}
