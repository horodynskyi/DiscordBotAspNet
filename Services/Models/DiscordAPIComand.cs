using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordAPICommand : ICommand
    {
        public abstract String Name { get; }

        public abstract string[] Parameters { get; set; }

        public abstract void Execute(DiscordSocketClient client, object data);

        public abstract void Execute(DiscordSocketClient client, SocketSlashCommand msg);
    }
}
