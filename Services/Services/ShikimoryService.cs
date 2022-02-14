using Microsoft.Extensions.Logging;
using Services.Enums;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;
using shikiApi = ShikimoriSharp;


namespace Infrastructure.Services
{
    public class ShikimoryService
    {
        private readonly ClientSettings _clientSettings = new(
            "Hentai guardian v1.0", 
            "2xaf_-rSFJcnGJvBAiF9chXsfce_6GvGBTalNfRoDoA", 
            "mAg8sXVgIh2aA3iIwGN1Sb_LyCAsAwl7IxaFxQ29f1s"
        );

        public async Task<Calendar[]> FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr parametr, int? day = null) {
            switch (parametr) {
                case ShikimoryCalendarFetchParametr.Today: {
                        var client = new shikiApi.ShikimoriClient(
                            new Logger<ShikimoryService>(new LoggerFactory()),
                            _clientSettings
                        );

                        var calendar = await client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => x.NextEpisodeAt.Day == DateTime.Now.Date.Day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                case ShikimoryCalendarFetchParametr.DayOfWeek:
                    {
                        var z = day;
                        var client = new shikiApi.ShikimoriClient(
                            new Logger<ShikimoryService>(new LoggerFactory()),
                            _clientSettings
                        );

                        var calendar = await client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => (int)x.NextEpisodeAt.DayOfWeek == day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                case ShikimoryCalendarFetchParametr.DayOfMonth:
                    {
                        var client = new shikiApi.ShikimoriClient(
                            new Logger<ShikimoryService>(new LoggerFactory()),
                            _clientSettings
                        );

                        var calendar = await client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => x.NextEpisodeAt.Day == day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                default: return null;            
            }
        }
    }
}
