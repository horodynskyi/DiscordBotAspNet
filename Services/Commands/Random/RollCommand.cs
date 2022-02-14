using Discord.WebSocket;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands.RandomCommands
{
    public class RollCommands : DiscordSlashCommand
    {
        public override string Name => "random";

        readonly Random _random = new();

        public override void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var min = 0;

            var max = msg.Data.Options.First();
            Console.WriteLine($"Random from {0} to {100}");
            msg.RespondAsync("Random value = " + _random.Next(min, int.Parse(max.Value.ToString())).ToString());
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
