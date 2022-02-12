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
    public class FetchCalendarData : DiscordComand
    {
        public override string Name => "/send calendar";

        public override async void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            var shikimoryService = new ShikimoryService();
            var calendars = new List<Calendar>();

            calendars = (await shikimoryService.FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr.Today)).ToList();

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
