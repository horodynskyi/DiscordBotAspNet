using Discord;
using Discord.WebSocket;
using Services.Models;

namespace Services.Commands
{
    public class BulkDelete : DiscordComand
    {

        public override string Name => "clear all";

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override async void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var _adminService = new AdminService();
            var adminId = await _adminService.GetAdminDataAsync();
            if (msg.User.Id == ulong.Parse(adminId)) 
            {
                var channel = client.GetChannel(msg.Channel.Id) as SocketTextChannel;
                var messages = await channel.GetMessagesAsync().FlattenAsync();
                await ((ITextChannel)channel).DeleteMessagesAsync(messages.Where(x => x.Timestamp >= DateTimeOffset.Now.Subtract(TimeSpan.FromDays(14))));
            }
        }
    }
}
