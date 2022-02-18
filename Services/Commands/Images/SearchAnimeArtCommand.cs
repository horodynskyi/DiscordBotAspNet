﻿using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands.Images
{
    public class SearchAnimeArtCommand : DiscordSlashCommand
    {
        public override string Name => "searchanimeart";

        public override async Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            
            var danbooruService = new DanbooruService();
            var url = await danbooruService.GetArt((string)msg.Data.Options.First().Value, (Int64)msg.Data.Options.Last().Value == 1);
            await msg.RespondAsync(url ?? "Not found");
        }

        public override Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var setupRandomArtCommand = new SlashCommandBuilder
            {
                Name = Name,
                Description = "Search Anime Art"
            };

            setupRandomArtCommand.AddOption
                (
                "tags",
                ApplicationCommandOptionType.String,
                "Biba and boba",
                isRequired: true).AddOption
                (
                "censorship",
                ApplicationCommandOptionType.Integer,
                "Biba and boba",
                isRequired: true,
                choices: new ApplicationCommandOptionChoiceProperties[2]
                    {
                        new ApplicationCommandOptionChoiceProperties
                        {
                            Name = "Yes",
                            Value = 0
                        },
                        new ApplicationCommandOptionChoiceProperties
                        {
                            Name = "No",
                            Value = 1
                        },
                    }
                );

            return setupRandomArtCommand;
        }
    }
}