using Discord;
using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands.RandomCommands
{
    public class RollCommands : DiscordSlashCommand
    {
        public override string Name => "roll";

        readonly Random _random = new();

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                Console.WriteLine($"Random from {0} to {100}");
                await command.RespondAsync(command.User.Username.ToUpper() + " rolling - **" + _random.Next(0, 100).ToString() + "**");
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
                Description = "Send a random value between 0 and 100",
            };
        }
    }
}
