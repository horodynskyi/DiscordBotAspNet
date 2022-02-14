using System.Text.Json;

namespace Infrastructure.Services
{
    public class DanbooruService
    {
        private readonly HttpClient _httpClient = new();   

        public async Task<String> GetRandomArt(bool? cencorship = false) 
        {
            String domain;
            if ((bool)cencorship)
                domain = "https://danbooru.donmai.us/";
            else
                domain = "https://safebooru.donmai.us/";

            Console.WriteLine("cens = " + !(bool)cencorship + " domain " + domain);
            var res = await _httpClient.GetAsync(domain + "posts/random.json");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await res.Content.ReadAsStringAsync());
            var fileUrlDictionary = data.FirstOrDefault(x => x.Key == "file_url");
            var ifNotExistUrl = fileUrlDictionary.Value == null;
            Console.WriteLine("ifNotExistUrl = " + ifNotExistUrl);
            while (ifNotExistUrl) 
            {
                return null;
            }

            return data["file_url"].ToString();
        }
    }
}
