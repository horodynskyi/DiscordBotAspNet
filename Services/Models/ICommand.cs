using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public interface ICommand
    {
        public abstract String Name { get; }
        public abstract String[] Parameters { get; set; }
        public bool Contains(SocketSlashCommand msg)
        {
            if (msg.User.IsBot)
                return false;
            if (msg.Data.Name.Contains(Name))
                return true;
            return false;
        }

        public void SetParameters()
        {
            var parameters = Name.Split(' ');
            Parameters = parameters;
        }

        public void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg);

        public void Execute(DiscordSocketClient client, object data);
    }
}
