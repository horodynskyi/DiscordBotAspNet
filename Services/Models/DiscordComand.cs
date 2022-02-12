
using Discord.WebSocket;

namespace Services.Models
{
    public abstract class DiscordComand : ICommand
    {
        public abstract String Name { get; }

        public string[] Parameters { get; set; }

        public abstract void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg);

        public abstract void Execute(DiscordSocketClient client, object data);
    }
}
