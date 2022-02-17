﻿using Discord;
using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordSlashCommand : ICommand
    {
        public abstract String Name { get; }

        public abstract Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg);

        public abstract Task ExecuteAsync(DiscordSocketClient client, object data);

        public abstract SlashCommandBuilder GetSlashCommandBuilder();
    }
}
