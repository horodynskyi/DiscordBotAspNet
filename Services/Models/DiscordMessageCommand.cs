using Discord;
using Discord.WebSocket;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public abstract class DiscordMessageCommand : ICommand
    {
        public abstract String Name { get; }

        public abstract Task ExecuteAsync(DiscordSocketClient client, object data);

        public abstract SlashCommandBuilder GetSlashCommandBuilder();
    }
}
