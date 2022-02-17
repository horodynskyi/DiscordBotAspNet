using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;
using Services.Enums;
using ShikimoriSharp.Classes;

namespace Infrastructure.Commands.Shikimory
{
    public class FetchCalendarDataCommand : DiscordSlashCommand
    {
        public override string Name => "sendcalendar";
        private readonly ShikimoryService _shikimoryService;

        public FetchCalendarDataCommand(ShikimoryService shikimoryService) 
        {
            _shikimoryService = shikimoryService;
        }

        public override async Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {            
            var shikimoryCalendarFetchParametr = msg.Data.Options.First().Value switch
            {
                "today" => ShikimoryCalendarFetchParametr.Today,
                "Day of week" => ShikimoryCalendarFetchParametr.DayOfWeek,
                "Day of month" => ShikimoryCalendarFetchParametr.DayOfMonth,
                _ => ShikimoryCalendarFetchParametr.Today,
            };

            var day = msg.Data.Options.ElementAt(1).Value;

            var calendars = (await _shikimoryService.FetchShikimoryCalendarData(shikimoryCalendarFetchParametr, int.Parse(day.ToString()))).ToList();

            foreach (var calendar in calendars)
            {
                var embed = new EmbedBuilder
                {
                    Title = $"{calendar.Anime.Name}",
                    ImageUrl = "https://moe.shikimori.one" + calendar.Anime.Image.Original,
                    Url = "https://shikimori.one" + calendar.Anime.Url,
                    Color = new Color(255, 16, 240),
                    Footer = BuildEmbedFootter(calendar)
                };

                await msg.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

        private static EmbedFooterBuilder BuildEmbedFootter(Calendar calendar) {
            var embedFooterBuilder = new EmbedFooterBuilder
            {
                Text = calendar.Anime.Russian,
                IconUrl = "https://moe.shikimori.one" + calendar.Anime.Image.Preview,
            };
            return embedFooterBuilder;
        }

        public override Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var setupFetchAnimeCalendarCommand = new SlashCommandBuilder
            {
                Name = Name,
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
            return setupFetchAnimeCalendarCommand;
        }
    }
}
