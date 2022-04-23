using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands.Shikimory
{
    public class SearchAnimeCommand : DiscordSlashCommand
    {
        public override string Name => "searchanime";

        private readonly ShikimoryService _shikimoryService;

        public SearchAnimeCommand(ShikimoryService shikimoryService)
        {
            _shikimoryService = shikimoryService;
        }

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                var query = command.Data.Options.First().Value;
                var animes = await _shikimoryService.SearchAnimeByQueryString((String)query);
                var embed = new EmbedBuilder
                {
                    Title = $"{animes.First().Name}",
                    ImageUrl = "https://moe.shikimori.one" + animes.First().Image.Original,
                    Url = "https://shikimori.one" + animes.First().Url,
                    Color = new Color(255, 16, 240),
                };
                await command.RespondAsync(embed: embed.Build());
            }
            else
            {
                return;
            }
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var searchAnimeSlashCommandBuilder = new SlashCommandBuilder
            {
                Name = Name,
                Description = "Searching anime"
            };
            searchAnimeSlashCommandBuilder.AddOption(
                "query",
                ApplicationCommandOptionType.String,
                "Biba and boba",
                isRequired: true
            ).AddOption(
                "tags",
                ApplicationCommandOptionType.String,
                "Biba and boba",
                isRequired: false
            );

            return searchAnimeSlashCommandBuilder;
        }
    }
}
