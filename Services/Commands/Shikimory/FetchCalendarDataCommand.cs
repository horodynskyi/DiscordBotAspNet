using Discord;
using Discord.WebSocket;
using Services.Enums;
using Services.Models;
using ShikimoriSharp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class FetchCalendarDataCommand : DiscordSlashCommand
    {
        public override string Name => "sendcalendar";

        public override async void Execute(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var shikimoryService = new ShikimoryService();
            var shikimoryCalendarFetchParametr = msg.Data.Options.First().Value switch
            {
                "today" => ShikimoryCalendarFetchParametr.Today,
                "Day of week" => ShikimoryCalendarFetchParametr.DayOfWeek,
                "Day of month" => ShikimoryCalendarFetchParametr.DayOfMonth,
                _ => ShikimoryCalendarFetchParametr.Today,
            };

            var day = msg.Data.Options.ElementAt(1).Value;

            var calendars = (await shikimoryService.FetchShikimoryCalendarData(shikimoryCalendarFetchParametr, int.Parse(day.ToString()))).ToList();

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

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }
    }
}
