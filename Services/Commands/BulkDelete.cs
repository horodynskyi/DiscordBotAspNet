using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands
{
    public class BulkDelete : DiscordSlashCommand
    {
        public override string Name => "clear";

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                if (command.User.Id == ulong.Parse("783264879826042900"))
                {
                    await command.RespondAsync("Clearing starded");
                    var channel = client.GetChannel(command.Channel.Id) as SocketTextChannel;
                    var messages = await channel.GetMessagesAsync().FlattenAsync();
                    await ((ITextChannel)channel).DeleteMessagesAsync(messages.Where(x => x.Timestamp >= DateTimeOffset.Now.Subtract(TimeSpan.FromDays(14))));
                    await command.Channel.SendMessageAsync("Clearing success");
                }
                else
                {
                    await command.RespondAsync("Чел ти");
                }
            }
            else
            {             
                return;
            }
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            return new SlashCommandBuilder
            {
                Name = Name,
                Description = "Clear channel"
            };
        }
    }
}
