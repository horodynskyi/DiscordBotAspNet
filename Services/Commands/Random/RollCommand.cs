using Discord;
using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands.RandomCommands
{
    public class RollCommands : DiscordSlashCommand
    {
        public override string Name => "roll";

        readonly Random _random = new();

        public override async Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            Console.WriteLine($"Random from {0} to {100}");
            await msg.RespondAsync(msg.User.Username.ToUpper() + " rolling - **" + _random.Next(0, 100).ToString() + "**");

        }

        public override async Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            return new SlashCommandBuilder
            {
                Name = Name,
                Description = "Send a random value between 0 and 100",
            };
        }
    }
}
