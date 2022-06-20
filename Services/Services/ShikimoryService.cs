using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Options.Shikimory;
using Services.Enums;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;
using shikiApi = ShikimoriSharp;


namespace Infrastructure.Services
{
    public class ShikimoryService
    {
        private readonly ShikimoryClientOptions _options;
        private readonly shikiApi.ShikimoriClient _client;

        public ShikimoryService(IOptions<ShikimoryClientOptions> options) 
        {
            _options = options.Value;
            _client = new shikiApi.ShikimoriClient(
                            new Logger<ShikimoryService>(new LoggerFactory()),
                            new ClientSettings(_options.ClientName, _options.ClientId, _options.ClientSecret)
                        );
        }

        public async Task<Calendar[]> FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr parametr, int? day = null) {
            switch (parametr) {
                case ShikimoryCalendarFetchParametr.Today: 
                    {                    
                        var calendar = await _client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => x.NextEpisodeAt.Day == DateTime.Now.Date.Day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                case ShikimoryCalendarFetchParametr.DayOfWeek:
                    {
                        var calendar = await _client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => (int)x.NextEpisodeAt.DayOfWeek == day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                case ShikimoryCalendarFetchParametr.DayOfMonth:
                    {
                        var calendar = await _client.Calendars.GetCalendar();

                        return calendar
                            .Where(x => x.NextEpisodeAt.Day == day && x.Anime.EpisodesAired != 0)
                            .ToArray();
                    };
                default: return null;            
            }
        }

        public async Task<Anime[]> SearchAnimeByQueryString(String query) 
        {
            var animeRequestSettings = new AnimeRequestSettings
            {
                search = query
            };

            var result = await _client.Animes.GetAnime(
                animeRequestSettings
            );

            return result;
        }
    }
}
