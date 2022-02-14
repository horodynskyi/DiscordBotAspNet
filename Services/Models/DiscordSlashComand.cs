using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordSlashCommand : ICommand
    {
        public abstract String Name { get; }

        public abstract void Execute(DiscordSocketClient client, SocketSlashCommand msg);

        public abstract void Execute(DiscordSocketClient client, object data);
    }
}
