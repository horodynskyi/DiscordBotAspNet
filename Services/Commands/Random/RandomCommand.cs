using Discord;
using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands.RandomCommands
{
    public class RandomCommand : DiscordSlashCommand
    {
        public override string Name => "random";

        readonly Random _random = new();

        public override async Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var min = 0;

            var max = msg.Data.Options.First();
            Console.WriteLine($"Random from {min} to {max.Value}");
            Console.WriteLine(min);
            Console.WriteLine(max);
            await msg.RespondAsync("Random value = " + _random.Next(min, int.Parse(max.Value.ToString())).ToString());
        }

        public override async Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var setupRandomCommand = new SlashCommandBuilder
            {
                Name = Name,
                Description = "Return random value in range between 0 and {value}",
            };

            setupRandomCommand.AddOption(
                "value",
                ApplicationCommandOptionType.Number,
                "Biba and boba",
                isRequired: true,
                minValue: 0.0
            );
            return setupRandomCommand;
        }
    }
}
