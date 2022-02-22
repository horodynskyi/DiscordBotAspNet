using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICommand
    {
        public abstract String Name { get; }
        
        public bool Contains(SocketMessage msg)
        {
            if (msg.Author.IsBot)
                return false;
            if (msg.Content.Contains(Name))
                return true;
            return false;
        }

        public SlashCommandBuilder GetSlashCommandBuilder();

        public Task ExecuteAsync(DiscordSocketClient client, object commandObj);
    }
}
