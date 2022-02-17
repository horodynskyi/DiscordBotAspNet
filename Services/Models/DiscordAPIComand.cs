using Discord;
using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordAPICommand : ICommand
    {
        public abstract String Name { get; }

        public abstract string[] Parameters { get; set; }

        public abstract Task ExecuteAsync(DiscordSocketClient client, object data);

        public abstract Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg);

        public abstract SlashCommandBuilder GetSlashCommandBuilder();
    }
}
