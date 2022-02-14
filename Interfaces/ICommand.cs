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
        public bool Contains(SocketSlashCommand msg)
        {
            if (msg.User.IsBot)
                return false;
            if (msg.Data.Name.Contains(Name))
                return true;
            return false;
        }

        public bool Contains(SocketMessage msg)
        {
            if (msg.Author.IsBot)
                return false;
            if (msg.Content.Contains(Name))
                return true;
            return false;
        }

        public void Execute(DiscordSocketClient client, SocketSlashCommand msg);

        public void Execute(DiscordSocketClient client, object data);
    }
}
