using Microsoft.Extensions.Logging;
using Services.Enums;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;
using shikiApi = ShikimoriSharp;


namespace Services
{
    public class ShikimoryService
    {
        private readonly ClientSettings _clientSettings = new(
            "Hentai guardian v1.0", 
            "2xaf_-rSFJcnGJvBAiF9chXsfce_6GvGBTalNfRoDoA", 
            "mAg8sXVgIh2aA3iIwGN1Sb_LyCAsAwl7IxaFxQ29f1s"
        );

        public async Task<Calendar[]> FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr parametr) {
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
                default: return null;            
            }
        }
    }
}
