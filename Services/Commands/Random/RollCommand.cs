using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands.RandomCommands
{
    public class RollCommands : DiscordSlashCommand
    {
        public override string Name => "roll";

        readonly Random _random = new();

        public override void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            Console.WriteLine($"Random from {0} to {100}");
            msg.RespondAsync(msg.User.Username.ToUpper() + " rolling - **" + _random.Next(0, 100).ToString() + "**");
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
