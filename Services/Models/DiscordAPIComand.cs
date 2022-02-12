using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public abstract class DiscordAPICommand : ICommand
    {
        public abstract String Name { get; }
        public abstract string[] Parameters { get; set; }

        public abstract void Execute(DiscordSocketClient client, object data);

        public abstract void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg);
    }
}
