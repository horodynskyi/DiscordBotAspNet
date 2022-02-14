using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands
{
    public class BulkDelete : DiscordSlashCommand
    {
        public override string Name => "clear";

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override async void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var _adminService = new AdminService();
            var adminId = await AdminService.GetAdminDataAsync();
            if (msg.User.Id == ulong.Parse(adminId)) 
            {
                var channel = client.GetChannel(msg.Channel.Id) as SocketTextChannel;
                var messages = await channel.GetMessagesAsync().FlattenAsync();
                await ((ITextChannel)channel).DeleteMessagesAsync(messages.Where(x => x.Timestamp >= DateTimeOffset.Now.Subtract(TimeSpan.FromDays(14))));
                await msg.RespondAsync("Clearing success");
            }
        }
    }
}
