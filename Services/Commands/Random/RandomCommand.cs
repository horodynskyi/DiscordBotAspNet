using Discord.WebSocket;
using Services.Models;

namespace Services.Commands.RandomCommands
{
    public class RandomCommand : DiscordSlashCommand
    {
        public override string Name => "random";

        readonly Random _random = new();

        public override void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var min = 0;

            var max = msg.Data.Options.First();
            Console.WriteLine($"Random from {min} to {max.Value}");
            Console.WriteLine(min);
            Console.WriteLine(max);
            msg.RespondAsync("Random value = " + _random.Next(min, int.Parse(max.Value.ToString())).ToString());
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
