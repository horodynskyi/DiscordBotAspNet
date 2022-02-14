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
    public class SetupSlashCommands : DiscordSlashCommand
    {

        public override string Name => "setupslashcommand";


        public override async void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            await msg.RespondAsync("Command setup started\nProcessing... Please wait");

            var guild = client.GetGuild(873689102569054268);
            var setupSlashCommandCommand = new SlashCommandBuilder
            {
                Name = "setupslashcommand",
                Description = "Call for setup slash commands",
            };

            var setupRollCommand = new SlashCommandBuilder
            {
                Name = "roll",
                Description = "Send a random value between 0 and 100",
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

            var setupFetchAnimeCalendarCommand = new SlashCommandBuilder
            {
                Name = "sendcalendar",
                Description = "Send anime calendar for choosen date"
            };

            setupFetchAnimeCalendarCommand
                .AddOption(
                    "value",
                    ApplicationCommandOptionType.String,
                    "Biba and boba",
                    choices: new ApplicationCommandOptionChoiceProperties[3]
                    {
                        new ApplicationCommandOptionChoiceProperties
                        {
                            Name = "Today",
                            Value = "Today"
                        },
                        new ApplicationCommandOptionChoiceProperties
                        {
                            Name = "Day of week",
                            Value = "Day of week"
                        },
                        new ApplicationCommandOptionChoiceProperties
                        {
                            Name = "Day of month",
                            Value = "Day of month"
                        },
                    },
                    isRequired: true,
                    minValue: 0.0
                ).
                AddOption(
                    "dayvalue",
                    ApplicationCommandOptionType.Integer,
                    "Biba and boba",
                    isRequired: true,
                    minValue: 0.0

                );

            var setupAnimeSearchCommand = new SlashCommandBuilder
            {
                Name = "searchanime",
                Description = "Searching anime"
            };

            try
            {
                await msg.Channel.SendMessageAsync("0%");
                await guild.CreateApplicationCommandAsync(setupSlashCommandCommand.Build());
                await guild.CreateApplicationCommandAsync(setupRollCommand.Build());
                await msg.Channel.SendMessageAsync("25%");
                await guild.CreateApplicationCommandAsync(setupRandomCommand.Build());
                await guild.CreateApplicationCommandAsync(setupClearCommand.Build());
                await msg.Channel.SendMessageAsync("50%");
                await guild.CreateApplicationCommandAsync(setupAnimeSearchCommand.Build());
                await msg.Channel.SendMessageAsync("80%");
                await guild.CreateApplicationCommandAsync(setupFetchAnimeCalendarCommand.Build());
            }
            catch (HttpException exception)
            {
                Console.Error.WriteLine(exception.Message);
            }

            await msg.Channel.SendMessageAsync("Command setup success");
        }

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
