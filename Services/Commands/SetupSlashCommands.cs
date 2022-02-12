using Discord;
using Discord.Net;
using Discord.WebSocket;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class SetupSlashCommands
    {
        public string Name => "!setupslashcommand";

        public async void Execute(DiscordSocketClient client, SocketMessage msg)
        {
            var guild = client.GetGuild(873689102569054268);
            var setupSlashCommandCommand = new SlashCommandBuilder
            {
                Name = "setupslashcommand",
                Description = "Call for setup slash commands",
            };

            var setupRandomCommand = new SlashCommandBuilder
            {
                Name = "random",
                Description = "Return random value in range between 0 and {value}",
            };

            setupRandomCommand.AddOption(
                "value",
                ApplicationCommandOptionType.Number,
                "Biba and boba",
                isRequired: true,
                minValue: 0.0
                );

            var setupClearCommand = new SlashCommandBuilder
            {
                Name = "clear",
                Description = "Clear channel"
            };

            try
            {
                await guild.CreateApplicationCommandAsync(setupSlashCommandCommand.Build());
                await guild.CreateApplicationCommandAsync(setupRandomCommand.Build());
                await guild.CreateApplicationCommandAsync(setupClearCommand.Build());
            }
            catch (HttpException exception)
            {
                exception.ToString();
            }

        }
    }
}
